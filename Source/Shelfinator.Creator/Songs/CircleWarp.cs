using System.Linq;
using System.Windows;
using System;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class CircleWarp : ISong
	{
		public int SongNumber => 38;

		public Song Render()
		{
			const double Brightness = 1f / 16;

			var segment = new Segment();

			var layout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");

			var color = new LightColor(0, 5, Helpers.Rainbow6.Multiply(Brightness).ToList());

			var center = new Point(48, 48);
			var distances = layout.GetAllLights().Select(light => Tuple.Create(light, (layout.GetLightPosition(light) - center).Length)).ToList();

			for (var square = 0; square < 18; ++square)
			{
				for (var x = 0; x <= 66; ++x)
				{
					var time = square * 17 + x;
					var lights = distances.Where(tuple => (tuple.Item2 >= x) && (tuple.Item2 < x + 2)).Select(tuple => tuple.Item1).ToList();
					foreach (var light in lights)
						segment.AddLight(light, time, color, square % 6);
					foreach (var light in lights)
						segment.AddLight(light, time + 1, Segment.Absolute, 0x000000);
				}
			}

			var song = new Song();
			song.AddSegment(segment, 0, 102, 5000);
			song.AddSegment(segment, 102, 204, 5000, 2);
			song.AddSegment(segment, 204, 356, 7655);
			return song;
		}
	}
}
