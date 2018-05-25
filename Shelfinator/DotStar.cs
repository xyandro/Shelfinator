using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Shelfinator.Patterns
{
	class DotStar
	{
		public WriteableBitmap Bitmap { get; }
		public int NumLights { get; }
		readonly uint[] buffer;
		readonly Dictionary<int, List<int>> bufferPosition;

		public DotStar(Stream stream)
		{
			Bitmap = new WriteableBitmap(BitmapFrame.Create(stream));

			buffer = new uint[Bitmap.PixelWidth * Bitmap.PixelHeight];
			Bitmap.CopyPixels(buffer, Bitmap.BackBufferStride, 0);

			bufferPosition = new Dictionary<int, List<int>>();
			var index = 0;
			for (var y = 0; y < Bitmap.PixelHeight; ++y)
				for (var x = 0; x < Bitmap.PixelWidth; ++x)
				{
					var value = (int)(buffer[index++] & 0xffffff);
					if (value == 0xffffff)
						continue;
					if (!bufferPosition.ContainsKey(value))
						bufferPosition[value] = new List<int>();
					bufferPosition[value].Add(index);
				}

			NumLights = bufferPosition.Count;

			Clear();
			Show();
		}

		public void Clear()
		{
			for (var ctr = 0; ctr < buffer.Length; ++ctr)
				buffer[ctr] = 0xff000000;
		}

		public void SetLight(int light, uint color)
		{
			if (!bufferPosition.ContainsKey(light))
				return;
			foreach (var position in bufferPosition[light])
				buffer[position] = color;
		}

		public void Show() => Bitmap.WritePixels(new Int32Rect(0, 0, Bitmap.PixelWidth, Bitmap.PixelHeight), buffer, Bitmap.BackBufferStride, 0);

		public void Save(string fileName)
		{
			using (var output = File.Create(fileName))
			{
				var encoder = new PngBitmapEncoder();
				encoder.Frames.Add(BitmapFrame.Create(Bitmap));
				encoder.Save(output);
			}
		}
	}
}
