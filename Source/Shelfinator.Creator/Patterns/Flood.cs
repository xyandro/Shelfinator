using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Shelfinator.Creator.Patterns
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

			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");
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
					case 0: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), layout.GetPositionLight(11, 58))); break;
					case 112: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), layout.GetPositionLight(20, 30))); break;
					case 146: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), layout.GetPositionLight(75, 58))); break;
					case 218: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), layout.GetPositionLight(90, 95))); break;
					case 252: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), layout.GetPositionLight(1, 8))); break;
					case 323: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), layout.GetPositionLight(16, 96))); break;
					case 335: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), layout.GetPositionLight(94, 1))); break;
					case 345: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), layout.GetPositionLight(38, 94))); break;
					case 393: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), layout.GetPositionLight(67, 96))); break;
					case 430: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), layout.GetPositionLight(47, 76))); break;
					case 450: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), layout.GetPositionLight(0, 33))); break;
					case 538: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), layout.GetPositionLight(58, 35))); break;
					case 627: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), layout.GetPositionLight(96, 2))); break;
					case 697: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), layout.GetPositionLight(96, 23))); break;
					case 767: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), layout.GetPositionLight(2, 76))); break;
					case 851: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), layout.GetPositionLight(47, 1))); break;
					case 936: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), layout.GetPositionLight(20, 7))); break;
					case 991: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), layout.GetPositionLight(49, 96))); break;
					case 1012: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), layout.GetPositionLight(30, 95))); break;
					case 1093: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), layout.GetPositionLight(95, 4))); break;
					case 1174: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), layout.GetPositionLight(39, 4))); break;
					case 1235: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), layout.GetPositionLight(26, 95))); break;
					case 1365: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), layout.GetPositionLight(57, 32))); break;
					case 1495: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), layout.GetPositionLight(20, 14))); break;
					case 1535: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), layout.GetPositionLight(96, 64))); break;
					case 1567: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), layout.GetPositionLight(96, 18))); break;
					case 1604: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), layout.GetPositionLight(64, 95))); break;
					case 1637: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), layout.GetPositionLight(38, 93))); break;
					case 1647: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), layout.GetPositionLight(38, 70))); break;
					case 1731: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), layout.GetPositionLight(20, 96))); break;
					case 1787: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), layout.GetPositionLight(20, 51))); break;
					case 1808: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), layout.GetPositionLight(95, 95))); break;
					case 1815: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), layout.GetPositionLight(0, 87))); break;
					case 1817: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), layout.GetPositionLight(58, 92))); break;
					case 1900: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), layout.GetPositionLight(77, 93))); break;
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
