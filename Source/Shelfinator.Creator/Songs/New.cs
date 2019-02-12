using System.Linq;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class New : SongCreator
	{
		public override int SongNumber => 9;
		public override bool Test => true;

		readonly Layout bodyLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");

		Segment TimeWarp(out int time)
		{
			const int Repeat = 10;
			var frame1 = Enumerable.Range(0, 3).SelectMany(y => Enumerable.Range(0, 3).SelectMany(x => bodyLayout.GetPositionLights(x * 38, y * 38, 21, 21))).ToList();
			var frame2 = Enumerable.Range(0, 2).SelectMany(y => Enumerable.Range(0, 2).SelectMany(x => bodyLayout.GetPositionLights(x * 38 + 19, y * 38 + 19, 21, 21))).ToList();
			var segment = new Segment();
			time = 0;
			var delay = 1 << Repeat;
			for (var ctr = 0; ctr < Repeat; ++ctr)
			{
				segment.Clear(time);
				foreach (var light in frame1)
					segment.AddLight(light, time, 0x101010);
				time += delay;

				segment.Clear(time);
				foreach (var light in frame2)
					segment.AddLight(light, time, 0x101010);
				time += delay;
				delay >>= 1;
			}
			return segment;
		}

		public override Song Render()
		{
			var song = new Song("new.ogg");

			// TimeWarp (0)
			var timeWarp = TimeWarp(out var timeWarpTime);
			song.AddSegment(timeWarp, 0, timeWarpTime, 0, 10000);

			return song;
		}
	}
}
