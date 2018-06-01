﻿using System.Linq;
using System.Windows;

namespace Shelfinator.Patterns
{
	class Spin : IPattern
	{
		public int PatternNumber => 13;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			const int BladeCount = 8;
			const int Fade = 200;
			const int Delay = 300;

			var pattern = new Pattern();
			var layout = new Layout("Shelfinator.Patterns.Layout.Layout-Body.png");
			var allLights = layout.GetAllLights();
			var allLocations = allLights.Select(light => layout.GetLightPosition(light)).NonNull().ToList();
			var topLeft = allLocations.OrderBy(p => p.X).ThenBy(p => p.Y).First();
			var bottomRight = allLocations.OrderBy(p => p.X).ThenBy(p => p.Y).Last();
			var center = new Point((topLeft.X + bottomRight.X) / 2, (topLeft.Y + bottomRight.Y) / 2);

			var rainbow7 = Helpers.Rainbow7.Multiply(Brightness).Reverse().ToList();
			var useColors = allLocations.GetDistance(center).MixColors(rainbow7).ToList();

			var angles = allLocations.GetAngles(center).Cycle(0, 360 / BladeCount).AdjustToZero().Scale(0, 360 / BladeCount, 0, 1000).Round().ToList();
			for (var ctr = 0; ctr < allLights.Count; ++ctr)
				for (var repeat = 0; repeat < 2000; repeat += 1000)
				{
					pattern.Lights.Add(allLights[ctr], angles[ctr] + repeat, angles[ctr] + repeat + Fade, null, useColors[ctr]);
					pattern.Lights.Add(allLights[ctr], angles[ctr] + repeat + Delay, angles[ctr] + repeat + Delay + Fade, null, 0x000000);
				}
			pattern.Sequences.Add(new Sequence(0, 1000));
			pattern.Sequences.Add(new Sequence(1000, 2000, 7));
			pattern.Sequences.Add(new Sequence(2000, 2000 + Fade + Delay));
			return pattern;
		}
	}
}
