using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Shelfinator
{
	class DotStarEmulator : IDotStar
	{
		public WriteableBitmap Bitmap { get; }
		public int NumLights { get; }

		readonly Dispatcher dispatcher;
		readonly uint[] buffer;
		readonly Dictionary<int, List<int>> bufferPosition;

		public DotStarEmulator(Dispatcher dispatcher, BitmapSource source)
		{
			this.dispatcher = dispatcher;
			Bitmap = new WriteableBitmap(source);

			buffer = new uint[Bitmap.PixelWidth * Bitmap.PixelHeight];
			Bitmap.CopyPixels(buffer, Bitmap.BackBufferStride, 0);

			bufferPosition = new Dictionary<int, List<int>>();
			var index = -1;
			for (var y = 0; y < Bitmap.PixelHeight; ++y)
				for (var x = 0; x < Bitmap.PixelWidth; ++x)
				{
					++index;
					if ((buffer[index] & 0xff000000) != 0x01000000)
						continue;
					var light = (int)(buffer[index] & 0xffffff);
					if (!bufferPosition.ContainsKey(light))
						bufferPosition[light] = new List<int>();
					bufferPosition[light].Add(index);
				}

			NumLights = bufferPosition.Count;

			Clear();
			Show();
		}

		public void Clear()
		{
			foreach (var data in bufferPosition)
				foreach (var value in data.Value)
					buffer[value] = 0xff000000;
		}

		public void SetPixelColor(int light, int color)
		{
			if (!bufferPosition.ContainsKey(light))
				return;
			foreach (var position in bufferPosition[light])
				buffer[position] = (uint)(0xff000000 | Helpers.MultiplyColor(color, 16));
		}

		public void Show()
		{
			dispatcher.Invoke(() => Bitmap.WritePixels(new Int32Rect(0, 0, Bitmap.PixelWidth, Bitmap.PixelHeight), buffer, Bitmap.BackBufferStride, 0));
			Thread.Sleep(1);
		}

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
