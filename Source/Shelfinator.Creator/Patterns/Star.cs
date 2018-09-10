using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Shelfinator.Creator.Patterns
{
	class Star : IPattern
	{
		public int PatternNumber => 34;

		readonly Random rand = new Random();
		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");

			var pattern = new Pattern();

			var useColor = new Dictionary<int, int>
			{
				[1] = 0xff0000,
				[2] = 0xff0000,
				[3] = 0xff0000,
				[4] = 0x00ff00,
				[5] = 0x0000ff,
			};
			var time = 0;
			for (var numPoints = 2; numPoints <= 5; ++numPoints)
			{
				var startTime = time;
				for (var angle = 0; angle < 360; angle += 5)
				{
					var percent = angle / 360D;
					AddPoints(pattern, layout, numPoints, angle, percent, time, Brightness, Helpers.GradientColor(useColor[numPoints - 1], useColor[numPoints], percent));
					time += 5;
				}
				pattern.AddLightSequence(startTime, time, 2500, 1);

				startTime = time;
				for (var angle = 0; angle < 360; angle += 5)
				{
					AddPoints(pattern, layout, numPoints, angle, 1, time, Brightness, useColor[numPoints]);
					time += 5;
				}
				pattern.AddLightSequence(startTime, time, 2500, 2);
			}


			return pattern;
		}

		private void AddPoints(Pattern pattern, Layout layout, int numPoints, double addAngle, double firstPercent, int time, double Brightness, int color)
		{
			var points = GetPoints(numPoints, addAngle, firstPercent);

			var canvas = new Canvas() { Width = 97, Height = 97 };
			var bytes = BitConverter.GetBytes(color);
			var brush = new SolidColorBrush(Color.FromArgb(255, bytes[2], bytes[1], bytes[0]));
			var pg = new Polygon { Stroke = Brushes.White, Fill = brush };
			foreach (var point in points)
				pg.Points.Add(point);
			canvas.Children.Add(pg);

			canvas.Measure(new Size((int)canvas.Width, (int)canvas.Height));
			canvas.Arrange(new Rect(new Size((int)canvas.Width, (int)canvas.Height)));

			var rtb = new RenderTargetBitmap((int)canvas.Width, (int)canvas.Height, 96, 96, PixelFormats.Pbgra32);
			rtb.Render(canvas);
			var buffer = new uint[rtb.PixelWidth * rtb.PixelHeight];
			rtb.CopyPixels(buffer, rtb.PixelWidth * 4, 0);

			var bufferPos = 0;
			for (var y = 0; y < rtb.PixelHeight; ++y)
				for (var x = 0; x < rtb.PixelWidth; ++x)
				{
					var value = (int)(buffer[bufferPos++] & 0xffffff);
					foreach (var light in layout.GetPositionLights(x, y, 1, 1))
						pattern.AddLight(light, time, pattern.Absolute, Helpers.MultiplyColor(value, Brightness));
				}
		}

		List<Point> GetPoints(int numPoints, double addAngle, double firstPercent)
		{
			var points = new List<Point>();
			var dist = 48D;
			if (numPoints == 2)
			{
				dist *= firstPercent;
				firstPercent = 1;
			}
			for (var ctr = 0; ctr < numPoints; ++ctr)
			{
				var prevAngle = 360 / (numPoints - 1) * ctr;
				var curAngle = 360 / numPoints * ctr;
				var angle = (prevAngle + (curAngle - prevAngle) * firstPercent + addAngle) * Math.PI / 180;
				var x = 48 + Math.Sin(angle) * dist;
				var y = 48 - Math.Cos(angle) * dist;
				points.Add(new Point(x, y));
			}

			return points;
		}
	}
}
