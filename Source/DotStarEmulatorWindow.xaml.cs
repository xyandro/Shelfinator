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

		protected override void OnKeyDown(KeyEventArgs e)
		{
			e.Handled = true;
			switch (e.Key)
			{
				case Key.Enter: remote.Add(RefRemoteCode.Play); break;
				case Key.Space: remote.Add(RefRemoteCode.Pause); break;
				default: e.Handled = false; break;
			}
			base.OnKeyDown(e);
		}
	}
}
