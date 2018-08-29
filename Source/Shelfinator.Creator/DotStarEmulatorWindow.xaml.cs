using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Shelfinator.Interop;

namespace Shelfinator.Creator
{
	partial class DotStarEmulatorWindow
	{
		readonly Controller controller;
		public DotStarEmulatorWindow(bool small)
		{
			var image = small ? "Shelfinator.Creator.Patterns.Layout.DotStar-Small.png" : "Shelfinator.Creator.Patterns.Layout.DotStar.png";
			var dotStar = new DotStarEmulator(Dispatcher, Helpers.GetEmbeddedBitmap(image));
			controller = new Controller(dotStar);

			InitializeComponent();
			if (small)
			{
				SizeToContent = SizeToContent.WidthAndHeight;
				dotStarBitmap.Width = dotStar.Bitmap.PixelWidth;
				dotStarBitmap.Height = dotStar.Bitmap.PixelHeight;
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
			dotStarBitmap.Source = dotStar.Bitmap;
		}

		public void Run(List<int> patternNumbers) => new Thread(() => controller.Run(patternNumbers)).Start();
		public void Test(int firstLight, int lightCount, int concurrency, int delay, byte brightness) => new Thread(() => controller.Test(firstLight, lightCount, concurrency, delay, brightness)).Start();
		public void TestAll(int lightCount, int delay, byte brightness) => new Thread(() => controller.TestAll(lightCount, delay, brightness)).Start();

		bool ControlDown => Keyboard.Modifiers.HasFlag(ModifierKeys.Control);

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
				default: e.Handled = false; break;
			}
			base.OnKeyDown(e);
		}
	}
}
