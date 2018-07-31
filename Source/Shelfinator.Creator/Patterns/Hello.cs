using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Shelfinator.Creator.Patterns
{
	class Hello : IPattern
	{
		public int PatternNumber => 1;

		List<int> GetLights(Layout layout)
		{
			const string data =
"2,1/2,2/2,3/2,4/2,5/3,1/3,2/3,3/3,4/3,5/4,3/5,3/6,1/6,2/6,3/6,4/6,5/7,1/7,2/7,3/7,4/7,5/9,1/9,2/9,3/9,4/9,5/10,1/10,2/10,3/10,4/10,5/11,1/11,3/11,5/12,1/12,3/12,5/14,1/14,2/14,3/14,4/14,5/15,1/15,2/15,3/15,4/15,5/16,5/17,5/19,1/19,2/19,3/19,4/19,5/20,1/20,2/20,3/20,4/20,5/21,5/22,5/24,2/24,3/24,4/25,1/25,2/25,3/25,4/25,5/26,1/26,5/27,1/27,5/28,1/28,2/28,3/28,4/28,5/29,2/29,3/29,4";

			return data.Split('/').Select(p => p.Split(',').Select(int.Parse).ToList()).Select(p => layout.GetPositionLight(p[0], p[1])).ToList();
		}

		public Pattern Render()
		{
			const double ColorBrightness = 1f / 16;
			const double WhiteBrightness = 1f / 8;

			var pattern = new Pattern();
			var header = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Header.png");
			var allLights = header.GetAllLights();
			var allLocations = allLights.Select(light => header.GetLightPosition(light)).ToList();
			var ordered = allLocations.OrderBy(p => p.X).ThenBy(p => p.Y);
			var topLeft = ordered.First();
			var bottomRight = ordered.Last();
			var center = new Point((topLeft.X + bottomRight.X) / 2, (topLeft.Y + bottomRight.Y) / 2);

			var rainbow7 = Helpers.Rainbow7.Multiply(ColorBrightness).ToList();
			var useColor = new LightColor(topLeft.X.Round(), bottomRight.X.Round(), rainbow7);

			var origin = new Point(0, 0);
			var distances = allLocations.Select(p => new Point(Helpers.Scale(p.X, topLeft.X, bottomRight.X, -center.X, center.X), Helpers.Scale(p.Y, topLeft.Y, bottomRight.Y, -center.X, center.X))).Select(p => (p - origin).Length).ToList();
			var distanceInts = distances.Select(l => Helpers.Scale(l, distances.Min(), distances.Max(), 0, 500)).Select(l => (int)(l + 0.5)).ToList();

			var helloLights = GetLights(header);
			for (var ctr = 0; ctr < allLights.Count; ++ctr)
			{
				pattern.AddLight(allLights[ctr], distanceInts[ctr], distanceInts[ctr] + 100, pattern.Absolute, 0x000000, useColor, allLocations[ctr].X.Round());
				if (!helloLights.Contains(allLights[ctr]))
					pattern.AddLight(allLights[ctr], distanceInts[ctr] + 250, distanceInts[ctr] + 350, null, pattern.Absolute, 0x000000);
			}

			foreach (var light in helloLights)
			{
				pattern.AddLight(light, 2000, 2500, null, pattern.Absolute, Helpers.MultiplyColor(0xffffff, WhiteBrightness));
				pattern.AddLight(light, 3000, 3500, null, pattern.Absolute, 0x000000);
			}

			pattern.AddLightSequence(0, 3500);
			return pattern;
		}
	}
}
