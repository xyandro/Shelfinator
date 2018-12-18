using System.Linq;
using System;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Randomized : ISong
	{
		public int SongNumber => 41;

		Random rand = new Random(0xfacade);
		public Song Render()
		{
			const double Brightness = 1f / 16;
			const int BaseIncrement = 3500;

			var layout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");

			var segment = new Segment();
			var colors = Helpers.Rainbow6.Concat(0x000000).Multiply(Brightness).ToList();
			for (var colorCtr = 0; colorCtr < colors.Count; colorCtr++)
			{
				var baseTime = BaseIncrement * colorCtr;
				var lights = layout.GetAllLights().OrderBy(x => rand.Next()).ToList();
				for (var lightCtr = 0; lightCtr < lights.Count; lightCtr++)
					segment.AddLight(lights[lightCtr], baseTime + lightCtr, Segment.Absolute, colors[colorCtr]);
			}

			var song = new Song();
			song.AddSegment(segment, 0, segment.MaxLightTime(), 20000);
			return song;
		}
	}
}
