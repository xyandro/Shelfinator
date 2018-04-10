using System.Collections.Generic;

namespace Shelfinator.Patterns
{
	class Spiral : Pattern
	{
		public Spiral()
		{
			var points = new List<List<int[]>>();

			for (var x = 0; x <= 94; ++x)
				points.Add(new List<int[]> { new int[] { x, 0 }, new int[] { x, 1 } });

			points.Add(new List<int[]> { new int[] { 95, 0 } });
			points.Add(new List<int[]> { new int[] { 96, 0 }, new int[] { 95, 1 } });
			points.Add(new List<int[]> { new int[] { 96, 1 } });

			for (var y = 2; y <= 94; ++y)
				points.Add(new List<int[]> { new int[] { 95, y }, new int[] { 96, y } });

			points.Add(new List<int[]> { new int[] { 96, 95 } });
			points.Add(new List<int[]> { new int[] { 95, 95 }, new int[] { 96, 96 } });
			points.Add(new List<int[]> { new int[] { 95, 96 } });

			for (var x = 94; x >= 2; --x)
				points.Add(new List<int[]> { new int[] { x, 95 }, new int[] { x, 96 } });

			points.Add(new List<int[]> { new int[] { 1, 96 } });
			points.Add(new List<int[]> { new int[] { 1, 95 }, new int[] { 0, 96 } });
			points.Add(new List<int[]> { new int[] { 0, 95 } });

			for (var y = 94; y >= 21; --y)
				points.Add(new List<int[]> { new int[] { 0, y }, new int[] { 1, y } });

			points.Add(new List<int[]> { new int[] { 0, 20 } });
			points.Add(new List<int[]> { new int[] { 0, 19 }, new int[] { 1, 20 } });
			points.Add(new List<int[]> { new int[] { 1, 19 } });

			for (var x = 2; x <= 75; ++x)
				points.Add(new List<int[]> { new int[] { x, 19 }, new int[] { x, 20 } });

			points.Add(new List<int[]> { new int[] { 76, 19 } });
			points.Add(new List<int[]> { new int[] { 77, 19 }, new int[] { 76, 20 } });
			points.Add(new List<int[]> { new int[] { 77, 20 } });

			for (var y = 21; y <= 75; ++y)
				points.Add(new List<int[]> { new int[] { 76, y }, new int[] { 77, y } });

			points.Add(new List<int[]> { new int[] { 77, 76 } });
			points.Add(new List<int[]> { new int[] { 76, 76 }, new int[] { 77, 77 } });
			points.Add(new List<int[]> { new int[] { 76, 77 } });

			for (var x = 75; x >= 21; --x)
				points.Add(new List<int[]> { new int[] { x, 76 }, new int[] { x, 77 } });

			points.Add(new List<int[]> { new int[] { 20, 77 } });
			points.Add(new List<int[]> { new int[] { 20, 76 }, new int[] { 19, 77 } });
			points.Add(new List<int[]> { new int[] { 19, 76 } });

			for (var y = 75; y >= 40; --y)
				points.Add(new List<int[]> { new int[] { 19, y }, new int[] { 20, y } });

			points.Add(new List<int[]> { new int[] { 19, 39 } });
			points.Add(new List<int[]> { new int[] { 19, 38 }, new int[] { 20, 39 } });
			points.Add(new List<int[]> { new int[] { 20, 38 } });

			for (var x = 21; x <= 56; ++x)
				points.Add(new List<int[]> { new int[] { x, 38 }, new int[] { x, 39 } });

			points.Add(new List<int[]> { new int[] { 57, 38 } });
			points.Add(new List<int[]> { new int[] { 58, 38 }, new int[] { 57, 39 } });
			points.Add(new List<int[]> { new int[] { 58, 39 } });

			for (var y = 40; y <= 56; ++y)
				points.Add(new List<int[]> { new int[] { 57, y }, new int[] { 58, y } });

			points.Add(new List<int[]> { new int[] { 58, 57 } });
			points.Add(new List<int[]> { new int[] { 57, 57 }, new int[] { 58, 58 } });
			points.Add(new List<int[]> { new int[] { 57, 58 } });

			for (var x = 56; x >= 38; --x)
				points.Add(new List<int[]> { new int[] { x, 57 }, new int[] { x, 58 } });

			var seqStart = 0;
			var color = 0xff0000;
			for (var ctr = 0; ctr < points.Count; ++ctr)
			{
				int startTime = 900 * ctr / points.Count;
				foreach (var point in points[ctr])
				{
					var x = point[0];
					var y = point[1];
					AddLight(new LightData { X = x, Y = y, StartColor = 0x000000, EndColor = color, StartTime = seqStart + startTime, Duration = 300 });
					AddLight(new LightData { X = x, Y = y, StartColor = color, EndColor = 0x000000, StartTime = seqStart + startTime + 1200, Duration = 300 });
				}
			}

			seqStart = 1500;
			color = 0x00ff00;
			for (var ctr = 0; ctr < points.Count; ++ctr)
			{
				int startTime = 900 * ctr / points.Count;
				foreach (var point in points[ctr])
				{
					var x = 96 - point[1];
					var y = point[0];
					AddLight(new LightData { X = x, Y = y, StartColor = 0x000000, EndColor = color, StartTime = seqStart + startTime, Duration = 300 });
					AddLight(new LightData { X = x, Y = y, StartColor = color, EndColor = 0x000000, StartTime = seqStart + startTime + 1200, Duration = 300 });
				}
			}

			seqStart = 3000;
			color = 0x0000ff;
			for (var ctr = 0; ctr < points.Count; ++ctr)
			{
				int startTime = 900 * ctr / points.Count;
				foreach (var point in points[ctr])
				{
					var x = 96 - point[0];
					var y = 96 - point[1];
					AddLight(new LightData { X = x, Y = y, StartColor = 0x000000, EndColor = color, StartTime = seqStart + startTime, Duration = 300 });
					AddLight(new LightData { X = x, Y = y, StartColor = color, EndColor = 0x000000, StartTime = seqStart + startTime + 1200, Duration = 300 });
				}
			}

			seqStart = 4500;
			color = 0x800080;
			for (var ctr = 0; ctr < points.Count; ++ctr)
			{
				int startTime = 900 * ctr / points.Count;
				foreach (var point in points[ctr])
				{
					var x = point[1];
					var y = 96 - point[0];
					AddLight(new LightData { X = x, Y = y, StartColor = 0x000000, EndColor = color, StartTime = seqStart + startTime, Duration = 300 });
					AddLight(new LightData { X = x, Y = y, StartColor = color, EndColor = 0x000000, StartTime = seqStart + startTime + 1200, Duration = 300 });
				}
			}
		}
	}
}
