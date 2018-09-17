using System.Linq;

namespace Shelfinator.Creator.Patterns
{
	class SquareWarp : IPattern
	{
		public int PatternNumber => 38;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;

			var pattern = new Pattern();

			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");

			var color = new LightColor(0, 5, Helpers.Rainbow6.Multiply(Brightness).ToList());

			for (var square = 0; square < 18; ++square)
			{
				for (var x = 0; x <= 48; ++x)
				{
					var time = square * 19 + x;
					var lights = layout.GetPositionLights(48 - x, 48 - x, x * 2 + 1, x * 2 + 1).Except(layout.GetPositionLights(48 - x + 1, 48 - x + 1, x * 2 - 1, x * 2 - 1));
					foreach (var light in lights)
						pattern.AddLight(light, time, color, square % 6);
					foreach (var light in lights)
						pattern.AddLight(light, time + 1, pattern.Absolute, 0x000000);
				}
			}

			pattern.AddLightSequence(0, 114, 5000);
			pattern.AddLightSequence(114, 228, 5000, 2);
			pattern.AddLightSequence(228, 391, 7150);

			return pattern;
		}
	}
}
