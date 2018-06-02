using System;
using System.Collections.Generic;
using System.Linq;

namespace Shelfinator
{
	struct PixelColor
	{
		public byte Red { get => (byte)(color >> 16 & 0xff); set => color = value << 16 | color & 0x00ffff; }
		public byte Green { get => (byte)(color >> 8 & 0xff); set => color = value << 8 | color & 0xff00ff; }
		public byte Blue { get => (byte)(color & 0xff); set => color = value | color & 0xffff00; }
		public int Color { get => color; set => color = value; }

		int color;

		public PixelColor(byte red, byte green, byte blue)
		{
			color = 0;
			Red = red;
			Green = green;
			Blue = blue;
		}

		public PixelColor(int color)
		{
			this.color = 0;
			Color = color;
		}

		public static implicit operator PixelColor(int value) => new PixelColor(value);

		static byte ToByte(int value) => (byte)Math.Max(0, Math.Min(value, 255));
		static byte ToByte(double value) => (byte)Math.Max(0, Math.Min(value + .5, 255));

		public static PixelColor operator *(PixelColor pixel, double multiplier) => new PixelColor(ToByte(pixel.Red * multiplier), ToByte(pixel.Green * multiplier), ToByte(pixel.Blue * multiplier));

		public static PixelColor operator /(PixelColor pixel, double divisor) => pixel * (1 / divisor);

		public static PixelColor operator +(PixelColor pixel1, PixelColor pixel2) => new PixelColor(ToByte(pixel1.Red + pixel2.Red), ToByte(pixel1.Green + pixel2.Green), ToByte(pixel1.Blue + pixel2.Blue));

		public static PixelColor MixColor(PixelColor color1, PixelColor color2, double percent) => color1 == color2 ? color1 : color1 * (1 - percent) + color2 * percent;

		public static PixelColor MixColor(IReadOnlyList<PixelColor> colors, double value, double min, double max)
		{
			if (colors.Count == 0)
				throw new ArgumentException("Must provide at least one color");

			if (colors.Count == 1)
				return colors[0];

			var mult = (max - min) / (colors.Count - 1);
			var result = new PixelColor();
			for (var ctr = 0; ctr < colors.Count; ++ctr)
				result += colors[ctr] * Math.Max(0, 1 - Math.Abs(value - (min + mult * ctr)) / mult);
			return result;
		}

		public static bool operator ==(PixelColor pixel1, PixelColor pixel2) => pixel1.Color == pixel2.Color;

		public static bool operator !=(PixelColor pixel1, PixelColor pixel2) => !(pixel1.Color == pixel2.Color);

		public override bool Equals(object obj) => obj is PixelColor pixel2 ? this == pixel2 : false;

		public override int GetHashCode() => color;

		public override string ToString() => $"{color:x6}";
	}

	static class PixelColorExtensions
	{
		public static IEnumerable<PixelColor> Multiply(this IEnumerable<PixelColor> colors, double multiplier) => colors.Select(color => color * multiplier);
	}
}
