using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Shelfinator.Patterns
{
	class LightsBuffer
	{
		public int Width { get; private set; }
		public int Height { get; private set; }
		readonly int[] buffer;

		public LightsBuffer(int width, int height)
		{
			Width = width;
			Height = height;
			buffer = new int[width * height];
		}

		public int this[int x, int y]
		{
			get => buffer[x + y * Width];
			set => buffer[x + y * Width] = value;
		}

		unsafe public void WriteBitmap(WriteableBitmap bitmap)
		{
			bitmap.Lock();
			var length = buffer.Length * 4;
			fixed (void* p = &buffer[0])
				Buffer.MemoryCopy(p, (void*)bitmap.BackBuffer, length, length);
			bitmap.AddDirtyRect(new Int32Rect(0, 0, Width, Height));
			bitmap.Unlock();
		}
	}
}
