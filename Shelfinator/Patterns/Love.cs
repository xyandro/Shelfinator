using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Shelfinator.Patterns
{
	class Love
	{
		public Love()
		{
			var lights = new Lights();
			var header = new Layout("Shelfinator.LayoutData.Header.png");
			var allLights = header.GetAllLights();
			var allLocations = allLights.Select(light => header.GetLightPosition(light)).NonNull().ToList();
			var ordered = allLocations.OrderBy(p => p.X).ThenBy(p => p.Y);
			var topLeft = ordered.First();
			var bottomRight = ordered.Last();
			var center = new Point((topLeft.X + bottomRight.X) / 2, (topLeft.Y + bottomRight.Y) / 2);

			//var useColors = allLocations.Select(p => Helpers.MixColor(p.X, topLeft.X, bottomRight.X, Helpers.Rainbow7)).ToList();
			var cir = allLocations.Select(p => (new Point(p.X, p.Y) - center).Length).ToList();
			var useColors = cir.Select(p => Helpers.MixColor(p, cir.Min(), cir.Max(), Helpers.Rainbow7)).ToList();

			var xPos = allLocations.Select(p => p.X + p.Y).ToList();
			xPos = xPos.Select(x => Helpers.Scale(x, xPos.Min(), xPos.Max(), 0, 500)).ToList();
			var xPosInts = xPos.Select(x => (int)(x + .5)).ToList();
			var textI = new Layout("Shelfinator.LayoutData.I.png");
			var iLights = new HashSet<int>(header.GetMappedLights(textI, 0));
			for (var ctr = 0; ctr < allLights.Count; ++ctr)
			{
				lights.Add(allLights[ctr], xPosInts[ctr], xPosInts[ctr] + 100, null, useColors[ctr]);
				if (!iLights.Contains(allLights[ctr]))
					lights.Add(allLights[ctr], xPosInts[ctr] + 150, xPosInts[ctr] + 250, useColors[ctr], 0x000000);
			}

			var angles = allLocations.Select(p => new Point(Helpers.Scale(p.X, topLeft.X, bottomRight.X, -center.X, center.X), Helpers.Scale(p.Y, topLeft.Y, bottomRight.Y, -center.X, center.X))).Select(p => Helpers.Cycle(Math.Atan2(p.Y, p.X), 0, Math.PI / 2)).ToList();
			angles = angles.Select(x => Helpers.Scale(x, angles.Min(), angles.Max(), 1000, 1500)).ToList();
			var anglesInts = angles.Select(l => Helpers.Scale(l, angles.Min(), angles.Max(), 1000, 1500)).Select(l => (int)(l + 0.5)).ToList();
			var textLove = new Layout("Shelfinator.LayoutData.Love.png");
			var loveLights = new HashSet<int>(header.GetMappedLights(textLove, 0));
			for (var ctr = 0; ctr < allLights.Count; ++ctr)
			{
				lights.Add(allLights[ctr], anglesInts[ctr], anglesInts[ctr] + 100, null, useColors[ctr]);
				if (!loveLights.Contains(allLights[ctr]))
					lights.Add(allLights[ctr], anglesInts[ctr] + 150, anglesInts[ctr] + 250, useColors[ctr], 0x000000);
			}

			var origin = new Point(0, 0);
			var distances = allLocations.Select(p => new Point(Helpers.Scale(p.X, topLeft.X, bottomRight.X, -center.X, center.X), Helpers.Scale(p.Y, topLeft.Y, bottomRight.Y, -center.X, center.X))).Select(p => (p - origin).Length).ToList();
			var distanceInts = distances.Select(l => Helpers.Scale(l, distances.Min(), distances.Max(), 2000, 2500)).Select(l => (int)(l + 0.5)).ToList();
			var textPerson = new Layout("Shelfinator.LayoutData.Benji.png");
			var personLights = new HashSet<int>(header.GetMappedLights(textPerson, 0));
			for (var ctr = 0; ctr < allLights.Count; ++ctr)
			{
				lights.Add(allLights[ctr], distanceInts[ctr], distanceInts[ctr] + 100, null, useColors[ctr]);
				if (!personLights.Contains(allLights[ctr]))
					lights.Add(allLights[ctr], distanceInts[ctr] + 150, distanceInts[ctr] + 250, useColors[ctr], 0x000000);
			}

			foreach (var light in personLights)
			{
				lights.Add(light, 4500, 4750, null, 0xffffff);
				lights.Add(light, 4750, 5000, null, 0x000000);
			}

			var rand = new Random();
			var flash = allLights.Except(personLights).OrderBy(x => rand.Next()).ToList();
			flash = flash.Concat(flash).Concat(flash).Concat(flash).Concat(flash).Concat(flash).ToList();
			var flashDelay = 2500f / flash.Count();
			var flashShow = flashDelay * 5;
			for (var ctr = 0; ctr < flash.Count; ++ctr)
			{
				lights.Add(flash[ctr], (int)(3000 + ctr * flashDelay), Helpers.MixColor(1 - ctr * flashDelay / 2500, 0xffffff));
				lights.Add(flash[ctr], (int)(3000 + ctr * flashDelay + flashShow), 0x000000);
			}

			lights.Save(@"Z:\a\Pattern.dat", 6000, 2440);
			Environment.Exit(0);
		}
	}
}
