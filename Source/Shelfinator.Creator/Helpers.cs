using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator
{
	static class Helpers
	{
		readonly static public IReadOnlyList<int> Rainbow6 = new List<int> { 0x100000, 0x100800, 0x101000, 0x001000, 0x000010, 0x090010 };
		readonly static public IReadOnlyList<int> Rainbow7 = new List<int> { 0x100000, 0x100800, 0x101000, 0x001000, 0x000010, 0x050008, 0x09000d };
		readonly static public Point Center = new Point(48, 48);

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

		public static int Round(this double value)
		{
			var mult = value >= 0 ? 1 : -1;
			return mult * (int)(mult * value + .5);
		}

		public static byte ToByte(double value)
		{
			var intValue = value.Round();
			if (intValue < 0)
				return 0;
			if (intValue > 255)
				return 255;
			return (byte)intValue;
		}

		public static int MultiplyColor(int color, double multiplier) => MakeColor((color >> 16 & 0xff) * multiplier, (color >> 8 & 0xff) * multiplier, (color & 0xff) * multiplier);

		public static int GradientColor(int color1, int color2, double percent) => MakeColor((color1 >> 16 & 0xff) * (1 - percent) + (color2 >> 16 & 0xff) * percent, (color1 >> 8 & 0xff) * (1 - percent) + (color2 >> 8 & 0xff) * percent, (color1 & 0xff) * (1 - percent) + (color2 & 0xff) * percent);

		public static int AddColor(int color1, int color2) => MakeColor((color1 >> 16 & 0xff) + (color2 >> 16 & 0xff), (color1 >> 8 & 0xff) + (color2 >> 8 & 0xff), (color1 & 0xff) + (color2 & 0xff));

		public static int MakeColor(double red, double green, double blue) => MakeColor(ToByte(red), ToByte(green), ToByte(blue));

		public static int MakeColor(byte red, byte green, byte blue) => red << 16 | green << 8 | blue;

		public static int LimitColor(int color, int maxValue) => MakeColor(Math.Min(maxValue, color >> 16 & 0xff), Math.Min(maxValue, color >> 8 & 0xff), Math.Min(maxValue, color & 0xff));

		public static Point GetCenter(IEnumerable<Point> points)
		{
			bool first = true;
			double xMin = 0, yMin = 0, xMax = 0, yMax = 0;
			foreach (var point in points)
			{
				if (first)
				{
					xMin = xMax = point.X;
					yMin = yMax = point.Y;
					first = false;
					continue;
				}

				xMin = Math.Min(xMin, point.X);
				xMax = Math.Max(xMax, point.X);
				yMin = Math.Min(yMin, point.Y);
				yMax = Math.Max(yMax, point.Y);
			}
			if (first)
				return default(Point);
			return new Point((xMin + xMax) / 2, (yMin + yMax) / 2);
		}

		public static string PatternDirectory { get; } = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(typeof(Helpers).Assembly.Location))))), "Patterns");
		public static string AudioDirectory { get; } = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(typeof(Helpers).Assembly.Location)))), "Audio");

		public static void Swap<T>(ref T item1, ref T item2)
		{
			var tmp = item1;
			item1 = item2;
			item2 = tmp;
		}

		public static int[,] LoadImage(string streamName, double brightness)
		{
			int[,] pixels;
			using (var stream = typeof(Helpers).Assembly.GetManifestResourceStream(streamName))
			using (var image = System.Drawing.Image.FromStream(stream))
			using (var bmp = new System.Drawing.Bitmap(image))
			{
				pixels = new int[bmp.Width, bmp.Height];
				var lockBits = bmp.LockBits(new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				var data = new byte[lockBits.Stride * bmp.Height];
				Marshal.Copy(lockBits.Scan0, data, 0, data.Length);
				bmp.UnlockBits(lockBits);

				var lineSkip = lockBits.Stride - bmp.Width * sizeof(int);
				var ofs = 0;
				for (var y = 0; y < bmp.Height; ++y)
				{
					for (var x = 0; x < bmp.Width; ++x)
					{
						pixels[x, y] = Helpers.MultiplyColor(BitConverter.ToInt32(data, ofs), brightness);
						ofs += sizeof(int);
					}
					ofs += lineSkip;
				}
				var hexChars = Enumerable.Range(0, 16).Select(num => $"{num:x}"[0]).ToArray();
			}

			return pixels;
		}

		public static void DrawImage(int[,] pixels, Layout layout, Segment segment, int time, int x, int y)
		{
			for (var yCtr = 0; yCtr < pixels.GetLength(1); ++yCtr)
				for (var xCtr = 0; xCtr < pixels.GetLength(0); ++xCtr)
					foreach (var light in layout.GetPositionLights((xCtr + x) % 97, (yCtr + y) % 97, 1, 1))
						segment.AddLight(light, time, pixels[xCtr, yCtr]);
		}
	}
}
