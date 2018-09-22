using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Shelfinator.Creator.Patterns
{
	class Running : IPattern
	{
		public int PatternNumber => 40;

		readonly Random rand = new Random();
		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			const int Time = 2000;
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");

			var empty = new int[97, 97];
			var pixels = GetPixels("Shelfinator.Creator.Patterns.Layout.Running.png", Brightness);

			var pattern = new Pattern();

			DrawPixels(pattern, layout, empty, pixels, 0);
			DrawPixels(pattern, layout, pixels, pixels, 97);
			DrawPixels(pattern, layout, pixels, empty, 194);

			pattern.AddLightSequence(0, 97, Time);
			pattern.AddLightSequence(97, 194, Time, 10);
			pattern.AddLightSequence(194, 291, Time);

			return pattern;
		}

		int[,] GetPixels(string fileName, double Brightness)
		{
			int[,] pixels;
			using (var stream = typeof(Running).Assembly.GetManifestResourceStream(fileName))
			using (var image = System.Drawing.Image.FromStream(stream))
			using (var bmp = new System.Drawing.Bitmap(image))
			{
				if ((bmp.Width != 97) || (bmp.Height != 97))
					throw new Exception("Invalid image");

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
						pixels[x, y] = Helpers.MultiplyColor(BitConverter.ToInt32(data, ofs), Brightness);
						ofs += sizeof(int);
					}
					ofs += lineSkip;
				}
				var hexChars = Enumerable.Range(0, 16).Select(num => $"{num:x}"[0]).ToArray();
			}

			return pixels;
		}

		void DrawPixels(Pattern pattern, Layout layout, int[,] pixels1, int[,] pixels2, int startTime)
		{
			for (var offset = 0; offset < 97; ++offset)
			{
				var mixPercent = (double)offset / 96;
				for (var y = 0; y < 97; ++y)
					for (var x = 0; x < 97; ++x)
						foreach (var light in layout.GetPositionLights((x + offset) % 97, (y + offset) % 97, 1, 1))
							pattern.AddLight(light, startTime + offset, pattern.Absolute, Helpers.GradientColor(pixels1[x, y], pixels2[x, y], mixPercent));
			}
		}
	}
}
