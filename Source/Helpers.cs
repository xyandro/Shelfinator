using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Shelfinator
{
	static class Helpers
	{
		static public IReadOnlyList<int> Rainbow6 = new List<int> { 0xff0000, 0xff7f00, 0xffff00, 0x00ff00, 0x0000ff, 0x8b00ff };
		static public IReadOnlyList<int> Rainbow7 = new List<int> { 0xff0000, 0xff7f00, 0xffff00, 0x00ff00, 0x0000ff, 0x4b0082, 0x9400d3 };

		static public Stream GetEmbeddedStream(string embeddedName) => typeof(Helpers).Assembly.GetManifestResourceStream(embeddedName);
		static public BitmapSource GetEmbeddedBitmap(string embeddedName) => BitmapFrame.Create(GetEmbeddedStream(embeddedName));

		static public double Scale(double value, double oldMin, double oldMax, double newMin, double newMax) => (value - oldMin) * (newMax - newMin) / (oldMax - oldMin) + newMin;

		static public IEnumerable<double> Scale(this IEnumerable<double> values, double? oldMin, double? oldMax, double newMin, double newMax)
		{
			oldMin = oldMin ?? values.DefaultIfEmpty(0).Min();
			oldMax = oldMax ?? values.DefaultIfEmpty(0).Max();
			return values.Select(value => Scale(value, oldMin.Value, oldMax.Value, newMin, newMax));
		}

		static public IEnumerable<Point> Scale(this IEnumerable<Point> points, double? oldMinX, double? oldMinY, double? oldMaxX, double? oldMaxY, double newMinX, double newMinY, double newMaxX, double newMaxY)
		{
			oldMinX = oldMinX ?? points.Select(point => point.X).DefaultIfEmpty(0).Min();
			oldMaxX = oldMaxX ?? points.Select(point => point.X).DefaultIfEmpty(0).Max();
			oldMinY = oldMinY ?? points.Select(point => point.Y).DefaultIfEmpty(0).Min();
			oldMaxY = oldMaxY ?? points.Select(point => point.Y).DefaultIfEmpty(0).Max();
			return points.Select(point => new Point(Scale(point.X, oldMinX.Value, oldMaxX.Value, newMinX, newMaxX), Scale(point.Y, oldMinY.Value, oldMaxY.Value, newMinY, newMaxY)));
		}

		public static double Cycle(double value, double min, double max)
		{
			var range = max - min;
			if (range <= 0)
				throw new Exception("Minimum must be less than maximum.");
			value -= min;
			var mult = (int)(value / range);
			if (value < 0)
				--mult;
			value -= mult * range;
			value += min;
			return value;
		}

		public static IEnumerable<double> Cycle(this IEnumerable<double> values, double min, double max) => values.Select(value => Cycle(value, min, max));

		public static IEnumerable<double> AdjustToZero(this IEnumerable<double> values, double? min = null)
		{
			min = min ?? values.DefaultIfEmpty(0).Min();
			return values.Select(value => value - min.Value);
		}

		public static IEnumerable<double> GetDistance(this IEnumerable<Point> points, Point? reference = null)
		{
			reference = reference ?? new Point();
			return points.Select(point => (point - reference.Value).Length);
		}

		public static double GetAngle(Point point, Point? reference = null) => Math.Atan2(point.Y - (reference?.Y ?? 0), point.X - (reference?.X ?? 0)) / Math.PI * 180;

		public static IEnumerable<double> GetAngles(this IEnumerable<Point> points, Point? reference = null) => points.Select(point => GetAngle(point, reference));

		public static IEnumerable<int> Round(this IEnumerable<double> values) => values.Select(Round);

		public static int Round(this double value) => (int)(value + .5);

		public static int ToByte(double value)
		{
			var intValue = value.Round();
			if (intValue < 0)
				return 0;
			if (intValue > 255)
				return 255;
			return intValue;
		}

		public static int MultiplyColor(int color, double multiplier) => ToByte((color >> 16 & 0xff) * multiplier) << 16 | ToByte((color >> 8 & 0xff) * multiplier) << 8 | ToByte((color & 0xff) * multiplier);

		public static int GradientColor(int color1, int color2, double percent) => ToByte((color1 >> 16 & 0xff) * (1 - percent) + (color2 >> 16 & 0xff) * percent) << 16 | ToByte((color1 >> 8 & 0xff) * (1 - percent) + (color2 >> 8 & 0xff) * percent) << 8 | ToByte((color1 & 0xff) * (1 - percent) + (color2 & 0xff) * percent);
	}
}
