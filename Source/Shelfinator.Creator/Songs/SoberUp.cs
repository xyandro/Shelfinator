using System;
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
		public override int SongNumber => 8;

		readonly Layout headerLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Header.png");
		readonly Layout bodyLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");

		static List<Point> HelloLights = new List<Point>
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

		static List<Point> QMPoints = new List<Point>
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

		Segment HelloHello()
		{
			const int BeatBorder1 = 2;
			const int BeatBorder2 = 6;
			const double Fade = 0.9;
			const int TotalBubbleTime = 300;
			const double BubbleWidth = 10;
			const double BubbleBorder = 2;

			var segment = new Segment();

			var beatTimes = new List<Tuple<int, double>>
			{
				Tuple.Create(0, 1.0),
				Tuple.Create(200, 0.6774193548387099),
				Tuple.Create(300, 0.8387096774193538),
				Tuple.Create(500, 0.6774193548387099),
				Tuple.Create(600, 0.6774193548387099),
				Tuple.Create(700, 0.6774193548387099),
			};

			beatTimes = Enumerable.Range(0, 20).SelectMany(x => beatTimes.Select(y => Tuple.Create(y.Item1 + x * 800, y.Item2))).ToList();
			var bubbleTimes = new List<Tuple<int, Point>>
			{
				Tuple.Create(3200, new Point(0, 0)),
				Tuple.Create(3500, new Point(0, 0)),
				Tuple.Create(9600, new Point(96, 96)),
				Tuple.Create(9900, new Point(96, 96)),
			};

			var borderColor = new LightColor(new List<List<int>> { new List<int> { 0x020101 }, new List<int> { 0x00000c } });
			var bodyColor = new LightColor(new List<List<int>> { new List<int> { 0x040301 }, new List<int> { 0x00000c } });
			var beatColor = new LightColor(0, 1, new List<List<int>> { new List<int> { 0x000010, 0x040e20 }, new List<int> { 0x00000c } });
			var bubbleColor = new LightColor(0, 1, new List<int> { 0x201e0b, 0x1a1a15 });

			var onBeatIndex = -1;
			var onBubbleIndex = 0;
			double amp = 0;
			for (var time = 0; time < 16000; time += 20)
			{
				bodyLayout.GetPositionLights(0, 0, 97, 97).ForEach(light => segment.AddLight(light, time, bodyColor, 0));

				if ((onBeatIndex + 1 < beatTimes.Count) && (time >= beatTimes[onBeatIndex + 1].Item1))
				{
					++onBeatIndex;
					amp = beatTimes[onBeatIndex].Item2;
				}
				else
					amp *= Fade;

				for (var ctr = 0; ctr < 3; ++ctr)
				{
					var x = ctr * 19;
					var y = 95 - ctr * 19;
					var size = ((97 - ctr * 38) * amp).Round();
					var beatBorder2 = BeatBorder2 - ctr * 2;

					foreach (var light in bodyLayout.GetPositionLights(x, y - size + 2, 2, size))
						segment.AddLight(light, time, beatColor, 0);
					foreach (var light in bodyLayout.GetPositionLights(x, y, size, 2))
						segment.AddLight(light, time, beatColor, 0);
					foreach (var light in bodyLayout.GetPositionLights(y, x, 2, size))
						segment.AddLight(light, time, beatColor, 0);
					foreach (var light in bodyLayout.GetPositionLights(y - size + 2, x, size, 2))
						segment.AddLight(light, time, beatColor, 0);

					foreach (var light in bodyLayout.GetPositionLights(x, y - size + 2 + BeatBorder1, 2, beatBorder2))
						segment.AddLight(light, time, beatColor, 1);
					foreach (var light in bodyLayout.GetPositionLights(x + size - BeatBorder1 - beatBorder2, y, beatBorder2, 2))
						segment.AddLight(light, time, beatColor, 1);
					foreach (var light in bodyLayout.GetPositionLights(y, x + size - BeatBorder1 - beatBorder2, 2, beatBorder2))
						segment.AddLight(light, time, beatColor, 1);
					foreach (var light in bodyLayout.GetPositionLights(y - size + 2 + BeatBorder1, x, beatBorder2, 2))
						segment.AddLight(light, time, beatColor, 1);
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
			}

			foreach (var point in HelloLights)
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
			const double Amplitude = 10;
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
				 new LightColor(0, 4, new List<List<int>> { new List<int> { 0x00000c }, new List<int> { 0x00000c, 0x100f0d, 0x030f0f, 0x100f0d, 0x00000c }, new List<int> { 0x200b0c, 0x080004, 0x101010, 0x080004, 0x200b0c } }),
				 new LightColor(0, 4, new List<List<int>> { new List<int> { 0x00000c }, new List<int> { 0x101000, 0x100f09, 0x0f0a00, 0x100f09, 0x101000 }, new List<int> { 0x101000, 0x100f09, 0x0f0a00, 0x100f09, 0x101000 }}),
			};
			int flashTime = 0;
			for (var time = 0; time < 16000; time += 20)
			{
				segment.Clear(time);

				bodyLayout.GetAllLights().ForEach(light => segment.AddLight(light, time, 0x00000c));

				if (sparkleTimes.Contains(time))
					sparkleLights = bodyLayout.GetAllLights().ToDictionary(light => light, light => rand.Next(MinSparkleTime, MaxSparkleTime));

				var startAngle = -(time % 800) * 360 / 800;

				for (var y = 0; y < 97; ++y)
				{
					var x = new List<int> { 0 };
					var angle = (Start1 + startAngle + 360 * NumWaves * (y / 96d)) / 180 * Math.PI;
					x.Add((X1 + Math.Sin(angle) * Amplitude - (Width - 1) / 2).Round());
					x.Add(x.Last() + Width);

					angle = (Start2 + startAngle + 360 * NumWaves * (y / 96d)) / 180 * Math.PI;
					x.Add((X2 + Math.Sin(angle) * Amplitude - (Width - 1) / 2).Round());
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

			foreach (var point in QMPoints)
			{
				var light = headerLayout.GetPositionLight(point);
				segment.AddLight(light, 15200, 0x101010);
			}

			return segment;
		}

		void ShapeChangeRender(Canvas canvas, Segment segment, Layout layout, int time, Func<int, int, int?> substituteColor = null)
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
					{
						var color = substituteColor(light, value);
						if (color.HasValue)
							segment.AddLight(light, time, color.Value);
					}
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

			var bubbleColor = new LightColor(0, 1, new List<int> { 0x201e0b, 0x1a1a15 });

			var backgroundBrush = new SolidColorBrush(Color.FromRgb(2, 1, 1));
			var polygonFill1 = new SolidColorBrush(Color.FromRgb(0, 12, 16));
			var polygonFill2 = new SolidColorBrush(Color.FromRgb(16, 16, 5));
			var polygonStroke1 = new SolidColorBrush(Color.FromRgb(5, 19, 43));
			var polygonStroke2 = new SolidColorBrush(Color.FromRgb(32, 32, 32));

			backgroundBrush.Freeze();
			polygonFill1.Freeze();
			polygonFill2.Freeze();
			polygonStroke1.Freeze();
			polygonStroke2.Freeze();

			var segment = new Segment();
			var center = new Point(48, 48);

			var beatTimes = new List<Tuple<int, double>>
			{
				Tuple.Create(0, 1d),
				Tuple.Create(200, 0.67741935483871d),
				Tuple.Create(300, 0.838709677419355d),
				Tuple.Create(500, 0.67741935483871d),
				Tuple.Create(600, 0.67741935483871d),
				Tuple.Create(700, 0.67741935483871d),
			};

			beatTimes = Enumerable.Range(0, 20).SelectMany(x => beatTimes.Select(y => Tuple.Create(y.Item1 + x * 800, y.Item2))).ToList();

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
				if ((onZigZagIndex + 1 < beatTimes.Count) && (time >= beatTimes[onZigZagIndex + 1].Item1))
				{
					++onZigZagIndex;
					amp = beatTimes[onZigZagIndex].Item2 * MaxDist;
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

			var sparkleLights = bodyLayout.GetAllLights().ToDictionary(light => light, light => Tuple.Create(0, int.MaxValue));

			int? SubstituteColor(int light, int color)
			{
				var r = (color >> 16) & 255;
				var g = (color >> 8) & 255;
				var b = (color >> 0) & 255;

				var sparkle = sparkleLights[light].Item1;

				if ((r > g) && (r > b))
				{
					switch (sparkle)
					{
						case 0: return 0x00000c;
						case 2: return 0x100008;
						case 4: return 0x100008;
						case 6: return 0x00000c;
						case 1: case 3: case 5: return 0x101000;
					}
				}
				if (g <= b)
				{
					switch (sparkle)
					{
						case 0: return 0x100f0d;
						case 2: return 0x150720;
						case 4: return 0x150720;
						case 6: return 0x100f0d;
						case 1: case 3: case 5: return 0x100f09;
					}
				}
				switch (sparkle)
				{
					case 0: return 0x030f0f;
					case 2: return 0x000510;
					case 4: return 0x000510;
					case 6: return 0x030f0f;
					case 1: case 3: case 5: return 0x0f0a00;
				}

				throw new Exception("Weird stuff happened");
			}

			int flashTime = 0;
			for (var time = 0; time < 16000; time += 20)
			{
				if (sparkleTimes.Contains(time))
					sparkleLights = sparkleLights.ToDictionary(pair => pair.Key, pair => Tuple.Create(pair.Value.Item1 + 1, rand.Next(MinSparkleTime, MaxSparkleTime)));

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

				sparkleLights = sparkleLights.ToDictionary(pair => pair.Key, pair => pair.Value.Item2 > 0 ? Tuple.Create(pair.Value.Item1, pair.Value.Item2 - 1) : Tuple.Create(pair.Value.Item1 + 1, int.MaxValue));
			}

			foreach (var light in bodyLayout.GetAllLights().Concat(headerLayout.GetAllLights()))
				segment.AddLight(light, 15200, 15400, 0x101010, 0x000000);

			foreach (var point in QMPoints)
			{
				var light = headerLayout.GetPositionLight(point);
				segment.AddLight(light, 15200, 0x101010);
			}

			return segment;
		}

		Segment Lines()
		{
			var lineTimes = new List<int>
			{
				0, 200, 300, 500, 600, 700,
				800, 1000, 1100, 1300, 1400, 1500,
				1600, 1800, 1900, 2100, 2200, 2300,
				2400, 2600, 2700, 2900, 3000, 3100,
				3200, 3400, 3500, 3700, 3800, 3900,
				4000, 4200, 4300, 4500, 4600, 4700,
				4800, 5000, 5100, 5300, 5400, 5500,
				5600, 5800, 5800, 6000, 6000, 6200,
			};

			var segment = new Segment();

			var corner = new Point(0, 0);
			var color = new LightColor(0, 13576, new List<int> { 0x101010, 0x000020 }, new List<int> { 0x002020, 0x000020 });

			for (var pass = 0; pass < 2; ++pass)
			{
				for (var ctr = 0; ctr < 6; ++ctr)
				{
					var time = lineTimes[ctr + 0 + 24 * pass];
					foreach (var light in bodyLayout.GetPositionLights(ctr * 19, 0, 2, 97))
					{
						var value = ((bodyLayout.GetLightPosition(light) - corner).Length * 100).Round();
						segment.AddLight(light, time, color, value);
					}
				}

				for (var ctr = 0; ctr < 6; ++ctr)
				{
					var time = lineTimes[ctr + 6 + 24 * pass];
					foreach (var light in bodyLayout.GetPositionLights(0, ctr * 19, 97, 2))
					{
						var value = ((bodyLayout.GetLightPosition(light) - corner).Length * 100).Round();
						segment.AddLight(light, time, color, value);
					}
				}

				for (var ctr = 0; ctr < 6; ++ctr)
				{
					var time = lineTimes[ctr + 12 + 24 * pass];
					for (var ctr2 = 2; ctr2 < 97; ctr2 += 19)
						foreach (var light in bodyLayout.GetPositionLights(ctr * 19, ctr2, 2, 17))
							segment.AddLight(light, time, 0x000000);
				}

				for (var ctr = 0; ctr < 6; ++ctr)
				{
					var time = lineTimes[ctr + 18 + 24 * pass];
					foreach (var light in bodyLayout.GetPositionLights(0, ctr * 19, 97, 2))
						segment.AddLight(light, time, 0x000000);
				}
			}

			foreach (var point in QMPoints)
			{
				var light = headerLayout.GetPositionLight(point);
				segment.AddLight(light, 2600, 0x101010);
				segment.AddLight(light, 3200, 0x000000);
			}

			return segment;
		}

		Segment FavoriteColor()
		{
			const double XFrequency = 50;
			const double YFrequency = 20;
			const double XAmplitude = 3;
			const double YAmplitude = .75;
			const double FadeSpeed = 1200;
			const int ShrinkStartTime = 4200;
			const double ShrinkLength = 2000;

			var segment = new Segment();

			var squares = new List<Tuple<Point, Size, List<int>>>
			{
				Tuple.Create(new Point(-37, -37), new Size(171, 171), new List<int> { 0x000000, 0x000000, 0x000000, 0x000000 }),
				Tuple.Create(new Point(-18, -18), new Size(133, 133), new List<int> { 0x000008, 0x000011, 0x000606, 0x000c0c }),
				Tuple.Create(new Point(1, 1), new Size(95, 95), new List<int> { 0x00090e, 0x00131c, 0x060006, 0x0c000c }),
				Tuple.Create(new Point(20, 20), new Size(57, 57), new List<int> { 0x000008, 0x000011, 0x060600, 0x0c0c00 }),
				Tuple.Create(new Point(39, 39), new Size(19, 19), new List<int> { 0x00090e, 0x00131c, 0x000606, 0x000c0c }),
			};

			void DrawSquares(int time, int startSquare, int stopSquare, int useColor)
			{
				segment.Clear(time);
				for (var ctr = startSquare; ctr <= stopSquare; ++ctr)
					bodyLayout.GetPositionLights(squares[ctr].Item1, squares[ctr].Item2).ForEach(light => segment.AddLight(light, time, squares[ctr].Item3[useColor]));
			}

			DrawSquares(0, 4, 4, 0);
			DrawSquares(200, 3, 4, 0);
			DrawSquares(400, 2, 4, 0);
			DrawSquares(600, 1, 4, 0);
			DrawSquares(800, 0, 4, 1);

			for (var beat = 1200; beat < 1600 + FadeSpeed; beat += 20)
			{
				var brightness = 1 - (beat - 1600) / FadeSpeed;
				var xPositionOffset = Math.Sin(beat / 800d * XFrequency * Math.PI) * XAmplitude;
				var yPositionOffset = Math.Sin(beat / 800d * YFrequency * Math.PI) * YAmplitude;
				squares.ForEach(square => bodyLayout.GetPositionLights(square.Item1 + new Vector(xPositionOffset, yPositionOffset), square.Item2).ForEach(light => segment.AddLight(light, beat, Helpers.MultiplyColor(square.Item3[1], brightness))));
			}

			DrawSquares(3200, 4, 4, 2);
			DrawSquares(3400, 3, 4, 2);
			DrawSquares(3600, 2, 4, 2);
			DrawSquares(3800, 1, 4, 2);
			DrawSquares(4000, 0, 4, 3);

			var centerOffset = new Vector(-48, -48);
			for (var beat = 0; beat < ShrinkLength; beat += 20)
			{
				segment.Clear(ShrinkStartTime + beat);
				var scale = 1 - beat / ShrinkLength;
				foreach (var square in squares)
				{
					var x = ((square.Item1.X - 48) * scale + 48).Round();
					var width = 97 - x * 2;
					bodyLayout.GetPositionLights(x, x, width, width).ForEach(light => segment.AddLight(light, ShrinkStartTime + beat, square.Item3[3]));
				}
			}

			return segment;
		}

		enum Phase { Start, Vert, Horiz };

		Segment Saws()
		{
			const int TotalBubbleTime = 300;
			const double BubbleWidth = 10;
			const double BubbleBorder = 2;
			const double SawThickness = 5;
			const double Fade = .8;
			const int Rotations = 2;
			const int SawBladeSize = 30;
			const int SawBladeEdgeSize = 2;
			const int SawBladePoints = 20;
			const int FinalRotations = 2;
			const int FlashTime = 5;
			const int MinSparkleTime = 15;
			const int MaxSparkleTime = 45;

			var segment = new Segment();

			var zigZagTimes = new List<Tuple<int, double>>
			{
				Tuple.Create(0, 8.2),
				Tuple.Create(200, 5.55483870967742),
				Tuple.Create(300, 6.8774193548387),
				Tuple.Create(500, 5.55483870967742),
				Tuple.Create(600, 5.55483870967742),
				Tuple.Create(700, 5.55483870967742),
			};

			zigZagTimes = Enumerable.Range(0, 24).SelectMany(x => zigZagTimes.Select(y => Tuple.Create(y.Item1 + x * 800, y.Item2))).ToList();
			var bubbleTimes = new List<Tuple<int, Phase, List<Point>>>
			{
				Tuple.Create(0, Phase.Vert, new List<Point> { new Point(0, 48), new Point(96, 48) }),
				Tuple.Create(300, Phase.Vert, new List<Point> { new Point(0, 48), new Point(96, 48) }),
				Tuple.Create(6400, Phase.Horiz, new List<Point> { new Point(48, 0), new Point(48, 96) }),
				Tuple.Create(6700, Phase.Horiz, new List<Point> { new Point(48, 0), new Point(48, 96) }),
			};

			var flashTimes = new List<int> { 17600, 17800, 18000, 18200 };
			var flashColor = new LightColor(900, 6788, new List<int> { 0x101010, 0x101000 });

			var sparkleTimes = new HashSet<int> { 13200, 16400 };
			var sparkleLights = new Dictionary<int, int>();

			var borderColor = new LightColor(new List<List<int>> { new List<int> { 0x100d04 }, new List<int> { 0x00000c } });
			var bodyColor = 0x020101;
			var bubbleColor = new LightColor(0, 1, new List<int> { 0x151407, 0x11110e });

			var rand = new Random(0xf0dface);

			int GetColor(int light, int color)
			{
				var r = (color >> 16) & 255;
				var g = (color >> 8) & 255;
				var b = (color >> 0) & 255;

				var sparkling = sparkleLights.ContainsKey(light);

				if ((r > g) && (r > b))
					return sparkling ? 0x101000 : 0x00000c;
				if (g > b)
					return sparkling ? 0x0f0a00 : 0x030f0f;
				return sparkling ? 0x100f09 : 0x201e1a;
			}


			var sawFill = new SolidColorBrush(Color.FromRgb(0, 255, 0));
			var sawStroke = new SolidColorBrush(Color.FromRgb(0, 0, 255));
			var onZigZagIndex = -1;
			var onBubbleIndex = 0;
			int flashTime = 0;
			var phase = bodyLayout.GetAllLights().ToDictionary(light => light, light => Phase.Start);
			double amp = 0;
			for (var time = 0; time < 19200; time += 20)
			{
				if (sparkleTimes.Contains(time))
					sparkleLights = bodyLayout.GetAllLights().ToDictionary(light => light, light => rand.Next(MinSparkleTime, MaxSparkleTime));

				bodyLayout.GetAllLights().ForEach(light => segment.AddLight(light, time, GetColor(light, bodyColor)));

				if ((onZigZagIndex + 1 < zigZagTimes.Count) && (time >= zigZagTimes[onZigZagIndex + 1].Item1))
				{
					++onZigZagIndex;
					amp = zigZagTimes[onZigZagIndex].Item2;
				}
				else
					amp = amp - Fade;

				var canvas = new Canvas { Width = 97, Height = 97 };
				var points = new List<Point>();
				for (var point = 0; point < SawBladePoints; ++point)
				{
					var angle = (double)point / SawBladePoints * 2 * Math.PI;
					var dist = SawBladeSize + (point % 2 * 2 - 1) * (SawBladeEdgeSize + amp);
					points.Add(new Point(dist * Math.Sin(angle) + 48, dist * Math.Cos(angle) + 48));
				}
				points.Add(points[0]);
				var saw = new Polygon { Points = new PointCollection(points), Fill = sawFill, Stroke = sawStroke, StrokeThickness = SawThickness };
				canvas.Children.Add(saw);

				var bladeSpinTransform = new RotateTransform { CenterX = 97d / 2, CenterY = 97d / 2 };
				var translateTransform = new TranslateTransform();
				var rotateTransform = new RotateTransform { CenterX = 97d / 2, CenterY = 97d / 2 };

				var transformGroup = new TransformGroup();
				transformGroup.Children.Add(bladeSpinTransform);
				transformGroup.Children.Add(translateTransform);
				transformGroup.Children.Add(rotateTransform);

				canvas.RenderTransform = transformGroup;

				rotateTransform.Angle = time < 12800 ? 0d : (time - 12800d) / 6400 * FinalRotations * 360;

				bladeSpinTransform.Angle = (double)time / 800 / Rotations * 360;

				var used = new HashSet<int>();
				int? SubstituteColor(Phase inPhase, int light, int color)
				{
					if ((color == 0x000000) || (phase[light] != inPhase) || (used.Contains(light)))
						return default(int?);
					used.Add(light);

					return GetColor(light, color);
				}

				if (phase.Values.Any(x => x == Phase.Vert))
				{
					translateTransform.X = 0;
					translateTransform.Y = -48;
					ShapeChangeRender(canvas, segment, bodyLayout, time, (light, color) => SubstituteColor(Phase.Vert, light, color));
					translateTransform.Y = 48;
					ShapeChangeRender(canvas, segment, bodyLayout, time, (light, color) => SubstituteColor(Phase.Vert, light, color));
				}
				if (phase.Values.Any(x => x == Phase.Horiz))
				{
					translateTransform.X = -48;
					translateTransform.Y = 0;
					ShapeChangeRender(canvas, segment, bodyLayout, time, (light, color) => SubstituteColor(Phase.Horiz, light, color));
					translateTransform.X = 48;
					ShapeChangeRender(canvas, segment, bodyLayout, time, (light, color) => SubstituteColor(Phase.Horiz, light, color));
				}
				if (onBubbleIndex < bubbleTimes.Count)
				{
					var bubbleTime = time - bubbleTimes[onBubbleIndex].Item1;
					if (bubbleTime >= 0)
					{
						foreach (var point in bubbleTimes[onBubbleIndex].Item3)
						{
							var useDist = bubbleTime * (135.764501987817 + BubbleWidth + BubbleBorder * 2) / TotalBubbleTime;
							foreach (var light in bodyLayout.GetAllLights())
							{
								var pos = bodyLayout.GetLightPosition(light);
								var dist = (pos - point).Length;
								if ((dist > useDist - BubbleWidth - BubbleBorder * 2) && (dist < useDist))
								{
									phase[light] = bubbleTimes[onBubbleIndex].Item2;
									segment.AddLight(light, time, bubbleColor, 1);
								}
								if ((dist > useDist - BubbleWidth - BubbleBorder) && (dist < useDist - BubbleBorder))
								{
									phase[light] = bubbleTimes[onBubbleIndex].Item2;
									segment.AddLight(light, time, bubbleColor, 0);
								}
							}
						}
					}
					if (bubbleTime >= TotalBubbleTime)
						++onBubbleIndex;
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

			bodyLayout.GetPositionLights(0, 0, 97, 97).Except(bodyLayout.GetPositionLights(1, 1, 95, 95)).ForEach(light => segment.AddLight(light, 0, borderColor, 0));

			foreach (var point in HelloLights)
			{
				var light = headerLayout.GetPositionLight(point);
				foreach (var bubbleTime in bubbleTimes)
					segment.AddLight(light, bubbleTime.Item1, bubbleTime.Item1 + TotalBubbleTime, bubbleColor, 0, 0x000000);
			}

			foreach (var light in bodyLayout.GetAllLights().Concat(headerLayout.GetAllLights()))
				segment.AddLight(light, 18400, 18600, 0x101010, 0x000000);

			foreach (var point in QMPoints)
			{
				var light = headerLayout.GetPositionLight(point);
				segment.AddLight(light, 18400, 0x101010);
			}

			return segment;
		}

		class Drop
		{
			public Point Center { get; set; }
			public int Time { get; set; }

			public Drop(Point center, int time)
			{
				Center = center;
				Time = time;
			}
		}

		class ChangeLight
		{
			public int StartTime { get; set; }
			public int EndTime { get; set; }
			public int StartColor { get; set; }
			public int EndColor { get; set; }

			public ChangeLight(int startTime, int endTime, int startColor, int endColor)
			{
				StartTime = startTime;
				EndTime = endTime;
				StartColor = startColor;
				EndColor = endColor;
			}
		}

		Segment Finale(out int time)
		{
			const double NumRipples = 5;
			const double RippleSize = 30;
			const double RippleTime = 20;
			const double AdjustTime = 250;
			const int MinSparkleTime = 100;
			const int MaxSparkleTime = 300;
			const int Background = 0x000808;

			var segment = new Segment();
			var drops = new List<Drop>
			{
				new Drop(new Point(48, 48), 0),
				new Drop(new Point(48, 10), 800),
				new Drop(new Point(48, 86), 1600),
				new Drop(new Point(86, 48), 2400),
				new Drop(new Point(10, 48), 3200),
				new Drop(new Point(67, 29), 4000),
				new Drop(new Point(29, 67), 4800),
				new Drop(new Point(29, 29), 5600),
				new Drop(new Point(67, 67), 6400),
				new Drop(new Point(10, 48), 7200),
				new Drop(new Point(48, 48), 8000),
			};

			var changeLights = new Dictionary<int, List<ChangeLight>>();
			void AddChangeLights(IEnumerable<int> lights, ChangeLight changeLight)
			{
				foreach (var light in lights)
				{
					if (!changeLights.ContainsKey(light))
						changeLights[light] = new List<ChangeLight>();
					changeLights[light].Add(changeLight);
				}
			}

			var favoriteColorTimes = new List<int> { 2400, 5600, 7200 };
			var favoriteColorColors = new List<List<int>>
			{
				new List<int> { 0x000208, 0x0a0310, 0x080004, 0x000208, 0x000510, 0x150720, 0x100008, 0x000510 },
				new List<int> { 0x030610, 0x100506, 0x080808, 0x030610, 0x060d20, 0x200b0c, 0x101010, 0x060d20 },
				new List<int> { 0x000008, 0x080808, 0x000008, 0x080808, 0x000011, 0x101010, 0x000011, 0x101010 },
			};
			for (var ctr = 0; ctr < favoriteColorTimes.Count; ++ctr)
			{
				AddChangeLights(bodyLayout.GetPositionLights(39, 39, 19, 19), new ChangeLight(favoriteColorTimes[ctr], favoriteColorTimes[ctr] + 800, favoriteColorColors[ctr][0], favoriteColorColors[ctr][0]));
				AddChangeLights(bodyLayout.GetPositionLights(20, 20, 57, 57).Except(bodyLayout.GetPositionLights(39, 39, 19, 19)), new ChangeLight(favoriteColorTimes[ctr] + 200, favoriteColorTimes[ctr] + 800, favoriteColorColors[ctr][1], favoriteColorColors[ctr][1]));
				AddChangeLights(bodyLayout.GetPositionLights(1, 1, 95, 95).Except(bodyLayout.GetPositionLights(20, 20, 57, 57)), new ChangeLight(favoriteColorTimes[ctr] + 400, favoriteColorTimes[ctr] + 800, favoriteColorColors[ctr][2], favoriteColorColors[ctr][2]));
				AddChangeLights(bodyLayout.GetPositionLights(0, 0, 97, 97).Except(bodyLayout.GetPositionLights(1, 1, 95, 95)), new ChangeLight(favoriteColorTimes[ctr] + 600, favoriteColorTimes[ctr] + 800, favoriteColorColors[ctr][3], favoriteColorColors[ctr][3]));

				AddChangeLights(bodyLayout.GetPositionLights(39, 39, 19, 19), new ChangeLight(favoriteColorTimes[ctr] + 800, favoriteColorTimes[ctr] + 1200, favoriteColorColors[ctr][4], Background));
				AddChangeLights(bodyLayout.GetPositionLights(20, 20, 57, 57).Except(bodyLayout.GetPositionLights(39, 39, 19, 19)), new ChangeLight(favoriteColorTimes[ctr] + 800, favoriteColorTimes[ctr] + 1200, favoriteColorColors[ctr][5], Background));
				AddChangeLights(bodyLayout.GetPositionLights(1, 1, 95, 95).Except(bodyLayout.GetPositionLights(20, 20, 57, 57)), new ChangeLight(favoriteColorTimes[ctr] + 800, favoriteColorTimes[ctr] + 1200, favoriteColorColors[ctr][6], Background));
				AddChangeLights(bodyLayout.GetPositionLights(0, 0, 97, 97).Except(bodyLayout.GetPositionLights(1, 1, 95, 95)), new ChangeLight(favoriteColorTimes[ctr] + 800, favoriteColorTimes[ctr] + 1200, favoriteColorColors[ctr][7], Background));
			}

			var rand = new Random(0xf0dface);
			var feelTimes = new List<int> { 1800, 5000, 6600, 8200 };
			foreach (var feelTime in feelTimes)
				foreach (var light in bodyLayout.GetAllLights())
					AddChangeLights(new List<int> { light }, new ChangeLight(feelTime, feelTime + rand.Next(MinSparkleTime, MaxSparkleTime), 0x101000, 0x101000));

			var beatTimes = new List<int> { 8800, 8900, 9000, 9100 };
			foreach (var beatTime in beatTimes)
				AddChangeLights(bodyLayout.GetAllLights(), new ChangeLight(beatTime, beatTime + 100, 0x101010, 0x000000));

			for (time = 0; time < 9600; time += 10)
			{
				foreach (var light in bodyLayout.GetAllLights())
				{
					var point = bodyLayout.GetLightPosition(light);
					var multiplier = 0d;
					foreach (var drop in drops)
					{
						var dist = (drop.Center - point).Length;
						var timeDist = (time - drop.Time + AdjustTime) / RippleTime - dist;
						if ((timeDist < 0) || (timeDist > RippleSize))
							continue;
						var size = (RippleSize - timeDist) / RippleSize;
						multiplier += Math.Sin(2 * Math.PI * timeDist / NumRipples) * size;
					}
					multiplier = 8d / Math.Max(1, Math.Min(multiplier * 8 + 8, 16));

					var useColor = Background;
					foreach (var changeLight in changeLights[light])
						if ((time >= changeLight.StartTime) && (time < changeLight.EndTime))
						{
							var colorPercent = ((double)time - changeLight.StartTime) / (changeLight.EndTime - changeLight.StartTime);
							useColor = Helpers.GradientColor(changeLight.StartColor, changeLight.EndColor, colorPercent);
						}

					segment.AddLight(light, time, Helpers.MultiplyColor(useColor, multiplier));
				}
			}

			foreach (var light in bodyLayout.GetAllLights().Concat(headerLayout.GetAllLights()))
				segment.AddLight(light, 9200, 9400, 0x101010, 0x000000);

			foreach (var point in QMPoints)
			{
				var light = headerLayout.GetPositionLight(point);
				segment.AddLight(light, 9200, 0x101010);
			}

			return segment;
		}

		public override Song Render()
		{
			var song = new Song("soberup.ogg"); // First sound is at 1000; Measures start at 1000, repeat every 2580, and stop at 217720. Beats appear quantized to 2580/16 = 161.25

			song.AddMeasure(1000); // Empty lead-in time
			song.AddMeasure(2580, 84); // Measures
									   // Beats appear quantized to 1/16

			// HelloHello (1)
			var helloHello = HelloHello();
			song.AddSegment(helloHello, 0, 16000, 1, 10);
			song.AddPaletteChange(1, 0);
			song.AddPaletteChange(10.5, 11, 1);
			song.AddPaletteChange(11, 0);

			// Waves (11)
			var waves = Waves();
			song.AddSegment(waves, 0, 16000, 11, 10);
			song.AddPaletteChange(11, 0);
			song.AddPaletteChange(11, 11.5, 1);
			song.AddPaletteChange(13.25, 2);
			song.AddPaletteChange(17.25, 2);
			song.AddPaletteChange(19.25, 1);
			song.AddPaletteChange(21, 0);

			// Goodbye (21)
			var goodbye = Goodbye();
			song.AddSegment(goodbye, 0, 16000, 21, 10);

			// Ocean (31)
			var ocean = Ocean();
			song.AddSegment(ocean, 0, 16000, 31, 10);
			song.AddPaletteChange(31, 0);
			song.AddPaletteChange(33.25, 1);
			song.AddPaletteChange(37.25, 2);
			song.AddPaletteChange(39.25, 3);
			song.AddPaletteChange(41, 0);

			// Lines (41)
			var lines = Lines();
			song.AddSegment(lines, 0, 6400, 41, 4);
			song.AddPaletteChange(41, 0);
			song.AddPaletteChange(43, 1);
			song.AddPaletteChange(45, 0);

			// FavoriteColor (45)
			var favoriteColor = FavoriteColor();
			song.AddSegment(favoriteColor, 0, 6400, 45, 8, 2);

			// Saws (61)
			var saws = Saws();
			song.AddSegment(saws, 0, 19200, 61, 12);

			// Finale (73)
			var finale = Finale(out var finaleTime);
			song.AddSegment(finale, 0, 9600, 73, 12);

			// End (85)

			return song;
		}
	}
}
