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
		readonly RemoteEmulator remote;
		public DotStarEmulatorWindow()
		{
			var dotStar = new DotStarEmulator(Dispatcher, Helpers.GetEmbeddedBitmap("Shelfinator.Creator.Patterns.Layout.DotStar.png"));
			remote = new RemoteEmulator();
			controller = new Controller(dotStar, remote);

			InitializeComponent();
			DotStarBitmap = dotStar.Bitmap;
		}

		public void Run(List<int> patternNumbers) => new Thread(() => controller.Run(patternNumbers)).Start();
		public void Test(int lightCount, int concurrency, int delay) => new Thread(() => controller.Test(lightCount, concurrency, delay)).Start();

		bool ControlDown => Keyboard.Modifiers.HasFlag(ModifierKeys.Control);

		protected override void OnKeyDown(KeyEventArgs e)
		{
			e.Handled = true;
			switch (e.Key)
			{
				case Key.Escape: remote.Add(RemoteCode.Play); break;
				case Key.Space: remote.Add(RemoteCode.Pause); break;
				case Key.Left: remote.Add(ControlDown ? RemoteCode.Previous : RemoteCode.Rewind); break;
				case Key.Right: remote.Add(ControlDown ? RemoteCode.Next : RemoteCode.FastForward); break;
				case Key.Enter: remote.Add(RemoteCode.Enter); break;
				case Key.D0: remote.Add(RemoteCode.D0); break;
				case Key.D1: remote.Add(RemoteCode.D1); break;
				case Key.D2: remote.Add(RemoteCode.D2); break;
				case Key.D3: remote.Add(RemoteCode.D3); break;
				case Key.D4: remote.Add(RemoteCode.D4); break;
				case Key.D5: remote.Add(RemoteCode.D5); break;
				case Key.D6: remote.Add(RemoteCode.D6); break;
				case Key.D7: remote.Add(RemoteCode.D7); break;
				case Key.D8: remote.Add(RemoteCode.D8); break;
				case Key.D9: remote.Add(RemoteCode.D9); break;
				case Key.I: remote.Add(RemoteCode.Info); break;
				default: e.Handled = false; break;
			}
			base.OnKeyDown(e);
		}
	}
}
