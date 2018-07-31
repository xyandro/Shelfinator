using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Shelfinator.Creator.Patterns
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
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");
			var allLights = layout.GetAllLights();
			var allLocations = allLights.Select(light => layout.GetLightPosition(light)).ToList();
			var topLeft = allLocations.OrderBy(p => p.X).ThenBy(p => p.Y).First();
			var bottomRight = allLocations.OrderBy(p => p.X).ThenBy(p => p.Y).Last();
			var center = new Point((topLeft.X + bottomRight.X) / 2, (topLeft.Y + bottomRight.Y) / 2);

			var blues = new List<int> { 0x32d0d3, 0x0000ff, 0x66ffff }.Multiply(Brightness).ToList();
			var reds = new List<int> { 0xff6969, 0xd34949, 0xda3232, 0xb80000, 0x620000 }.Multiply(Brightness).ToList();
			var rainbow = Helpers.Rainbow7.Multiply(Brightness).Reverse().ToList();
			var distances = allLocations.GetDistance(center).ToList();
			var useColors = new LightColor(distances.Min().Round(), distances.Max().Round(), blues, reds, rainbow);

			var angles = allLocations.GetAngles(center).Cycle(0, 360 / BladeCount).AdjustToZero().Scale(0, 360 / BladeCount, 0, 1000).Round().ToList();
			for (var ctr = 0; ctr < allLights.Count; ++ctr)
				for (var repeat = 0; repeat < 2000; repeat += 1000)
				{
					pattern.AddLight(allLights[ctr], angles[ctr] + repeat, angles[ctr] + repeat + Fade, null, 0, useColors, distances[ctr].Round());
					pattern.AddLight(allLights[ctr], angles[ctr] + repeat + Delay, angles[ctr] + repeat + Delay + Fade, null, pattern.Absolute, 0x000000);
				}

			pattern.AddLightSequence(0, 1000);
			pattern.AddLightSequence(1000, 2000, repeat: 14);
			pattern.AddLightSequence(2000, 2000 + Fade + Delay);

			pattern.AddPaletteSequence(5000, 6000, null, 1);
			pattern.AddPaletteSequence(10000, 11000, null, 2);

			return pattern;
		}
	}
}
