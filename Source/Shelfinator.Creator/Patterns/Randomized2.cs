using System;
using System.Collections.Generic;
using System.Linq;

namespace Shelfinator.Creator.Patterns
{
	class Randomized2 : IPattern
	{
		public int PatternNumber => 42;

		const double Brightness = 1f / 16;
		const int RandomCount = 5;

		List<int> GetColors()
		{
			var result = new List<int>();
			result.Add(0x000000);
			for (var rainbowCtr = 0; rainbowCtr < Helpers.Rainbow6.Count; rainbowCtr++)
			{
				if (rainbowCtr != 0)
					for (var ctr = 0; ctr < RandomCount; ++ctr)
						result.Add(Helpers.MakeColor((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256)));
				result.Add(Helpers.Rainbow6[rainbowCtr]);
			}
			result.Add(0x000000);
			return result.Multiply(Brightness).ToList();
		}

		Random rand = new Random(0xfacade);
		public Pattern Render()
		{
			var pattern = new Pattern();
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");
			foreach (var light in layout.GetAllLights())
			{
				var color = new LightColor(0, 1000, GetColors());
				pattern.AddLight(light, 0, 1000, color, 0, color, 1000, true);
			}

			pattern.AddLightSequence(0, 1000, 20000);

			return pattern;
		}
	}
}
