﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class ShutUpAndDance : ISong
	{
		public int SongNumber => 4;

		const double Brightness = 1f / 16;
		readonly Layout bodyLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");

		void ShapeChangeRender(Segment segment, Layout layout, int currentValue, double addAngle, double firstPercent, int time, double brightness, int color)
		{
			var canvas = ShapeChangeGetCanvas(currentValue, addAngle, firstPercent, color);
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
						segment.AddLight(light, time, Segment.Absolute, Helpers.MultiplyColor(value, brightness));
				}
		}

		Canvas ShapeChangeGetCanvas(int currentValue, double addAngle, double firstPercent, int color)
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
						dist = 39 * firstPercent + 9;
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

				var distancePercent = currentValue == 3 ? 1D : 1 - firstPercent * 39 / 48;

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

		Segment ShapeChange()
		{
			var segment = new Segment();

			var useColor = new List<int> { 0xff0000, 0xff0000, 0xff0000, 0x00ff00, 0x0000ff, 0xff00ff };
			var time = 0;
			for (var value = 0; value < 5; ++value)
			{
				for (var angle = 0; angle < 360; angle += 5)
				{
					ShapeChangeRender(segment, bodyLayout, value, angle, angle / 360D, time, Brightness, Helpers.GradientColor(useColor[value], useColor[value + 1], angle / 360D));
					time += 5;
				}

				if (value < 3)
				{
					for (var angle = 0; angle < 360; angle += 5)
					{
						ShapeChangeRender(segment, bodyLayout, value, angle, 1, time, Brightness, useColor[value + 1]);
						time += 5;
					}
				}
			}
			return segment;
		}

		Segment Randomized()
		{
			const int BaseIncrement = 2912;

			Random rand = new Random(0xfacade);
			var segment = new Segment();
			var colors = Helpers.Rainbow6.Concat(0x000000).Multiply(Brightness).ToList();
			for (var colorCtr = 0; colorCtr < colors.Count; colorCtr++)
			{
				var baseTime = BaseIncrement * colorCtr;
				var lights = bodyLayout.GetAllLights().OrderBy(x => rand.Next()).ToList();
				for (var lightCtr = 0; lightCtr < lights.Count; lightCtr++)
					segment.AddLight(lights[lightCtr], baseTime + lightCtr, Segment.Absolute, colors[colorCtr]);
			}

			return segment;
		}

		Segment CornerRotate()
		{
			var segment = new Segment();

			var whiteRed = new List<int> { 0xffffff, 0xff0000, 0xffffff, 0xff0000, 0xffffff, 0xff0000 }.Multiply(Brightness).ToList();
			var pastel = new List<int> { 0x17b7ab, 0xbcd63d, 0xe71880, 0xf15a25, 0x17b7ab, 0xbcd63d, 0xe71880, 0xf15a25, 0x17b7ab, 0xbcd63d, 0xe71880, 0xf15a25 }.Multiply(Brightness).ToList();
			var rgb = new List<int> { 0xff00ff, 0x00ffff, 0xff00ff, 0x00ffff, 0xff00ff, 0x00ffff }.Multiply(Brightness).ToList();
			var rainbow = Helpers.Rainbow6.Multiply(Brightness).ToList();
			rainbow.InsertRange(0, rainbow.Skip(1).Reverse().ToList());

			var color = new LightColor(-96, 96, whiteRed, pastel, rgb, rainbow);

			var center = new Point(0, 0);
			for (var angle = 0; angle < 90; ++angle)
				foreach (var light in bodyLayout.GetAllLights())
				{
					var position = bodyLayout.GetLightPosition(light);
					var vector = position - center;
					var curAngle = Math.Atan2(vector.Y, vector.X) + angle * Math.PI / 180;
					var newPosition = new Point(vector.Length * Math.Cos(curAngle), vector.Length * Math.Sin(curAngle));
					segment.AddLight(light, angle, color, (int)newPosition.X);
				}

			center = new Point(96, 0);
			for (var angle = 90; angle < 180; ++angle)
				foreach (var light in bodyLayout.GetAllLights())
				{
					var position = bodyLayout.GetLightPosition(light);
					var vector = position - center;
					var curAngle = Math.Atan2(vector.Y, vector.X) + angle * Math.PI / 180;
					var newPosition = new Point(vector.Length * Math.Cos(curAngle), vector.Length * Math.Sin(curAngle));
					segment.AddLight(light, angle, color, (int)newPosition.X);
				}

			center = new Point(96, 96);
			for (var angle = 180; angle < 270; ++angle)
				foreach (var light in bodyLayout.GetAllLights())
				{
					var position = bodyLayout.GetLightPosition(light);
					var vector = position - center;
					var curAngle = Math.Atan2(vector.Y, vector.X) + angle * Math.PI / 180;
					var newPosition = new Point(vector.Length * Math.Cos(curAngle), vector.Length * Math.Sin(curAngle));
					segment.AddLight(light, angle, color, (int)newPosition.X);
				}

			center = new Point(0, 96);
			for (var angle = 270; angle < 360; ++angle)
				foreach (var light in bodyLayout.GetAllLights())
				{
					var position = bodyLayout.GetLightPosition(light);
					var vector = position - center;
					var curAngle = Math.Atan2(vector.Y, vector.X) + angle * Math.PI / 180;
					var newPosition = new Point(vector.Length * Math.Cos(curAngle), vector.Length * Math.Sin(curAngle));
					segment.AddLight(light, angle, color, (int)newPosition.X);
				}

			return segment;
		}

		Segment Corners()
		{
			const string lightLocations =
"1,1/2,1/3,1/4,1/5,1/6,1/7,1/8,1/9,1/10,1/11,1/12,1/13,1/14,1/15,1/16,1/17,1/18,1/19,1/19,2/19,3/19,4/19,5/19,6/19,7/19,8/19,9/19,10/19,11/19,12/19,13/19,14/19,15/19,16/19,17/19,18/19,19/18,19/17,19/16,19/15,19/14,19/13,19/12,19/11,19/10,19/9,19/8,19/7,19/6,19/5,19/4,19/3,19/2,19/1,19/1,18/1,17/1,16/1,15/1,14/1,13/1,12/1,11/1,10/1,9/1,8/1,7/1,6/1,5/1,4/1,3/1,2|" +
"20,1/20,2/20,3/20,4/20,5/20,6/20,7/20,8/20,9/20,10/20,11/20,12/20,13/20,14/20,15/20,16/20,17/20,18/20,19/21,19/22,19/23,19/24,19/25,19/26,19/27,19/28,19/29,19/30,19/31,19/32,19/33,19/34,19/35,19/36,19/37,19/38,19/38,18/38,17/38,16/38,15/38,14/38,13/38,12/38,11/38,10/38,9/38,8/38,7/38,6/38,5/38,4/38,3/38,2/38,1/37,1/36,1/35,1/34,1/33,1/32,1/31,1/30,1/29,1/28,1/27,1/26,1/25,1/24,1/23,1/22,1/21,1|" +
"39,1/40,1/41,1/42,1/43,1/44,1/45,1/46,1/47,1/48,1/49,1/50,1/51,1/52,1/53,1/54,1/55,1/56,1/57,1/57,2/57,3/57,4/57,5/57,6/57,7/57,8/57,9/57,10/57,11/57,12/57,13/57,14/57,15/57,16/57,17/57,18/57,19/56,19/55,19/54,19/53,19/52,19/51,19/50,19/49,19/48,19/47,19/46,19/45,19/44,19/43,19/42,19/41,19/40,19/39,19/39,18/39,17/39,16/39,15/39,14/39,13/39,12/39,11/39,10/39,9/39,8/39,7/39,6/39,5/39,4/39,3/39,2|" +
"58,1/58,2/58,3/58,4/58,5/58,6/58,7/58,8/58,9/58,10/58,11/58,12/58,13/58,14/58,15/58,16/58,17/58,18/58,19/59,19/60,19/61,19/62,19/63,19/64,19/65,19/66,19/67,19/68,19/69,19/70,19/71,19/72,19/73,19/74,19/75,19/76,19/76,18/76,17/76,16/76,15/76,14/76,13/76,12/76,11/76,10/76,9/76,8/76,7/76,6/76,5/76,4/76,3/76,2/76,1/75,1/74,1/73,1/72,1/71,1/70,1/69,1/68,1/67,1/66,1/65,1/64,1/63,1/62,1/61,1/60,1/59,1|" +
"77,1/78,1/79,1/80,1/81,1/82,1/83,1/84,1/85,1/86,1/87,1/88,1/89,1/90,1/91,1/92,1/93,1/94,1/95,1/95,2/95,3/95,4/95,5/95,6/95,7/95,8/95,9/95,10/95,11/95,12/95,13/95,14/95,15/95,16/95,17/95,18/95,19/94,19/93,19/92,19/91,19/90,19/89,19/88,19/87,19/86,19/85,19/84,19/83,19/82,19/81,19/80,19/79,19/78,19/77,19/77,18/77,17/77,16/77,15/77,14/77,13/77,12/77,11/77,10/77,9/77,8/77,7/77,6/77,5/77,4/77,3/77,2|" +
"1,20/1,21/1,22/1,23/1,24/1,25/1,26/1,27/1,28/1,29/1,30/1,31/1,32/1,33/1,34/1,35/1,36/1,37/1,38/2,38/3,38/4,38/5,38/6,38/7,38/8,38/9,38/10,38/11,38/12,38/13,38/14,38/15,38/16,38/17,38/18,38/19,38/19,37/19,36/19,35/19,34/19,33/19,32/19,31/19,30/19,29/19,28/19,27/19,26/19,25/19,24/19,23/19,22/19,21/19,20/18,20/17,20/16,20/15,20/14,20/13,20/12,20/11,20/10,20/9,20/8,20/7,20/6,20/5,20/4,20/3,20/2,20|" +
"20,20/21,20/22,20/23,20/24,20/25,20/26,20/27,20/28,20/29,20/30,20/31,20/32,20/33,20/34,20/35,20/36,20/37,20/38,20/38,21/38,22/38,23/38,24/38,25/38,26/38,27/38,28/38,29/38,30/38,31/38,32/38,33/38,34/38,35/38,36/38,37/38,38/37,38/36,38/35,38/34,38/33,38/32,38/31,38/30,38/29,38/28,38/27,38/26,38/25,38/24,38/23,38/22,38/21,38/20,38/20,37/20,36/20,35/20,34/20,33/20,32/20,31/20,30/20,29/20,28/20,27/20,26/20,25/20,24/20,23/20,22/20,21|" +
"39,20/39,21/39,22/39,23/39,24/39,25/39,26/39,27/39,28/39,29/39,30/39,31/39,32/39,33/39,34/39,35/39,36/39,37/39,38/40,38/41,38/42,38/43,38/44,38/45,38/46,38/47,38/48,38/49,38/50,38/51,38/52,38/53,38/54,38/55,38/56,38/57,38/57,37/57,36/57,35/57,34/57,33/57,32/57,31/57,30/57,29/57,28/57,27/57,26/57,25/57,24/57,23/57,22/57,21/57,20/56,20/55,20/54,20/53,20/52,20/51,20/50,20/49,20/48,20/47,20/46,20/45,20/44,20/43,20/42,20/41,20/40,20|" +
"58,20/59,20/60,20/61,20/62,20/63,20/64,20/65,20/66,20/67,20/68,20/69,20/70,20/71,20/72,20/73,20/74,20/75,20/76,20/76,21/76,22/76,23/76,24/76,25/76,26/76,27/76,28/76,29/76,30/76,31/76,32/76,33/76,34/76,35/76,36/76,37/76,38/75,38/74,38/73,38/72,38/71,38/70,38/69,38/68,38/67,38/66,38/65,38/64,38/63,38/62,38/61,38/60,38/59,38/58,38/58,37/58,36/58,35/58,34/58,33/58,32/58,31/58,30/58,29/58,28/58,27/58,26/58,25/58,24/58,23/58,22/58,21|" +
"77,20/77,21/77,22/77,23/77,24/77,25/77,26/77,27/77,28/77,29/77,30/77,31/77,32/77,33/77,34/77,35/77,36/77,37/77,38/78,38/79,38/80,38/81,38/82,38/83,38/84,38/85,38/86,38/87,38/88,38/89,38/90,38/91,38/92,38/93,38/94,38/95,38/95,37/95,36/95,35/95,34/95,33/95,32/95,31/95,30/95,29/95,28/95,27/95,26/95,25/95,24/95,23/95,22/95,21/95,20/94,20/93,20/92,20/91,20/90,20/89,20/88,20/87,20/86,20/85,20/84,20/83,20/82,20/81,20/80,20/79,20/78,20|" +
"1,39/2,39/3,39/4,39/5,39/6,39/7,39/8,39/9,39/10,39/11,39/12,39/13,39/14,39/15,39/16,39/17,39/18,39/19,39/19,40/19,41/19,42/19,43/19,44/19,45/19,46/19,47/19,48/19,49/19,50/19,51/19,52/19,53/19,54/19,55/19,56/19,57/18,57/17,57/16,57/15,57/14,57/13,57/12,57/11,57/10,57/9,57/8,57/7,57/6,57/5,57/4,57/3,57/2,57/1,57/1,56/1,55/1,54/1,53/1,52/1,51/1,50/1,49/1,48/1,47/1,46/1,45/1,44/1,43/1,42/1,41/1,40|" +
"20,39/20,40/20,41/20,42/20,43/20,44/20,45/20,46/20,47/20,48/20,49/20,50/20,51/20,52/20,53/20,54/20,55/20,56/20,57/21,57/22,57/23,57/24,57/25,57/26,57/27,57/28,57/29,57/30,57/31,57/32,57/33,57/34,57/35,57/36,57/37,57/38,57/38,56/38,55/38,54/38,53/38,52/38,51/38,50/38,49/38,48/38,47/38,46/38,45/38,44/38,43/38,42/38,41/38,40/38,39/37,39/36,39/35,39/34,39/33,39/32,39/31,39/30,39/29,39/28,39/27,39/26,39/25,39/24,39/23,39/22,39/21,39|" +
"39,39/40,39/41,39/42,39/43,39/44,39/45,39/46,39/47,39/48,39/49,39/50,39/51,39/52,39/53,39/54,39/55,39/56,39/57,39/57,40/57,41/57,42/57,43/57,44/57,45/57,46/57,47/57,48/57,49/57,50/57,51/57,52/57,53/57,54/57,55/57,56/57,57/56,57/55,57/54,57/53,57/52,57/51,57/50,57/49,57/48,57/47,57/46,57/45,57/44,57/43,57/42,57/41,57/40,57/39,57/39,56/39,55/39,54/39,53/39,52/39,51/39,50/39,49/39,48/39,47/39,46/39,45/39,44/39,43/39,42/39,41/39,40|" +
"58,39/58,40/58,41/58,42/58,43/58,44/58,45/58,46/58,47/58,48/58,49/58,50/58,51/58,52/58,53/58,54/58,55/58,56/58,57/59,57/60,57/61,57/62,57/63,57/64,57/65,57/66,57/67,57/68,57/69,57/70,57/71,57/72,57/73,57/74,57/75,57/76,57/76,56/76,55/76,54/76,53/76,52/76,51/76,50/76,49/76,48/76,47/76,46/76,45/76,44/76,43/76,42/76,41/76,40/76,39/75,39/74,39/73,39/72,39/71,39/70,39/69,39/68,39/67,39/66,39/65,39/64,39/63,39/62,39/61,39/60,39/59,39|" +
"77,39/78,39/79,39/80,39/81,39/82,39/83,39/84,39/85,39/86,39/87,39/88,39/89,39/90,39/91,39/92,39/93,39/94,39/95,39/95,40/95,41/95,42/95,43/95,44/95,45/95,46/95,47/95,48/95,49/95,50/95,51/95,52/95,53/95,54/95,55/95,56/95,57/94,57/93,57/92,57/91,57/90,57/89,57/88,57/87,57/86,57/85,57/84,57/83,57/82,57/81,57/80,57/79,57/78,57/77,57/77,56/77,55/77,54/77,53/77,52/77,51/77,50/77,49/77,48/77,47/77,46/77,45/77,44/77,43/77,42/77,41/77,40|" +
"1,58/1,59/1,60/1,61/1,62/1,63/1,64/1,65/1,66/1,67/1,68/1,69/1,70/1,71/1,72/1,73/1,74/1,75/1,76/2,76/3,76/4,76/5,76/6,76/7,76/8,76/9,76/10,76/11,76/12,76/13,76/14,76/15,76/16,76/17,76/18,76/19,76/19,75/19,74/19,73/19,72/19,71/19,70/19,69/19,68/19,67/19,66/19,65/19,64/19,63/19,62/19,61/19,60/19,59/19,58/18,58/17,58/16,58/15,58/14,58/13,58/12,58/11,58/10,58/9,58/8,58/7,58/6,58/5,58/4,58/3,58/2,58|" +
"20,58/21,58/22,58/23,58/24,58/25,58/26,58/27,58/28,58/29,58/30,58/31,58/32,58/33,58/34,58/35,58/36,58/37,58/38,58/38,59/38,60/38,61/38,62/38,63/38,64/38,65/38,66/38,67/38,68/38,69/38,70/38,71/38,72/38,73/38,74/38,75/38,76/37,76/36,76/35,76/34,76/33,76/32,76/31,76/30,76/29,76/28,76/27,76/26,76/25,76/24,76/23,76/22,76/21,76/20,76/20,75/20,74/20,73/20,72/20,71/20,70/20,69/20,68/20,67/20,66/20,65/20,64/20,63/20,62/20,61/20,60/20,59|" +
"39,58/39,59/39,60/39,61/39,62/39,63/39,64/39,65/39,66/39,67/39,68/39,69/39,70/39,71/39,72/39,73/39,74/39,75/39,76/40,76/41,76/42,76/43,76/44,76/45,76/46,76/47,76/48,76/49,76/50,76/51,76/52,76/53,76/54,76/55,76/56,76/57,76/57,75/57,74/57,73/57,72/57,71/57,70/57,69/57,68/57,67/57,66/57,65/57,64/57,63/57,62/57,61/57,60/57,59/57,58/56,58/55,58/54,58/53,58/52,58/51,58/50,58/49,58/48,58/47,58/46,58/45,58/44,58/43,58/42,58/41,58/40,58|" +
"58,58/59,58/60,58/61,58/62,58/63,58/64,58/65,58/66,58/67,58/68,58/69,58/70,58/71,58/72,58/73,58/74,58/75,58/76,58/76,59/76,60/76,61/76,62/76,63/76,64/76,65/76,66/76,67/76,68/76,69/76,70/76,71/76,72/76,73/76,74/76,75/76,76/75,76/74,76/73,76/72,76/71,76/70,76/69,76/68,76/67,76/66,76/65,76/64,76/63,76/62,76/61,76/60,76/59,76/58,76/58,75/58,74/58,73/58,72/58,71/58,70/58,69/58,68/58,67/58,66/58,65/58,64/58,63/58,62/58,61/58,60/58,59|" +
"77,58/77,59/77,60/77,61/77,62/77,63/77,64/77,65/77,66/77,67/77,68/77,69/77,70/77,71/77,72/77,73/77,74/77,75/77,76/78,76/79,76/80,76/81,76/82,76/83,76/84,76/85,76/86,76/87,76/88,76/89,76/90,76/91,76/92,76/93,76/94,76/95,76/95,75/95,74/95,73/95,72/95,71/95,70/95,69/95,68/95,67/95,66/95,65/95,64/95,63/95,62/95,61/95,60/95,59/95,58/94,58/93,58/92,58/91,58/90,58/89,58/88,58/87,58/86,58/85,58/84,58/83,58/82,58/81,58/80,58/79,58/78,58|" +
"1,77/2,77/3,77/4,77/5,77/6,77/7,77/8,77/9,77/10,77/11,77/12,77/13,77/14,77/15,77/16,77/17,77/18,77/19,77/19,78/19,79/19,80/19,81/19,82/19,83/19,84/19,85/19,86/19,87/19,88/19,89/19,90/19,91/19,92/19,93/19,94/19,95/18,95/17,95/16,95/15,95/14,95/13,95/12,95/11,95/10,95/9,95/8,95/7,95/6,95/5,95/4,95/3,95/2,95/1,95/1,94/1,93/1,92/1,91/1,90/1,89/1,88/1,87/1,86/1,85/1,84/1,83/1,82/1,81/1,80/1,79/1,78|" +
"20,77/20,78/20,79/20,80/20,81/20,82/20,83/20,84/20,85/20,86/20,87/20,88/20,89/20,90/20,91/20,92/20,93/20,94/20,95/21,95/22,95/23,95/24,95/25,95/26,95/27,95/28,95/29,95/30,95/31,95/32,95/33,95/34,95/35,95/36,95/37,95/38,95/38,94/38,93/38,92/38,91/38,90/38,89/38,88/38,87/38,86/38,85/38,84/38,83/38,82/38,81/38,80/38,79/38,78/38,77/37,77/36,77/35,77/34,77/33,77/32,77/31,77/30,77/29,77/28,77/27,77/26,77/25,77/24,77/23,77/22,77/21,77|" +
"39,77/40,77/41,77/42,77/43,77/44,77/45,77/46,77/47,77/48,77/49,77/50,77/51,77/52,77/53,77/54,77/55,77/56,77/57,77/57,78/57,79/57,80/57,81/57,82/57,83/57,84/57,85/57,86/57,87/57,88/57,89/57,90/57,91/57,92/57,93/57,94/57,95/56,95/55,95/54,95/53,95/52,95/51,95/50,95/49,95/48,95/47,95/46,95/45,95/44,95/43,95/42,95/41,95/40,95/39,95/39,94/39,93/39,92/39,91/39,90/39,89/39,88/39,87/39,86/39,85/39,84/39,83/39,82/39,81/39,80/39,79/39,78|" +
"58,77/58,78/58,79/58,80/58,81/58,82/58,83/58,84/58,85/58,86/58,87/58,88/58,89/58,90/58,91/58,92/58,93/58,94/58,95/59,95/60,95/61,95/62,95/63,95/64,95/65,95/66,95/67,95/68,95/69,95/70,95/71,95/72,95/73,95/74,95/75,95/76,95/76,94/76,93/76,92/76,91/76,90/76,89/76,88/76,87/76,86/76,85/76,84/76,83/76,82/76,81/76,80/76,79/76,78/76,77/75,77/74,77/73,77/72,77/71,77/70,77/69,77/68,77/67,77/66,77/65,77/64,77/63,77/62,77/61,77/60,77/59,77|" +
"77,77/78,77/79,77/80,77/81,77/82,77/83,77/84,77/85,77/86,77/87,77/88,77/89,77/90,77/91,77/92,77/93,77/94,77/95,77/95,78/95,79/95,80/95,81/95,82/95,83/95,84/95,85/95,86/95,87/95,88/95,89/95,90/95,91/95,92/95,93/95,94/95,95/94,95/93,95/92,95/91,95/90,95/89,95/88,95/87,95/86,95/85,95/84,95/83,95/82,95/81,95/80,95/79,95/78,95/77,95/77,94/77,93/77,92/77,91/77,90/77,89/77,88/77,87/77,86/77,85/77,84/77,83/77,82/77,81/77,80/77,79/77,78";
			var startTime = new List<int> { 0, 50, 0, 50, 0, 150, 100, 150, 100, 150, 0, 50, 0, 50, 0, 150, 100, 150, 100, 150, 0, 50, 0, 50, 0 };
			var squareLights = lightLocations.Split('|').Select(l => l.Split('/').Select(p => bodyLayout.GetPositionLight(Point.Parse(p))).ToList()).ToList();

			var segment = new Segment();
			var color = new LightColor(0, 3, new List<List<int>>
			{
				new List<int> { 0xffffff }.Multiply(Brightness).ToList(),
				new List<int> { 0x0000ff, 0x00ff00, 0xff0000, 0x800080 }.Multiply(Brightness).ToList(),
			});
			for (var squareCtr = 0; squareCtr < squareLights.Count; squareCtr++)
			{
				var light = default(int?);
				var colorIndex = 0;
				var time = startTime[squareCtr];

				for (var pass = 0; pass < 2; ++pass)
					for (var ctr = 0; ctr < 72; ++ctr)
					{
						if (light.HasValue)
							segment.AddLight(light.Value, time, Segment.Absolute, 0x000000);
						light = squareLights[squareCtr][ctr];
						if (ctr % 18 == 0)
						{
							var newColorIndex = (colorIndex + 1) % 4;
							segment.AddLight(light.Value, time, time + 33, color, colorIndex, color, newColorIndex);
							colorIndex = newColorIndex;
							time += 33;
						}
						else
						{
							segment.AddLight(light.Value, time, color, colorIndex);
							++time;
						}
					}
			}

			return segment;
		}

		class StarData
		{
			static Random rand = new Random(0x0badf00d);

			public Point Point { get; set; }
			public double Size { get; set; }
			public Vector Vector { get; set; }
			public double Brightness { get; set; }

			public StarData(bool randomizeDistance = false)
			{
				Point = new Point(48, 48);
				Size = 1;// rand.Next(50, 201) / 100.0;
				var angle = rand.Next(0, 36000) * Math.PI / 18000;
				Vector = new Vector(Math.Sin(angle), Math.Cos(angle));
				if (randomizeDistance)
					Point += Vector * rand.Next(0, 48);
				Vector *= rand.Next(100, 151) / 100.0;
				Brightness = rand.Next(192, 256) / 255.0;
			}

			public void Update()
			{
				Point += Vector;
				Brightness *= 0.99;
			}
		}

		void RenderStars(Layout layout, Segment segment, List<StarData> stars, int time, double Brightness)
		{
			var board = new double[97, 97];
			stars.ForEach(star => RenderStar(board, star));
			for (var y = 0; y < 96; ++y)
				for (var x = 0; x < 96; ++x)
					foreach (var light in layout.GetPositionLights(x, y, 1, 1))
						segment.AddLight(light, time, Segment.Absolute, Helpers.MultiplyColor(0xffffff, Math.Min(1, board[x, y]) * Brightness));
		}

		void RenderStar(double[,] board, StarData star)
		{
			var x = star.Point.X.Round();
			var y = star.Point.Y.Round();
			if ((x >= 0) && (y >= 0) && (x < board.GetLength(0)) && (y < board.GetLength(1)))
				++board[x, y];
		}

		void UpdateStars(List<StarData> stars)
		{
			for (var ctr = 0; ctr < stars.Count; ctr++)
				stars[ctr] = UpdateStar(stars[ctr]);
		}

		StarData UpdateStar(StarData star)
		{
			star.Update();
			if ((star.Point.X + star.Size < 0) || (star.Point.Y + star.Size < 0) || (star.Point.X >= 96) || (star.Point.Y >= 96))
				star = new StarData();
			return star;
		}

		Segment Stars()
		{
			const int NumStars = 100;
			const int Duration = 200;

			var segment = new Segment();

			var stars = Enumerable.Range(0, NumStars).Select(x => new StarData(true)).ToList();
			for (var time = 0; time < Duration; ++time)
			{
				RenderStars(bodyLayout, segment, stars, time, Brightness);
				UpdateStars(stars);
			}

			return segment;
		}

		Segment CircleWarp()
		{
			var segment = new Segment();

			var color = new LightColor(0, 5, Helpers.Rainbow6.Multiply(Brightness).ToList());

			var center = new Point(48, 48);
			var distances = bodyLayout.GetAllLights().Select(light => Tuple.Create(light, (bodyLayout.GetLightPosition(light) - center).Length)).ToList();

			for (var circle = 0; circle < 17; ++circle)
			{
				for (var x = 9; x < 68; ++x)
				{
					var time = circle * 17 + x - 9;
					var lights = distances.Where(tuple => (tuple.Item2 >= x) && (tuple.Item2 < x + 2)).Select(tuple => tuple.Item1).ToList();
					foreach (var light in lights)
						segment.AddLight(light, time, color, circle % 6);
					foreach (var light in lights)
						segment.AddLight(light, time + 1, Segment.Absolute, 0x000000);
				}
			}

			return segment;
		}

		Segment Bounce()
		{
			var segment = new Segment();

			var columns = new List<int> { 0, 19, 38, 57, 76, 95 };
			var columnTime = new List<int> { 48, 64, 0, 32, 80, 16 };

			var paddleDest = columnTime.Select((val, index) => new { time = val, column = columns[index] }).OrderBy(o => o.time).Select(o => o.column).ToList();

			var color = new LightColor(0, 95, Helpers.Rainbow6.Multiply(Brightness).ToList());

			double paddlePos = 0;
			int paddleIndex = 0, paddleSteps = 0;
			for (var time = 0; time <= 437; ++time)
			{
				segment.Clear(time);
				for (var columnCtr = 0; columnCtr < columns.Count; columnCtr++)
				{
					int y;
					var useTime = time - columnTime[columnCtr];
					if (time < 96)
					{
						y = 95 - time;
						foreach (var light in bodyLayout.GetPositionLights(columns[columnCtr], y + 2, 2, 1))
							segment.AddLight(light, time, Segment.Absolute, Helpers.MultiplyColor(0xffffff, Brightness));
					}
					else if (useTime < 116)
						y = 0;
					else if (useTime == 116)
					{
						y = 0;
						foreach (var light in bodyLayout.GetPositionLights(columns[columnCtr], 2, 2, 1))
						{
							segment.AddLight(light, 96, Segment.Absolute, Helpers.MultiplyColor(0xffffff, Brightness));
							segment.AddLight(light, time - 20, time, null, Segment.Absolute, 0x000000);
						}
					}
					else if (useTime <= 356)
					{
						var yTime = Helpers.Cycle(useTime - 68, 0, 96).Round();
						y = (31d / 768 * yTime * yTime - 31d / 8 * yTime + 93).Round();
					}
					else
						continue;

					foreach (var light in bodyLayout.GetPositionLights(columns[columnCtr], y, 2, 2))
						segment.AddLight(light, time, color, columns[columnCtr]);
				}

				if (time < 148)
				{
				}
				else if (time == 148)
				{
					foreach (var light in bodyLayout.GetPositionLights(0, 95, 97, 2))
					{
						var x = bodyLayout.GetLightPosition(light).X.Round();
						segment.AddLight(light, 5, 48, null, color, x);
						if ((x <= 43) || (x > 53))
							segment.AddLight(light, 48, 96, null, Segment.Absolute, 0x000000);
					}
					paddlePos = 48;
					paddleSteps = 164 - time;
				}
				else
				{
					if (paddleSteps == 0)
					{
						++paddleIndex;
						if (paddleIndex >= paddleDest.Count)
							paddleIndex = 0;
						paddleSteps = time > 340 ? 64 : 16;
					}

					var dest = time > 340 ? -10 : paddleDest[paddleIndex];
					paddlePos += (dest - paddlePos) / paddleSteps;
					--paddleSteps;

					for (var y = 0; y < 2; ++y)
						for (var x = 0; x < 10; ++x)
						{
							var pos = paddlePos.Round();
							var light = bodyLayout.TryGetPositionLight(x + pos - 4, y + 95);
							if (light.HasValue)
								segment.AddLight(light.Value, time, color, x + pos);
						}
				}
			}

			return segment;
		}

		Segment Flash()
		{
			const int Concurrency = 25;

			var color = new LightColor(0, 6, new List<List<int>> { new List<int> { Helpers.MultiplyColor(0xffffff, 1f / 8) }, Helpers.Rainbow6.Multiply(Brightness).ToList() });

			var segment = new Segment();
			var rand = new Random(0xbadf00d);
			var lights = bodyLayout.GetAllLights().OrderBy(x => rand.Next()).ToList();
			for (var time = 0; time < lights.Count; ++time)
			{
				segment.AddLight(lights[time], time, color, time % 7);
				segment.AddLight(lights[time], time + Concurrency, Segment.Absolute, 0x000000);
			}

			return segment;
		}

		Segment Halves()
		{
			var segment = new Segment();

			var color0 = new LightColor(0, 1, new List<List<int>>
			{
				new List<int> { 0xff0000 }.Multiply(Brightness).ToList(),
				new List<int> { 0xff0000 }.Multiply(Brightness).ToList(),
				new List<int> { 0xffff00 }.Multiply(Brightness).ToList(),
				new List<int> { 0xffff00 }.Multiply(Brightness).ToList(),
				new List<int> { 0x0000ff }.Multiply(Brightness).ToList(),
				new List<int> { 0x0000ff }.Multiply(Brightness).ToList(),
				new List<int> { 0xff0000 }.Multiply(Brightness).ToList(),
				new List<int> { 0xff0000 }.Multiply(Brightness).ToList(),

			});
			var color1 = new LightColor(0, 1, new List<List<int>>
			{
				new List<int> { 0x000000 }.Multiply(Brightness).ToList(),
				new List<int> { 0xff7f00 }.Multiply(Brightness).ToList(),
				new List<int> { 0xff7f00 }.Multiply(Brightness).ToList(),
				new List<int> { 0x00ff00 }.Multiply(Brightness).ToList(),
				new List<int> { 0x00ff00 }.Multiply(Brightness).ToList(),
				new List<int> { 0x4b0082 }.Multiply(Brightness).ToList(),
				new List<int> { 0x4b0082 }.Multiply(Brightness).ToList(),
				new List<int> { 0x000000 }.Multiply(Brightness).ToList(),
			});

			foreach (var light in bodyLayout.GetAllLights())
			{
				var pos = bodyLayout.GetLightPosition(light);
				var color = (pos.X % 2) == (pos.Y % 2) ? color0 : color1;
				segment.AddLight(light, 0, color);
			}

			return segment;
		}

		Segment Paths()
		{
			const double DarkBrightness = 1f / 48;
			const string Paths =
"0,0&1,0&2,0&3,0&4,0&5,0&6,0&7,0&8,0&9,0&10,0&10,1&9,1&8,1&7,1&6,1&5,1&4,1&3,1&2,1&1,1&1,2&0,2&0,1/11,0&12,0&13,0&14,0&15,0&16,0&17,0&18,0&19,0&20,0&20,1&20,2&20,3&19,3&19,2&19,1&18,1&17,1&16,1&15,1&14,1&13,1&12,1&11,1/21,0&22,0&23,0&24,0&25,0&26,0&27,0&28,0&29,0&30,0&31,0&32,0&32,1&31,1&30,1&29,1&28,1&27,1&26,1&25,1&24,1&23,1&22,1&21,1/33,0&34,0&35,0&36,0&37,0&38,0&39,0&40,0&41,0&41,1&40,1&39,1&39,2&39,3&39,4&38,4&38,3&38,2&38,1&37,1&36,1&35,1&34,1&33,1/42,0&43,0&44,0&45,0&46,0&47,0&48,0&49,0&50" +
",0&50,1&49,1&48,1&47,1&46,1&45,1&44,1&43,1&42,1/51,0&52,0&53,0&54,0&55,0&56,0&57,0&58,0&59,0&60,0&60,1&59,1&58,1&58,2&58,3&57,3&57,2&57,1&56,1&55,1&54,1&53,1&52,1&51,1/61,0&62,0&63,0&64,0&65,0&66,0&67,0&68,0&69,0&70,0&71,0&72,0&72,1&71,1&70,1&69,1&68,1&67,1&66,1&65,1&64,1&63,1&62,1&61,1/73,0&74,0&75,0&76,0&77,0&78,0&79,0&80,0&81,0&81,1&80,1&79,1&78,1&77,1&77,2&77,3&77,4&76,4&76,3&76,2&76,1&75,1&74,1&73,1/82,0&83,0&84,0&85,0&86,0&87,0&88,0&89,0&90,0&91,0&92,0&93,0&93,1&92,1&91,1&90,1&89,1&88,1&87" +
",1&86,1&85,1&84,1&83,1&82,1/94,0&95,0&96,0&96,1&96,2&96,3&96,4&96,5&96,6&96,7&96,8&96,9&96,10&95,10&95,9&95,8&95,7&95,6&95,5&95,4&95,3&95,2&95,1&94,1/0,3&1,3&1,4&1,5&1,6&1,7&1,8&1,9&1,10&1,11&1,12&1,13&1,14&0,14&0,13&0,12&0,11&0,10&0,9&0,8&0,7&0,6&0,5&0,4/19,4&20,4&20,5&20,6&20,7&20,8&20,9&20,10&20,11&20,12&20,13&20,14&20,15&19,15&19,14&19,13&19,12&19,11&19,10&19,9&19,8&19,7&19,6&19,5/57,4&58,4&58,5&58,6&58,7&58,8&58,9&58,10&58,11&58,12&58,13&58,14&58,15&57,15&57,14&57,13&57,12&57,11&57,10&57,9&" +
"57,8&57,7&57,6&57,5/38,5&39,5&39,6&39,7&39,8&39,9&39,10&39,11&39,12&39,13&39,14&39,15&39,16&38,16&38,15&38,14&38,13&38,12&38,11&38,10&38,9&38,8&38,7&38,6/76,5&77,5&77,6&77,7&77,8&77,9&77,10&77,11&77,12&77,13&77,14&77,15&77,16&76,16&76,15&76,14&76,13&76,12&76,11&76,10&76,9&76,8&76,7&76,6/95,11&96,11&96,12&96,13&96,14&96,15&96,16&96,17&96,18&96,19&96,20&95,20&94,20&93,20&93,19&94,19&95,19&95,18&95,17&95,16&95,15&95,14&95,13&95,12/0,15&1,15&1,16&1,17&1,18&1,19&2,19&3,19&4,19&4,20&3,20&2,20&1,20&1,2" +
"1&1,22&1,23&0,23&0,22&0,21&0,20&0,19&0,18&0,17&0,16/19,16&20,16&20,17&20,18&20,19&21,19&22,19&22,20&21,20&20,20&20,21&20,22&20,23&19,23&19,22&19,21&19,20&18,20&17,20&17,19&18,19&19,19&19,18&19,17/57,16&58,16&58,17&58,18&58,19&59,19&60,19&60,20&59,20&58,20&58,21&58,22&58,23&57,23&57,22&57,21&57,20&56,20&55,20&55,19&56,19&57,19&57,18&57,17/38,17&39,17&39,18&39,19&40,19&41,19&42,19&42,20&41,20&40,20&39,20&39,21&39,22&38,22&38,21&38,20&37,20&36,20&35,20&35,19&36,19&37,19&38,19&38,18/76,17&77,17&77,1" +
"8&77,19&78,19&79,19&80,19&80,20&79,20&78,20&77,20&77,21&77,22&76,22&76,21&76,20&75,20&74,20&73,20&73,19&74,19&75,19&76,19&76,18/5,19&6,19&7,19&8,19&9,19&10,19&11,19&12,19&13,19&14,19&15,19&16,19&16,20&15,20&14,20&13,20&12,20&11,20&10,20&9,20&8,20&7,20&6,20&5,20/23,19&24,19&25,19&26,19&27,19&28,19&29,19&30,19&31,19&32,19&33,19&34,19&34,20&33,20&32,20&31,20&30,20&29,20&28,20&27,20&26,20&25,20&24,20&23,20/43,19&44,19&45,19&46,19&47,19&48,19&49,19&50,19&51,19&52,19&53,19&54,19&54,20&53,20&52,20&51,2" +
"0&50,20&49,20&48,20&47,20&46,20&45,20&44,20&43,20/61,19&62,19&63,19&64,19&65,19&66,19&67,19&68,19&69,19&70,19&71,19&72,19&72,20&71,20&70,20&69,20&68,20&67,20&66,20&65,20&64,20&63,20&62,20&61,20/81,19&82,19&83,19&84,19&85,19&86,19&87,19&88,19&89,19&90,19&91,19&92,19&92,20&91,20&90,20&89,20&88,20&87,20&86,20&85,20&84,20&83,20&82,20&81,20/95,21&96,21&96,22&96,23&96,24&96,25&96,26&96,27&96,28&96,29&96,30&96,31&96,32&95,32&95,31&95,30&95,29&95,28&95,27&95,26&95,25&95,24&95,23&95,22/38,23&39,23&39,24&" +
"39,25&39,26&39,27&39,28&39,29&39,30&39,31&39,32&39,33&39,34&38,34&38,33&38,32&38,31&38,30&38,29&38,28&38,27&38,26&38,25&38,24/76,23&77,23&77,24&77,25&77,26&77,27&77,28&77,29&77,30&77,31&77,32&77,33&77,34&76,34&76,33&76,32&76,31&76,30&76,29&76,28&76,27&76,26&76,25&76,24/0,24&1,24&1,25&1,26&1,27&1,28&1,29&1,30&1,31&1,32&1,33&1,34&1,35&0,35&0,34&0,33&0,32&0,31&0,30&0,29&0,28&0,27&0,26&0,25/19,24&20,24&20,25&20,26&20,27&20,28&20,29&20,30&20,31&20,32&20,33&20,34&20,35&19,35&19,34&19,33&19,32&19,31&19" +
",30&19,29&19,28&19,27&19,26&19,25/57,24&58,24&58,25&58,26&58,27&58,28&58,29&58,30&58,31&58,32&58,33&58,34&58,35&57,35&57,34&57,33&57,32&57,31&57,30&57,29&57,28&57,27&57,26&57,25/95,33&96,33&96,34&96,35&96,36&96,37&96,38&96,39&96,40&96,41&95,41&95,40&95,39&94,39&93,39&92,39&92,38&93,38&94,38&95,38&95,37&95,36&95,35&95,34/38,35&39,35&39,36&39,37&39,38&40,38&41,38&41,39&40,39&39,39&39,40&39,41&39,42&38,42&38,41&38,40&38,39&37,39&36,39&36,38&37,38&38,38&38,37&38,36/76,35&77,35&77,36&77,37&77,38&78,3" +
"8&79,38&79,39&78,39&77,39&77,40&77,41&77,42&76,42&76,41&76,40&76,39&75,39&74,39&74,38&75,38&76,38&76,37&76,36/0,36&1,36&1,37&1,38&2,38&3,38&3,39&2,39&1,39&1,40&1,41&1,42&1,43&1,44&1,45&0,45&0,44&0,43&0,42&0,41&0,40&0,39&0,38&0,37/19,36&20,36&20,37&20,38&21,38&22,38&23,38&23,39&22,39&21,39&20,39&20,40&20,41&19,41&19,40&19,39&18,39&17,39&16,39&16,38&17,38&18,38&19,38&19,37/57,36&58,36&58,37&58,38&59,38&60,38&61,38&61,39&60,39&59,39&58,39&58,40&58,41&57,41&57,40&57,39&56,39&55,39&54,39&54,38&55,38&" +
"56,38&57,38&57,37/4,38&5,38&6,38&7,38&8,38&9,38&10,38&11,38&12,38&13,38&14,38&15,38&15,39&14,39&13,39&12,39&11,39&10,39&9,39&8,39&7,39&6,39&5,39&4,39/24,38&25,38&26,38&27,38&28,38&29,38&30,38&31,38&32,38&33,38&34,38&35,38&35,39&34,39&33,39&32,39&31,39&30,39&29,39&28,39&27,39&26,39&25,39&24,39/42,38&43,38&44,38&45,38&46,38&47,38&48,38&49,38&50,38&51,38&52,38&53,38&53,39&52,39&51,39&50,39&49,39&48,39&47,39&46,39&45,39&44,39&43,39&42,39/62,38&63,38&64,38&65,38&66,38&67,38&68,38&69,38&70,38&71,38&72" +
",38&73,38&73,39&72,39&71,39&70,39&69,39&68,39&67,39&66,39&65,39&64,39&63,39&62,39/80,38&81,38&82,38&83,38&84,38&85,38&86,38&87,38&88,38&89,38&90,38&91,38&91,39&90,39&89,39&88,39&87,39&86,39&85,39&84,39&83,39&82,39&81,39&80,39/19,42&20,42&20,43&20,44&20,45&20,46&20,47&20,48&20,49&20,50&20,51&20,52&20,53&19,53&19,52&19,51&19,50&19,49&19,48&19,47&19,46&19,45&19,44&19,43/57,42&58,42&58,43&58,44&58,45&58,46&58,47&58,48&58,49&58,50&58,51&58,52&58,53&57,53&57,52&57,51&57,50&57,49&57,48&57,47&57,46&57,4" +
"5&57,44&57,43/95,42&96,42&96,43&96,44&96,45&96,46&96,47&96,48&96,49&96,50&95,50&95,49&95,48&95,47&95,46&95,45&95,44&95,43/38,43&39,43&39,44&39,45&39,46&39,47&39,48&39,49&39,50&39,51&39,52&39,53&39,54&38,54&38,53&38,52&38,51&38,50&38,49&38,48&38,47&38,46&38,45&38,44/76,43&77,43&77,44&77,45&77,46&77,47&77,48&77,49&77,50&77,51&77,52&77,53&77,54&76,54&76,53&76,52&76,51&76,50&76,49&76,48&76,47&76,46&76,45&76,44/0,46&1,46&1,47&1,48&1,49&1,50&1,51&1,52&1,53&1,54&0,54&0,53&0,52&0,51&0,50&0,49&0,48&0,47/" +
"95,51&96,51&96,52&96,53&96,54&96,55&96,56&96,57&96,58&96,59&96,60&95,60&95,59&95,58&94,58&93,58&93,57&94,57&95,57&95,56&95,55&95,54&95,53&95,52/19,54&20,54&20,55&20,56&20,57&21,57&22,57&22,58&21,58&20,58&20,59&20,60&20,61&19,61&19,60&19,59&19,58&18,58&17,58&17,57&18,57&19,57&19,56&19,55/57,54&58,54&58,55&58,56&58,57&59,57&60,57&60,58&59,58&58,58&58,59&58,60&58,61&57,61&57,60&57,59&57,58&56,58&55,58&55,57&56,57&57,57&57,56&57,55/0,55&1,55&1,56&1,57&2,57&3,57&4,57&4,58&3,58&2,58&1,58&1,59&1,60&1,6" +
"1&1,62&1,63&0,63&0,62&0,61&0,60&0,59&0,58&0,57&0,56/38,55&39,55&39,56&39,57&40,57&41,57&42,57&42,58&41,58&40,58&39,58&39,59&39,60&38,60&38,59&38,58&37,58&36,58&35,58&35,57&36,57&37,57&38,57&38,56/76,55&77,55&77,56&77,57&78,57&79,57&80,57&80,58&79,58&78,58&77,58&77,59&77,60&76,60&76,59&76,58&75,58&74,58&73,58&73,57&74,57&75,57&76,57&76,56/5,57&6,57&7,57&8,57&9,57&10,57&11,57&12,57&13,57&14,57&15,57&16,57&16,58&15,58&14,58&13,58&12,58&11,58&10,58&9,58&8,58&7,58&6,58&5,58/23,57&24,57&25,57&26,57&27" +
",57&28,57&29,57&30,57&31,57&32,57&33,57&34,57&34,58&33,58&32,58&31,58&30,58&29,58&28,58&27,58&26,58&25,58&24,58&23,58/43,57&44,57&45,57&46,57&47,57&48,57&49,57&50,57&51,57&52,57&53,57&54,57&54,58&53,58&52,58&51,58&50,58&49,58&48,58&47,58&46,58&45,58&44,58&43,58/61,57&62,57&63,57&64,57&65,57&66,57&67,57&68,57&69,57&70,57&71,57&72,57&72,58&71,58&70,58&69,58&68,58&67,58&66,58&65,58&64,58&63,58&62,58&61,58/81,57&82,57&83,57&84,57&85,57&86,57&87,57&88,57&89,57&90,57&91,57&92,57&92,58&91,58&90,58&89,5" +
"8&88,58&87,58&86,58&85,58&84,58&83,58&82,58&81,58/38,61&39,61&39,62&39,63&39,64&39,65&39,66&39,67&39,68&39,69&39,70&39,71&39,72&38,72&38,71&38,70&38,69&38,68&38,67&38,66&38,65&38,64&38,63&38,62/76,61&77,61&77,62&77,63&77,64&77,65&77,66&77,67&77,68&77,69&77,70&77,71&77,72&76,72&76,71&76,70&76,69&76,68&76,67&76,66&76,65&76,64&76,63&76,62/95,61&96,61&96,62&96,63&96,64&96,65&96,66&96,67&96,68&96,69&96,70&96,71&96,72&95,72&95,71&95,70&95,69&95,68&95,67&95,66&95,65&95,64&95,63&95,62/19,62&20,62&20,63&" +
"20,64&20,65&20,66&20,67&20,68&20,69&20,70&20,71&20,72&20,73&19,73&19,72&19,71&19,70&19,69&19,68&19,67&19,66&19,65&19,64&19,63/57,62&58,62&58,63&58,64&58,65&58,66&58,67&58,68&58,69&58,70&58,71&58,72&58,73&57,73&57,72&57,71&57,70&57,69&57,68&57,67&57,66&57,65&57,64&57,63/0,64&1,64&1,65&1,66&1,67&1,68&1,69&1,70&1,71&1,72&1,73&1,74&1,75&0,75&0,74&0,73&0,72&0,71&0,70&0,69&0,68&0,67&0,66&0,65/38,73&39,73&39,74&39,75&39,76&40,76&41,76&41,77&40,77&39,77&39,78&39,79&39,80&38,80&38,79&38,78&38,77&37,77&36" +
",77&36,76&37,76&38,76&38,75&38,74/76,73&77,73&77,74&77,75&77,76&78,76&79,76&79,77&78,77&77,77&77,78&77,79&77,80&76,80&76,79&76,78&76,77&75,77&74,77&74,76&75,76&76,76&76,75&76,74/95,73&96,73&96,74&96,75&96,76&96,77&96,78&96,79&96,80&96,81&95,81&95,80&95,79&95,78&95,77&94,77&93,77&92,77&92,76&93,76&94,76&95,76&95,75&95,74/19,74&20,74&20,75&20,76&21,76&22,76&23,76&23,77&22,77&21,77&20,77&20,78&20,79&19,79&19,78&19,77&18,77&17,77&16,77&16,76&17,76&18,76&19,76&19,75/57,74&58,74&58,75&58,76&59,76&60,7" +
"6&61,76&61,77&60,77&59,77&58,77&58,78&58,79&57,79&57,78&57,77&56,77&55,77&54,77&54,76&55,76&56,76&57,76&57,75/0,76&1,76&2,76&3,76&3,77&2,77&1,77&1,78&1,79&1,80&1,81&1,82&1,83&1,84&1,85&0,85&0,84&0,83&0,82&0,81&0,80&0,79&0,78&0,77/4,76&5,76&6,76&7,76&8,76&9,76&10,76&11,76&12,76&13,76&14,76&15,76&15,77&14,77&13,77&12,77&11,77&10,77&9,77&8,77&7,77&6,77&5,77&4,77/24,76&25,76&26,76&27,76&28,76&29,76&30,76&31,76&32,76&33,76&34,76&35,76&35,77&34,77&33,77&32,77&31,77&30,77&29,77&28,77&27,77&26,77&25,77&" +
"24,77/42,76&43,76&44,76&45,76&46,76&47,76&48,76&49,76&50,76&51,76&52,76&53,76&53,77&52,77&51,77&50,77&49,77&48,77&47,77&46,77&45,77&44,77&43,77&42,77/62,76&63,76&64,76&65,76&66,76&67,76&68,76&69,76&70,76&71,76&72,76&73,76&73,77&72,77&71,77&70,77&69,77&68,77&67,77&66,77&65,77&64,77&63,77&62,77/80,76&81,76&82,76&83,76&84,76&85,76&86,76&87,76&88,76&89,76&90,76&91,76&91,77&90,77&89,77&88,77&87,77&86,77&85,77&84,77&83,77&82,77&81,77&80,77/19,80&20,80&20,81&20,82&20,83&20,84&20,85&20,86&20,87&20,88&20" +
",89&20,90&20,91&19,91&19,90&19,89&19,88&19,87&19,86&19,85&19,84&19,83&19,82&19,81/57,80&58,80&58,81&58,82&58,83&58,84&58,85&58,86&58,87&58,88&58,89&58,90&58,91&57,91&57,90&57,89&57,88&57,87&57,86&57,85&57,84&57,83&57,82&57,81/38,81&39,81&39,82&39,83&39,84&39,85&39,86&39,87&39,88&39,89&39,90&39,91&39,92&38,92&38,91&38,90&38,89&38,88&38,87&38,86&38,85&38,84&38,83&38,82/76,81&77,81&77,82&77,83&77,84&77,85&77,86&77,87&77,88&77,89&77,90&77,91&77,92&76,92&76,91&76,90&76,89&76,88&76,87&76,86&76,85&76,8" +
"4&76,83&76,82/95,82&96,82&96,83&96,84&96,85&96,86&96,87&96,88&96,89&96,90&96,91&96,92&96,93&95,93&95,92&95,91&95,90&95,89&95,88&95,87&95,86&95,85&95,84&95,83/0,86&1,86&1,87&1,88&1,89&1,90&1,91&1,92&1,93&1,94&1,95&2,95&2,96&1,96&0,96&0,95&0,94&0,93&0,92&0,91&0,90&0,89&0,88&0,87/19,92&20,92&20,93&20,94&20,95&21,95&22,95&23,95&23,96&22,96&21,96&20,96&19,96&18,96&17,96&16,96&15,96&15,95&16,95&17,95&18,95&19,95&19,94&19,93/57,92&58,92&58,93&58,94&58,95&59,95&60,95&61,95&62,95&63,95&63,96&62,96&61,96&" +
"60,96&59,96&58,96&57,96&56,96&55,96&55,95&56,95&57,95&57,94&57,93/38,93&39,93&39,94&39,95&40,95&41,95&42,95&43,95&44,95&45,95&45,96&44,96&43,96&42,96&41,96&40,96&39,96&38,96&37,96&36,96&36,95&37,95&38,95&38,94/76,93&77,93&77,94&77,95&78,95&79,95&80,95&81,95&82,95&83,95&84,95&85,95&85,96&84,96&83,96&82,96&81,96&80,96&79,96&78,96&77,96&76,96&76,95&76,94/95,94&96,94&96,95&96,96&95,96&94,96&93,96&92,96&91,96&90,96&89,96&88,96&87,96&86,96&86,95&87,95&88,95&89,95&90,95&91,95&92,95&93,95&94,95&95,95/3," +
"95&4,95&5,95&6,95&7,95&8,95&9,95&10,95&11,95&12,95&13,95&14,95&14,96&13,96&12,96&11,96&10,96&9,96&8,96&7,96&6,96&5,96&4,96&3,96/24,95&25,95&26,95&27,95&28,95&29,95&30,95&31,95&32,95&33,95&34,95&35,95&35,96&34,96&33,96&32,96&31,96&30,96&29,96&28,96&27,96&26,96&25,96&24,96/46,95&47,95&48,95&49,95&50,95&51,95&52,95&53,95&54,95&54,96&53,96&52,96&51,96&50,96&49,96&48,96&47,96&46,96/64,95&65,95&66,95&67,95&68,95&69,95&70,95&71,95&72,95&73,95&74,95&75,95&75,96&74,96&73,96&72,96&71,96&70,96&69,96&68,96&" +
"67,96&66,96&65,96&64,96";
			var paths = Paths.Split('/').Select(l => l.Split('&').Select(p => bodyLayout.GetPositionLight(Point.Parse(p))).ToList()).ToList();
			var pathColor = new List<int> { 2, 6, 4, 3, 1, 6, 1, 4, 5, 4, 6, 5, 1, 2, 5, 5, 4, 4, 2, 5, 1, 6, 3, 3, 3, 3, 2, 1, 5, 2, 3, 5, 1, 6, 4, 3, 1, 6, 4, 3, 5, 3, 3, 5, 5, 2, 1, 2, 6, 4, 1, 2, 1, 3, 4, 5, 2, 5, 3, 5, 6, 1, 3, 3, 1, 3, 5, 5, 5, 2, 6, 6, 5, 4, 3, 3, 3, 3, 5, 2, 4, 1, 3, 1, 6, 5, 2, 4, 5, 4, 2, 4 };

			var pastels = new List<int> { 0xf7ec45, 0xc8e06e, 0xf6b65c, 0xc7a3c7, 0xf2a2bb, 0x9cc9ea };
			var brightColor = new LightColor(1, 6, pastels.Multiply(Brightness).ToList(), Helpers.Rainbow6.Multiply(Brightness).ToList());
			var darkColor = new LightColor(1, 6, pastels.Multiply(DarkBrightness).ToList(), Helpers.Rainbow6.Multiply(DarkBrightness).ToList());

			var segment = new Segment();

			for (var time = 0; time < 72; ++time)
				for (var pathCtr = 0; pathCtr < paths.Count; pathCtr++)
				{
					var path = paths[pathCtr];
					var pos = time % path.Count;
					for (var ctr = 0; ctr < path.Count; ++ctr)
						segment.AddLight(path[ctr], time, ctr == pos ? brightColor : darkColor, pathColor[pathCtr]);
				}

			return segment;
		}

		Segment Corners2()
		{
			const string lightLocations =
"1,1/2,1/3,1/4,1/5,1/6,1/7,1/8,1/9,1/10,1/11,1/12,1/13,1/14,1/15,1/16,1/17,1/18,1/19,1/19,2/19,3/19,4/19,5/19,6/19,7/19,8/19,9/19,10/19,11/19,12/19,13/19,14/19,15/19,16/19,17/19,18/19,19/18,19/17,19/16,19/15,19/14,19/13,19/12,19/11,19/10,19/9,19/8,19/7,19/6,19/5,19/4,19/3,19/2,19/1,19/1,18/1,17/1,16/1,15/1,14/1,13/1,12/1,11/1,10/1,9/1,8/1,7/1,6/1,5/1,4/1,3/1,2|20,1/21,1/22,1/23,1/24,1/25,1/26,1/27,1/28,1/29,1/30,1/31,1/32,1/33,1/34,1/35,1/36,1/37,1/38,1/38,2/38,3/38,4/38,5/38,6/38,7/38,8/38,9/38," +
"10/38,11/38,12/38,13/38,14/38,15/38,16/38,17/38,18/38,19/37,19/36,19/35,19/34,19/33,19/32,19/31,19/30,19/29,19/28,19/27,19/26,19/25,19/24,19/23,19/22,19/21,19/20,19/20,18/20,17/20,16/20,15/20,14/20,13/20,12/20,11/20,10/20,9/20,8/20,7/20,6/20,5/20,4/20,3/20,2|39,1/40,1/41,1/42,1/43,1/44,1/45,1/46,1/47,1/48,1/49,1/50,1/51,1/52,1/53,1/54,1/55,1/56,1/57,1/57,2/57,3/57,4/57,5/57,6/57,7/57,8/57,9/57,10/57,11/57,12/57,13/57,14/57,15/57,16/57,17/57,18/57,19/56,19/55,19/54,19/53,19/52,19/51,19/50,19/49,1" +
"9/48,19/47,19/46,19/45,19/44,19/43,19/42,19/41,19/40,19/39,19/39,18/39,17/39,16/39,15/39,14/39,13/39,12/39,11/39,10/39,9/39,8/39,7/39,6/39,5/39,4/39,3/39,2|58,1/59,1/60,1/61,1/62,1/63,1/64,1/65,1/66,1/67,1/68,1/69,1/70,1/71,1/72,1/73,1/74,1/75,1/76,1/76,2/76,3/76,4/76,5/76,6/76,7/76,8/76,9/76,10/76,11/76,12/76,13/76,14/76,15/76,16/76,17/76,18/76,19/75,19/74,19/73,19/72,19/71,19/70,19/69,19/68,19/67,19/66,19/65,19/64,19/63,19/62,19/61,19/60,19/59,19/58,19/58,18/58,17/58,16/58,15/58,14/58,13/58,12" +
"/58,11/58,10/58,9/58,8/58,7/58,6/58,5/58,4/58,3/58,2|77,1/78,1/79,1/80,1/81,1/82,1/83,1/84,1/85,1/86,1/87,1/88,1/89,1/90,1/91,1/92,1/93,1/94,1/95,1/95,2/95,3/95,4/95,5/95,6/95,7/95,8/95,9/95,10/95,11/95,12/95,13/95,14/95,15/95,16/95,17/95,18/95,19/94,19/93,19/92,19/91,19/90,19/89,19/88,19/87,19/86,19/85,19/84,19/83,19/82,19/81,19/80,19/79,19/78,19/77,19/77,18/77,17/77,16/77,15/77,14/77,13/77,12/77,11/77,10/77,9/77,8/77,7/77,6/77,5/77,4/77,3/77,2|1,20/2,20/3,20/4,20/5,20/6,20/7,20/8,20/9,20/10,20" +
"/11,20/12,20/13,20/14,20/15,20/16,20/17,20/18,20/19,20/19,21/19,22/19,23/19,24/19,25/19,26/19,27/19,28/19,29/19,30/19,31/19,32/19,33/19,34/19,35/19,36/19,37/19,38/18,38/17,38/16,38/15,38/14,38/13,38/12,38/11,38/10,38/9,38/8,38/7,38/6,38/5,38/4,38/3,38/2,38/1,38/1,37/1,36/1,35/1,34/1,33/1,32/1,31/1,30/1,29/1,28/1,27/1,26/1,25/1,24/1,23/1,22/1,21|20,20/21,20/22,20/23,20/24,20/25,20/26,20/27,20/28,20/29,20/30,20/31,20/32,20/33,20/34,20/35,20/36,20/37,20/38,20/38,21/38,22/38,23/38,24/38,25/38,26/38," +
"27/38,28/38,29/38,30/38,31/38,32/38,33/38,34/38,35/38,36/38,37/38,38/37,38/36,38/35,38/34,38/33,38/32,38/31,38/30,38/29,38/28,38/27,38/26,38/25,38/24,38/23,38/22,38/21,38/20,38/20,37/20,36/20,35/20,34/20,33/20,32/20,31/20,30/20,29/20,28/20,27/20,26/20,25/20,24/20,23/20,22/20,21|39,20/40,20/41,20/42,20/43,20/44,20/45,20/46,20/47,20/48,20/49,20/50,20/51,20/52,20/53,20/54,20/55,20/56,20/57,20/57,21/57,22/57,23/57,24/57,25/57,26/57,27/57,28/57,29/57,30/57,31/57,32/57,33/57,34/57,35/57,36/57,37/57,38" +
"/56,38/55,38/54,38/53,38/52,38/51,38/50,38/49,38/48,38/47,38/46,38/45,38/44,38/43,38/42,38/41,38/40,38/39,38/39,37/39,36/39,35/39,34/39,33/39,32/39,31/39,30/39,29/39,28/39,27/39,26/39,25/39,24/39,23/39,22/39,21|58,20/59,20/60,20/61,20/62,20/63,20/64,20/65,20/66,20/67,20/68,20/69,20/70,20/71,20/72,20/73,20/74,20/75,20/76,20/76,21/76,22/76,23/76,24/76,25/76,26/76,27/76,28/76,29/76,30/76,31/76,32/76,33/76,34/76,35/76,36/76,37/76,38/75,38/74,38/73,38/72,38/71,38/70,38/69,38/68,38/67,38/66,38/65,38/6" +
"4,38/63,38/62,38/61,38/60,38/59,38/58,38/58,37/58,36/58,35/58,34/58,33/58,32/58,31/58,30/58,29/58,28/58,27/58,26/58,25/58,24/58,23/58,22/58,21|77,20/78,20/79,20/80,20/81,20/82,20/83,20/84,20/85,20/86,20/87,20/88,20/89,20/90,20/91,20/92,20/93,20/94,20/95,20/95,21/95,22/95,23/95,24/95,25/95,26/95,27/95,28/95,29/95,30/95,31/95,32/95,33/95,34/95,35/95,36/95,37/95,38/94,38/93,38/92,38/91,38/90,38/89,38/88,38/87,38/86,38/85,38/84,38/83,38/82,38/81,38/80,38/79,38/78,38/77,38/77,37/77,36/77,35/77,34/77," +
"33/77,32/77,31/77,30/77,29/77,28/77,27/77,26/77,25/77,24/77,23/77,22/77,21|1,39/2,39/3,39/4,39/5,39/6,39/7,39/8,39/9,39/10,39/11,39/12,39/13,39/14,39/15,39/16,39/17,39/18,39/19,39/19,40/19,41/19,42/19,43/19,44/19,45/19,46/19,47/19,48/19,49/19,50/19,51/19,52/19,53/19,54/19,55/19,56/19,57/18,57/17,57/16,57/15,57/14,57/13,57/12,57/11,57/10,57/9,57/8,57/7,57/6,57/5,57/4,57/3,57/2,57/1,57/1,56/1,55/1,54/1,53/1,52/1,51/1,50/1,49/1,48/1,47/1,46/1,45/1,44/1,43/1,42/1,41/1,40|20,39/21,39/22,39/23,39/24,3" +
"9/25,39/26,39/27,39/28,39/29,39/30,39/31,39/32,39/33,39/34,39/35,39/36,39/37,39/38,39/38,40/38,41/38,42/38,43/38,44/38,45/38,46/38,47/38,48/38,49/38,50/38,51/38,52/38,53/38,54/38,55/38,56/38,57/37,57/36,57/35,57/34,57/33,57/32,57/31,57/30,57/29,57/28,57/27,57/26,57/25,57/24,57/23,57/22,57/21,57/20,57/20,56/20,55/20,54/20,53/20,52/20,51/20,50/20,49/20,48/20,47/20,46/20,45/20,44/20,43/20,42/20,41/20,40|39,39/40,39/41,39/42,39/43,39/44,39/45,39/46,39/47,39/48,39/49,39/50,39/51,39/52,39/53,39/54,39/" +
"55,39/56,39/57,39/57,40/57,41/57,42/57,43/57,44/57,45/57,46/57,47/57,48/57,49/57,50/57,51/57,52/57,53/57,54/57,55/57,56/57,57/56,57/55,57/54,57/53,57/52,57/51,57/50,57/49,57/48,57/47,57/46,57/45,57/44,57/43,57/42,57/41,57/40,57/39,57/39,56/39,55/39,54/39,53/39,52/39,51/39,50/39,49/39,48/39,47/39,46/39,45/39,44/39,43/39,42/39,41/39,40|58,39/59,39/60,39/61,39/62,39/63,39/64,39/65,39/66,39/67,39/68,39/69,39/70,39/71,39/72,39/73,39/74,39/75,39/76,39/76,40/76,41/76,42/76,43/76,44/76,45/76,46/76,47/76" +
",48/76,49/76,50/76,51/76,52/76,53/76,54/76,55/76,56/76,57/75,57/74,57/73,57/72,57/71,57/70,57/69,57/68,57/67,57/66,57/65,57/64,57/63,57/62,57/61,57/60,57/59,57/58,57/58,56/58,55/58,54/58,53/58,52/58,51/58,50/58,49/58,48/58,47/58,46/58,45/58,44/58,43/58,42/58,41/58,40|77,39/78,39/79,39/80,39/81,39/82,39/83,39/84,39/85,39/86,39/87,39/88,39/89,39/90,39/91,39/92,39/93,39/94,39/95,39/95,40/95,41/95,42/95,43/95,44/95,45/95,46/95,47/95,48/95,49/95,50/95,51/95,52/95,53/95,54/95,55/95,56/95,57/94,57/93,5" +
"7/92,57/91,57/90,57/89,57/88,57/87,57/86,57/85,57/84,57/83,57/82,57/81,57/80,57/79,57/78,57/77,57/77,56/77,55/77,54/77,53/77,52/77,51/77,50/77,49/77,48/77,47/77,46/77,45/77,44/77,43/77,42/77,41/77,40|1,58/2,58/3,58/4,58/5,58/6,58/7,58/8,58/9,58/10,58/11,58/12,58/13,58/14,58/15,58/16,58/17,58/18,58/19,58/19,59/19,60/19,61/19,62/19,63/19,64/19,65/19,66/19,67/19,68/19,69/19,70/19,71/19,72/19,73/19,74/19,75/19,76/18,76/17,76/16,76/15,76/14,76/13,76/12,76/11,76/10,76/9,76/8,76/7,76/6,76/5,76/4,76/3,7" +
"6/2,76/1,76/1,75/1,74/1,73/1,72/1,71/1,70/1,69/1,68/1,67/1,66/1,65/1,64/1,63/1,62/1,61/1,60/1,59|20,58/21,58/22,58/23,58/24,58/25,58/26,58/27,58/28,58/29,58/30,58/31,58/32,58/33,58/34,58/35,58/36,58/37,58/38,58/38,59/38,60/38,61/38,62/38,63/38,64/38,65/38,66/38,67/38,68/38,69/38,70/38,71/38,72/38,73/38,74/38,75/38,76/37,76/36,76/35,76/34,76/33,76/32,76/31,76/30,76/29,76/28,76/27,76/26,76/25,76/24,76/23,76/22,76/21,76/20,76/20,75/20,74/20,73/20,72/20,71/20,70/20,69/20,68/20,67/20,66/20,65/20,64/2" +
"0,63/20,62/20,61/20,60/20,59|39,58/40,58/41,58/42,58/43,58/44,58/45,58/46,58/47,58/48,58/49,58/50,58/51,58/52,58/53,58/54,58/55,58/56,58/57,58/57,59/57,60/57,61/57,62/57,63/57,64/57,65/57,66/57,67/57,68/57,69/57,70/57,71/57,72/57,73/57,74/57,75/57,76/56,76/55,76/54,76/53,76/52,76/51,76/50,76/49,76/48,76/47,76/46,76/45,76/44,76/43,76/42,76/41,76/40,76/39,76/39,75/39,74/39,73/39,72/39,71/39,70/39,69/39,68/39,67/39,66/39,65/39,64/39,63/39,62/39,61/39,60/39,59|58,58/59,58/60,58/61,58/62,58/63,58/64," +
"58/65,58/66,58/67,58/68,58/69,58/70,58/71,58/72,58/73,58/74,58/75,58/76,58/76,59/76,60/76,61/76,62/76,63/76,64/76,65/76,66/76,67/76,68/76,69/76,70/76,71/76,72/76,73/76,74/76,75/76,76/75,76/74,76/73,76/72,76/71,76/70,76/69,76/68,76/67,76/66,76/65,76/64,76/63,76/62,76/61,76/60,76/59,76/58,76/58,75/58,74/58,73/58,72/58,71/58,70/58,69/58,68/58,67/58,66/58,65/58,64/58,63/58,62/58,61/58,60/58,59|77,58/78,58/79,58/80,58/81,58/82,58/83,58/84,58/85,58/86,58/87,58/88,58/89,58/90,58/91,58/92,58/93,58/94,58" +
"/95,58/95,59/95,60/95,61/95,62/95,63/95,64/95,65/95,66/95,67/95,68/95,69/95,70/95,71/95,72/95,73/95,74/95,75/95,76/94,76/93,76/92,76/91,76/90,76/89,76/88,76/87,76/86,76/85,76/84,76/83,76/82,76/81,76/80,76/79,76/78,76/77,76/77,75/77,74/77,73/77,72/77,71/77,70/77,69/77,68/77,67/77,66/77,65/77,64/77,63/77,62/77,61/77,60/77,59|1,77/2,77/3,77/4,77/5,77/6,77/7,77/8,77/9,77/10,77/11,77/12,77/13,77/14,77/15,77/16,77/17,77/18,77/19,77/19,78/19,79/19,80/19,81/19,82/19,83/19,84/19,85/19,86/19,87/19,88/19,8" +
"9/19,90/19,91/19,92/19,93/19,94/19,95/18,95/17,95/16,95/15,95/14,95/13,95/12,95/11,95/10,95/9,95/8,95/7,95/6,95/5,95/4,95/3,95/2,95/1,95/1,94/1,93/1,92/1,91/1,90/1,89/1,88/1,87/1,86/1,85/1,84/1,83/1,82/1,81/1,80/1,79/1,78|20,77/21,77/22,77/23,77/24,77/25,77/26,77/27,77/28,77/29,77/30,77/31,77/32,77/33,77/34,77/35,77/36,77/37,77/38,77/38,78/38,79/38,80/38,81/38,82/38,83/38,84/38,85/38,86/38,87/38,88/38,89/38,90/38,91/38,92/38,93/38,94/38,95/37,95/36,95/35,95/34,95/33,95/32,95/31,95/30,95/29,95/28" +
",95/27,95/26,95/25,95/24,95/23,95/22,95/21,95/20,95/20,94/20,93/20,92/20,91/20,90/20,89/20,88/20,87/20,86/20,85/20,84/20,83/20,82/20,81/20,80/20,79/20,78|39,77/40,77/41,77/42,77/43,77/44,77/45,77/46,77/47,77/48,77/49,77/50,77/51,77/52,77/53,77/54,77/55,77/56,77/57,77/57,78/57,79/57,80/57,81/57,82/57,83/57,84/57,85/57,86/57,87/57,88/57,89/57,90/57,91/57,92/57,93/57,94/57,95/56,95/55,95/54,95/53,95/52,95/51,95/50,95/49,95/48,95/47,95/46,95/45,95/44,95/43,95/42,95/41,95/40,95/39,95/39,94/39,93/39,9" +
"2/39,91/39,90/39,89/39,88/39,87/39,86/39,85/39,84/39,83/39,82/39,81/39,80/39,79/39,78|58,77/59,77/60,77/61,77/62,77/63,77/64,77/65,77/66,77/67,77/68,77/69,77/70,77/71,77/72,77/73,77/74,77/75,77/76,77/76,78/76,79/76,80/76,81/76,82/76,83/76,84/76,85/76,86/76,87/76,88/76,89/76,90/76,91/76,92/76,93/76,94/76,95/75,95/74,95/73,95/72,95/71,95/70,95/69,95/68,95/67,95/66,95/65,95/64,95/63,95/62,95/61,95/60,95/59,95/58,95/58,94/58,93/58,92/58,91/58,90/58,89/58,88/58,87/58,86/58,85/58,84/58,83/58,82/58,81/" +
"58,80/58,79/58,78|77,77/78,77/79,77/80,77/81,77/82,77/83,77/84,77/85,77/86,77/87,77/88,77/89,77/90,77/91,77/92,77/93,77/94,77/95,77/95,78/95,79/95,80/95,81/95,82/95,83/95,84/95,85/95,86/95,87/95,88/95,89/95,90/95,91/95,92/95,93/95,94/95,95/94,95/93,95/92,95/91,95/90,95/89,95/88,95/87,95/86,95/85,95/84,95/83,95/82,95/81,95/80,95/79,95/78,95/77,95/77,94/77,93/77,92/77,91/77,90/77,89/77,88/77,87/77,86/77,85/77,84/77,83/77,82/77,81/77,80/77,79/77,78";

			var squareLights = lightLocations.Split('|').Select(l => l.Split('/').Select(p => bodyLayout.GetPositionLight(Point.Parse(p))).ToList()).ToList();

			var segment = new Segment();
			var color = new LightColor(0, 72, new List<int> { 0xff0000, 0xffff00, 0x0000ff, 0x00ff00, 0xff0000 }.Multiply(Brightness).ToList());
			Func<int, int> delay = position => position % 18 == 0 ? 500 : 20;
			for (var squareCtr = 0; squareCtr < squareLights.Count; squareCtr++)
			{
				var position = 0;
				var time = 0;

				var useTime = delay(position);

				for (var pass = 0; pass < 2; ++pass)
					for (var ctr = 0; ctr <= 72; ++ctr)
					{
						segment.AddLight(squareLights[squareCtr][position], time, time + delay(position) + 840, color, position, Segment.Absolute, 0x000000);

						time += useTime;
						++position;
						while (position >= 72)
							position -= 72;
						useTime = delay(position);
					}
			}

			return segment;
		}

		Segment Randomized2()
		{
			const int Spacing = 80;

			var segment = new Segment();
			var color = new LightColor(0, 5, Helpers.Rainbow6.Multiply(Brightness).ToList());
			var rand = new Random(0xfacade);
			var time = 0;
			for (var colorCtr = 0; colorCtr < Helpers.Rainbow6.Count; ++colorCtr)
			{
				foreach (var light in bodyLayout.GetAllLights())
				{
					var count = rand.Next(10, 60);
					for (var ctr = 0; ctr < count; ++ctr)
					{
						int colorIndex;
						while (true)
						{
							colorIndex = rand.Next(Helpers.Rainbow6.Count);
							if (colorIndex != colorCtr)
								break;
						}
						segment.AddLight(light, time + ctr, color, colorIndex);
					}
					segment.AddLight(light, time + count, color, colorCtr);
				}
				time += Spacing;
			}

			return segment;
		}

		public Song Render()
		{
			var song = new Song("shutupanddance.wav");
			song.AddPaletteSequence(0, 0);

			// ShapeChange (500)
			var shapeChange = ShapeChange();
			song.AddSegmentWithRepeat(shapeChange, 0, 360, 500, 1875);
			song.AddSegmentWithRepeat(shapeChange, 360, 720, song.MaxTime(), 1875, 3);
			song.AddSegmentWithRepeat(shapeChange, 720, 1080, song.MaxTime(), 1875);
			song.AddSegmentWithRepeat(shapeChange, 1080, 1440, song.MaxTime(), 1875, 3);
			song.AddSegmentWithRepeat(shapeChange, 1440, 1800, song.MaxTime(), 1875);
			song.AddSegmentWithRepeat(shapeChange, 1800, 2160, song.MaxTime(), 1875, 3);
			song.AddSegmentWithRepeat(shapeChange, 2160, 2880, song.MaxTime(), 3750);

			// Randomized (26750)
			var randomized = Randomized();
			song.AddSegmentWithRepeat(randomized, 0, 2912 * 6, 26750, 3750 * 6);

			// CornerRotate (49250)
			var cornerRotate = CornerRotate();
			song.AddSegmentWithRepeat(cornerRotate, 0, 360, 49250, 3750, 4);
			song.AddPaletteSequence(49250, 0);
			song.AddPaletteSequence(52500, 53500, null, 1);
			song.AddPaletteSequence(56250, 57250, null, 2);
			song.AddPaletteSequence(60000, 61000, null, 3);
			song.AddPaletteSequence(64250, 0);

			// Corners (64250)
			var corners = Corners();
			song.AddSegmentWithRepeat(corners, 200, 400, 64250, 3750, 5);
			song.AddPaletteSequence(64250, 0);
			song.AddPaletteSequence(73125, 74125, null, 1);
			song.AddPaletteSequence(83000, 0);

			// Stars (83000)
			var stars = Stars();
			song.AddSegmentWithRepeat(stars, 0, 200, 83000, 7500);

			// CircleWarp (90500)
			var circleWarp = CircleWarp();
			song.AddSegmentWithRepeat(circleWarp, 0, 340, 90500, 18750);

			// Bounce (109250) Duration: 30500
			var bounce = Bounce();
			song.AddSegmentWithRepeat(bounce, 0, 96, 109250, 3750);
			song.AddSegmentWithRepeat(bounce, 96, 196, song.MaxTime(), 1875);
			song.AddSegmentWithRepeat(bounce, 196, 292, song.MaxTime(), 1875, 13);

			// Flash (139250)
			var flash = Flash();
			song.AddSegmentWithRepeat(flash, 0, flash.MaxTime() + 1, 139250, 1875, 4);

			song.AddPaletteSequence(139250, 0);
			song.AddPaletteSequence(143000, 1);
			song.AddPaletteSequence(146750, 0);

			// Halves (146750)
			var halves = Halves();
			song.AddSegmentWithRepeat(halves, 0, 0, 146750, 15000);
			song.AddPaletteSequence(146750, 0);
			song.AddPaletteSequence(148125, 149125, null, 1);
			song.AddPaletteSequence(150000, 151000, null, 2);
			song.AddPaletteSequence(151875, 152875, null, 3);
			song.AddPaletteSequence(153750, 154750, null, 4);
			song.AddPaletteSequence(155625, 156625, null, 5);
			song.AddPaletteSequence(157500, 158500, null, 6);
			song.AddPaletteSequence(159375, 160375, null, 7);
			song.AddPaletteSequence(161750, 0);

			// Paths (161750)
			var paths = Paths();
			song.AddSegmentWithRepeat(paths, 0, 72, 161750, 5625, 2);
			song.AddPaletteSequence(161750, 0);
			song.AddPaletteSequence(166875, 167875, null, 1);
			song.AddPaletteSequence(173000, 0);

			// Corners2 (173000)
			var corners2 = Corners2();
			song.AddSegmentWithRepeat(corners2, 0, 3360, 173000, 3750);
			song.AddSegmentWithRepeat(corners2, 3360, 6720, song.MaxTime(), 3750, 2);

			// Randomized2 (184250)
			var randomized2 = Randomized2();
			song.AddSegmentWithRepeat(randomized2, 0, 80 * 6, 184250, 1875 * 6);

			return song;
		}
	}
}
