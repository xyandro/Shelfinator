using System;
using System.Linq;
using System.Threading;
using System.Windows;
using Shelfinator.Patterns;

namespace Shelfinator
{
	partial class App
	{
		App()
		{
			DispatcherUnhandledException += (s, e) =>
			{
				var message = "";
				for (var ex = e.Exception; ex != null; ex = ex.InnerException)
					message += $"{ex.Message}\n";

				MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				Environment.Exit(0);
			};
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			if (e.Args.Length == 0)
				throw new Exception("Must provide filename");

			var fileName = e.Args[0];
			if (e.Args.Length >= 2)
			{
				var patterns = typeof(IPattern).Assembly.DefinedTypes.Where(t => (!t.IsInterface) && (typeof(IPattern).IsAssignableFrom(t))).Select(t => Activator.CreateInstance(t)).Cast<IPattern>().ToList();

				var dups = string.Join("\n", patterns.GroupBy(p => p.PatternNumber).Where(group => group.Skip(1).Any()).Select(group => $"{group.Key}: {string.Join(", ", group.Select(p => p.GetType().Name))}"));
				if (dups != "")
					throw new Exception($"Found duplicate pattern numbers:\n{dups}");

				if (!int.TryParse(e.Args[1], out var patternNumber))
					throw new Exception($"Failed to parse int {e.Args[1]}");

				var pattern = patterns.FirstOrDefault(p => p.PatternNumber == patternNumber);
				if (pattern == null)
					throw new Exception($"Pattern {patternNumber} not found");

				pattern.Render().Save(fileName);
			}

			var dotStar = new DotStarEmulator(Dispatcher, Helpers.GetEmbeddedBitmap("Shelfinator.Patterns.Layout.DotStar.png"));
			var window = new DotStarEmulatorWindow(dotStar.Bitmap);
			window.Show();
			new Thread(() => DriverRunner.Run(fileName, dotStar)).Start();
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
			Environment.Exit(0);
		}
	}
}
