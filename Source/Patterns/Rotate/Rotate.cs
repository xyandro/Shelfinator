﻿using System.Linq;
using System.Windows;

namespace Shelfinator.Patterns
{
	class Rotate : IPattern
	{
		public int PatternNumber => 11;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			const int BladeCount = 8;
			const int Fade = 100;
			const int Delay = 150;

			var pattern = new Pattern();
			var layout = new Layout("Shelfinator.Patterns.Layout.Layout-Body.png");
			var squares = new Layout("Shelfinator.Patterns.Layout.Squares.png");

			foreach (var square in squares.GetAllLights())
			{
				var allLights = layout.GetMappedLights(squares, square);
				var allLocations = allLights.Select(light => layout.GetLightPosition(light)).ToList();
				var topLeft = allLocations.OrderBy(p => p.X).ThenBy(p => p.Y).First();
				var bottomRight = allLocations.OrderBy(p => p.X).ThenBy(p => p.Y).Last();
				var center = new Point((topLeft.X + bottomRight.X) / 2, (topLeft.Y + bottomRight.Y) / 2);

				var angles = allLocations.GetAngles(center).Cycle(0, 360 / BladeCount).AdjustToZero().Scale(0, 360 / BladeCount, 0, 500).Round().ToList();

				var rainbow = Helpers.Rainbow7.Concat(Helpers.Rainbow7[0]).Multiply(Brightness).ToList();
				var useColors = angles.MixColors(rainbow).ToList();
				for (var ctr = 0; ctr < allLights.Count; ++ctr)
					for (var repeat = 0; repeat < 9000; repeat += 500)
					{
						pattern.Lights.Add(allLights[ctr], angles[ctr] + repeat, angles[ctr] + repeat + Fade, null, useColors[ctr]);
						pattern.Lights.Add(allLights[ctr], angles[ctr] + repeat + Delay, angles[ctr] + repeat + Delay + Fade, null, 0x000000);
					}
			}
			pattern.Sequences.Add(new Sequence(0, 10000));
			return pattern;
		}
	}
}
