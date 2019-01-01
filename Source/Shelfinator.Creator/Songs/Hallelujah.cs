using System.Collections.Generic;
using System.Linq;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Hallelujah : ISong
	{
		public int SongNumber => 6;

		const double Brightness = 1f / 16;
		readonly Layout bodyLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");

		//Segment FadeIn()
		//{
		//	var segment = new Segment();
		//	var color = new LightColor(0, 96, Helpers.Rainbow6.Multiply(Brightness).ToList());
		//	foreach (var light in bodyLayout.GetAllLights())
		//	{
		//		var pos = bodyLayout.GetLightPosition(light);
		//		segment.AddLight(light, 0, pos.Y.Round(), color, 0, color, pos.Y.Round());
		//		segment.AddLight(light, 200, 200 + 96 - pos.Y.Round(), color, pos.Y.Round(), color, 96);
		//		segment.AddLight(light, 400, 496, color, 96, color, 0);
		//	}
		//	return segment;
		//}

		Segment Cycles()
		{
			var segment = new Segment();
			var times = new List<int> { 365, 700, 1365, 1700, 2365, 2700, 3035, 3365, 4365, 4700, 5365, 5700, 6365, 6700, 7030, 7365, 8365, 8700, 9365, 10030, 10365, 10700, 11365, 11700, 12365, 12700, 13700 };
			for (var ctr = 0; ctr < times.Count; ++ctr)
				foreach (var light in bodyLayout.GetAllLights())
					segment.AddLight(light, times[ctr], Segment.Absolute, Helpers.MultiplyColor(Helpers.Rainbow6[ctr % Helpers.Rainbow6.Count], Brightness));
			return segment;
		}

		public Song Render()
		{
			// First measure starts at 700, measures are 2000 throughout the song until the end

			var song = new Song("hallelujah.wav");

			// Cycles (500)
			var cycles = Cycles();
			song.AddSegmentWithRepeat(cycles, 0, 13700, 0);

			// Snowfall accumulating
			// Fireworks
			
			return song;
		}
	}
}
