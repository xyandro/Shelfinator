using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Shelfinator.Creator.Songs;

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

		bool CheckExistsAndRemove(List<string> args, string value) => args.RemoveAll(x => x.Equals(value, StringComparison.OrdinalIgnoreCase)) != 0;

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			var songs = typeof(ISong).Assembly.DefinedTypes.Where(t => (!t.IsInterface) && (typeof(ISong).IsAssignableFrom(t))).Select(t => Activator.CreateInstance(t)).Cast<ISong>().ToList();

			var dups = string.Join("\n", songs.GroupBy(p => p.SongNumber).Where(group => group.Skip(1).Any()).Select(group => $"{group.Key}: {string.Join(", ", group.Select(p => p.GetType().Name))}"));
			if (dups != "")
				throw new Exception($"Found duplicate song numbers:\n\n{dups}\n\nNext available is {songs.Select(p => p.SongNumber).DefaultIfEmpty(0).Max() + 1}.");

			var args = e.Args.ToList();
			var build = CheckExistsAndRemove(args, "build");
			var all = CheckExistsAndRemove(args, "all");
			var test = CheckExistsAndRemove(args, "test");
			var testAll = CheckExistsAndRemove(args, "testall");
			var small = CheckExistsAndRemove(args, "small");
			var startPaused = CheckExistsAndRemove(args, "pause");

			var songNumbers = args.Select(arg => { try { return int.Parse(arg); } catch { throw new Exception($"Unable to parse number: {arg}"); } }).ToList();
			if ((test) && (songNumbers.Count != 5))
				throw new Exception("Test must provide firstLight, lightCount, concurrency, delay, and brightness");
			if ((testAll) && (songNumbers.Count != 3))
				throw new Exception("TestAll must provide lightCount, delay, and brightness");

			if (build)
			{
				var match = songs.Where(p => (all) || (songNumbers.Contains(p.SongNumber)));
				if (!match.Any())
					throw new Exception($"Song(s) not found");

				foreach (var song in match)
				{
					var fileName = Path.Combine(Path.GetDirectoryName(typeof(App).Assembly.Location), $"{song.SongNumber}.pat");
					var rendered = song.Render();
					rendered.Save(fileName);
				}
			}

			var window = new Emulator(small);
			if (test)
				window.Test(songNumbers[0], songNumbers[1], songNumbers[2], songNumbers[3], (byte)songNumbers[4]);
			else if (testAll)
				window.TestAll(songNumbers[0], songNumbers[1], (byte)songNumbers[2]);
			else
				window.Run(songNumbers, startPaused);
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
			Environment.Exit(0);
		}
	}
}
