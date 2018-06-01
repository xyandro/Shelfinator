using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Shelfinator.Patterns
{
	class Hello : IPattern
	{
		public int PatternNumber => 1;

		public Pattern Render()
		{
			const double ColorBrightness = 1f / 16;
			const double WhiteBrightness = 1f / 8;

			var pattern = new Pattern();
			var header = new Layout("Shelfinator.Patterns.Layout.Layout-Header.png");
			var allLights = header.GetAllLights();
			var allLocations = allLights.Select(light => header.GetLightPosition(light)).NonNull().ToList();
			var ordered = allLocations.OrderBy(p => p.X).ThenBy(p => p.Y);
			var topLeft = ordered.First();
			var bottomRight = ordered.Last();
			var center = new Point((topLeft.X + bottomRight.X) / 2, (topLeft.Y + bottomRight.Y) / 2);

			var rainbow7 = Helpers.Rainbow7.Multiply(ColorBrightness).ToList();
			var useColors = allLocations.Select(p => PixelColor.MixColor(rainbow7, p.X, topLeft.X, bottomRight.X)).ToList();

			var origin = new Point(0, 0);
			var distances = allLocations.Select(p => new Point(Helpers.Scale(p.X, topLeft.X, bottomRight.X, -center.X, center.X), Helpers.Scale(p.Y, topLeft.Y, bottomRight.Y, -center.X, center.X))).Select(p => (p - origin).Length).ToList();
			var distanceInts = distances.Select(l => Helpers.Scale(l, distances.Min(), distances.Max(), 0, 500)).Select(l => (int)(l + 0.5)).ToList();

			var hello = new Layout("Shelfinator.Patterns.Hello.Hello.png");
			var helloLights = new HashSet<int>(header.GetMappedLights(hello, 0));
			for (var ctr = 0; ctr < allLights.Count; ++ctr)
			{
				pattern.Lights.Add(allLights[ctr], distanceInts[ctr], distanceInts[ctr] + 100, 0x000000, useColors[ctr]);
				if (!helloLights.Contains(allLights[ctr]))
					pattern.Lights.Add(allLights[ctr], distanceInts[ctr] + 250, distanceInts[ctr] + 350, useColors[ctr], 0x000000);
			}

			foreach (var light in helloLights)
			{
				pattern.Lights.Add(light, 2000, 2500, pattern.Lights.GetColor(light, 2000), new PixelColor(0xffffff) * WhiteBrightness);
				pattern.Lights.Add(light, 3000, 3500, pattern.Lights.GetColor(light, 3000), 0x000000);
			}

			pattern.Sequences.Add(new Sequence(0, 3500));
			return pattern;
		}
	}
}
