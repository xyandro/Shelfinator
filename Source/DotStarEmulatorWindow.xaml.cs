using System.Windows;
using System.Windows.Media;

namespace Shelfinator
{
	partial class DotStarEmulatorWindow
	{
		public static DependencyProperty DotStarProperty = DependencyProperty.Register(nameof(DotStar), typeof(ImageSource), typeof(DotStarEmulatorWindow));

		public ImageSource DotStar { get => (ImageSource)GetValue(DotStarProperty); set => SetValue(DotStarProperty, value); }

		public DotStarEmulatorWindow(ImageSource bitmap)
		{
			InitializeComponent();
			DotStar = bitmap;
		}
	}
}
