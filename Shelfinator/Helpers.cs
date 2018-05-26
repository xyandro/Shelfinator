using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;

namespace Shelfinator
{
	class Helpers
	{
		static public List<PixelColor> Rainbow6 = new List<PixelColor> { 0xff0000, 0xff7f00, 0xffff00, 0x00ff00, 0x0000ff, 0x8b00ff };
		static public List<PixelColor> Rainbow7 = new List<PixelColor> { 0xff0000, 0xff7f00, 0xffff00, 0x00ff00, 0x0000ff, 0x4b0082, 0x9400d3 };

		static public Stream GetEmbeddedStream(string embeddedName) => typeof(Helpers).Assembly.GetManifestResourceStream(embeddedName);
		static public BitmapSource GetEmbeddedBitmap(string embeddedName) => BitmapFrame.Create(GetEmbeddedStream(embeddedName));
		static public double Scale(double value, double oldMin, double oldMax, double newMin, double newMax) => (value - oldMin) * (newMax - newMin) / (oldMax - oldMin) + newMin;

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
