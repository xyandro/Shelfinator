//using System.Collections.Generic;
//using System.Linq;

//namespace Shelfinator.Patterns
//{
//	class Spiral : Pattern
//	{
//		public Spiral()
//		{
//			var points = new List<List<int[]>>();

//			var width = Layout.SPACEBETWEENROWS + 2;
//			var size = Layout.WIDTH - 1;

//			var x2_4 = Layout.GetX2(4);
//			for (var x = 0; x < x2_4; ++x)
//				points.Add(Enumerable.Range(0, width).Select(y => new int[] { x, y }).ToList());
//			for (var x = x2_4; x < Layout.WIDTH; ++x)
//				points[points.Count - 1].AddRange(Enumerable.Range(0, width).Select(y => new int[] { x, y }));

//			var y2_4 = Layout.GetY2(4);
//			for (var y = width; y < y2_4; ++y)
//				points.Add(Enumerable.Range(x2_4 - 1, width).Select(x => new int[] { x, y }).ToList());
//			for (var y = y2_4; y < Layout.HEIGHT; ++y)
//				points[points.Count - 1].AddRange(Enumerable.Range(x2_4 - 1, width).Select(x => new int[] { x, y }).ToList());

//			for (var x = Layout.HEIGHT - width - 1; x >= width - 1; --x)
//				points.Add(Enumerable.Range(y2_4 - 1, width).Select(y => new int[] { x, y }).ToList());
//			for (var x = width - 2; x >= 0; --x)
//				points[points.Count - 1].AddRange(Enumerable.Range(y2_4 - 1, width).Select(y => new int[] { x, y }).ToList());

//			var y1_1 = Layout.GetY1(1);
//			for (var y = Layout.HEIGHT - width - 1; y >= y1_1; --y)
//				points.Add(Enumerable.Range(0, width).Select(x => new int[] { x, y }).ToList());
//			for (var y = y1_1 - 1; y > y1_1 - width; --y)
//				points[points.Count - 1].AddRange(Enumerable.Range(0, width).Select(x => new int[] { x, y }).ToList());

//			var x2_3 = Layout.GetX2(3);
//			for (var x = width; x < x2_3; ++x)
//				points.Add(Enumerable.Range(y1_1 - width + 1, width).Select(y => new int[] { x, y }).ToList());
//			for (var x = x2_3; x < x2_3 + width - 1; ++x)
//				points[points.Count - 1].AddRange(Enumerable.Range(y1_1 - width + 1, width).Select(y => new int[] { x, y }).ToList());

//			var y2_3 = Layout.GetY2(3);
//			var y1_4 = Layout.GetY1(4);
//			for (var y = y1_1 + 1; y < y2_3; ++y)
//				points.Add(Enumerable.Range(x2_3 - 1, width).Select(x => new int[] { x, y }).ToList());
//			for (var y = y2_3; y <= y1_4; ++y)
//				points[points.Count - 1].AddRange(Enumerable.Range(x2_3 - 1, width).Select(x => new int[] { x, y }).ToList());

//			var x1_1 = Layout.GetX1(1);
//			for (var x = x2_3 - 2; x >= x1_1; --x)
//				points.Add(Enumerable.Range(y1_4 - width + 1, width).Select(y => new int[] { x, y }).ToList());
//			for (var x = x1_1 - 1; x >= x1_1 - width + 1; --x)
//				points[points.Count - 1].AddRange(Enumerable.Range(y1_4 - width + 1, width).Select(y => new int[] { x, y }).ToList());

//			var y1_2 = Layout.GetY1(2);
//			for (var y = y1_4 - width; y >= y1_2; --y)
//				points.Add(Enumerable.Range(x1_1 - width + 1, width).Select(x => new int[] { x, y }).ToList());
//			for (var y = y1_2 - 1; y >= y1_2 - width + 1; --y)
//				points[points.Count - 1].AddRange(Enumerable.Range(x1_1 - width + 1, width).Select(x => new int[] { x, y }).ToList());

//			var x2_2 = Layout.GetX2(2);
//			for (var x = x1_1 + 1; x < x2_2; ++x)
//				points.Add(Enumerable.Range(y1_2 - width + 1, width).Select(y => new int[] { x, y }).ToList());
//			for (var x = x2_2; x < x2_2 + width - 1; ++x)
//				points[points.Count - 1].AddRange(Enumerable.Range(y1_2 - width + 1, width).Select(y => new int[] { x, y }).ToList());

//			var y2_2 = Layout.GetY2(2);
//			for (var y = y1_2 + 1; y < y2_2; ++y)
//				points.Add(Enumerable.Range(x2_2 - 1, width).Select(x => new int[] { x, y }).ToList());
//			for (var y = y2_2; y < y2_2 + width - 1; ++y)
//				points[points.Count - 1].AddRange(Enumerable.Range(x2_2 - 1, width).Select(x => new int[] { x, y }).ToList());

//			var x1_2 = Layout.GetX1(2);
//			for (var x = x2_2 - 2; x >= x1_2 - width + 1; --x)
//				points.Add(Enumerable.Range(y2_2 - 1, width).Select(y => new int[] { x, y }).ToList());

//			var seqStart = 0;
//			var color = 0xff0000;
//			for (var ctr = 0; ctr < points.Count; ++ctr)
//			{
//				int startTime = 900 * ctr / points.Count;
//				foreach (var point in points[ctr])
//				{
//					var x = point[0];
//					var y = point[1];
//					AddLight(new LightData { X = x, Y = y, StartColor = 0x000000, EndColor = color, StartTime = seqStart + startTime, Duration = 300 });
//					AddLight(new LightData { X = x, Y = y, StartColor = color, EndColor = 0x000000, StartTime = seqStart + startTime + 1200, Duration = 300 });
//				}
//			}

//			seqStart = 1500;
//			color = 0x00ff00;
//			for (var ctr = 0; ctr < points.Count; ++ctr)
//			{
//				int startTime = 900 * ctr / points.Count;
//				foreach (var point in points[ctr])
//				{
//					var x = size - point[1];
//					var y = point[0];
//					AddLight(new LightData { X = x, Y = y, StartColor = 0x000000, EndColor = color, StartTime = seqStart + startTime, Duration = 300 });
//					AddLight(new LightData { X = x, Y = y, StartColor = color, EndColor = 0x000000, StartTime = seqStart + startTime + 1200, Duration = 300 });
//				}
//			}

//			seqStart = 3000;
//			color = 0x0000ff;
//			for (var ctr = 0; ctr < points.Count; ++ctr)
//			{
//				int startTime = 900 * ctr / points.Count;
//				foreach (var point in points[ctr])
//				{
//					var x = size - point[0];
//					var y = size - point[1];
//					AddLight(new LightData { X = x, Y = y, StartColor = 0x000000, EndColor = color, StartTime = seqStart + startTime, Duration = 300 });
//					AddLight(new LightData { X = x, Y = y, StartColor = color, EndColor = 0x000000, StartTime = seqStart + startTime + 1200, Duration = 300 });
//				}
//			}

//			seqStart = 4500;
//			color = 0x800080;
//			for (var ctr = 0; ctr < points.Count; ++ctr)
//			{
//				int startTime = 900 * ctr / points.Count;
//				foreach (var point in points[ctr])
//				{
//					var x = point[1];
//					var y = size - point[0];
//					AddLight(new LightData { X = x, Y = y, StartColor = 0x000000, EndColor = color, StartTime = seqStart + startTime, Duration = 300 });
//					AddLight(new LightData { X = x, Y = y, StartColor = color, EndColor = 0x000000, StartTime = seqStart + startTime + 1200, Duration = 300 });
//				}
//			}
//		}
//	}
//}
