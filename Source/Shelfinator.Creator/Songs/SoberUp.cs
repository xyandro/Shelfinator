using System;
using System.Collections.Generic;
using System.Diagnostics;
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
			const int ZigZagBorder = 2;
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
				Tuple.Create(0, 3.1),
				Tuple.Create(200, 2.1),
				Tuple.Create(300, 2.6),
				Tuple.Create(500, 2.1),
				Tuple.Create(600, 2.1),
				Tuple.Create(700, 2.1),
			};

			zigZagTimes = Enumerable.Range(0, 20).SelectMany(x => zigZagTimes.Select(y => Tuple.Create(y.Item1 + x * 800, y.Item2))).ToList();
			var bubbleTimes = new List<Tuple<int, Point>>
			{
				Tuple.Create(3200, new Point(0, 0)),
				Tuple.Create(3500, new Point(0, 0)),
				Tuple.Create(9600, new Point(96, 96)),
				Tuple.Create(9900, new Point(96, 96)),
			};

			var borderColor = new LightColor(0x100d04);
			var bodyColor = new LightColor(0x040101);
			var zigZagColor = new LightColor(0, 1, new List<int> { 0x000c10, 0x020710 });
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

		public override Song Render()
		{
			var song = new Song("soberup.ogg"); // First sound is at 1000; Measures start at 1000, repeat every 2580, and stop at 217720. Beats appear quantized to 2580/16 = 161.25

			// HelloHello (1000)
			var helloHello = HelloHello();
			song.AddSegment(helloHello, 0, 16000, 1000, 25800);
			Emulator.TestPosition = 1000 + 2580 * 2;

			//// End (217720)

			return song;
		}
	}
}
