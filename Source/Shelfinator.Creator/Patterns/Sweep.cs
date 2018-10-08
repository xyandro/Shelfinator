using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Shelfinator.Creator.Patterns
{
	class Sweep : IPattern
	{
		public int PatternNumber => 43;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;

			var pattern = new Pattern();
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");

			var colors = new List<List<int>>
			{
				new List<int> { 0x00ff00, 0x0000ff },
				new List<int> { 0x00ff00, 0xff0000 },
				new List<int> { 0x0000ff, 0xff0000  },
			};

			var center = new Point(48, 48);
			var color = new Dictionary<double, Dictionary<double, LightColor>>();
			for (var time = 0; time < 4320; time += 5)
			{
				var angle1 = (double)time / 3 * Math.PI / 180;
				var angle2 = ((double)-time / 4 + 120) * Math.PI / 180;
				foreach (var light in layout.GetAllLights())
				{
					var diff = layout.GetLightPosition(light) - center;
					var angle = Math.Atan2(diff.Y, diff.X);
					var colorPercent1 = Math.Round(Math.Max(1 - Math.Abs(Helpers.Cycle(angle1 - angle, -Math.PI, Math.PI)) / (Math.PI / 6), 0), 3);
					var colorPercent2 = Math.Round(Math.Max(1 - Math.Abs(Helpers.Cycle(angle2 - angle, -Math.PI, Math.PI)) / (Math.PI / 6), 0), 3);
					if (!color.ContainsKey(colorPercent1))
						color[colorPercent1] = new Dictionary<double, LightColor>();
					if (!color[colorPercent1].ContainsKey(colorPercent2))
						color[colorPercent1][colorPercent2] = new LightColor(0, 0, colors.Select(item => new List<int> { Helpers.MultiplyColor(Helpers.AddColor(Helpers.MultiplyColor(item[0], colorPercent1), Helpers.MultiplyColor(item[1], colorPercent2)), Brightness) }).ToList());
					pattern.AddLight(light, time, time + 1, null, color[colorPercent1][colorPercent2]);
				}
			}

			pattern.AddLightSequence(0, 4320, 10000, 3);

			pattern.AddPaletteSequence(0, 0);
			pattern.AddPaletteSequence(9500, 10500, null, 1);
			pattern.AddPaletteSequence(19500, 20500, null, 2);

			return pattern;
		}
	}
}
