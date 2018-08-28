using System.Collections.Generic;
using System.Linq;

namespace Shelfinator.Creator.Patterns
{
	class Halves : IPattern
	{
		public int PatternNumber => 32;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");
			var lights0 = Enumerable.Range(0, 97).SelectMany(y => Enumerable.Range(0, 97).Where(x => (x % 2) == (y % 2)).SelectMany(x => layout.GetPositionLights(x, y, 1, 1))).ToList();
			var lights1 = layout.GetAllLights().Except(lights0).ToList();

			var pattern = new Pattern();

			var color0 = new LightColor(0, 1, new List<List<int>>
			{
				new List<int> { 0xff0000 }.Multiply(Brightness).ToList(),
				new List<int> { 0xffff00 }.Multiply(Brightness).ToList(),
				new List<int> { 0xffff00 }.Multiply(Brightness).ToList(),
				new List<int> { 0x0000ff }.Multiply(Brightness).ToList(),
				new List<int> { 0x0000ff }.Multiply(Brightness).ToList(),
			});
			var color1 = new LightColor(0, 1, new List<List<int>>
			{
				new List<int> { 0xff7f00 }.Multiply(Brightness).ToList(),
				new List<int> { 0xff7f00 }.Multiply(Brightness).ToList(),
				new List<int> { 0x00ff00 }.Multiply(Brightness).ToList(),
				new List<int> { 0x00ff00 }.Multiply(Brightness).ToList(),
				new List<int> { 0x8b00ff }.Multiply(Brightness).ToList(),
			});

			foreach (var light in lights0)
			{
				pattern.AddLight(light, 0, 1, null, 0, color0, 1);
				pattern.AddLight(light, 4, 5, null, 0, pattern.Absolute, 0x000000);
				pattern.AddLight(light, 6, 7, null, 0, color0, 1);
				pattern.AddLight(light, 12, 13, null, 0, pattern.Absolute, 0x000000);
			}

			foreach (var light in lights1)
			{
				pattern.AddLight(light, 2, 3, null, 0, color1, 1);
				pattern.AddLight(light, 8, 9, null, 0, pattern.Absolute, 0x000000);
				pattern.AddLight(light, 10, 11, null, 0, color1, 1);
				pattern.AddLight(light, 14, 15, null, 0, pattern.Absolute, 0x000000);
			}

			const int Speed = 2000;
			pattern.AddLightSequence(0, 4, Speed * 4);
			pattern.AddLightSequence(4, 12, Speed * 8, 2);
			pattern.AddLightSequence(12, 16, Speed * 4);

			pattern.AddPaletteSequence(0 * Speed, 0);
			pattern.AddPaletteSequence(6 * Speed, 1);
			pattern.AddPaletteSequence(10 * Speed, 2);
			pattern.AddPaletteSequence(14 * Speed, 3);
			pattern.AddPaletteSequence(18 * Speed, 4);

			return pattern;
		}
	}
}
