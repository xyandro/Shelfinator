﻿using System;
using System.Linq;
using System.Windows;

namespace Shelfinator.Creator.Patterns
{
	class CircleWarp : IPattern
	{
		public int PatternNumber => 38;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;

			var pattern = new Pattern();

			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");

			var color = new LightColor(0, 5, Helpers.Rainbow6.Multiply(Brightness).ToList());

			var center = new Point(48, 48);
			var distances = layout.GetAllLights().Select(light => Tuple.Create(light, (layout.GetLightPosition(light) - center).Length)).ToList();

			for (var square = 0; square < 18; ++square)
			{
				for (var x = 0; x <= 66; ++x)
				{
					var time = square * 17 + x;
					var lights = distances.Where(tuple => (tuple.Item2 >= x) && (tuple.Item2 < x + 2)).Select(tuple => tuple.Item1).ToList();
					foreach (var light in lights)
						pattern.AddLight(light, time, color, square % 6);
					foreach (var light in lights)
						pattern.AddLight(light, time + 1, pattern.Absolute, 0x000000);
				}
			}

			pattern.AddLightSequence(0, 102, 5000);
			pattern.AddLightSequence(102, 204, 5000, 2);
			pattern.AddLightSequence(204, 356, 7655);

			return pattern;
		}
	}
}