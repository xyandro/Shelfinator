using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Shelfinator
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
				case Key.Escape: remote.Add(RefRemoteCode.Play); break;
				case Key.Space: remote.Add(RefRemoteCode.Pause); break;
				case Key.Left: remote.Add(ControlDown ? RefRemoteCode.Previous : RefRemoteCode.Rewind); break;
				case Key.Right: remote.Add(ControlDown ? RefRemoteCode.Next : RefRemoteCode.FastForward); break;
				case Key.Enter: remote.Add(RefRemoteCode.Enter); break;
				case Key.D0: remote.Add(RefRemoteCode.D0); break;
				case Key.D1: remote.Add(RefRemoteCode.D1); break;
				case Key.D2: remote.Add(RefRemoteCode.D2); break;
				case Key.D3: remote.Add(RefRemoteCode.D3); break;
				case Key.D4: remote.Add(RefRemoteCode.D4); break;
				case Key.D5: remote.Add(RefRemoteCode.D5); break;
				case Key.D6: remote.Add(RefRemoteCode.D6); break;
				case Key.D7: remote.Add(RefRemoteCode.D7); break;
				case Key.D8: remote.Add(RefRemoteCode.D8); break;
				case Key.D9: remote.Add(RefRemoteCode.D9); break;
				case Key.I: remote.Add(RefRemoteCode.Info); break;
				default: e.Handled = false; break;
			}
			base.OnKeyDown(e);
		}
	}
}
