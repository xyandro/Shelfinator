using System;
using System.Windows;

namespace Shelfinator.Creator.Patterns
{
	class Sweep : IPattern
	{
		public int PatternNumber => 43;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			const int Color1 = 0xff0000;
			const int Color2 = 0x0000ff;

			var pattern = new Pattern();
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");

			var center = new Point(48, 48);
			for (var time = 0; time < 4320; time += 5)
			{
				var angle1 = (double)time / 3 * Math.PI / 180;
				var angle2 = ((double)-time / 4 + 120) * Math.PI / 180;
				foreach (var light in layout.GetAllLights())
				{
					var diff = layout.GetLightPosition(light) - center;
					var angle = Math.Atan2(diff.Y, diff.X);
					var colorPercent1 = Math.Max(1 - Math.Abs(Helpers.Cycle(angle1 - angle, -Math.PI, Math.PI)) / (Math.PI / 12), 0);
					var colorPercent2 = Math.Max(1 - Math.Abs(Helpers.Cycle(angle2 - angle, -Math.PI, Math.PI)) / (Math.PI / 12), 0);
					var color = Helpers.AddColor(Helpers.MultiplyColor(Color1, colorPercent1), Helpers.MultiplyColor(Color2, colorPercent2));
					pattern.AddLight(light, time, time + 1, null, pattern.Absolute, Helpers.MultiplyColor(color, Brightness));
				}
			}

			pattern.AddLightSequence(0, 4320, 10000, 3);

			return pattern;
		}
	}
}
