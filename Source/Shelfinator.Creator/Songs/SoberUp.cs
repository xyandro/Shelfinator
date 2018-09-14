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
	class SoberUp : SongCreator
	{
		public override int SongNumber => 9;

		readonly Layout headerLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Header.png");
		readonly Layout bodyLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");

		Segment HelloHello()
		{
			const int StartY = 48;
			const int Height = 9;
			const int ZigZagBorder = 4;
			const double Fade = .4;
			const int TotalBubbleTime = 300;
			const double BubbleWidth = 10;
			const double BubbleBorder = 2;

			var segment = new Segment();

			var helloLights = new List<Point>
			{
				new Point(2, 1), new Point(2, 2), new Point(2, 3), new Point(2, 4), new Point(2, 5), new Point(3, 1), new Point(3, 2), new Point(3, 3),
				new Point(3, 4), new Point(3, 5), new Point(4, 3), new Point(5, 3), new Point(6, 1), new Point(6, 2), new Point(6, 3), new Point(6, 4),
				new Point(6, 5), new Point(7, 1), new Point(7, 2), new Point(7, 3), new Point(7, 4), new Point(7, 5), new Point(9, 1), new Point(9, 2),
				new Point(9, 3), new Point(9, 4), new Point(9, 5), new Point(10, 1), new Point(10, 2), new Point(10, 3), new Point(10, 4), new Point(10, 5),
				new Point(11, 1), new Point(11, 3), new Point(11, 5), new Point(12, 1), new Point(12, 3), new Point(12, 5), new Point(14, 1), new Point(14, 2),
				new Point(14, 3), new Point(14, 4), new Point(14, 5), new Point(15, 1), new Point(15, 2), new Point(15, 3), new Point(15, 4), new Point(15, 5),
				new Point(16, 5), new Point(17, 5), new Point(19, 1), new Point(19, 2), new Point(19, 3), new Point(19, 4), new Point(19, 5), new Point(20, 1),
				new Point(20, 2), new Point(20, 3), new Point(20, 4), new Point(20, 5), new Point(21, 5), new Point(22, 5), new Point(24, 2), new Point(24, 3),
				new Point(24, 4), new Point(25, 1), new Point(25, 2), new Point(25, 3), new Point(25, 4), new Point(25, 5), new Point(26, 1), new Point(26, 5),
				new Point(27, 1), new Point(27, 5), new Point(28, 1), new Point(28, 2), new Point(28, 3), new Point(28, 4), new Point(28, 5), new Point(29, 2),
				new Point(29, 3), new Point(29, 4),
			};

			var zigZagTimes = new List<Tuple<int, double>>
			{
				Tuple.Create(0, 4.1),
				Tuple.Create(200, 2.77741935483871),
				Tuple.Create(300, 3.43870967741935),
				Tuple.Create(500, 2.77741935483871),
				Tuple.Create(600, 2.77741935483871),
				Tuple.Create(700, 2.77741935483871),
			};

			zigZagTimes = Enumerable.Range(0, 20).SelectMany(x => zigZagTimes.Select(y => Tuple.Create(y.Item1 + x * 800, y.Item2))).ToList();
			var bubbleTimes = new List<Tuple<int, Point>>
			{
				Tuple.Create(3200, new Point(0, 0)),
				Tuple.Create(3500, new Point(0, 0)),
				Tuple.Create(9600, new Point(96, 96)),
				Tuple.Create(9900, new Point(96, 96)),
			};

			var borderColor = new LightColor(new List<List<int>> { new List<int> { 0x100d04 }, new List<int> { 0x00000c } });
			var bodyColor = new LightColor(new List<List<int>> { new List<int> { 0x020101 }, new List<int> { 0x00000c } });
			var zigZagColor = new LightColor(0, 1, new List<List<int>> { new List<int> { 0x000304, 0x040e20 }, new List<int> { 0x00000c } });
			var bubbleColor = new LightColor(0, 1, new List<int> { 0x151407, 0x11110e });

			var onZigZagIndex = -1;
			var onBubbleIndex = 0;
			double amp = 0;
			for (var time = 0; time < 16000; time += 20)
			{
				bodyLayout.GetPositionLights(1, 1, 95, 95).ForEach(light => segment.AddLight(light, time, bodyColor, 0));

				if ((onZigZagIndex + 1 < zigZagTimes.Count) && (time >= zigZagTimes[onZigZagIndex + 1].Item1))
				{
					++onZigZagIndex;
					amp = zigZagTimes[onZigZagIndex].Item2;
				}
				else
					amp = amp - Fade;

				var x = 1;
				var y = 9;
				for (var ctr = 0; ctr < 5; ++ctr)
				{
					var direction = (ctr % 2) * 2 - 1;
					for (var xCtr = 0; xCtr < 19; ++xCtr)
					{
						var y1 = StartY + y * amp * ((onZigZagIndex % 2) * 2 - 1) - (Height - 1) / 2;
						foreach (var light in bodyLayout.GetPositionLights(x, y1 - ZigZagBorder, 1, Height + ZigZagBorder * 2))
							segment.AddLight(light, time, zigZagColor, 0);
						foreach (var light in bodyLayout.GetPositionLights(x, y1, 1, Height))
							segment.AddLight(light, time, zigZagColor, 1);

						++x;
						y += direction;
					}
					y -= direction;
				}

				if (onBubbleIndex < bubbleTimes.Count)
				{
					var bubbleTime = time - bubbleTimes[onBubbleIndex].Item1;
					if (bubbleTime >= 0)
					{
						var point = bubbleTimes[onBubbleIndex].Item2;
						var useDist = bubbleTime * (135.764501987817 + BubbleWidth + BubbleBorder * 2) / TotalBubbleTime;
						foreach (var light in bodyLayout.GetAllLights())
						{
							var pos = bodyLayout.GetLightPosition(light);
							var dist = (pos - point).Length;
							if ((dist > useDist - BubbleWidth - BubbleBorder * 2) && (dist < useDist))
								segment.AddLight(light, time, bubbleColor, 1);
							if ((dist > useDist - BubbleWidth - BubbleBorder) && (dist < useDist - BubbleBorder))
								segment.AddLight(light, time, bubbleColor, 0);
						}
					}
					if (bubbleTime >= TotalBubbleTime)
						++onBubbleIndex;
				}

				bodyLayout.GetPositionLights(0, 0, 97, 97).Except(bodyLayout.GetPositionLights(1, 1, 95, 95)).ForEach(light => segment.AddLight(light, time, borderColor, 0));
			}

			foreach (var point in helloLights)
			{
				var light = headerLayout.GetPositionLight(point);
				foreach (var bubbleTime in bubbleTimes)
					segment.AddLight(light, bubbleTime.Item1, bubbleTime.Item1 + TotalBubbleTime, bubbleColor, 0, 0x000000);
			}

			return segment;
		}

		Segment Waves()
		{
			const int Width = 5;
			const double MinAmplitude = 8;
			const double MaxAmplitude = 17;
			const double AmplitudeFrequency = 1600d;
			const double NumWaves = 2;
			const double Start1 = 0;
			const double Start2 = 120;
			const double X1 = 29;
			const double X2 = 67;

			const int MinSparkleTime = 15;
			const int MaxSparkleTime = 45;

			const int FlashTime = 5;

			var rand = new Random(0xf0dface);

			var segment = new Segment();

			var sparkleTimes = new HashSet<int> { 3600, 10000, 13200 };
			var sparkleLights = new Dictionary<int, int>();

			var flashTimes = new List<int> { 14400, 14600, 14800, 15000 };
			var flashColor = new LightColor(900, 6788, new List<int> { 0x101010, 0x101000 });

			var wavyColors = new List<LightColor>
			{
				 new LightColor(0, 4, new List<List<int>> { new List<int> { 0x00000c }, new List<int> { 0x00000c, 0x100f0d, 0x030f0f, 0x100f0d, 0x00000c }}),
				 new LightColor(0, 4, new List<List<int>> { new List<int> { 0x00000c }, new List<int> { 0x101000, 0x100f09, 0x0f0a00, 0x100f09, 0x101000 }}),
			};
			int flashTime = 0;
			for (var time = 0; time < 16000; time += 20)
			{
				segment.Clear(time);

				bodyLayout.GetAllLights().ForEach(light => segment.AddLight(light, time, 0x00000c));

				if (sparkleTimes.Contains(time))
					sparkleLights = bodyLayout.GetAllLights().ToDictionary(light => light, light => rand.Next(MinSparkleTime, MaxSparkleTime));

				var startAngle = -(time % 800) * 360 / 800;
				var amplitude = (Math.Sin((time % AmplitudeFrequency) / AmplitudeFrequency * 360 / 180 * Math.PI) + 1) / 2 * (MaxAmplitude - MinAmplitude) + MinAmplitude;

				for (var y = 0; y < 97; ++y)
				{
					var x = new List<int> { 0 };
					var angle = (Start1 + startAngle + 360 * NumWaves * (y / 96d)) / 180 * Math.PI;
					x.Add((X1 + Math.Sin(angle) * amplitude - (Width - 1) / 2).Round());
					x.Add(x.Last() + Width);

					angle = (Start2 + startAngle + 360 * NumWaves * (y / 96d)) / 180 * Math.PI;
					x.Add((X2 + Math.Sin(angle) * amplitude - (Width - 1) / 2).Round());
					x.Add(x.Last() + Width);

					x.Add(97);

					for (var ctr = 0; ctr < x.Count - 1; ++ctr)
					{
						foreach (var light in bodyLayout.GetPositionLights(x[ctr], y, x[ctr + 1] - x[ctr], 1))
						{
							var useColor = sparkleLights.ContainsKey(light) ? 1 : 0;
							segment.AddLight(light, time, wavyColors[useColor], ctr);
						}
					}
				}

				if (flashTimes.Contains(time))
					flashTime = FlashTime;
				if (flashTime > 0)
				{
					var center = new Point(48, 48);
					foreach (var light in bodyLayout.GetAllLights())
					{
						var pos = bodyLayout.GetLightPosition(light);
						segment.AddLight(light, time, flashColor, ((pos - center).Length * 100).Round());
					}
					--flashTime;
				}

				sparkleLights = sparkleLights.Where(pair => pair.Value > 0).ToDictionary(pair => pair.Key, pair => pair.Value - 1);
			}

			foreach (var light in bodyLayout.GetAllLights().Concat(headerLayout.GetAllLights()))
				segment.AddLight(light, 15200, 15400, 0x101010, 0x000000);

			var qmPoints = new List<Point>
			{
				new Point(5, 0), new Point(6, 0), new Point(7, 0), new Point(8, 0), new Point(9, 0), new Point(13, 0), new Point(14, 0), new Point(15, 0),
				new Point(16, 0), new Point(17, 0), new Point(21, 0), new Point(22, 0), new Point(23, 0), new Point(24, 0), new Point(25, 0), new Point(4, 1),
				new Point(5, 1), new Point(9, 1), new Point(10, 1), new Point(12, 1), new Point(13, 1), new Point(17, 1), new Point(18, 1), new Point(20, 1),
				new Point(21, 1), new Point(25, 1), new Point(26, 1), new Point(9, 2), new Point(10, 2), new Point(17, 2), new Point(18, 2), new Point(25, 2),
				new Point(26, 2), new Point(8, 3), new Point(9, 3), new Point(16, 3), new Point(17, 3), new Point(24, 3), new Point(25, 3), new Point(7, 4),
				new Point(8, 4), new Point(15, 4), new Point(16, 4), new Point(23, 4), new Point(24, 4), new Point(7, 5), new Point(8, 5), new Point(15, 5),
				new Point(16, 5), new Point(23, 5), new Point(24, 5), new Point(7, 7), new Point(8, 7), new Point(15, 7), new Point(16, 7), new Point(23, 7),
				new Point(24, 7),
			};

			foreach (var point in qmPoints)
			{
				var light = headerLayout.GetPositionLight(point);
				segment.AddLight(light, 15200, 0x101010);
			}

			return segment;
		}

		void ShapeChangeRender(Canvas canvas, Segment segment, Layout layout, int time, Func<int, int, int> substituteColor = null)
		{
			if (substituteColor == null)
				substituteColor = (light, color) => color;

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
						segment.AddLight(light, time, substituteColor(light, value));
				}
		}

		Segment Goodbye()
		{
			const double LineWidth = 5;
			const double Fade = 0.7;
			const double Size1 = 40;
			const double Size2 = 28;
			const double MaxDist = 8;
			const double Ratio2 = .8;
			const int NumPoints = 16;
			const int TotalBubbleTime = 300;
			const double BubbleWidth = 10;
			const double BubbleBorder = 2;

			var goodbyeLights = new List<Point>
			{
				new Point(1, 0), new Point(2, 0), new Point(6, 0), new Point(7, 0), new Point(11, 0), new Point(12, 0), new Point(15, 0), new Point(16, 0),
				new Point(17, 0), new Point(20, 0), new Point(21, 0), new Point(22, 0), new Point(25, 0), new Point(27, 0), new Point(29, 0), new Point(30, 0),
				new Point(31, 0), new Point(0, 1), new Point(3, 1), new Point(5, 1), new Point(8, 1), new Point(10, 1), new Point(13, 1), new Point(15, 1),
				new Point(18, 1), new Point(20, 1), new Point(23, 1), new Point(25, 1), new Point(27, 1), new Point(29, 1), new Point(0, 2), new Point(3, 2),
				new Point(5, 2), new Point(8, 2), new Point(10, 2), new Point(13, 2), new Point(15, 2), new Point(18, 2), new Point(20, 2), new Point(23, 2),
				new Point(25, 2), new Point(27, 2), new Point(29, 2), new Point(0, 3), new Point(5, 3), new Point(8, 3), new Point(10, 3), new Point(13, 3),
				new Point(15, 3), new Point(18, 3), new Point(20, 3), new Point(21, 3), new Point(22, 3), new Point(26, 3), new Point(29, 3), new Point(30, 3),
				new Point(0, 4), new Point(2, 4), new Point(3, 4), new Point(5, 4), new Point(8, 4), new Point(10, 4), new Point(13, 4), new Point(15, 4),
				new Point(18, 4), new Point(20, 4), new Point(23, 4), new Point(26, 4), new Point(29, 4), new Point(0, 5), new Point(3, 5), new Point(5, 5),
				new Point(8, 5), new Point(10, 5), new Point(13, 5), new Point(15, 5), new Point(18, 5), new Point(20, 5), new Point(23, 5), new Point(26, 5),
				new Point(29, 5), new Point(0, 6), new Point(3, 6), new Point(5, 6), new Point(8, 6), new Point(10, 6), new Point(13, 6), new Point(15, 6),
				new Point(18, 6), new Point(20, 6), new Point(23, 6), new Point(26, 6), new Point(29, 6), new Point(1, 7), new Point(2, 7), new Point(6, 7),
				new Point(7, 7), new Point(11, 7), new Point(12, 7), new Point(15, 7), new Point(16, 7), new Point(17, 7), new Point(20, 7), new Point(21, 7),
				new Point(22, 7), new Point(26, 7), new Point(29, 7), new Point(30, 7), new Point(31, 7),
			};

			var bubbleColor = new LightColor(0, 1, new List<int> { 0x151407, 0x11110e });

			var backgroundBrush = new SolidColorBrush(Color.FromRgb(2, 1, 1));
			var polygonFill1 = new SolidColorBrush(Color.FromRgb(0, 3, 4));
			var polygonFill2 = new SolidColorBrush(Color.FromRgb(3, 3, 1));
			var polygonStroke1 = new SolidColorBrush(Color.FromRgb(5, 19, 43));
			var polygonStroke2 = new SolidColorBrush(Color.FromRgb(23, 23, 23));

			backgroundBrush.Freeze();
			polygonFill1.Freeze();
			polygonFill2.Freeze();
			polygonStroke1.Freeze();
			polygonStroke2.Freeze();

			var segment = new Segment();
			var center = new Point(48, 48);

			var zigZagTimes = new List<Tuple<int, double>>
			{
				Tuple.Create(0, 1d),
				Tuple.Create(200, 0.67741935483871d),
				Tuple.Create(300, 0.838709677419355d),
				Tuple.Create(500, 0.67741935483871d),
				Tuple.Create(600, 0.67741935483871d),
				Tuple.Create(700, 0.67741935483871d),
			};

			zigZagTimes = Enumerable.Range(0, 20).SelectMany(x => zigZagTimes.Select(y => Tuple.Create(y.Item1 + x * 800, y.Item2))).ToList();

			var bubbleTimes = new List<Tuple<int, Point>>
			{
				Tuple.Create(3200, center),
				Tuple.Create(3500, center),
			};

			var onZigZagIndex = -1;
			var onBubbleIndex = 0;
			var amp = 0d;
			for (var time = 0; time < 16000; time += 20)
			{
				if ((onZigZagIndex + 1 < zigZagTimes.Count) && (time >= zigZagTimes[onZigZagIndex + 1].Item1))
				{
					++onZigZagIndex;
					amp = zigZagTimes[onZigZagIndex].Item2 * MaxDist;
				}
				else
					amp = Math.Max(0, amp - Fade);

				var canvas = new Canvas { Width = 97, Height = 97 };

				var background = new Rectangle { Width = 97, Height = 97, Fill = backgroundBrush };
				Canvas.SetLeft(background, 0);
				Canvas.SetTop(background, 0);
				canvas.Children.Add(background);

				{
					var points = new List<Point>();
					for (var point = 0; point < NumPoints; ++point)
					{
						var angle = point * 360d / NumPoints / 180d * Math.PI;
						var direction = new Vector(Math.Sin(angle), -Math.Cos(angle));
						points.Add(center + direction * (Size1 + amp));
						amp = -amp;
					}

					var polygon = new Polygon { Points = new PointCollection(points), Fill = polygonFill1, Stroke = polygonStroke1, StrokeThickness = LineWidth };
					canvas.Children.Add(polygon);
				}
				{
					var points = new List<Point>();
					for (var point = 0; point < NumPoints; ++point)
					{
						var angle = point * 360d / NumPoints / 180d * Math.PI;
						var direction = new Vector(Math.Sin(angle), -Math.Cos(angle));
						points.Add(center + direction * (Size2 + amp) * Ratio2);
						amp = -amp;
					}

					var polygon = new Polygon { Points = new PointCollection(points), Fill = polygonFill2, Stroke = polygonStroke2, StrokeThickness = LineWidth };
					canvas.Children.Add(polygon);
				}

				ShapeChangeRender(canvas, segment, bodyLayout, time);

				if (onBubbleIndex < bubbleTimes.Count)
				{
					var bubbleTime = time - bubbleTimes[onBubbleIndex].Item1;
					if (bubbleTime >= 0)
					{
						var point = bubbleTimes[onBubbleIndex].Item2;
						var useDist = bubbleTime * (135.764501987817 + BubbleWidth + BubbleBorder * 2) / TotalBubbleTime;
						foreach (var light in bodyLayout.GetAllLights())
						{
							var pos = bodyLayout.GetLightPosition(light);
							var dist = (pos - point).Length;
							if ((dist > useDist - BubbleWidth - BubbleBorder * 2) && (dist < useDist))
								segment.AddLight(light, time, bubbleColor, 1);
							if ((dist > useDist - BubbleWidth - BubbleBorder) && (dist < useDist - BubbleBorder))
								segment.AddLight(light, time, bubbleColor, 0);
						}
					}
					if (bubbleTime >= TotalBubbleTime)
						++onBubbleIndex;
				}
			}

			foreach (var point in goodbyeLights)
			{
				var light = headerLayout.GetPositionLight(point);
				foreach (var bubbleTime in bubbleTimes)
					segment.AddLight(light, bubbleTime.Item1, bubbleTime.Item1 + TotalBubbleTime, bubbleColor, 0, 0x000000);
			}
			return segment;
		}

		Segment Ocean()
		{
			const double Height = 20;
			const double WaveThickness = 5;
			const double WaveLocation = 48;
			const double WaveTime = 800;
			const double Rotate = 20;
			const double Translate = -48;
			const double Spacing = 0.8;
			const int MinSparkleTime = 15;
			const int MaxSparkleTime = 45;
			const int FlashTime = 5;

			var backgroundBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
			var waveFill = new SolidColorBrush(Color.FromRgb(0, 255, 0));
			var waveStroke = new SolidColorBrush(Color.FromRgb(0, 0, 255));

			backgroundBrush.Freeze();
			waveFill.Freeze();
			waveStroke.Freeze();

			var rand = new Random(0xfeed);

			var sparkleTimes = new HashSet<int> { 3600, 10000, 13200 };

			var flashTimes = new List<int> { 14400, 14600, 14800, 15000 };
			var flashColor = new LightColor(900, 6788, new List<int> { 0x101010, 0x101000 });

			var segment = new Segment();

			var canvas = new Canvas { Width = 97, Height = 97 };

			var background = new Rectangle { Width = 289, Height = 289, Fill = backgroundBrush };
			Canvas.SetLeft(background, -96);
			Canvas.SetTop(background, -96);
			canvas.Children.Add(background);

			var points = new List<Point>();
			for (var x = -96; x <= 193; ++x)
			{
				var angle = x / 97d * 360d * Spacing / 180d * Math.PI;
				var height = WaveLocation - Math.Cos(angle) * Height / 2;
				points.Add(new Point(x, 97 - height));
			}
			points.Add(new Point(193, 193));
			points.Add(new Point(0, 193));
			points.Add(points[0]);
			var wave = new Polygon { Points = new PointCollection(points), Fill = waveFill, Stroke = waveStroke, StrokeThickness = WaveThickness };
			canvas.Children.Add(wave);

			var rotateTransform = new RotateTransform { CenterX = 97d / 2, CenterY = 97d / 2 };
			var translateTransform = new TranslateTransform();

			var transformGroup = new TransformGroup();
			transformGroup.Children.Add(translateTransform);
			transformGroup.Children.Add(rotateTransform);

			canvas.RenderTransform = transformGroup;

			var sparkleLights = new Dictionary<int, int>();

			int SubstituteColor(int light, int color)
			{
				var r = (color >> 16) & 255;
				var g = (color >> 8) & 255;
				var b = (color >> 0) & 255;

				var sparkling = sparkleLights.ContainsKey(light);

				if ((r > g) && (r > b))
					return sparkling ? 0x101000 : 0x00000c;
				if (g > b)
					return sparkling ? 0x0f0a00 : 0x030f0f;
				return sparkling ? 0x100f09 : 0x100f0d;
			}

			int flashTime = 0;
			for (var time = 0; time < 16000; time += 20)
			{
				if (sparkleTimes.Contains(time))
					sparkleLights = bodyLayout.GetAllLights().ToDictionary(light => light, light => rand.Next(MinSparkleTime, MaxSparkleTime));

				var dist = Math.Cos(time % WaveTime / WaveTime * 360d / 180d * Math.PI);
				rotateTransform.Angle = dist * Rotate;
				translateTransform.X = dist * Translate;
				ShapeChangeRender(canvas, segment, bodyLayout, time, SubstituteColor);

				if (flashTimes.Contains(time))
					flashTime = FlashTime;
				if (flashTime > 0)
				{
					var center = new Point(48, 48);
					foreach (var light in bodyLayout.GetAllLights())
					{
						var pos = bodyLayout.GetLightPosition(light);
						segment.AddLight(light, time, flashColor, ((pos - center).Length * 100).Round());
					}
					--flashTime;
				}

				sparkleLights = sparkleLights.Where(pair => pair.Value > 0).ToDictionary(pair => pair.Key, pair => pair.Value - 1);
			}

			foreach (var light in bodyLayout.GetAllLights().Concat(headerLayout.GetAllLights()))
				segment.AddLight(light, 15200, 15400, 0x101010, 0x000000);

			var qmPoints = new List<Point>
			{
				new Point(5, 0), new Point(6, 0), new Point(7, 0), new Point(8, 0), new Point(9, 0), new Point(13, 0), new Point(14, 0), new Point(15, 0),
				new Point(16, 0), new Point(17, 0), new Point(21, 0), new Point(22, 0), new Point(23, 0), new Point(24, 0), new Point(25, 0), new Point(4, 1),
				new Point(5, 1), new Point(9, 1), new Point(10, 1), new Point(12, 1), new Point(13, 1), new Point(17, 1), new Point(18, 1), new Point(20, 1),
				new Point(21, 1), new Point(25, 1), new Point(26, 1), new Point(9, 2), new Point(10, 2), new Point(17, 2), new Point(18, 2), new Point(25, 2),
				new Point(26, 2), new Point(8, 3), new Point(9, 3), new Point(16, 3), new Point(17, 3), new Point(24, 3), new Point(25, 3), new Point(7, 4),
				new Point(8, 4), new Point(15, 4), new Point(16, 4), new Point(23, 4), new Point(24, 4), new Point(7, 5), new Point(8, 5), new Point(15, 5),
				new Point(16, 5), new Point(23, 5), new Point(24, 5), new Point(7, 7), new Point(8, 7), new Point(15, 7), new Point(16, 7), new Point(23, 7),
				new Point(24, 7),
			};

			foreach (var point in qmPoints)
			{
				var light = headerLayout.GetPositionLight(point);
				segment.AddLight(light, 15200, 0x101010);
			}

			return segment;
		}

		public override Song Render()
		{
			var song = new Song("soberup.ogg"); // First sound is at 1000; Measures start at 1000, repeat every 2580, and stop at 217720. Beats appear quantized to 2580/16 = 161.25

			// HelloHello (1000)
			var helloHello = HelloHello();
			song.AddSegment(helloHello, 0, 16000, 1000, 25800);
			song.AddPaletteChange(1000, 0);
			song.AddPaletteChange(25510, 26800, 1);
			song.AddPaletteChange(26800, 0);

			// Waves (26800)
			var waves = Waves();
			song.AddSegment(waves, 0, 16000, 26800, 25800);
			song.AddPaletteChange(26800, 0);
			song.AddPaletteChange(26800, 28090, 1);
			song.AddPaletteChange(52600, 0);

			// Goodbye (52600)
			var goodbye = Goodbye();
			song.AddSegment(goodbye, 0, 16000, 52600, 25800);

			// Ocean (78400)
			var ocean = Ocean();
			song.AddSegment(ocean, 0, 16000, 78400, 25800);

			// Next (182600)

			// End (217720)

			return song;
		}
	}
}
