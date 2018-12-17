using System;
using System.Linq;
using System.Runtime.InteropServices;
using Shelfinator.Creator.PatternData;

namespace Shelfinator.Creator.Patterns
{
	class Plasma : IPattern
	{
		public int PatternNumber => 27;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");

			var segment = new Segment();
			var time = 0;

			using (var stream = typeof(Plasma).Assembly.GetManifestResourceStream("Shelfinator.Creator.Patterns.Layout.Plasma.gif"))
			using (var image = System.Drawing.Image.FromStream(stream))
			{
				var dimension = new System.Drawing.Imaging.FrameDimension(image.FrameDimensionsList[0]);
				var frameCount = image.GetFrameCount(dimension);
				for (var frame = 0; frame < frameCount; ++frame)
				{
					image.SelectActiveFrame(dimension, frame);
					using (var bmp = new System.Drawing.Bitmap(image))
					{
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
								var color = Helpers.MultiplyColor(BitConverter.ToInt32(data, ofs), Brightness);
								foreach (var light in layout.GetPositionLights(x, y, 1, 1))
									segment.AddLight(light, time, Segment.Absolute, color);

								ofs += sizeof(int);
							}
							ofs += lineSkip;
						}
						var hexChars = Enumerable.Range(0, 16).Select(num => $"{num:x}"[0]).ToArray();
					}
					time += 100;
				}
			}

			var pattern = new Pattern();
			pattern.AddSegment(segment, 0, time, 5000, 5);
			return pattern;
		}
	}
}
