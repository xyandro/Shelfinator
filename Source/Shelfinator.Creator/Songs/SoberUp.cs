using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
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

				sparkleLights = sparkleLights.Where(pair => pair.Value > 0).ToDictionary(pair => pair.Key, pair => pair.Value - 1);

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

			// Next (52600)

			// End (217720)

			return song;
		}
	}
}
