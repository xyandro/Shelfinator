using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Shelfinator.Interop;

namespace Shelfinator.Creator
{
	partial class DotStarEmulatorWindow
	{
		public static DependencyProperty DotStarProperty = DependencyProperty.Register(nameof(DotStar), typeof(ImageSource), typeof(DotStarEmulatorWindow));

		public ImageSource DotStar { get => (ImageSource)GetValue(DotStarProperty); set => SetValue(DotStarProperty, value); }

		readonly RemoteEmulator remote;
		public DotStarEmulatorWindow(ImageSource bitmap, RemoteEmulator remote)
		{
			this.remote = remote;
			InitializeComponent();
			DotStar = bitmap;
		}

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
