using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Working : ISong
	{
		public int SongNumber => 8;

		readonly Layout bodyLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");

		Segment Squares()
		{
			const int NumSquares = 6;
			const double MinRadius = 8.999;
			const double MaxRadius = 48.001;
			const double MaxVariance = 30;
			const double EvenSize = (MaxRadius - MinRadius) / NumSquares;

			var segment = new Segment();
			var color = new LightColor(0, NumSquares - 1, Helpers.Rainbow6);
			for (var angle = 0; angle < 360; ++angle)
			{
				segment.Clear(angle);
				var variance = Math.Pow(MaxVariance, Math.Sin(angle * Math.PI / 180));
				var multValue = Math.Pow(variance, 1d / (NumSquares - 1));
				var firstLen = (MaxRadius - MinRadius) / ((Math.Pow(multValue, NumSquares) - multValue) / (multValue - 1) + 1);
				foreach (var light in bodyLayout.GetAllLights())
				{
					var point = bodyLayout.GetLightPosition(light);
					var distance = Math.Max(Math.Abs(point.X - 48), Math.Abs(point.Y - 48));

					int square;
					if (double.IsNaN(firstLen))
						square = (int)Math.Floor((distance - MinRadius) / EvenSize);
					else
						square = (int)Math.Floor(Math.Log(((distance - MinRadius) / firstLen - 1) * (multValue - 1) / multValue + 1) / Math.Log(multValue) + 1);

					if ((square >= 0) && (square < NumSquares))
						segment.AddLight(light, angle, color, square);
				}
			}

			return segment;
		}

		int? GetSquare(Point point, bool horiz)
		{
			var x = point.X.Round();
			var y = point.Y.Round();
			if ((x < 0) || (y < 0) || (x > 96) || (y > 96))
				return null;
			if ((!horiz) && (x % 19 < 2) && (y % 19 < 2))
				return null;
			if ((x == 0) || (y == 0) || (x == 96) || (y == 96))
				return 0;
			return (y - 1) / 19 * 5 + (x - 1) / 19 + 1;
		}

		Segment Wavy()
		{
			const int TotalTime = 1000;
			var segment = new Segment();
			var center = new Point(48, 48);
			var color = new LightColor(0, 1000, Helpers.Rainbow6);
			var lines = new List<Tuple<Point, Size, int, int, bool>>
			{
				Tuple.Create(new Point(0, 0), new Size(1, 97), -80, 12, false),
				Tuple.Create(new Point(1, 0), new Size(1, 97), 63, 17, false),
				Tuple.Create(new Point(19, 0), new Size(1, 97), -76, 16, false),
				Tuple.Create(new Point(20, 0), new Size(1, 97), 72, 17, false),
				Tuple.Create(new Point(38, 0), new Size(1, 97), -80, 14, false),
				Tuple.Create(new Point(39, 0), new Size(1, 97), 78, 15, false),
				Tuple.Create(new Point(57, 0), new Size(1, 97), -71, 17, false),
				Tuple.Create(new Point(58, 0), new Size(1, 97), 81, 19, false),
				Tuple.Create(new Point(76, 0), new Size(1, 97), -80, 20, false),
				Tuple.Create(new Point(77, 0), new Size(1, 97), 73, 10, false),
				Tuple.Create(new Point(95, 0), new Size(1, 97), -92, 13, false),
				Tuple.Create(new Point(96, 0), new Size(1, 97), 71, 17, false),
				Tuple.Create(new Point(0, 0), new Size(97, 1), -84, 15, true),
				Tuple.Create(new Point(0, 1), new Size(97, 1), 80, 17, true),
				Tuple.Create(new Point(0, 19), new Size(97, 1), -85, 12, true),
				Tuple.Create(new Point(0, 20), new Size(97, 1), 95, 12, true),
				Tuple.Create(new Point(0, 38), new Size(97, 1), -91, 10, true),
				Tuple.Create(new Point(0, 39), new Size(97, 1), 88, 16, true),
				Tuple.Create(new Point(0, 57), new Size(97, 1), -82, 15, true),
				Tuple.Create(new Point(0, 58), new Size(97, 1), 82, 19, true),
				Tuple.Create(new Point(0, 76), new Size(97, 1), -72, 17, true),
				Tuple.Create(new Point(0, 77), new Size(97, 1), 72, 11, true),
				Tuple.Create(new Point(0, 95), new Size(97, 1), -88, 13, true),
				Tuple.Create(new Point(0, 96), new Size(97, 1), 63, 14, true),
			};
			var colors = new List<int> { 0x101010, 0x100000, 0x100800, 0x101000, 0x001000, 0x000010, 0x050008, 0x09000d };
			var squareColor = "0/1&2&6/3&7&11/4&8&12&16/5&9&13&17&21/10&14&18&22/15&19&23/20&24&25".Split('/').SelectMany((l, index) => l.Split('&').Select(s => new { square = int.Parse(s), color = colors[index] })).ToDictionary(obj => obj.square, obj => obj.color);
			for (var time = 0; time < TotalTime; ++time)
			{
				segment.Clear(time);
				foreach (var line in lines)
				{
					var amplitude = line.Item3 * Math.Pow(Math.E, -Math.Pow(time * 2.4 / TotalTime, 2d)) * Math.Cos(line.Item4 * 2d * Math.PI * time / TotalTime);
					foreach (var light in bodyLayout.GetPositionLights(line.Item1, line.Item2))
					{
						var point = bodyLayout.GetLightPosition(light);
						if (line.Item5)
							point.X += amplitude;
						else
							point.Y += amplitude;
						var square = GetSquare(point, line.Item5);
						if (!square.HasValue)
							continue;
						segment.AddLight(light, time, squareColor[square.Value]);
					}
				}
			}
			return segment;
		}

		public Song Render()
		{
			var song = new Song("orchestra.mp3");

			//var squares = Squares();
			//song.AddSegment(squares, 0, 360, 0, 1890, 10);

			var wavy = Wavy();
			song.AddSegment(wavy, 0, 1000, 0, 1890 * 10);

			return song;
		}
	}
}
