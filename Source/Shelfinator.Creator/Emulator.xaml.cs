using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Shelfinator.Interop;

namespace Shelfinator.Creator
{
	partial class Emulator : IDotStar, IAudio
	{
		public static int TestPosition { get; set; }
		readonly Controller controller;
		readonly Dictionary<int, List<int>> bufferPosition = new Dictionary<int, List<int>>();
		readonly WriteableBitmap bitmap;
		readonly uint[] buffer;
		bool playing = false;

		public Emulator(bool small)
		{
			InitializeComponent();

			var image = small ? "Shelfinator.Creator.Songs.Layout.DotStar-Small.png" : "Shelfinator.Creator.Songs.Layout.DotStar.png";
			bitmap = new WriteableBitmap(Helpers.GetEmbeddedBitmap(image));
			buffer = new uint[bitmap.PixelWidth * bitmap.PixelHeight];
			bitmap.CopyPixels(buffer, bitmap.BackBufferStride, 0);

			bufferPosition = new Dictionary<int, List<int>>();
			var index = -1;
			for (var y = 0; y < bitmap.PixelHeight; ++y)
				for (var x = 0; x < bitmap.PixelWidth; ++x)
				{
					++index;
					if ((buffer[index] & 0xff000000) != 0x01000000)
						continue;
					var light = (int)(buffer[index] & 0xffffff);
					if (!bufferPosition.ContainsKey(light))
						bufferPosition[light] = new List<int>();
					bufferPosition[light].Add(index);
				}

			if (small)
			{
				SizeToContent = SizeToContent.WidthAndHeight;
				dotStarBitmap.Width = bitmap.PixelWidth;
				dotStarBitmap.Height = bitmap.PixelHeight;
				RenderOptions.SetBitmapScalingMode(dotStarBitmap, BitmapScalingMode.NearestNeighbor);

				Loaded += (s, e) =>
				{
					var workArea = SystemParameters.WorkArea;
					Left = (workArea.Width - Width) / 2 + workArea.Left;
					Top = workArea.Height - Height + workArea.Top;
				};
			}
			else
			{
				WindowStyle = WindowStyle.None;
				WindowState = WindowState.Maximized;
			}
			dotStarBitmap.Source = bitmap;
			controller = new Controller(this, this);
			mediaPlayer.MediaEnded += (s, e) => Stop();
		}

		public void Run(List<int> songNumbers, bool startPaused) => new Thread(() => controller.Run(songNumbers, startPaused)).Start();
		public void Test(int firstLight, int lightCount, int concurrency, int delay, byte brightness) => new Thread(() => controller.Test(firstLight, lightCount, concurrency, delay, brightness)).Start();
		public void TestAll(int lightCount, int delay, byte brightness) => new Thread(() => controller.TestAll(lightCount, delay, brightness)).Start();

		bool ControlDown => Keyboard.Modifiers.HasFlag(ModifierKeys.Control);
		bool ShiftDown => Keyboard.Modifiers.HasFlag(ModifierKeys.Shift);

		protected override void OnKeyDown(KeyEventArgs e)
		{
			e.Handled = true;
			switch (e.Key)
			{
				case Key.Escape: controller.AddRemoteCode(RemoteCode.Play); break;
				case Key.Space: controller.AddRemoteCode(RemoteCode.Pause); break;
				case Key.Left: controller.AddRemoteCode(ControlDown ? RemoteCode.Previous : RemoteCode.Rewind); break;
				case Key.Right: controller.AddRemoteCode(ControlDown ? RemoteCode.Next : RemoteCode.FastForward); break;
				case Key.Up: controller.AddRemoteCode(RemoteCode.VolumeUp); break;
				case Key.Down: controller.AddRemoteCode(RemoteCode.VolumeDown); break;
				case Key.Enter: controller.AddRemoteCode(RemoteCode.Enter); break;
				case Key.D0: controller.AddRemoteCode(RemoteCode.D0); break;
				case Key.D1: controller.AddRemoteCode(RemoteCode.D1); break;
				case Key.D2: controller.AddRemoteCode(RemoteCode.D2); break;
				case Key.D3: controller.AddRemoteCode(RemoteCode.D3); break;
				case Key.D4: controller.AddRemoteCode(RemoteCode.D4); break;
				case Key.D5: controller.AddRemoteCode(RemoteCode.D5); break;
				case Key.D6: controller.AddRemoteCode(RemoteCode.D6); break;
				case Key.D7: controller.AddRemoteCode(RemoteCode.D7); break;
				case Key.D8: controller.AddRemoteCode(RemoteCode.D8); break;
				case Key.D9: controller.AddRemoteCode(RemoteCode.D9); break;
				case Key.I: controller.AddRemoteCode(RemoteCode.Info); break;
				case Key.S: controller.Stop(); break;
				case Key.P: CopyPosition(); break;
				case Key.T: SetTime(TestPosition); break;
				default: e.Handled = false; break;
			}
			base.OnKeyDown(e);
		}

		void CopyPosition()
		{
			var str = (ShiftDown ? DateTime.Now.TimeOfDay : mediaPlayer.Position).TotalMilliseconds.ToString();
			if (ControlDown)
				str = Clipboard.GetText() + "," + str;
			Clipboard.SetText(str);
		}

		public void Show(int[] lights)
		{
			for (var light = 0; light < lights.Length; ++light)
				if (bufferPosition.ContainsKey(light))
				{
					var color = (uint)(0xff000000 | Helpers.MultiplyColor(lights[light] >> 8, 16));
					foreach (var position in bufferPosition[light])
						buffer[position] = color;
				}
			Dispatcher.Invoke(() => bitmap.WritePixels(new Int32Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight), buffer, bitmap.BackBufferStride, 0));
		}

		public void Play(string fileName)
		{
			Stop();

			Dispatcher.Invoke(() =>
			{
				mediaPlayer.Source = new Uri(Path.Combine(Directory.GetCurrentDirectory(), fileName));
				mediaPlayer.Play();
			});
			playing = true;
		}

		public void Stop()
		{
			if (!playing)
				return;
			Dispatcher.Invoke(() => mediaPlayer.Stop());
			playing = false;
		}

		public int GetTime()
		{
			if (!playing)
				return -1;
			return Dispatcher.Invoke(() => mediaPlayer.Position.TotalMilliseconds.Round());
		}

		public void SetTime(int time)
		{
			if (!playing)
				return;
			Dispatcher.Invoke(() => mediaPlayer.Position = TimeSpan.FromMilliseconds(time));
		}
	}
}
