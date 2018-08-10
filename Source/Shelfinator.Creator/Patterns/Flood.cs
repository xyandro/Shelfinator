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
				if ((!points.Any()) && (time > 3000))
					break;

				switch (time)
				{
					case 0: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), layout.GetPositionLight(0, 95))); break;
					case 200: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), layout.GetPositionLight(0, 0))); break;
					case 400: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), layout.GetPositionLight(95, 0))); break;
					case 600: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), layout.GetPositionLight(95, 95))); break;
					case 800:
						points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), layout.GetPositionLight(0, 95)));
						points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), layout.GetPositionLight(95, 0)));
						break;
					case 1000:
						points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), layout.GetPositionLight(0, 0)));
						points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), layout.GetPositionLight(95, 95)));
						break;
					case 1200:
						points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), layout.GetPositionLight(0, 95)));
						points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), layout.GetPositionLight(95, 0)));
						break;
					case 1400: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), layout.GetPositionLight(20, 30))); break;
					case 1461: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), layout.GetPositionLight(75, 58))); break;
					case 1593: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), layout.GetPositionLight(90, 95))); break;
					case 1654: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), layout.GetPositionLight(1, 8))); break;
					case 1784: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), layout.GetPositionLight(16, 96))); break;
					case 1807: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), layout.GetPositionLight(94, 1))); break;
					case 1825: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), layout.GetPositionLight(38, 94))); break;
					case 1910: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), layout.GetPositionLight(67, 96))); break;
					case 1979: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), layout.GetPositionLight(47, 76))); break;
					case 2014: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), layout.GetPositionLight(0, 33))); break;
					case 2175: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), layout.GetPositionLight(58, 35))); break;
					case 2337: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), layout.GetPositionLight(96, 2))); break;
					case 2463: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), layout.GetPositionLight(96, 23))); break;
					case 2591: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), layout.GetPositionLight(2, 76))); break;
					case 2744: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), layout.GetPositionLight(47, 1))); break;
					case 2898: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), layout.GetPositionLight(20, 7))); break;
					case 3000: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), layout.GetPositionLight(49, 96))); break;
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
