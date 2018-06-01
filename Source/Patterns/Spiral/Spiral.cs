using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Shelfinator.Patterns
{
	class Spiral : IPattern
	{
		public int PatternNumber => 14;

		public Lights Render()
		{
			const double Brightness = 1f / 16;

			var lights = new Lights();
			var layout = new Layout("Shelfinator.Patterns.Layout.Layout-Body.png");
			var spiral = new Layout("Shelfinator.Patterns.Spiral.Spiral.png");

			var points = spiral.GetAllLights().OrderBy(light => light).Select(light => spiral.GetLightPositions(light)).ToList();

			var colors = new List<PixelColor> { 0xff0000, 0x00ff00, 0x0000ff, 0x800080 };

			for (var pass = 0; pass < 4; ++pass)
			{
				Func<Point, int> GetLight;
				switch (pass)
				{
					case 0: GetLight = point => layout.GetPositionLight(point); break;
					case 1: GetLight = point => layout.GetPositionLight(new Point(layout.Height - 1 - point.Y, point.X)); break;
					case 2: GetLight = point => layout.GetPositionLight(new Point(layout.Width - 1 - point.X, layout.Height - 1 - point.Y)); break;
					case 3: GetLight = point => layout.GetPositionLight(new Point(point.Y, layout.Width - 1 - point.X)); break;
					default: throw new Exception("Invalid pass");
				}
				var color = colors[pass] * Brightness;
				for (var ctr = 0; ctr < points.Count; ++ctr)
				{
					int startTime = 1500 * pass + 900 * ctr / points.Count;
					foreach (var point in points[ctr])
					{
						var light = GetLight(point);
						lights.Add(light, startTime, startTime + 300, 0x000000, color);
						lights.Add(light, startTime + 1200, startTime + 1200 + 300, color, 0x000000);
					}
				}
			}
			lights.Length = 6900;

			return lights;
		}
	}
}
