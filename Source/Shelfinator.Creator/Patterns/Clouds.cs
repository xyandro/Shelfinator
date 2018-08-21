using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;

namespace Shelfinator.Creator.Patterns
{
	class Clouds : IPattern
	{
		public int PatternNumber => 28;

		readonly Random rand = new Random();
		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");

			int[,] pixels;
			using (var stream = typeof(Plasma).Assembly.GetManifestResourceStream("Shelfinator.Creator.Patterns.Layout.Clouds.png"))
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
						pixels[x, y] = Helpers.MultiplyColor(BitConverter.ToInt32(data, ofs), Brightness);
						ofs += sizeof(int);
					}
					ofs += lineSkip;
				}
				var hexChars = Enumerable.Range(0, 16).Select(num => $"{num:x}"[0]).ToArray();
			}

			var pattern = new Pattern();
			var time = 0;

			for (var ctr = pixels.GetLength(0); ctr > 0; --ctr)
			{
				DrawPixels(pattern, layout, pixels, ctr, 0, time);
				time += 100;
			}

			for (var ctr = pixels.GetLength(1); ctr > 0; --ctr)
			{
				DrawPixels(pattern, layout, pixels, 0, ctr, time);
				time += 100;
			}

			for (var ctr = 0; ctr < pixels.GetLength(0); ++ctr)
			{
				DrawPixels(pattern, layout, pixels, ctr, 0, time);
				time += 100;
			}

			for (var ctr = 0; ctr < pixels.GetLength(1); ++ctr)
			{
				DrawPixels(pattern, layout, pixels, 0, ctr, time);
				time += 100;
			}

			pattern.AddLightSequence(0, time, 5000, 5);

			return pattern;
		}

		List<Tuple<int, int, int>> steps = new List<Tuple<int, int, int>>();
		void DrawPixels(Pattern pattern, Layout layout, int[,] pixels, int xOfs, int yOfs, int time)
		{
			steps.Add(Tuple.Create(xOfs, yOfs, time));
			for (var y = 0; y < pixels.GetLength(1); ++y)
				for (var x = 0; x < pixels.GetLength(0); ++x)
					foreach (var light in layout.GetPositionLights(x, y, 1, 1))
						pattern.AddLight(light, time, pattern.Absolute, pixels[(x + xOfs) % pixels.GetLength(0), (y + yOfs) % pixels.GetLength(1)]);
		}
	}
}
