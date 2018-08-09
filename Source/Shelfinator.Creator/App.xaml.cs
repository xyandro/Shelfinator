using System;
using System.IO;
using System.Linq;
using System.Windows;
using Shelfinator.Creator.Patterns;

namespace Shelfinator.Creator
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

			var patterns = typeof(IPattern).Assembly.DefinedTypes.Where(t => (!t.IsInterface) && (typeof(IPattern).IsAssignableFrom(t))).Select(t => Activator.CreateInstance(t)).Cast<IPattern>().ToList();

			var dups = string.Join("\n", patterns.GroupBy(p => p.PatternNumber).Where(group => group.Skip(1).Any()).Select(group => $"{group.Key}: {string.Join(", ", group.Select(p => p.GetType().Name))}"));
			if (dups != "")
				throw new Exception($"Found duplicate pattern numbers:\n\n{dups}\n\nNext available is {patterns.Select(p => p.PatternNumber).DefaultIfEmpty(0).Max() + 1}.");

			var args = e.Args.ToList();
			var build = args.Contains("build", StringComparer.OrdinalIgnoreCase);
			var all = args.Contains("all", StringComparer.OrdinalIgnoreCase);
			var test = args.Contains("test", StringComparer.OrdinalIgnoreCase);
			if ((build) || (all) || (test))
				args.RemoveAll(arg => (string.Equals(arg, "build", StringComparison.OrdinalIgnoreCase)) || (string.Equals(arg, "all", StringComparison.OrdinalIgnoreCase)) || (string.Equals(arg, "test", StringComparison.OrdinalIgnoreCase)));

			var patternNumbers = args.Select(arg => { try { return int.Parse(arg); } catch { throw new Exception($"Unable to parse number: {arg}"); } }).ToList();
			if ((test) && (patternNumbers.Count != 5))
				throw new Exception("Test must provide firstLight, lightCount, concurrency, delay, and brightness");

			if (build)
			{
				var match = patterns.Where(p => (all) || (patternNumbers.Contains(p.PatternNumber)));
				if (!match.Any())
					throw new Exception($"Pattern(s) not found");

				foreach (var pattern in match)
				{
					var fileName = Path.Combine(Path.GetDirectoryName(typeof(App).Assembly.Location), $"{pattern.PatternNumber}.pat");
					pattern.Render().Save(fileName);
				}
			}

			var window = new DotStarEmulatorWindow();
			if (test)
				window.Test(patternNumbers[0], patternNumbers[1], patternNumbers[2], patternNumbers[3], patternNumbers[4]);
			else
				window.Run(patternNumbers);
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
			Environment.Exit(0);
		}
	}
}
