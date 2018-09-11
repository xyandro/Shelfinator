using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Shelfinator.Creator
{
	class Layout
	{
		readonly Dictionary<int, List<Point>> lightLocations = new Dictionary<int, List<Point>>();

		readonly Dictionary<int, Dictionary<int, int>> locationLights = new Dictionary<int, Dictionary<int, int>>();
		public int Width { get; }
		public int Height { get; }

		public Layout(string embeddedName) : this(Helpers.GetEmbeddedBitmap(embeddedName))
		{
		}

		public Layout(BitmapSource bitmap)
		{
			var buffer = new uint[bitmap.PixelWidth * bitmap.PixelHeight];
			var stride = bitmap.PixelWidth * ((bitmap.Format.BitsPerPixel + 7) / 8);
			bitmap.CopyPixels(buffer, stride, 0);

			var index = -1;
			for (var y = 0; y < bitmap.PixelHeight; ++y)
				for (var x = 0; x < bitmap.PixelWidth; ++x)
				{
					++index;
					if ((buffer[index] & 0xff000000) == 0)
						continue;
					var light = (int)(buffer[index] & 0xffffff);
					if (!lightLocations.ContainsKey(light))
						lightLocations[light] = new List<Point>();
					lightLocations[light].Add(new Point(x, y));
					if (!locationLights.ContainsKey(x))
						locationLights[x] = new Dictionary<int, int>();
					locationLights[x][y] = light;
				}

			Width = bitmap.PixelWidth;
			Height = bitmap.PixelHeight;
		}

		public List<int> GetPositionLights(List<Point> list) => list.Select(p => GetPositionLight(p)).ToList();

		public List<int> GetPositionLights(Rect rect) => GetPositionLights(rect.X.Round(), rect.Y.Round(), rect.Width.Round(), rect.Height.Round());

		public List<int> GetPositionLights(Point p, Size size) => GetPositionLights(p.X.Round(), p.Y.Round(), size.Width.Round(), size.Height.Round());

		public List<int> GetPositionLights(Point p, double width, double height) => GetPositionLights(p.X.Round(), p.Y.Round(), width.Round(), height.Round());

		public List<int> GetPositionLights(double x, double y, double width, double height) => GetPositionLights(x.Round(), y.Round(), width.Round(), height.Round());

		public List<int> GetPositionLights(int x, int y, int width, int height)
		{
			var result = new List<int>();
			for (var yCtr = y; yCtr < y + height; ++yCtr)
				for (var xCtr = x; xCtr < x + width; ++xCtr)
				{
					var light = TryGetPositionLight(xCtr, yCtr);
					if (light.HasValue)
						result.Add(light.Value);
				}
			return result;
		}

		public int GetPositionLight(Point point) => TryGetPositionLight(point).Value;

		public int GetPositionLight(int x, int y) => TryGetPositionLight(x, y).Value;

		public int GetPositionLight(double x, double y) => TryGetPositionLight(x, y).Value;

		public List<int> TryGetPositionLights(List<Point> list) => list.Select(p => TryGetPositionLight(p)).NonNull().ToList();

		public int? TryGetPositionLight(Point point) => TryGetPositionLight(point.X.Round(), point.Y.Round());

		public int? TryGetPositionLight(int x, int y)
		{
			if (!locationLights.ContainsKey(x))
				return null;
			if (!locationLights[x].ContainsKey(y))
				return null;
			return locationLights[x][y];
		}

		public int? TryGetPositionLight(double x, double y) => TryGetPositionLight((int)(x + .5), (int)(y + .5));

		public Point GetLightPosition(int light) => TryGetLightPosition(light).Value;

		public Point? TryGetLightPosition(int light)
		{
			if (!lightLocations.ContainsKey(light))
				return null;
			return lightLocations[light][0];
		}

		public List<Point> GetLightPositions(int light)
		{
			if (!lightLocations.ContainsKey(light))
				return new List<Point>();
			return lightLocations[light];
		}

		public List<int> GetMappedLights(Layout layout, int light) => layout.GetLightPositions(light).Select(point => GetPositionLight(point)).ToList();

		public List<int> GetAllLights() => lightLocations.Keys.ToList();
	}
}
