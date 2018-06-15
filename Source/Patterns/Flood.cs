using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Shelfinator.Patterns
{
	class Flood : IPattern
	{
		public int PatternNumber => 25;

		class FloodPoint
		{
			const int Length = 100;
			Dictionary<int, int?> points;
			readonly Dictionary<int, List<int>> next;
			readonly int color;

			public FloodPoint(Dictionary<int, List<int>> next, int color, int light)
			{
				this.next = next;
				this.color = color;
				points = next.ToDictionary(pair => pair.Key, pair => default(int?));
				points[light] = 0;
			}

			public bool Increment()
			{
				var result = false;
				var newPoints = points.ToDictionary(pair => pair.Key, pair => pair.Value + 1);
				foreach (var light in points.Keys)
				{
					if ((points[light] ?? 0) < Length)
						result = true;
					if (points[light] == 0)
					{
						foreach (var nextLight in next[light])
							if (!points[nextLight].HasValue)
								newPoints[nextLight] = 0;
					}
				}
				points = newPoints;
				return result;
			}

			public void AddColor(Dictionary<int, int> lights)
			{
				if (lights.Count != next.Count)
					throw new ArgumentException(nameof(lights));
				foreach (var light in lights.Keys.ToList())
					if (points[light].HasValue)
					{
						var mult = (double)points[light].Value;
						if (mult >= Length / 2)
							mult = Length - mult;
						mult = Math.Max(Math.Min(1, mult / 20), 0);
						var thisColor = Helpers.MultiplyColor(color, mult);
						lights[light] = Helpers.AddColor(lights[light], thisColor);
					}
			}
		}

		public Pattern Render()
		{
			const double Brightness = 1f / 16;

			var layout = new Layout("Shelfinator.Patterns.Layout.Layout-Body.png");
			var lights = layout.GetAllLights();
			var next = lights.Select(l => new { light = l, point = layout.GetLightPosition(l) }).Select(o => new { o.light, points = new List<Point> { new Point(o.point.X - 1, o.point.Y), new Point(o.point.X + 1, o.point.Y), new Point(o.point.X, o.point.Y - 1), new Point(o.point.X, o.point.Y + 1) } }).ToDictionary(o => o.light, o => layout.TryGetPositionLights(o.points));
			var pattern = new Pattern();

			var points = new List<FloodPoint>();
			var time = 0;
			while (true)
			{
				var newPoints = new List<FloodPoint>();
				foreach (var point in points)
					if (point.Increment())
						newPoints.Add(point);
				points = newPoints;
				if ((!points.Any()) && (time > 1900))
					break;

				switch (time)
				{
					case 0: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), 1558)); break;
					case 112: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), 959)); break;
					case 146: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), 1622)); break;
					case 218: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), 2336)); break;
					case 252: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), 523)); break;
					case 323: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), 2359)); break;
					case 335: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), 447)); break;
					case 345: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), 2238)); break;
					case 393: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), 2410)); break;
					case 430: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), 1895)); break;
					case 450: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), 992)); break;
					case 538: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), 1023)); break;
					case 627: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), 461)); break;
					case 697: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), 883)); break;
					case 767: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), 1850)); break;
					case 851: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), 400)); break;
					case 936: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), 513)); break;
					case 991: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), 2392)); break;
					case 1012: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), 2276)); break;
					case 1093: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), 484)); break;
					case 1174: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), 479)); break;
					case 1235: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), 2272)); break;
					case 1365: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), 986)); break;
					case 1495: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), 597)); break;
					case 1535: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), 1715)); break;
					case 1567: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), 653)); break;
					case 1604: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), 2310)); break;
					case 1637: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), 2226)); break;
					case 1647: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), 1780)); break;
					case 1731: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), 2363)); break;
					case 1787: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), 1381)); break;
					case 1808: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), 2341)); break;
					case 1815: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), 2150)); break;
					case 1817: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), 2217)); break;
					case 1900: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), 2231)); break;
				}

				var colors = next.ToDictionary(x => x.Key, x => 0);
				foreach (var point in points)
					point.AddColor(colors);

				foreach (var pair in colors)
					pattern.AddLight(pair.Key, time, pattern.Absolute, pair.Value);

				++time;
			}

			pattern.AddLightSequence(0, time, 30000);

			return pattern;
		}
	}
}
