using SwordsOfChat.Bot.Commands;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SwordsOfChat.Bot {
	internal static class TelegramBot {

		#region App behaviour

		private static TelegramBotClient? _bot;
		private static CancellationTokenSource? _cts;

		public static bool IsRunning { get; private set; }

		public static void Start() {
			if (IsRunning) {
				Log.Error("Fail to start bot: Already started.");
				return;
			}

			var token = BotConfig.GetToken();
			if (!BotConfig.ValidateConfig()) {
				Log.Error("Fail to start bot: Update config file.");
				return;
			}

			Log.Info("Bot starting...");

			_bot = new TelegramBotClient(token);
			_cts = new CancellationTokenSource();

			ReceiverOptions ropt = new() {
				//AllowedUpdates = Array.Empty<UpdateType>()
				AllowedUpdates = [
					UpdateType.Message,
					UpdateType.CallbackQuery
				]
			};

			_bot.StartReceiving(
				updateHandler: HandleUpdateAsync,
				errorHandler: HandleErrorAsync,
				receiverOptions: ropt,
				cancellationToken: _cts.Token
			);

			Log.Info("Bot started.");

			IsRunning = true;
		}

		public static void Restart() {
			Log.Info("Restarting bot...");

			if (IsRunning)
				StopInternal();

			Start();
		}

		public static void Stop() {
			if (!IsRunning) {
				Log.Warning("Fail to stop bot: Bot not running.");
				return;
			}

			Log.Info("Stopping bot...");

			StopInternal();

			Log.Info("Bot stopped.");
		}

		private static void StopInternal() {
			_cts?.Cancel();
			_cts?.Dispose();
			_cts = null;

			_bot = null;

			IsRunning = false;
		}

		#endregion
		#region Telegram behaviour

		private static async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken ct) {
			if (update.Message is not null)
				await HandleMessageAsync(bot, update.Message, ct);
			else if (update.CallbackQuery is not null)
				await HandleCallbackAsync(bot, update.CallbackQuery, ct);
		}

		private static async Task HandleMessageAsync(ITelegramBotClient bot, Message message, CancellationToken ct) {
			string text = message.Text!;

			if (text.StartsWith('/')) {
				await ProcessCommand(bot, message, ct);
				return;
			}

			await bot.SendMessage(message.Chat.Id, $"You said: {text}", cancellationToken: ct);
		}

		private static async Task HandleCallbackAsync(ITelegramBotClient bot, CallbackQuery query, CancellationToken ct) {
			await bot.AnswerCallbackQuery(query.Id, cancellationToken: ct);

			await bot.SendMessage(
				query.Message!.Chat.Id,
				$"Callback: {query.Data}",
				cancellationToken: ct);
		}

		private static Task HandleErrorAsync(ITelegramBotClient bot, Exception exception, CancellationToken ct) {
			Console.WriteLine(exception);
			return Task.CompletedTask;
		}

		public static async Task SendMessageAsync(long chatId, string text) {
			if (_bot == null) {
				Log.Error("Bot is not initialized.");
				return;
			}

			try {
				await _bot.SendMessage(chatId, text, cancellationToken: _cts!.Token);

			} catch (Exception ex) {
				Log.Error($"SendMessageAsync failed: {ex.Message}");
			}
		}

		#endregion
		#region Commands behaviour

		private readonly static Dictionary<string, IBotCommand> Commands = [];

		public static void RegisterDefaultCommands() {
			Commands.Clear();

			AddCommand(new MeBotCommand());
			AddCommand(new LicenseBotCommand());
			AddCommand(new HeroBotCommand());
			AddCommand(new LangBotCommand());
			AddCommand(new SettingsBotCommand());
		}

		public static void AddCommand(IBotCommand command) {
			Commands.TryAdd(command.Key, command);
		}

		public static string? TryCallCommandUnsafe(long userId, string[] args) {
			return Commands.TryGetValue(args[0], out var command) ? command.Run(userId, args) : null;
		}

		private static async Task ProcessCommand(ITelegramBotClient bot, Message message, CancellationToken ct) {
			string? commandText = message.Text;
			User? from = message.From;

			if (commandText == null || from == null)
				return;

			string[] args = commandText.Trim()[1..].Split('_');
			if (args.Length == 0)
				return;

			if (!Commands.TryGetValue(args[0], out var command))
				return;

			long userId = from.Id;

			try {
				Log.Info($"{userId} calls {command.Key} with input '{commandText}'");

				string? response = command.Run(userId, args);
				if (response != null)
					await bot.SendMessage(message.Chat.Id, response, cancellationToken: ct);

			} catch (Exception e) {
				Log.Error(e.Message);
			}
		}

		#endregion
	}
}