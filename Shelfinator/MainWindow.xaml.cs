using System.Threading.Tasks;
using System.Windows.Media;
using Shelfinator.Patterns;

namespace Shelfinator
{
	partial class MainWindow
	{
		public ImageSource MyBitmap { get; set; }
		readonly DotStar dotStar;
		//Pattern pattern;

		public MainWindow()
		{
			new Love();
			dotStar = new DotStar(Helpers.GetEmbeddedBitmap("Shelfinator.LayoutData.DotStar.png"));
			MyBitmap = dotStar.Bitmap;

			InitializeComponent();
			//pattern = new Spiral();
			DrawBitmap();
		}

		async void DrawBitmap()
		{
			var ctr = 0;
			while (true)
			{
				dotStar.SetLight(ctr++, 0xff0000);
				dotStar.Show();
				await Task.Delay(1);
			}
			//	var start = DateTime.Now;
			//	var lastTime = -1;
			//	var lightIndex = 0;
			//	var lights = pattern.Lights;
			//	var active = new List<LightData>();
			//	while (true)
			//	{
			//		var time = (int)(DateTime.Now - start).TotalMilliseconds;
			//		if (time <= lastTime)
			//		{
			//			await Task.Delay(1);
			//			continue;
			//		}
			//		lastTime = time;

			//		while ((lightIndex < lights.Count) && (time >= lights[lightIndex].StartTime))
			//		{
			//			active.Add(lights[lightIndex]);
			//			curLights[lights[lightIndex].X, lights[lightIndex].Y] = lights[lightIndex].EndColor;
			//			++lightIndex;
			//		}

			//		for (var ctr = 0; ctr < active.Count;)
			//			if (time >= active[ctr].EndTime)
			//				active.RemoveAt(ctr);
			//			else
			//			{
			//				curLights[active[ctr].X, active[ctr].Y] = active[ctr].ColorAtTime(time);
			//				++ctr;
			//			}

			//		curLights.WriteBitmap(MyBitmap);
			//	}
		}
	}
}
