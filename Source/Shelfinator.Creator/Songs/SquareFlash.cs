//using System.Collections.Generic;
//using System.Linq;
//using System;
//using Shelfinator.Creator.SongData;
//
//namespace Shelfinator.Creator.Songs
//{
//	class SquareFlash : ISong
//	{
//		public int SongNumber => 24;
//
//		List<List<int>> GetLights()
//		{
//			var layout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");
//			var squares = new Layout("Shelfinator.Creator.Songs.Layout.Squares.png");
//			return squares.GetAllLights().Except(0).OrderBy(light => light).Select(light => layout.GetPositionLights(squares.GetLightPositions(light))).ToList();
//		}
//
//		public Song Render()
//		{
//			const double Brightness = 1f / 16;
//			const int Count = 250;
//
//			var segment = new Segment();
//			var lights = GetLights();
//
//			var color = new LightColor(0, 24, Helpers.Rainbow7.Multiply(Brightness).ToList());
//
//			var rand = new Random(10);
//			var order = Enumerable.Range(0, lights.Count).OrderBy(x => rand.Next()).ToList();
//			var time = 0;
//			var timeOffset = 1000;
//			for (var ctr = 0; ctr < Count; ++ctr)
//			{
//				foreach (var light in lights[order[ctr % 25]])
//					segment.AddLight(light, time, color, order[ctr % 25]);
//				time += timeOffset;
//				foreach (var light in lights[order[ctr % 25]])
//					segment.AddLight(light, time, time + 300, null, Segment.Absolute, 0x000000);
//				timeOffset = Math.Max(25, timeOffset - Math.Min(25, timeOffset * 29 / 30));
//			}
//			segment.Clear(time);
//
//			var song = new Song();
//			song.AddSegment(segment, 0, time);
//			song.AddSegment(segment, time, time, 1000);
//			return song;
//		}
//	}
//}
