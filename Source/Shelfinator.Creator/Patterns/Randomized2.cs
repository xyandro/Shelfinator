using System;
using System.Linq;
using Shelfinator.Creator.PatternData;

namespace Shelfinator.Creator.Patterns
{
	class Randomized2 : IPattern
	{
		public int PatternNumber => 42;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			const int RandomCount = 5;
			const int EndDelay = 10;

			var pattern = new Pattern();
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");
			var color = new LightColor(0, 5, Helpers.Rainbow6.Multiply(Brightness).ToList());
			var rand = new Random(0xfacade);
			var time = 0;
			for (var ctr = 0; ctr < Helpers.Rainbow6.Count; ++ctr)
			{
				foreach (var light in layout.GetAllLights())
				{
					var count = 0;
					while (true)
					{
						var colorIndex = rand.Next(Helpers.Rainbow6.Count);
						pattern.AddLight(light, time + count, time + count + 1, null, color, colorIndex);
						if ((++count >= RandomCount) && (colorIndex == ctr))
							break;
					}
				}
				time = pattern.MaxLightTime() + EndDelay;
			}

			pattern.AddLightSequence(0, time, 20000);

			return pattern;
		}
	}
}
