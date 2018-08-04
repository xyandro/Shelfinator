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
		public static DependencyProperty DotStarBitmapProperty = DependencyProperty.Register(nameof(DotStarBitmap), typeof(ImageSource), typeof(DotStarEmulatorWindow));

		public ImageSource DotStarBitmap { get => (ImageSource)GetValue(DotStarBitmapProperty); set => SetValue(DotStarBitmapProperty, value); }

		readonly Controller controller;
		public DotStarEmulatorWindow()
		{
			var dotStar = new DotStarEmulator(Dispatcher, Helpers.GetEmbeddedBitmap("Shelfinator.Creator.Patterns.Layout.DotStar.png"));
			controller = new Controller(dotStar);

			InitializeComponent();
			DotStarBitmap = dotStar.Bitmap;
		}

		public void Run(List<int> patternNumbers) => new Thread(() => controller.Run(patternNumbers)).Start();
		public void Test(int lightCount, int concurrency, int delay, int brightness) => new Thread(() => controller.Test(lightCount, concurrency, delay, brightness)).Start();

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
