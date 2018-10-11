using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Shelfinator.Creator.Patterns
{
	class Smiley : IPattern
	{
		public int PatternNumber => 28;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			const int Radius = 15;
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");

			int[,] pixels;
			using (var stream = typeof(Plasma).Assembly.GetManifestResourceStream("Shelfinator.Creator.Patterns.Layout.Smiley.png"))
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

			for (var angle = 0; angle < 360; angle += 3)
			{
				var xOfs = Radius + Math.Sin(angle * Math.PI / 180) * Radius;
				var yOfs = Radius + Math.Cos(angle * Math.PI / 180) * Radius;
				DrawPixels(pattern, layout, pixels, xOfs.Round(), yOfs.Round(), angle);
			}

			pattern.AddLightSequence(0, 360, 5000, 3);

			return pattern;
		}

		void DrawPixels(Pattern pattern, Layout layout, int[,] pixels, int xOfs, int yOfs, int time)
		{
			for (var y = 0; y < pixels.GetLength(1); ++y)
				for (var x = 0; x < pixels.GetLength(0); ++x)
					foreach (var light in layout.GetPositionLights(x - xOfs, y - yOfs, 1, 1))
						pattern.AddLight(light, time, pattern.Absolute, pixels[x, y]);
		}
	}
}
