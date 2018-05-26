using System;
using System.Collections.Generic;

namespace Shelfinator
{
	struct PixelColor
	{
		public byte Red { get => red; set { red = value; color = value << 16 | color & 0x00ffff; } }
		public byte Green { get => green; set { green = value; color = value << 8 | color & 0xff00ff; } }
		public byte Blue { get => blue; set { blue = value; color = value | color & 0xffff00; } }
		public int Color { get => color; set { color = value; red = (byte)(color >> 16 & 0xff); green = (byte)(color >> 8 & 0xff); blue = (byte)(color & 0xff); } }

		byte red, green, blue;
		int color;

		public PixelColor(byte red, byte green, byte blue)
		{
			color = this.red = this.green = this.blue = 0;
			Red = red;
			Green = green;
			Blue = blue;
		}

		public PixelColor(int color)
		{
			this.color = red = green = blue = 0;
			Color = color;
		}

		public static implicit operator PixelColor(int value) => new PixelColor(value);

		static byte ToByte(int value) => (byte)Math.Max(0, Math.Min(value, 255));
		static byte ToByte(double value) => (byte)Math.Max(0, Math.Min(value + .5, 255));

		public static PixelColor operator *(PixelColor pixel, double multiplier) => new PixelColor(ToByte(pixel.Red * multiplier), ToByte(pixel.Green * multiplier), ToByte(pixel.Blue * multiplier));

		public static PixelColor operator /(PixelColor pixel, double divisor) => pixel * (1 / divisor);

		public static PixelColor operator +(PixelColor pixel1, PixelColor pixel2) => new PixelColor(ToByte(pixel1.Red + pixel2.Red), ToByte(pixel1.Green + pixel2.Green), ToByte(pixel1.Blue + pixel2.Blue));

		public static PixelColor MixColor(PixelColor color1, PixelColor color2, double percent) => color1 * (1 - percent) + color2 * percent;

		public static PixelColor MixColor(List<PixelColor> colors, double value, double min, double max)
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
	}
}
