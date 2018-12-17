using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Shelfinator.Creator.PatternData;

namespace Shelfinator.Creator.Patterns
{
	class Love : IPattern
	{
		public int PatternNumber => 10;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			const double FlashBrightness = .5;

			var segment = new Segment();
			var header = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Header.png");
			var allLights = header.GetAllLights();
			var allLocations = allLights.Select(light => header.GetLightPosition(light)).ToList();
			var ordered = allLocations.OrderBy(p => p.X).ThenBy(p => p.Y);
			var topLeft = ordered.First();
			var bottomRight = ordered.Last();
			var center = new Point((topLeft.X + bottomRight.X) / 2, (topLeft.Y + bottomRight.Y) / 2);

			var rainbow7 = Helpers.Rainbow7.Multiply(Brightness).ToList();
			var useColor = new LightColor(topLeft.X.Round(), bottomRight.X.Round(), rainbow7);
			//var cir = allLocations.Select(p => (new Point(p.X, p.Y) - center).Length).ToList();
			//var useColors = cir.Select(p => PixelColor.MixColor(rainbow7, p, cir.Min(), cir.Max())).ToList();
			//var col = rainbow7.AsEnumerable().Reverse().Concat(rainbow7.Skip(1)).ToList();
			//var useColors = allLocations.Select(p => PixelColor.MixColor(col, p.X, topLeft.X, bottomRight.X)).ToList();

			var xPos = allLocations.Select(p => p.X + p.Y).ToList();
			xPos = xPos.Select(x => Helpers.Scale(x, xPos.Min(), xPos.Max(), 0, 500)).ToList();
			var xPosInts = xPos.Select(x => (int)(x + .5)).ToList();
			var textI = new Layout("Shelfinator.Creator.Patterns.Love.I.png");
			var iLights = new HashSet<int>(header.GetMappedLights(textI, 0));
			for (var ctr = 0; ctr < allLights.Count; ++ctr)
			{
				segment.AddLight(allLights[ctr], xPosInts[ctr], xPosInts[ctr] + 100, null, 0, useColor, allLocations[ctr].X.Round());
				if (!iLights.Contains(allLights[ctr]))
					segment.AddLight(allLights[ctr], xPosInts[ctr] + 150, xPosInts[ctr] + 250, null, Segment.Absolute, 0x000000);
			}

			var angles = allLocations.Select(p => new Point(Helpers.Scale(p.X, topLeft.X, bottomRight.X, -center.X, center.X), Helpers.Scale(p.Y, topLeft.Y, bottomRight.Y, -center.X, center.X))).Select(p => Helpers.Cycle(Math.Atan2(p.Y, p.X), 0, Math.PI / 2)).ToList();
			angles = angles.Select(x => Helpers.Scale(x, angles.Min(), angles.Max(), 1000, 1500)).ToList();
			var anglesInts = angles.Select(l => Helpers.Scale(l, angles.Min(), angles.Max(), 1000, 1500)).Select(l => (int)(l + 0.5)).ToList();
			var textLove = new Layout("Shelfinator.Creator.Patterns.Love.Love.png");
			var loveLights = new HashSet<int>(header.GetMappedLights(textLove, 0));
			for (var ctr = 0; ctr < allLights.Count; ++ctr)
			{
				segment.AddLight(allLights[ctr], anglesInts[ctr], anglesInts[ctr] + 100, null, 0, useColor, allLocations[ctr].X.Round());
				if (!loveLights.Contains(allLights[ctr]))
					segment.AddLight(allLights[ctr], anglesInts[ctr] + 150, anglesInts[ctr] + 250, null, Segment.Absolute, 0x000000);
			}

			var origin = new Point(0, 0);
			var distances = allLocations.Select(p => new Point(Helpers.Scale(p.X, topLeft.X, bottomRight.X, -center.X, center.X), Helpers.Scale(p.Y, topLeft.Y, bottomRight.Y, -center.X, center.X))).Select(p => (p - origin).Length).ToList();
			var distanceInts = distances.Select(l => Helpers.Scale(l, distances.Min(), distances.Max(), 2000, 2500)).Select(l => (int)(l + 0.5)).ToList();
			var textPerson = new Layout("Shelfinator.Creator.Patterns.Love.Benji.png");
			var personLights = new HashSet<int>(header.GetMappedLights(textPerson, 0));
			for (var ctr = 0; ctr < allLights.Count; ++ctr)
			{
				segment.AddLight(allLights[ctr], distanceInts[ctr], distanceInts[ctr] + 100, null, 0, useColor, allLocations[ctr].X.Round());
				if (!personLights.Contains(allLights[ctr]))
					segment.AddLight(allLights[ctr], distanceInts[ctr] + 150, distanceInts[ctr] + 250, null, Segment.Absolute, 0x000000);
			}

			foreach (var light in personLights)
			{
				segment.AddLight(light, 4500, 4750, null, Segment.Absolute, Helpers.MultiplyColor(0xffffff, Brightness));
				segment.AddLight(light, 4750, 5000, null, Segment.Absolute, 0x000000);
			}

			var rand = new Random();
			var flash = allLights.Except(personLights).OrderBy(x => rand.Next()).ToList();
			flash = flash.Concat(flash).Concat(flash).Concat(flash).Concat(flash).Concat(flash).ToList();
			var flashDelay = 2500f / flash.Count();
			var flashShow = flashDelay * 5;
			var flashWhiteColor = Helpers.MultiplyColor(0xffffff, FlashBrightness);
			var flashWhite = new LightColor(0, flash.Count, new List<int> { flashWhiteColor, flashWhiteColor, flashWhiteColor, flashWhiteColor, flashWhiteColor, 0x000000 });
			for (var ctr = 0; ctr < flash.Count; ++ctr)
			{
				segment.AddLight(flash[ctr], (int)(3000 + ctr * flashDelay), flashWhite, ctr);
				segment.AddLight(flash[ctr], (int)(3000 + ctr * flashDelay + flashShow), Segment.Absolute, 0x000000);
			}
			var pattern = new Pattern();
			pattern.AddSegment(segment, 0, 5600);
			return pattern;
		}
	}
}
