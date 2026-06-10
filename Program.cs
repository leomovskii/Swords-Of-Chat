using SwordsOfChat.Bot;
using SwordsOfChat.Commands;
using SwordsOfChat.Database;
using SwordsOfChat.Game;
using SwordsOfChat.Game.Maps;
using SwordsOfChat.Localization;
using System.Runtime.InteropServices;

namespace SwordsOfChat {
	internal class Program {

		public readonly static Dictionary<string, ICommand> Commands = [];

		public static string CommandsHelp = string.Empty;

		private static void Main(string[] args) {
			SetConsoleCtrlHandler(Handler, true);

			BotConfig.TryLoad();
			GameConfig.TryLoad();
			LocalesManager.Initialize(LocalesManager.DefaultLocale);
			EmojiHelper.LoadEmojis();

			DBController.Init();
			MapsManager.Init();

			RegisterCommand(new HelpCommand());
			RegisterCommand(new StartCommand());
			RegisterCommand(new StopCommand());
			RegisterCommand(new RestartCommand());
			RegisterCommand(new ConfigCommand());
			RegisterCommand(new AdministratorCommand());
			RegisterCommand(new ModeratorCommand());
			RegisterCommand(new ArgsCommand());

			TelegramBot.RegisterDefaultCommands();

			ReadArgs(args);

			ListenCommands();
		}

		private static void ReadArgs(string[] args) {
			if (args == null || args.Length == 0)
				return;

			if (args.Contains(ArgsCommand.StartBotArg))
				if (Commands.TryGetValue(StartCommand.Key0, out var command))
					command.Run([]);

			if (args.Contains(ArgsCommand.LocateConfigArg))
				BotConfig.OpenFileLocationAndSelectConfig();
		}

		private static async Task ListenCommands() {
			while (!AppCts.IsCancellationRequested) {
				try {
					var readTask = Console.In.ReadLineAsync();
					var completed = await Task.WhenAny(readTask, Task.Delay(-1, AppCts.Token));
					if (completed != readTask)
						break;

					string? input = await readTask;

					string[] args = (input ?? string.Empty).Split(' ');

					if (args == null || args.Length == 0)
						continue;

					if (!Commands.TryGetValue(args[0].ToLower(), out var command)) {
						Log.Error($"Command '{args[0]}' not exists. Type 'help' to list available commands.");
						continue;
					}

					command.Run(args);

				} catch (Exception e) {
					Log.Error(e.Message);
				}
			}
		}

		public static void RegisterCommand(ICommand command) {
			if (Commands.TryAdd(command.Key, command)) {
				string t = CommandsHelp.Length > 0 ? "\n" : string.Empty;
				CommandsHelp += $"{t}  {command.Key} - {command.Help}";

			} else
				return;

			string[] arr = command.Aliases;
			if (arr == null)
				return;

			for (int i = 0; i < arr.Length; i++)
				Commands.TryAdd(arr[i], command);
		}

		#region Shutdown behaviour

		private static readonly CancellationTokenSource AppCts = new();

		private static bool _shutdownRequested;

		[DllImport("Kernel32")]
		private static extern bool SetConsoleCtrlHandler(HandlerRoutine handler, bool add);

		private delegate bool HandlerRoutine(CtrlType sig);

		private enum CtrlType {
			CTRL_C_EVENT = 0,
			CTRL_CLOSE_EVENT = 2,
			CTRL_SHUTDOWN_EVENT = 6
		}

		private static bool Handler(CtrlType sig) {
			if (_shutdownRequested)
				return true;

			if (sig is CtrlType.CTRL_CLOSE_EVENT or CtrlType.CTRL_SHUTDOWN_EVENT) {
				_shutdownRequested = true;

				Task.Run(() => {
					AppCts.Cancel();
					if (TelegramBot.IsRunning)
						TelegramBot.Stop();
					return true;
				});
			}

			return false;
		}
		#endregion
	}
}