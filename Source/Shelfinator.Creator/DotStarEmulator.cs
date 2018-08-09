using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Shelfinator.Interop;

namespace Shelfinator.Creator
{
	class DotStarEmulator : IDotStar
	{
		public WriteableBitmap Bitmap { get; }

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
		}

		public unsafe void Show(int* lights, int count)
		{
			for (var light = 0; light < count; ++light)
				if (bufferPosition.ContainsKey(light))
				{
					var color = (uint)(0xff000000 | Helpers.MultiplyColor(lights[light] >> 8, 16));
					foreach (var position in bufferPosition[light])
						buffer[position] = color;
				}
			dispatcher.Invoke(() => Bitmap.WritePixels(new Int32Rect(0, 0, Bitmap.PixelWidth, Bitmap.PixelHeight), buffer, Bitmap.BackBufferStride, 0));
		}
	}
}
