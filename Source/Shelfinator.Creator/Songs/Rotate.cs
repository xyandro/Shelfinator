using System.Linq;
using System.Windows;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Rotate : ISong
	{
		public int SongNumber => 11;

		public Song Render()
		{
			const double Brightness = 1f / 16;
			const int BladeCount = 8;
			const int Fade = 100;
			const int Delay = 150;

			var segment = new Segment();
			var layout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");
			var squares = new Layout("Shelfinator.Creator.Songs.Layout.Squares.png");

			foreach (var square in squares.GetAllLights())
			{
				var allLights = layout.GetMappedLights(squares, square);
				var allLocations = allLights.Select(light => layout.GetLightPosition(light)).ToList();
				var topLeft = allLocations.OrderBy(p => p.X).ThenBy(p => p.Y).First();
				var bottomRight = allLocations.OrderBy(p => p.X).ThenBy(p => p.Y).Last();
				var center = new Point((topLeft.X + bottomRight.X) / 2, (topLeft.Y + bottomRight.Y) / 2);

				var angles = allLocations.GetAngles(center).Cycle(0, 360 / BladeCount).AdjustToZero().Scale(0, 360 / BladeCount, 0, 500).Round().ToList();

				var rainbow = Helpers.Rainbow7.Concat(Helpers.Rainbow7[0]).Multiply(Brightness).ToList();
				var useColors = new LightColor(angles.Min(), angles.Max(), rainbow);
				for (var ctr = 0; ctr < allLights.Count; ++ctr)
					for (var repeat = 0; repeat < 9000; repeat += 500)
					{
						segment.AddLight(allLights[ctr], angles[ctr] + repeat, angles[ctr] + repeat + Fade, null, 0, useColors, angles[ctr]);
						segment.AddLight(allLights[ctr], angles[ctr] + repeat + Delay, angles[ctr] + repeat + Delay + Fade, null, Segment.Absolute, 0x000000);
					}
			}
			var song = new Song();
			song.AddSegment(segment, 0, 10000);
			return song;
		}
	}
}
