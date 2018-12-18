//using System.Collections.Generic;
//using System.Linq;
//using Shelfinator.Creator.SongData;
//
//namespace Shelfinator.Creator.Songs
//{
//	class Bounce : ISong
//	{
//		public int SongNumber => 23;
//
//		public Song Render()
//		{
//			const double Brightness = 1f / 16;
//
//			var segment = new Segment();
//
//			var layout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");
//			var columns = new List<int> { 0, 19, 38, 57, 76, 95 };
//			var columnTime = new List<int> { 48, 64, 0, 32, 80, 16 };
//
//			var paddleDest = columnTime.Select((val, index) => new { time = val, column = columns[index] }).OrderBy(o => o.time).Select(o => o.column).ToList();
//
//			var color = new LightColor(0, 95, Helpers.Rainbow6.Multiply(Brightness).ToList());
//
//			double paddlePos = 0;
//			int paddleIndex = 0, paddleSteps = 0;
//			for (var time = 0; time <= 437; ++time)
//			{
//				segment.Clear(time);
//				for (var columnCtr = 0; columnCtr < columns.Count; columnCtr++)
//				{
//					int y;
//					var useTime = time - columnTime[columnCtr];
//					if (time < 96)
//					{
//						y = 95 - time;
//						foreach (var light in layout.GetPositionLights(columns[columnCtr], y + 2, 2, 1))
//							segment.AddLight(light, time, Segment.Absolute, Helpers.MultiplyColor(0xffffff, Brightness));
//					}
//					else if (useTime < 116)
//						y = 0;
//					else if (useTime == 116)
//					{
//						y = 0;
//						foreach (var light in layout.GetPositionLights(columns[columnCtr], 2, 2, 1))
//						{
//							segment.AddLight(light, 96, Segment.Absolute, Helpers.MultiplyColor(0xffffff, Brightness));
//							segment.AddLight(light, time - 20, time, null, Segment.Absolute, 0x000000);
//						}
//					}
//					else if (useTime <= 356)
//					{
//						var yTime = Helpers.Cycle(useTime - 68, 0, 96).Round();
//						y = (31d / 768 * yTime * yTime - 31d / 8 * yTime + 93).Round();
//					}
//					else
//						continue;
//
//					foreach (var light in layout.GetPositionLights(columns[columnCtr], y, 2, 2))
//						segment.AddLight(light, time, color, columns[columnCtr]);
//				}
//
//				if (time < 148)
//				{
//				}
//				else if (time == 148)
//				{
//					foreach (var light in layout.GetPositionLights(0, 95, 97, 2))
//					{
//						var x = layout.GetLightPosition(light).X.Round();
//						segment.AddLight(light, 5, 48, null, color, x);
//						if ((x <= 43) || (x > 53))
//							segment.AddLight(light, 48, 96, null, Segment.Absolute, 0x000000);
//					}
//					paddlePos = 48;
//					paddleSteps = 164 - time;
//				}
//				else
//				{
//					if (paddleSteps == 0)
//					{
//						++paddleIndex;
//						if (paddleIndex >= paddleDest.Count)
//							paddleIndex = 0;
//						paddleSteps = time > 340 ? 64 : 16;
//					}
//
//					var dest = time > 340 ? -10 : paddleDest[paddleIndex];
//					paddlePos += (dest - paddlePos) / paddleSteps;
//					--paddleSteps;
//
//					for (var y = 0; y < 2; ++y)
//						for (var x = 0; x < 10; ++x)
//						{
//							var pos = paddlePos.Round();
//							var light = layout.TryGetPositionLight(x + pos - 4, y + 95);
//							if (light.HasValue)
//								segment.AddLight(light.Value, time, color, x + pos);
//						}
//				}
//			}
//
//			var song = new Song();
//			song.AddSegment(segment, 0, 96, 4000);
//			song.AddSegment(segment, 96, 196, 2083);
//			song.AddSegment(segment, 196, 292, 2000, 9);
//			song.AddSegment(segment, 292, 437, 3021);
//			song.AddSegment(segment, 437, 437, 1000);
//
//			return song;
//		}
//	}
//}
