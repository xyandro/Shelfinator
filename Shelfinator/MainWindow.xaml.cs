using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Shelfinator.Patterns;

namespace Shelfinator
{
	partial class MainWindow
	{
		public static DependencyProperty MyBitmapProperty = DependencyProperty.Register(nameof(MyBitmap), typeof(ImageSource), typeof(MainWindow));

		public ImageSource MyBitmap { get => (ImageSource)GetValue(MyBitmapProperty); set => SetValue(MyBitmapProperty, value); }
		Lights lights;

		public MainWindow()
		{
			lights = Spin.Render();
			//lights.Save(@"Z:\a\Pattern.dat", 6000, 2440);
			//Environment.Exit(0);

			InitializeComponent();
			DrawBitmap();
		}

		async void DrawBitmap()
		{
			var dotStar = new DotStar(Helpers.GetEmbeddedBitmap("Shelfinator.LayoutData.DotStar.png"));
			MyBitmap = dotStar.Bitmap;

			var time = 0;
			while (time < lights.Length)
			{
				for (var light = 0; light < dotStar.NumLights; ++light)
					dotStar.SetLight(light, (lights.GetColor(light, time) * 16).Color);
				dotStar.Show();

				await Task.Delay(1);
				time += 14;
			}

			dotStar.Clear();
			dotStar.Show();
		}
	}
}
