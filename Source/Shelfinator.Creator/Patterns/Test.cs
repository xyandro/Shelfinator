using System.Collections.Generic;
using System.Linq;

namespace Shelfinator.Patterns
{
	class Test : IPattern
	{
		public int PatternNumber => 5;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			const int NumLights = 2440;

			var pattern = new Pattern();

			for (var light = 0; light < NumLights; ++light)
			{
				pattern.AddLight(light, light, pattern.Absolute, Helpers.MultiplyColor(0xff0000, Brightness));
				pattern.AddLight(light, light + 50, pattern.Absolute, 0x000000);
				pattern.AddLight(light, light + NumLights, pattern.Absolute, Helpers.MultiplyColor(0x00ff00, Brightness));
				pattern.AddLight(light, light + NumLights + 50, pattern.Absolute, 0x000000);
				pattern.AddLight(light, light + NumLights * 2, pattern.Absolute, Helpers.MultiplyColor(0x0000ff, Brightness));
				pattern.AddLight(light, light + NumLights * 2 + 50, pattern.Absolute, 0x000000);
			}

			pattern.AddLightSequence(0, NumLights * 3 + 50, 10000, 100);

			return pattern;
		}
	}
}
