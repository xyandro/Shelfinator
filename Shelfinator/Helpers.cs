using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;

namespace Shelfinator
{
	class Helpers
	{
		static public List<int> Rainbow6 = new List<int> { 0xff0000, 0xff7f00, 0xffff00, 0x00ff00, 0x0000ff, 0x8b00ff };
		static public List<int> Rainbow7 = new List<int> { 0xff0000, 0xff7f00, 0xffff00, 0x00ff00, 0x0000ff, 0x4b0082, 0x9400d3 };

		static public Stream GetEmbeddedStream(string embeddedName) => typeof(Helpers).Assembly.GetManifestResourceStream(embeddedName);
		static public BitmapSource GetEmbeddedBitmap(string embeddedName) => BitmapFrame.Create(GetEmbeddedStream(embeddedName));
		static public double Scale(double value, double oldMin, double oldMax, double newMin, double newMax) => (value - oldMin) * (newMax - newMin) / (oldMax - oldMin) + newMin;

		public static int AddColor(int color1, int color2) => (((byte)Math.Min(255, ((color1 >> 16) & 0xff) + ((color2 >> 16) & 0xff))) << 16) | (((byte)Math.Min(255, ((color1 >> 8) & 0xff) + ((color2 >> 8) & 0xff))) << 8) | (((byte)Math.Min(255, ((color1 >> 0) & 0xff) + ((color2 >> 0) & 0xff))) << 0);

		public static int MixColor(double percent, int color)
		{
			percent = Math.Max(0, Math.Min(percent, 1));
			return (byte)Math.Min(255, ((color >> 16) & 0xff) * percent + 0.5) << 16 | (byte)Math.Min(255, ((color >> 8) & 0xff) * percent + 0.5) << 8 | (byte)Math.Min(255, ((color >> 0) & 0xff) * percent + 0.5) << 0;
		}

		public static int MixColor(double percent, int color1, int color2) => MixColor(1 - percent, color1) + MixColor(percent, color2);

		public static int MixColor(double value, double min, double max, List<int> colors)
		{
			if (colors.Count == 1)
				return colors[0];

			var mult = (max - min) / (colors.Count - 1);
			int result = 0;
			for (var ctr = 0; ctr < colors.Count; ++ctr)
			{
				var percent = Math.Max(0, 1 - Math.Abs(value - (min + mult * ctr)) / mult);
				var mixColor = MixColor(percent, colors[ctr]);
				result = AddColor(result, mixColor);
			}
			return result;
		}

		public static double Cycle(double value, double min, double max)
		{
			var diff = max - min;
			while (value < min)
				value += diff;
			while (value >= max)
				value -= diff;
			return value;
		}
	}
}
