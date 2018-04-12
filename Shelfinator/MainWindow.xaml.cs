using Shelfinator.Patterns;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shelfinator
{
	partial class MainWindow
	{
		public WriteableBitmap MyBitmap { get; set; }
		LightsBuffer curLights;
		Pattern pattern;

		public MainWindow()
		{
			curLights = new LightsBuffer(Layout.WIDTH, Layout.HEIGHT);
			MyBitmap = new WriteableBitmap(curLights.Width, curLights.Height, 96, 96, PixelFormats.Bgr32, null);
			InitializeComponent();
			pattern = new Spiral();
			DrawBitmap();
		}

		async void DrawBitmap()
		{
			var start = DateTime.Now;
			var lastTime = -1;
			var lightIndex = 0;
			var lights = pattern.Lights;
			var active = new List<LightData>();
			while (true)
			{
				var time = (int)(DateTime.Now - start).TotalMilliseconds;
				if (time <= lastTime)
				{
					await Task.Delay(1);
					continue;
				}
				lastTime = time;

				while ((lightIndex < lights.Count) && (time >= lights[lightIndex].StartTime))
				{
					active.Add(lights[lightIndex]);
					curLights[lights[lightIndex].X, lights[lightIndex].Y] = lights[lightIndex].EndColor;
					++lightIndex;
				}

				for (var ctr = 0; ctr < active.Count;)
					if (time >= active[ctr].EndTime)
						active.RemoveAt(ctr);
					else
					{
						curLights[active[ctr].X, active[ctr].Y] = active[ctr].ColorAtTime(time);
						++ctr;
					}

				curLights.WriteBitmap(MyBitmap);
			}
		}
	}
}
