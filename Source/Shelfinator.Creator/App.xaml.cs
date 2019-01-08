using System;
using System.Collections.Generic;
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
			var small = CheckExistsAndRemove(args, "small");
			var startPaused = CheckExistsAndRemove(args, "pause");

			var songNumbers = args.Select(arg => { try { return int.Parse(arg); } catch { throw new Exception($"Unable to parse number: {arg}"); } }).ToList();

			if (build)
			{
				var match = songs.Where(p => (all) || (songNumbers.Contains(p.SongNumber)));
				if (!match.Any())
					throw new Exception($"Song(s) not found");

				foreach (var song in match)
					song.Render().Save(song.SongNumber);
			}

			var window = new Emulator(small, songNumbers, startPaused);
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
			Environment.Exit(0);
		}
	}
}
