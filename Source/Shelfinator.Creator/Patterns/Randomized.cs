using System;
using System.Linq;
using Shelfinator.Creator.PatternData;

namespace Shelfinator.Creator.Patterns
{
	class Randomized : IPattern
	{
		public int PatternNumber => 41;

		Random rand = new Random(0xfacade);
		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			const int BaseIncrement = 3500;

			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");

			var pattern = new Pattern();
			var colors = Helpers.Rainbow6.Concat(0x000000).Multiply(Brightness).ToList();
			for (var colorCtr = 0; colorCtr < colors.Count; colorCtr++)
			{
				var baseTime = BaseIncrement * colorCtr;
				var lights = layout.GetAllLights().OrderBy(x => rand.Next()).ToList();
				for (var lightCtr = 0; lightCtr < lights.Count; lightCtr++)
					pattern.AddLight(lights[lightCtr], baseTime + lightCtr, pattern.Absolute, colors[colorCtr]);
			}

			pattern.AddLightSequence(0, pattern.MaxLightTime(), 20000);

			return pattern;
		}
	}
}
