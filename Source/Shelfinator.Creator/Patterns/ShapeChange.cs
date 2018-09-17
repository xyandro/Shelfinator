using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Shelfinator.Creator.Patterns
{
	class ShapeChange : IPattern
	{
		public int PatternNumber => 34;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");

			var pattern = new Pattern();

			var useColor = new List<int> { 0xff0000, 0xff0000, 0xff0000, 0x00ff00, 0x0000ff, 0xff00ff };
			var time = 0;
			var value = -1;
			while (true)
			{
				++value;
				var startTime = time;
				for (var angle = 0; angle < 360; angle += 5)
				{
					Render(pattern, layout, value, angle, angle / 360D, time, Brightness, Helpers.GradientColor(useColor[value], useColor[value + 1], angle / 360D));
					time += 5;
				}
				pattern.AddLightSequence(startTime, time, 2500, 1);
				if (value == 4)
					break;

				startTime = time;
				for (var angle = 0; angle < 360; angle += 5)
				{
					Render(pattern, layout, value, angle, 1, time, Brightness, useColor[value + 1]);
					time += 5;
				}
				pattern.AddLightSequence(startTime, time, 2500, 2);
			}

			return pattern;
		}

		void Render(Pattern pattern, Layout layout, int currentValue, double addAngle, double firstPercent, int time, double brightness, int color)
		{
			var canvas = GetCanvas(currentValue, addAngle, firstPercent, color);
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
						pattern.AddLight(light, time, pattern.Absolute, Helpers.MultiplyColor(value, brightness));
				}
		}

		Canvas GetCanvas(int currentValue, double addAngle, double firstPercent, int color)
		{
			var canvas = new Canvas() { Width = 97, Height = 97 };
			var pathFigure = new PathFigure { IsClosed = true };

			var bytes = BitConverter.GetBytes(color);
			var brush = new SolidColorBrush(Color.FromArgb(255, bytes[2], bytes[1], bytes[0]));

			if (currentValue < 3)
			{
				var numPoints = currentValue + 2;
				var dist = 48D;
				switch (currentValue)
				{
					case 0:
						dist *= firstPercent;
						firstPercent = 1;
						break;
					case 3:
						firstPercent = 10 * firstPercent + 5;
						numPoints = (int)firstPercent;
						firstPercent -= numPoints;
						break;
				}

				for (var ctr = 0; ctr < numPoints; ++ctr)
				{
					var prevAngle = 360 / (numPoints - 1) * ctr;
					var curAngle = 360 / numPoints * ctr;
					var angle = (prevAngle + (curAngle - prevAngle) * firstPercent + addAngle) * Math.PI / 180;
					var point = new Point(48 + Math.Sin(angle) * dist, 48 - Math.Cos(angle) * dist);
					if (ctr == 0)
						pathFigure.StartPoint = point;
					else
						pathFigure.Segments.Add(new LineSegment(point, true));
				}
			}
			else
			{
				if ((currentValue == 4) || (firstPercent >= 1))
					addAngle = 0;

				var distancePercent = currentValue == 3 ? 1D : 1 - firstPercent;

				var points1 = new List<Point> { new Point(48, 96), new Point(72, 72), new Point(72, 72), new Point(96, 48), new Point(72, 24), new Point(72, 24), new Point(48, 0), new Point(24, 24), new Point(24, 24), new Point(0, 48), new Point(24, 72), new Point(24, 72) };
				var points2 = new List<Point> { new Point(48, 86.4), new Point(69.12, 86.4), new Point(86.4, 69.12), new Point(86.4, 48), new Point(86.4, 26.88), new Point(69.12, 9.6), new Point(48, 9.6), new Point(26.88, 9.6), new Point(9.6, 26.88), new Point(9.6, 48), new Point(9.6, 69.12), new Point(26.88, 86.4) };

				var center = new Point(48, 48);

				var angles = points1.Select(p => p - center).Select(p => Math.Atan2(p.X, p.Y) + addAngle * Math.PI / 180).ToList();
				var distance = points1.Select(p => p - center).Select(p => p.Length * distancePercent).ToList();
				points1 = points1.Select((p, indexVal) => new Point(center.X + distance[indexVal] * Math.Sin(angles[indexVal]), center.Y - distance[indexVal] * Math.Cos(angles[indexVal]))).ToList();

				angles = points2.Select(p => p - center).Select(p => Math.Atan2(p.X, p.Y) + addAngle * Math.PI / 180).ToList();
				distance = points2.Select(p => p - center).Select(p => p.Length * distancePercent).ToList();
				points2 = points2.Select((p, indexVal) => new Point(center.X + distance[indexVal] * Math.Sin(angles[indexVal]), center.Y - distance[indexVal] * Math.Cos(angles[indexVal]))).ToList();

				List<Point> usePoints;
				if (currentValue == 3)
					usePoints = Enumerable.Range(0, points1.Count).Select(x => new Point(points1[x].X + (points2[x].X - points1[x].X) * firstPercent, points1[x].Y + (points2[x].Y - points1[x].Y) * firstPercent)).ToList();
				else
					usePoints = points2;

				pathFigure.StartPoint = usePoints[0];
				pathFigure.Segments.Add(new BezierSegment(usePoints[1], usePoints[2], usePoints[3], true));
				pathFigure.Segments.Add(new BezierSegment(usePoints[4], usePoints[5], usePoints[6], true));
				pathFigure.Segments.Add(new BezierSegment(usePoints[7], usePoints[8], usePoints[9], true));
				pathFigure.Segments.Add(new BezierSegment(usePoints[10], usePoints[11], usePoints[0], true));
			}

			canvas.Children.Add(new Path { Stroke = Brushes.White, Fill = brush, Data = new PathGeometry { Figures = new PathFigureCollection { pathFigure } } });
			return canvas;
		}
	}
}
