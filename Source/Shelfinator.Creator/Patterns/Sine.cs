using System;
using System.Linq;

namespace Shelfinator.Creator.Patterns
{
	class Sine : IPattern
	{
		public int PatternNumber => 12;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;

			var pattern = new Pattern();
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");
			var allLights = layout.GetAllLights();
			var allLocations = allLights.Select(light => layout.GetLightPosition(light)).ToList();
			var ordered = allLocations.OrderBy(p => p.X).ThenBy(p => p.Y);

			var rainbow7 = Helpers.Rainbow7.Multiply(Brightness).ToList();
			for (var time = 0; time < 1000; time += 20)
			{
				foreach (var light in allLights)
					pattern.AddLight(light, time, pattern.Absolute, 0x000000);

				for (var x = 0; x <= 96; ++x)
				{
					if ((x % 19) >= 2)
						continue;
					var val = (Math.Sin(2 * Math.PI * (time / 1000f + x / 96f)) + 1) * 87 / 2;
					for (var yCtr = 0; yCtr < 10; ++yCtr)
					{
						var y = yCtr + val;
						var light = layout.TryGetPositionLight(x, y);
						if (light.HasValue)
						{
							pattern.AddLight(light.Value, time, pattern.Absolute, Helpers.MultiplyColor(0xff0000, Brightness));
						}

						light = layout.TryGetPositionLight(y, x);
						if (light.HasValue)
						{
							pattern.AddLight(light.Value, time, pattern.Absolute, Helpers.MultiplyColor(0x0000ff, Brightness));
						}
					}
				}
			}
			pattern.AddLightSequence(0, 1000, 2000, 5);
			return pattern;
		}
	}
}
