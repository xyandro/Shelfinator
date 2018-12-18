//using System.Linq;
//using System.Runtime.InteropServices;
//using System;
//using Shelfinator.Creator.SongData;
//
//namespace Shelfinator.Creator.Songs
//{
//	class Clouds : ISong
//	{
//		public int SongNumber => 40;
//
//		readonly Random rand = new Random();
//		public Song Render()
//		{
//			const double Brightness = 1f / 16;
//			const int Time = 2000;
//			var layout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");
//
//			var empty = new int[97, 97];
//			var pixels = GetPixels("Shelfinator.Creator.Songs.Layout.Clouds.png", Brightness);
//
//			var segment = new Segment();
//
//			DrawPixels(segment, layout, empty, pixels, 0);
//			DrawPixels(segment, layout, pixels, pixels, 97);
//			DrawPixels(segment, layout, pixels, empty, 194);
//
//			var song = new Song();
//			song.AddSegment(segment, 0, 97, Time);
//			song.AddSegment(segment, 97, 194, Time, 10);
//			song.AddSegment(segment, 194, 291, Time);
//			return song;
//		}
//
//		int[,] GetPixels(string fileName, double Brightness)
//		{
//			int[,] pixels;
//			using (var stream = typeof(Clouds).Assembly.GetManifestResourceStream(fileName))
//			using (var image = System.Drawing.Image.FromStream(stream))
//			using (var bmp = new System.Drawing.Bitmap(image))
//			{
//				if ((bmp.Width != 97) || (bmp.Height != 97))
//					throw new Exception("Invalid image");
//
//				pixels = new int[bmp.Width, bmp.Height];
//				var lockBits = bmp.LockBits(new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
//				var data = new byte[lockBits.Stride * bmp.Height];
//				Marshal.Copy(lockBits.Scan0, data, 0, data.Length);
//				bmp.UnlockBits(lockBits);
//
//				var lineSkip = lockBits.Stride - bmp.Width * sizeof(int);
//				var ofs = 0;
//				for (var y = 0; y < bmp.Height; ++y)
//				{
//					for (var x = 0; x < bmp.Width; ++x)
//					{
//						pixels[x, y] = Helpers.MultiplyColor(BitConverter.ToInt32(data, ofs), Brightness);
//						ofs += sizeof(int);
//					}
//					ofs += lineSkip;
//				}
//				var hexChars = Enumerable.Range(0, 16).Select(num => $"{num:x}"[0]).ToArray();
//			}
//
//			return pixels;
//		}
//
//		void DrawPixels(Segment segment, Layout layout, int[,] pixels1, int[,] pixels2, int startTime)
//		{
//			for (var offset = 0; offset < 97; ++offset)
//			{
//				var mixPercent = (double)offset / 96;
//				for (var y = 0; y < 97; ++y)
//					for (var x = 0; x < 97; ++x)
//						foreach (var light in layout.GetPositionLights((x + offset) % 97, (y + offset) % 97, 1, 1))
//							segment.AddLight(light, startTime + offset, Segment.Absolute, Helpers.GradientColor(pixels1[x, y], pixels2[x, y], mixPercent));
//			}
//		}
//	}
//}
