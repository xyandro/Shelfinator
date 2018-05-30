using System;
using System.Collections.Generic;
using System.Windows;

namespace Shelfinator.Patterns
{
	class Text
	{
		const int WIDTH = 32;
		static Dictionary<char, List<byte>> chars = new Dictionary<char, List<byte>>
		{
			['A'] = new List<byte> { 0x20, 0x50, 0xf8, 0x88, 0x88 },
			['B'] = new List<byte> { 0xf0, 0x88, 0xf0, 0x88, 0xf0 },
			['C'] = new List<byte> { 0x70, 0x88, 0x80, 0x88, 0x70 },
			['D'] = new List<byte> { 0xf0, 0x88, 0x88, 0x88, 0xf0 },
			['E'] = new List<byte> { 0xf8, 0x80, 0xe0, 0x80, 0xf8 },
			['F'] = new List<byte> { 0xf8, 0x80, 0xe0, 0x80, 0x80 },
			['G'] = new List<byte> { 0x70, 0x80, 0xb8, 0x88, 0x70 },
			['H'] = new List<byte> { 0x88, 0x88, 0xf8, 0x88, 0x88 },
			['I'] = new List<byte> { 0xf8, 0x20, 0x20, 0x20, 0xf8 },
			['J'] = new List<byte> { 0xf8, 0x10, 0x10, 0x90, 0x70 },
			['K'] = new List<byte> { 0x88, 0x90, 0xe0, 0x90, 0x88 },
			['L'] = new List<byte> { 0x80, 0x80, 0x80, 0x80, 0xf8 },
			['M'] = new List<byte> { 0x88, 0xd8, 0xa8, 0x88, 0x88 },
			['N'] = new List<byte> { 0x88, 0xc8, 0xa8, 0x98, 0x88 },
			['O'] = new List<byte> { 0x70, 0x88, 0x88, 0x88, 0x70 },
			['P'] = new List<byte> { 0xf0, 0x88, 0xf0, 0x80, 0x80 },
			['Q'] = new List<byte> { 0x70, 0x88, 0x88, 0x98, 0x78 },
			['R'] = new List<byte> { 0xf0, 0x88, 0xf0, 0x90, 0x88 },
			['S'] = new List<byte> { 0x78, 0x80, 0x70, 0x08, 0xf0 },
			['T'] = new List<byte> { 0xf8, 0x20, 0x20, 0x20, 0x20 },
			['U'] = new List<byte> { 0x88, 0x88, 0x88, 0x88, 0x70 },
			['V'] = new List<byte> { 0x88, 0x88, 0x88, 0x50, 0x20 },
			['W'] = new List<byte> { 0x88, 0x88, 0xa8, 0xd8, 0x88 },
			['X'] = new List<byte> { 0x88, 0x50, 0x20, 0x50, 0x88 },
			['Y'] = new List<byte> { 0x88, 0x50, 0x20, 0x20, 0x20 },
			['Z'] = new List<byte> { 0xf8, 0x10, 0x20, 0x40, 0xf8 },
			['!'] = new List<byte> { 0x80, 0x80, 0x80, 0x00, 0x80 },
			['\''] = new List<byte> { 0x80, 0x80, 0x00, 0x00, 0x00 },
			[' '] = new List<byte> { 0x00, 0x00, 0x00, 0x00, 0x00 },
			['.'] = new List<byte> { 0x00, 0x00, 0x00, 0x80, 0x80 },
			[','] = new List<byte> { 0x00, 0x00, 0x40, 0x40, 0x80 },
			[':'] = new List<byte> { 0x00, 0xc0, 0x00, 0xc0, 0x00 },
			['-'] = new List<byte> { 0x00, 0x00, 0x70, 0x00, 0x00 },
			['?'] = new List<byte> { 0x70, 0x88, 0x30, 0x00, 0x20 },
		};

		public static Lights Render(string text, int duration, int quantize, List<PixelColor> colors)
		{
			const double Brightness = 1f / 16;

			var lights = new Lights();
			var header = new Layout("Shelfinator.LayoutData.Header.png");
			var width = text.Length * 6 - 1;
			var display = new bool[width, 5];
			for (var ctr = 0; ctr < text.Length; ++ctr)
				for (var y = 0; y < 5; ++y)
					for (var x = 0; x < 5; ++x)
						display[ctr * 6 + x, y] = (chars[text[ctr]][y] & (1 << (7 - x))) != 0;

			var posLight = new int[WIDTH, 8];
			for (var y = 0; y < posLight.GetLength(1); ++y)
				for (var x = 0; x < posLight.GetLength(0); ++x)
					posLight[x, y] = header.GetPositionLight(new Point(x, y));

			for (var time = 0; time < duration; time += quantize)
			{
				var grid = new PixelColor[WIDTH, 8];
				var xofs = (double)time * (-width - WIDTH) / duration + WIDTH;
				for (var y = 0; y < 5; ++y)
					for (var x = 0; x < width; ++x)
						if (display[x, y])
							SetColor(grid, xofs + x, y + 1, PixelColor.MixColor(colors, x, 0, width) * Brightness);

				for (var y = 0; y < grid.GetLength(1); ++y)
					for (var x = 0; x < grid.GetLength(0); ++x)
						lights.Add(posLight[x, y], time, grid[x, y]);
			}

			for (var y = 0; y < posLight.GetLength(1); ++y)
				for (var x = 0; x < posLight.GetLength(0); ++x)
					lights.Add(posLight[x, y], duration, 0x000000);

			lights.Length = duration;

			return lights;
		}

		static void SetColor(PixelColor[,] grid, double x, int y, PixelColor color)
		{
			var xStart = (int)Math.Floor(x);
			SetValue(grid, xStart, y, color * (1 - (x - xStart)));
			SetValue(grid, xStart + 1, y, color * (x - xStart));
		}

		static void SetValue(PixelColor[,] grid, int x, int y, PixelColor color)
		{
			if ((x < 0) || (y < 0) || (x >= grid.GetLength(0)) || (y >= grid.GetLength(1)))
				return;
			grid[x, y] += color;
		}
	}
}
