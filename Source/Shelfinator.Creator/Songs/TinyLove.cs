using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class TinyLove : SongCreator
	{
		public override int SongNumber => 11;

		readonly Layout bodyLayout;

		public TinyLove()
		{
			bodyLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");
		}

		Segment Shift()
		{
			var segment = new Segment();

			var color = new LightColor(0, 1000, new List<int> { 0x000000 }, new List<int> { 0x101010 }, new List<int> { 0x100000, 0x100800, 0x101000 }, new List<int> { 0x101010, 0x100000, 0x101010 }, new List<int> { 0x000010, 0x001010, 0x000010 });

			var center = new Point(48, 48);
			var lightColor = bodyLayout.GetAllLights().ToDictionary(light => light, light => (((bodyLayout.GetLightPosition(light) - center).Length - 9) * 16.9830463869911).Round());

			var time = 0;
			for (var ctr = 0; ctr < 42; ++ctr)
			{
				segment.Clear(time);
				var shift = Math.Max(0, Math.Min(19 - Math.Abs(21 - ctr), 17));
				for (var y = 0; y < 4; ++y)
					for (var x = 0; x < 4; ++x)
					{
						var lights = bodyLayout.GetPositionLights(x * 38 - shift, y * 38, 19, 2);
						lights.AddRange(bodyLayout.GetPositionLights(x * 38 + 2 + shift, y * 38 + 19, 19, 2));
						lights.AddRange(bodyLayout.GetPositionLights(x * 38 + 19, y * 38 - shift, 2, 19));
						lights.AddRange(bodyLayout.GetPositionLights(x * 38, y * 38 + 2 + shift, 2, 19));

						foreach (var light in lights)
							segment.AddLight(light, time, color, lightColor[light]);
					}
				time += 100;
			}
			return segment;
		}

		void AddIrregularBeats(Song song, Segment segment, int segmentMeasureStart, int segmentMeasureLength, int startTime, List<int> measures)
		{
			foreach (var measure in measures)
			{
				var segmentMeasureEnd = segmentMeasureStart + segmentMeasureLength;
				song.AddSegment(segment, segmentMeasureStart, segmentMeasureEnd, startTime, measure);
				segmentMeasureStart = segmentMeasureEnd;
				startTime += measure;
			}
		}

		public override Song Render()
		{
			var song = new Song("tinylove.ogg");

			// Shift (1670)
			var shift = Shift();
			song.AddSegment(shift, 0, 4300, 1670, 2266, 8);
			song.AddPaletteChange(0, 0);
			song.AddPaletteChange(1170, 2170, 1);
			song.AddPaletteChange(5702, 6702, 2);
			song.AddPaletteChange(10234, 11234, 3);
			song.AddPaletteChange(14766, 15766, 4);

			// Misc (19798)
			var segment = new Segment();
			for (var time = 0; time < 20000; time += 1000)
				for (var light = 0; light < 2440; ++light)
					segment.AddLight(light, time, time + 1000, 0x101010, 0x000000);
			song.AddSegment(segment, 0, 1000, 19798, 2266, 7);

			//song.AddSegment(segment, 0, 1000, 35660, 1678, 35);

			//AddIrregularBeats(song, segment, 0, 1000, 101330, new List<int> { 3122, 3031, 2975, 2789, 2809, 2746, 2822, 2758, 2801, 2863, 2821, 2929, 2830, 2863, 2851, 3003, 2865, 2847, 2779 });

			//Emulator.TestPosition = 100330;

			return song;
		}
	}
}
