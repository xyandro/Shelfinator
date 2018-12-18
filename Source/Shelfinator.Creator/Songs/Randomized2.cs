using System.Linq;
using System;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Randomized2 : ISong
	{
		public int SongNumber => 42;

		public Song Render()
		{
			const double Brightness = 1f / 16;
			const int RandomCount = 5;
			const int EndDelay = 10;

			var segment = new Segment();
			var layout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");
			var color = new LightColor(0, 5, Helpers.Rainbow6.Multiply(Brightness).ToList());
			var rand = new Random(0xfacade);
			var time = 0;
			for (var ctr = 0; ctr < Helpers.Rainbow6.Count; ++ctr)
			{
				foreach (var light in layout.GetAllLights())
				{
					var count = 0;
					while (true)
					{
						var colorIndex = rand.Next(Helpers.Rainbow6.Count);
						segment.AddLight(light, time + count, time + count + 1, null, color, colorIndex);
						if ((++count >= RandomCount) && (colorIndex == ctr))
							break;
					}
				}
				time = segment.MaxLightTime() + EndDelay;
			}

			var song = new Song();
			song.AddSegment(segment, 0, time, 20000);
			return song;
		}
	}
}
