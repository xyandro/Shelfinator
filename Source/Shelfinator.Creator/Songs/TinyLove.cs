using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class TinyLove : SongCreator
	{
		public override int SongNumber => 10;

		readonly Layout bodyLayout;

		public TinyLove()
		{
			bodyLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");
		}

		Segment Shift(out int time)
		{
			var segment = new Segment();

			var color = new LightColor(0, 1000, new List<int> { 0x000000 }, new List<int> { 0x101010 }, new List<int> { 0x100000, 0x100800, 0x101000 }, new List<int> { 0x101010, 0x100000, 0x101010 }, new List<int> { 0x000010, 0x001010, 0x000010 });

			var center = new Point(48, 48);
			var lightColor = bodyLayout.GetAllLights().ToDictionary(light => light, light => (((bodyLayout.GetLightPosition(light) - center).Length - 9) * 16.9830463869911).Round());

			time = 0;
			for (var ctr = 0; ctr < 42; ++ctr)
			{
				segment.Clear(time);
				var shift = Math.Max(0, Math.Min(19 - Math.Abs(23 - ctr), 17));
				for (var y = 0; y < 4; ++y)
					for (var x = 0; x < 4; ++x)
					{
						var lights = bodyLayout.GetPositionLights(x * 38 - shift, y * 38, 19, 2);
						lights.AddRange(bodyLayout.GetPositionLights(x * 38 + 2 + shift, y * 38 + 19, 19, 2));
						lights.AddRange(bodyLayout.GetPositionLights(x * 38 + 19, y * 38 - shift, 2, 19));
						lights.AddRange(bodyLayout.GetPositionLights(x * 38, y * 38 + 2 + shift, 2, 19));

						foreach (var light in lights)
							segment.AddLight(light, time, color, lightColor[light]);
					}
				time += 100;
			}
			return segment;
		}

		Segment Runners()
		{
			const int Speed = 15;
			var segment = new Segment();
			var aroundSquare = new Dictionary<int, List<Vector>>
			{
				[76] = new List<Vector> { new Vector(0, 0), new Vector(0, 1), new Vector(0, 2), new Vector(0, 3), new Vector(0, 4), new Vector(0, 5), new Vector(0, 6), new Vector(0, 7), new Vector(0, 8), new Vector(0, 9), new Vector(0, 10), new Vector(0, 11), new Vector(0, 12), new Vector(0, 13), new Vector(0, 14), new Vector(0, 15), new Vector(0, 16), new Vector(0, 17), new Vector(0, 18), new Vector(0, 19), new Vector(1, 19), new Vector(2, 19), new Vector(3, 19), new Vector(4, 19), new Vector(5, 19), new Vector(6, 19), new Vector(7, 19), new Vector(8, 19), new Vector(9, 19), new Vector(10, 19), new Vector(11, 19), new Vector(12, 19), new Vector(13, 19), new Vector(14, 19), new Vector(15, 19), new Vector(16, 19), new Vector(17, 19), new Vector(18, 19), new Vector(19, 19), new Vector(19, 18), new Vector(19, 17), new Vector(19, 16), new Vector(19, 15), new Vector(19, 14), new Vector(19, 13), new Vector(19, 12), new Vector(19, 11), new Vector(19, 10), new Vector(19, 9), new Vector(19, 8), new Vector(19, 7), new Vector(19, 6), new Vector(19, 5), new Vector(19, 4), new Vector(19, 3), new Vector(19, 2), new Vector(19, 1), new Vector(19, 0), new Vector(18, 0), new Vector(17, 0), new Vector(16, 0), new Vector(15, 0), new Vector(14, 0), new Vector(13, 0), new Vector(12, 0), new Vector(11, 0), new Vector(10, 0), new Vector(9, 0), new Vector(8, 0), new Vector(7, 0), new Vector(6, 0), new Vector(5, 0), new Vector(4, 0), new Vector(3, 0), new Vector(2, 0), new Vector(1, 0) },
				[152] = new List<Vector> { new Vector(0, 0), new Vector(0, 1), new Vector(0, 2), new Vector(0, 3), new Vector(0, 4), new Vector(0, 5), new Vector(0, 6), new Vector(0, 7), new Vector(0, 8), new Vector(0, 9), new Vector(0, 10), new Vector(0, 11), new Vector(0, 12), new Vector(0, 13), new Vector(0, 14), new Vector(0, 15), new Vector(0, 16), new Vector(0, 17), new Vector(0, 18), new Vector(0, 19), new Vector(0, 20), new Vector(0, 21), new Vector(0, 22), new Vector(0, 23), new Vector(0, 24), new Vector(0, 25), new Vector(0, 26), new Vector(0, 27), new Vector(0, 28), new Vector(0, 29), new Vector(0, 30), new Vector(0, 31), new Vector(0, 32), new Vector(0, 33), new Vector(0, 34), new Vector(0, 35), new Vector(0, 36), new Vector(0, 37), new Vector(0, 38), new Vector(1, 38), new Vector(2, 38), new Vector(3, 38), new Vector(4, 38), new Vector(5, 38), new Vector(6, 38), new Vector(7, 38), new Vector(8, 38), new Vector(9, 38), new Vector(10, 38), new Vector(11, 38), new Vector(12, 38), new Vector(13, 38), new Vector(14, 38), new Vector(15, 38), new Vector(16, 38), new Vector(17, 38), new Vector(18, 38), new Vector(19, 38), new Vector(20, 38), new Vector(21, 38), new Vector(22, 38), new Vector(23, 38), new Vector(24, 38), new Vector(25, 38), new Vector(26, 38), new Vector(27, 38), new Vector(28, 38), new Vector(29, 38), new Vector(30, 38), new Vector(31, 38), new Vector(32, 38), new Vector(33, 38), new Vector(34, 38), new Vector(35, 38), new Vector(36, 38), new Vector(37, 38), new Vector(38, 38), new Vector(38, 37), new Vector(38, 36), new Vector(38, 35), new Vector(38, 34), new Vector(38, 33), new Vector(38, 32), new Vector(38, 31), new Vector(38, 30), new Vector(38, 29), new Vector(38, 28), new Vector(38, 27), new Vector(38, 26), new Vector(38, 25), new Vector(38, 24), new Vector(38, 23), new Vector(38, 22), new Vector(38, 21), new Vector(38, 20), new Vector(38, 19), new Vector(38, 18), new Vector(38, 17), new Vector(38, 16), new Vector(38, 15), new Vector(38, 14), new Vector(38, 13), new Vector(38, 12), new Vector(38, 11), new Vector(38, 10), new Vector(38, 9), new Vector(38, 8), new Vector(38, 7), new Vector(38, 6), new Vector(38, 5), new Vector(38, 4), new Vector(38, 3), new Vector(38, 2), new Vector(38, 1), new Vector(38, 0), new Vector(37, 0), new Vector(36, 0), new Vector(35, 0), new Vector(34, 0), new Vector(33, 0), new Vector(32, 0), new Vector(31, 0), new Vector(30, 0), new Vector(29, 0), new Vector(28, 0), new Vector(27, 0), new Vector(26, 0), new Vector(25, 0), new Vector(24, 0), new Vector(23, 0), new Vector(22, 0), new Vector(21, 0), new Vector(20, 0), new Vector(19, 0), new Vector(18, 0), new Vector(17, 0), new Vector(16, 0), new Vector(15, 0), new Vector(14, 0), new Vector(13, 0), new Vector(12, 0), new Vector(11, 0), new Vector(10, 0), new Vector(9, 0), new Vector(8, 0), new Vector(7, 0), new Vector(6, 0), new Vector(5, 0), new Vector(4, 0), new Vector(3, 0), new Vector(2, 0), new Vector(1, 0) },
				[228] = new List<Vector> { new Vector(0, 0), new Vector(0, 1), new Vector(0, 2), new Vector(0, 3), new Vector(0, 4), new Vector(0, 5), new Vector(0, 6), new Vector(0, 7), new Vector(0, 8), new Vector(0, 9), new Vector(0, 10), new Vector(0, 11), new Vector(0, 12), new Vector(0, 13), new Vector(0, 14), new Vector(0, 15), new Vector(0, 16), new Vector(0, 17), new Vector(0, 18), new Vector(0, 19), new Vector(0, 20), new Vector(0, 21), new Vector(0, 22), new Vector(0, 23), new Vector(0, 24), new Vector(0, 25), new Vector(0, 26), new Vector(0, 27), new Vector(0, 28), new Vector(0, 29), new Vector(0, 30), new Vector(0, 31), new Vector(0, 32), new Vector(0, 33), new Vector(0, 34), new Vector(0, 35), new Vector(0, 36), new Vector(0, 37), new Vector(0, 38), new Vector(0, 39), new Vector(0, 40), new Vector(0, 41), new Vector(0, 42), new Vector(0, 43), new Vector(0, 44), new Vector(0, 45), new Vector(0, 46), new Vector(0, 47), new Vector(0, 48), new Vector(0, 49), new Vector(0, 50), new Vector(0, 51), new Vector(0, 52), new Vector(0, 53), new Vector(0, 54), new Vector(0, 55), new Vector(0, 56), new Vector(0, 57), new Vector(1, 57), new Vector(2, 57), new Vector(3, 57), new Vector(4, 57), new Vector(5, 57), new Vector(6, 57), new Vector(7, 57), new Vector(8, 57), new Vector(9, 57), new Vector(10, 57), new Vector(11, 57), new Vector(12, 57), new Vector(13, 57), new Vector(14, 57), new Vector(15, 57), new Vector(16, 57), new Vector(17, 57), new Vector(18, 57), new Vector(19, 57), new Vector(20, 57), new Vector(21, 57), new Vector(22, 57), new Vector(23, 57), new Vector(24, 57), new Vector(25, 57), new Vector(26, 57), new Vector(27, 57), new Vector(28, 57), new Vector(29, 57), new Vector(30, 57), new Vector(31, 57), new Vector(32, 57), new Vector(33, 57), new Vector(34, 57), new Vector(35, 57), new Vector(36, 57), new Vector(37, 57), new Vector(38, 57), new Vector(39, 57), new Vector(40, 57), new Vector(41, 57), new Vector(42, 57), new Vector(43, 57), new Vector(44, 57), new Vector(45, 57), new Vector(46, 57), new Vector(47, 57), new Vector(48, 57), new Vector(49, 57), new Vector(50, 57), new Vector(51, 57), new Vector(52, 57), new Vector(53, 57), new Vector(54, 57), new Vector(55, 57), new Vector(56, 57), new Vector(57, 57), new Vector(57, 56), new Vector(57, 55), new Vector(57, 54), new Vector(57, 53), new Vector(57, 52), new Vector(57, 51), new Vector(57, 50), new Vector(57, 49), new Vector(57, 48), new Vector(57, 47), new Vector(57, 46), new Vector(57, 45), new Vector(57, 44), new Vector(57, 43), new Vector(57, 42), new Vector(57, 41), new Vector(57, 40), new Vector(57, 39), new Vector(57, 38), new Vector(57, 37), new Vector(57, 36), new Vector(57, 35), new Vector(57, 34), new Vector(57, 33), new Vector(57, 32), new Vector(57, 31), new Vector(57, 30), new Vector(57, 29), new Vector(57, 28), new Vector(57, 27), new Vector(57, 26), new Vector(57, 25), new Vector(57, 24), new Vector(57, 23), new Vector(57, 22), new Vector(57, 21), new Vector(57, 20), new Vector(57, 19), new Vector(57, 18), new Vector(57, 17), new Vector(57, 16), new Vector(57, 15), new Vector(57, 14), new Vector(57, 13), new Vector(57, 12), new Vector(57, 11), new Vector(57, 10), new Vector(57, 9), new Vector(57, 8), new Vector(57, 7), new Vector(57, 6), new Vector(57, 5), new Vector(57, 4), new Vector(57, 3), new Vector(57, 2), new Vector(57, 1), new Vector(57, 0), new Vector(56, 0), new Vector(55, 0), new Vector(54, 0), new Vector(53, 0), new Vector(52, 0), new Vector(51, 0), new Vector(50, 0), new Vector(49, 0), new Vector(48, 0), new Vector(47, 0), new Vector(46, 0), new Vector(45, 0), new Vector(44, 0), new Vector(43, 0), new Vector(42, 0), new Vector(41, 0), new Vector(40, 0), new Vector(39, 0), new Vector(38, 0), new Vector(37, 0), new Vector(36, 0), new Vector(35, 0), new Vector(34, 0), new Vector(33, 0), new Vector(32, 0), new Vector(31, 0), new Vector(30, 0), new Vector(29, 0), new Vector(28, 0), new Vector(27, 0), new Vector(26, 0), new Vector(25, 0), new Vector(24, 0), new Vector(23, 0), new Vector(22, 0), new Vector(21, 0), new Vector(20, 0), new Vector(19, 0), new Vector(18, 0), new Vector(17, 0), new Vector(16, 0), new Vector(15, 0), new Vector(14, 0), new Vector(13, 0), new Vector(12, 0), new Vector(11, 0), new Vector(10, 0), new Vector(9, 0), new Vector(8, 0), new Vector(7, 0), new Vector(6, 0), new Vector(5, 0), new Vector(4, 0), new Vector(3, 0), new Vector(2, 0), new Vector(1, 0) },
			};
			var useTuples = new Queue<Tuple<Point, int, int>>(new List<Tuple<Point, int, int>> { Tuple.Create(new Point(0, 0), 228, 0), null, Tuple.Create(new Point(38, 38), 228, 0), null, Tuple.Create(new Point(38, 0), 228, 2), null, null, null, Tuple.Create(new Point(0, 38), 228, 2), null, Tuple.Create(new Point(0, 0), 76, 0), null, Tuple.Create(new Point(76, 76), 76, 0), null, Tuple.Create(new Point(76, 0), 76, 0), null, Tuple.Create(new Point(0, 76), 76, 0), null, Tuple.Create(default(Point), 0, 0), null, Tuple.Create(default(Point), 0, 0), null, null, null, Tuple.Create(default(Point), 0, 0), null, Tuple.Create(default(Point), 0, 0), null, Tuple.Create(new Point(0, 0), 152, 0), Tuple.Create(default(Point), 0, 0), Tuple.Create(new Point(57, 57), 152, 2), Tuple.Create(default(Point), 0, 0), Tuple.Create(new Point(57, 0), 152, 3), Tuple.Create(default(Point), 0, 0), Tuple.Create(new Point(0, 57), 152, 1), Tuple.Create(default(Point), 0, 0), Tuple.Create(new Point(19, 19), 228, 0) });
			var lightSize = new Size(2, 2);
			var curTuples = new Queue<Tuple<Point, int, int>>();
			var lightColor = new LightColor(0, 1000, new List<int> { 0x101010 }, new List<int> { 0x100000, 0x100800, 0x101000 }, new List<int> { 0x101010, 0x100000, 0x101010 }, new List<int> { 0x000010, 0x001010, 0x000010 });
			var center = new Point(48, 48);
			var lightDist = bodyLayout.GetAllLights().ToDictionary(light => light, light => (((bodyLayout.GetLightPosition(light) - center).Length - 9) * 16.9830463869911).Round());
			for (var time = 0; time < 4400; time += 5)
			{
				if ((time % 100) == 0)
					if (useTuples.Any())
					{
						var tuple = useTuples.Dequeue();
						if (tuple != null)
						{
							if (tuple.Item2 == 0)
								curTuples.Dequeue();
							else
								curTuples.Enqueue(tuple);
						}
					}

				segment.Clear(time);
				foreach (var tuple in curTuples)
				{
					var points = aroundSquare[tuple.Item2];
					var shift = time / Speed + tuple.Item3;
					for (var around = 0; around < points.Count; ++around)
						if ((around + shift) % 4 == 0)
						{
							var lights = bodyLayout.GetPositionLights(tuple.Item1 + points[around], lightSize);
							foreach (var light in lights)
								segment.AddLight(light, time, lightColor, lightDist[light]);
						}
				}
			}
			return segment;
		}

		class LineStopPoint
		{
			public Point Point { get; set; }
			public Point Dest { get; set; }
			public Vector Direction { get; set; }
			public int Color { get; set; }

			public LineStopPoint(Point point, Point dest, Vector direction, int color)
			{
				Point = point;
				Dest = dest;
				Direction = direction;
				Color = color;
			}
		}

		Segment LineStop()
		{
			var segment = new Segment();
			var lineStopPoints = new List<LineStopPoint>();
			for (var time = 0; time < 970; ++time)
			{
				if (time % 10 == 0)
				{
					for (var y = 0; y < 97; y += 19)
						lineStopPoints.Add(new LineStopPoint(new Point(-1, y), new Point(96 - time / 10, y), new Vector(1, 0), (y % 38 == 0) == (time % 20 == 0) ? 0x101010 : 0x100000));
					for (var y = 1; y < 97; y += 19)
						lineStopPoints.Add(new LineStopPoint(new Point(97, y), new Point(time / 10, y), new Vector(-1, 0), ((y - 1) % 38 == 0) == (time % 20 == 0) ? 0x100000 : 0x101010));
					for (var x = 0; x < 97; x += 19)
						lineStopPoints.Add(new LineStopPoint(new Point(x, -1), new Point(x, 96 - time / 10), new Vector(0, 1), (x % 38 == 0) == (time % 20 == 0) ? 0x101010 : 0x100000));
					for (var x = 1; x < 97; x += 19)
						lineStopPoints.Add(new LineStopPoint(new Point(x, 97), new Point(x, time / 10), new Vector(0, -1), ((x - 1) % 38 == 0) == (time % 20 == 0) ? 0x100000 : 0x101010));
				}

				segment.Clear(time);

				var isDone = new HashSet<int>();
				for (var ctr = 0; ctr < lineStopPoints.Count; ++ctr)
				{
					var lineStopPoint = lineStopPoints[ctr];
					var done = false;
					if (lineStopPoint.Point == lineStopPoint.Dest)
					{
						lineStopPoint.Direction = new Vector(0, 0);
						done = true;
					}

					var newPoint = lineStopPoint.Point + lineStopPoint.Direction;
					var light = bodyLayout.GetPositionLight(newPoint);
					lineStopPoint.Point = newPoint;
					if (!isDone.Contains(light))
						segment.AddLight(light, time, lineStopPoint.Color);
					if (done)
						isDone.Add(light);
				}
			}
			return segment;
		}

		Segment Heart()
		{
			var pixelData =
				"          ██████████                    █████████          /" +
				"        ██████████████                █████████████        /" +
				"       ████████████████             ████████████████       /" +
				"     ███████████████████           ██████████████████      /" +
				"     ████████████████████         ████████████████████     /" +
				"    ██████████████████████       ██████████████████████    /" +
				"   ████████████████████████     ████████████████████████   /" +
				"  █████████████████████████     ████████████████████████   /" +
				"  ██████████████████████████   ██████████████████████████  /" +
				"  ███████████████████████████ ███████████████████████████  /" +
				" ████████████████████████████ ████████████████████████████ /" +
				" █████████████████████████████████████████████████████████ /" +
				"███████████████████████████████████████████████████████████/" +
				"███████████████████████████████████████████████████████████/" +
				"███████████████████████████████████████████████████████████/" +
				"███████████████████████████████████████████████████████████/" +
				"███████████████████████████████████████████████████████████/" +
				"███████████████████████████████████████████████████████████/" +
				"███████████████████████████████████████████████████████████/" +
				"███████████████████████████████████████████████████████████/" +
				"███████████████████████████████████████████████████████████/" +
				"███████████████████████████████████████████████████████████/" +
				"███████████████████████████████████████████████████████████/" +
				"███████████████████████████████████████████████████████████/" +
				"███████████████████████████████████████████████████████████/" +
				"███████████████████████████████████████████████████████████/" +
				"███████████████████████████████████████████████████████████/" +
				"███████████████████████████████████████████████████████████/" +
				" █████████████████████████████████████████████████████████ /" +
				" █████████████████████████████████████████████████████████ /" +
				"  ███████████████████████████████████████████████████████  /" +
				"  ███████████████████████████████████████████████████████  /" +
				"   █████████████████████████████████████████████████████   /" +
				"    ███████████████████████████████████████████████████    /" +
				"    ██████████████████████████████████████████████████     /" +
				"     ████████████████████████████████████████████████      /" +
				"      ██████████████████████████████████████████████       /" +
				"       ████████████████████████████████████████████        /" +
				"         █████████████████████████████████████████         /" +
				"          ███████████████████████████████████████          /" +
				"           ████████████████████████████████████            /" +
				"             █████████████████████████████████             /" +
				"              ███████████████████████████████              /" +
				"                ███████████████████████████                /" +
				"                 █████████████████████████                 /" +
				"                  ███████████████████████                  /" +
				"                   █████████████████████                   /" +
				"                    ███████████████████                    /" +
				"                     █████████████████                     /" +
				"                      ███████████████                      /" +
				"                       █████████████                       /" +
				"                        ███████████                        /" +
				"                         █████████                         /" +
				"                          ███████                          /" +
				"                           ██████                          /" +
				"                           █████                           /" +
				"                            ███                            /" +
				"                            ███                            /" +
				"                             █                             ";

			var pixels = pixelData.Split('/').Select(str => str.Select(c => c != ' ').ToList()).ToList();
			var size = pixels.Count;

			var segment = new Segment();
			var lightColor = new LightColor(0, size, new List<int> { 0x001000, 0x000100 }, new List<int> { 0x000010, 0x000001 }, new List<int> { 0x100000, 0x101010, 0x100000, 0x101010, 0x100000 }, new List<int> { 0x100010, 0x001010, 0x100010, 0x001010, 0x100010 }, Helpers.Rainbow6);
			int x = (97 - size) / 2, y = 0, xOfs = 1, yOfs = 1;
			for (var time = 0; time <= 77; ++time)
			{
				var angle = -(double)time / 77 * 2 * Math.PI;
				var cos = Math.Cos(angle);
				var sin = Math.Sin(angle);

				segment.Clear(time);

				foreach (var light in bodyLayout.GetAllLights())
				{
					var point = bodyLayout.GetLightPosition(light);
					point.X -= x + size / 2;
					point.Y -= y + size / 2;
					var xVal = (point.X * cos - point.Y * sin + size / 2).Round();
					var yVal = (point.X * sin + point.Y * cos + size / 2).Round();
					var usePoint = new Point(xVal, yVal);
					if ((xVal >= 0) && (yVal >= 0) && (xVal < size) && (yVal < size))
						if (pixels[yVal][xVal])
							segment.AddLight(light, time, lightColor, yVal);
				}

				if ((x + xOfs < 0) || (x + size + xOfs > 97))
					xOfs = -xOfs;
				else
					x += xOfs;
				if ((y + yOfs < 0) || (y + size + yOfs > 97))
					yOfs = -yOfs;
				else
					y += yOfs;
			}

			return segment;
		}

		Segment LinesSparkle(out int time)
		{
			const int Duration = 5;
			const int Timing = 50;
			var rand = new Random(0xbadbeef);
			var horiz = Enumerable.Range(0, 6).SelectMany(x => bodyLayout.GetPositionLights(x * 19, 0, 2, 97)).ToList();
			var vert = Enumerable.Range(0, 6).SelectMany(y => bodyLayout.GetPositionLights(0, y * 19, 97, 2)).ToList();
			var segment = new Segment();
			var lightColor = new LightColor(0, 6, new List<int> { 0x101010 }, new List<int> { 0x100808, 0x100000 }, new List<int> { 0x100010, 0x001010 }, new List<int> { 0x001000, 0x081008, 0x081000, 0x001008 }, new List<int> { 0x000010, 0x100010, 0x101010 }, new List<int> { 0x101010, 0x100808, 0x100000, 0x100010, 0x001010, 0x001000, 0x081008, 0x081000, 0x001008, 0x000010 });
			var isHorizes = new List<bool?> { true, true, false, false, true, true, false, false, true, false, true, false, true, false, false, null, null, null, null, null };
			time = 0;
			foreach (var isHoriz in isHorizes)
			{
				List<int> lights;
				switch (isHoriz)
				{
					case true: lights = horiz; break;
					case false: lights = vert; break;
					case null: lights = horiz.Concat(vert).Distinct().ToList(); break;
					default: throw new Exception();
				}
				lights = lights.OrderBy(x => rand.Next()).ToList();
				for (var ctr = 0; ctr < lights.Count; ++ctr)
				{
					var lightTime = (time + (double)ctr / lights.Count * Timing).Round();
					segment.AddLight(lights[ctr], lightTime, lightTime + Duration, lightColor, rand.Next(7), 0x000000);
				}
				time += Timing;
			}
			return segment;
		}

		Segment MoveMelody()
		{
			const int BeatMoveBefore = 10;
			const int BeatMoveTime = 20;
			const int Size = 40;
			const int SegmentEndTime = 12800;
			const int FadeTime = 50;

			var beats = new List<Tuple<MidiNote, double>>
			{
				Tuple.Create(new MidiNote(MidiNote.PianoNote.G, 4, 100), 0d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.G, 4, 200), 9.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.G, 4, 300), 19d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.G, 4, 350), 28.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 500), 38d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.E, 4, 600), 47.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.D, 4, 700), 57d),

				Tuple.Create(new MidiNote(MidiNote.PianoNote.E, 4, 900), 0d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.E, 4, 1000), 9.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.E, 4, 1050), 19d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.E, 4, 1150), 28.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.D, 4, 1300), 38d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.C, 4, 1400), 47.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.B, 3, 1500), 57d),

				Tuple.Create(new MidiNote(MidiNote.PianoNote.A, 3, 1700), 0d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.C, 4, 1800), 9.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 1900), 19d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.E, 4, 2000), 28.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.D, 4, 2100), 38d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.C, 4, 2200), 47.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.A, 3, 2300), 57d),

				Tuple.Create(new MidiNote(MidiNote.PianoNote.C, 4, 2400), 0d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.C, 4, 2600), 28.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.G, 3, 2800), 57d),

				Tuple.Create(new MidiNote(MidiNote.PianoNote.G, 4, 3300), 0d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.G, 4, 3400), 9.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.G, 4, 3550), 19d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.G, 4, 3600), 28.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 3650), 38d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.E, 4, 3750), 47.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.D, 4, 3900), 57d),

				Tuple.Create(new MidiNote(MidiNote.PianoNote.E, 4, 4150), 0d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.E, 4, 4200), 9.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.E, 4, 4350), 19d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.E, 4, 4400), 28.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.D, 4, 4500), 38d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.C, 4, 4550), 47.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.B, 3, 4700), 57d),

				Tuple.Create(new MidiNote(MidiNote.PianoNote.A, 3, 4900), 0d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.C, 4, 5000), 9.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 5100), 19d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.E, 4, 5200), 28.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.D, 4, 5300), 38d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.C, 4, 5400), 47.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.A, 3, 5500), 57d),

				Tuple.Create(new MidiNote(MidiNote.PianoNote.C, 4, 5600), 0d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.C, 4, 5800), 28.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.G, 3, 6000), 57d),

				Tuple.Create(new MidiNote(MidiNote.PianoNote.G, 4, 6500), 0d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.G, 4, 6600), 5.7d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.G, 4, 6700), 11.4d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.G, 4, 6750), 17.1d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.E, 4, 7000), 22.8d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 7050), 28.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.G, 4, 7150), 34.2d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 7300), 39.9d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.E, 4, 7400), 45.6d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.D, 4, 7450), 51.3d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.DFlat, 4, 7550), 57d),

				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 8100), 0d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 8200), 5.7d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 8300), 11.4d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 8350), 17.1d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.D, 4, 8600), 22.8d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.E, 4, 8700), 28.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 8750), 34.2d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.G, 4, 8900), 39.9d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 8950), 45.6d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.E, 4, 9100), 51.3d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.D, 4, 9150), 57d),

				Tuple.Create(new MidiNote(MidiNote.PianoNote.G, 4, 9700), 0d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.G, 4, 9750), 5.7d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.G, 4, 9850), 11.4d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.G, 4, 9950), 17.1d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.E, 4, 10200), 22.8d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 10250), 28.5d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.G, 4, 10350), 34.2d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 10500), 39.9d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.E, 4, 10550), 45.6d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.D, 4, 10650), 51.3d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.DFlat, 4, 10750), 57d),

				Tuple.Create(new MidiNote(MidiNote.PianoNote.D, 4, 11000), 0d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.E, 4, 11050), 11.4d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 11250), 22.8d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 11350), 34.2d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 11500), 45.6d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 11550), 57d),

				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 12050), 0d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 12150), 11.4d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 12300), 22.8d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.F, 4, 12350), 34.2d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.E, 4, 12400), 45.6d),
				Tuple.Create(new MidiNote(MidiNote.PianoNote.D, 4, 12450), 57d),
			};

			var colors = new List<int> { 0x100000, 0x101000, 0x100010, 0x001000, 0x001010, 0x000010 };
			var notes = beats.Select(beat => beat.Item1.NoteValue).Distinct().OrderByDescending(x => x).ToList();
			var pos = Enumerable.Range(0, notes.Count).ToDictionary(index => notes[index], index => index * (97 - Size) / (notes.Count - 1));
			var segment = new Segment();
			double curX = -1, curY = -1;
			for (var beatCtr = 0; beatCtr < beats.Count; ++beatCtr)
			{
				var beatEndTime = beatCtr + 1 < beats.Count ? beats[beatCtr + 1].Item1.StartTime : SegmentEndTime;
				var beat = beats[beatCtr].Item1;
				var newX = beats[beatCtr].Item2;
				var newY = pos[beat.NoteValue];
				if (curX == -1)
					curX = newX;
				if (curY == -1)
					curY = newY;
				for (var time = 0; time <= BeatMoveTime; ++time)
				{
					var percent = (double)time / BeatMoveTime;
					var x = (newX - curX) * percent + curX;
					var y = (newY - curY) * percent + curY;
					var useTime = beat.StartTime - BeatMoveBefore + time;
					var endTime = time == BeatMoveTime ? int.MaxValue : useTime + FadeTime;
					var center = new Point(x + Size / 2, y + Size / 2);
					foreach (var light in bodyLayout.GetAllLights())
						if ((bodyLayout.GetLightPosition(light) - center).Length <= Size / 2 + .5)
							segment.AddLight(light, useTime, endTime, Helpers.GradientColor(colors[beatCtr % colors.Count], colors[(beatCtr + 1) % colors.Count], percent), 0x000000);
				}
				curX = newX;
				curY = newY;
			}
			return segment;
		}

		class MovingLine
		{
			public Point Center { get; set; }
			public int StartTime { get; set; }
			public int EndTime { get; set; }
			public double StartAngle { get; set; }
			public double EndAngle { get; set; }
		}

		Segment MovingLines()
		{
			const int Width = 4;

			var segment = new Segment();
			var lines = new List<MovingLine>
			{
				new MovingLine { Center = new Point(48, 48), StartTime = 0, EndTime = 10, StartAngle = 0, EndAngle = 0 },
				new MovingLine { Center = new Point(48, 48), StartTime = 0, EndTime = 10, StartAngle = 90, EndAngle = 90 },

				new MovingLine { Center = new Point(48, 48), StartTime = 10, EndTime = 90, StartAngle = 0, EndAngle = 45 },
				new MovingLine { Center = new Point(48, 48), StartTime = 10, EndTime = 90, StartAngle = 90, EndAngle = 135 },

				new MovingLine { Center = new Point(29, 29), StartTime = 100, EndTime = 180, StartAngle = 135, EndAngle = 180 },
				new MovingLine { Center = new Point(67, 67), StartTime = 100, EndTime = 180, StartAngle = 135, EndAngle = 180 },
				new MovingLine { Center = new Point(67, 29), StartTime = 100, EndTime = 180, StartAngle = 45, EndAngle = 90 },
				new MovingLine { Center = new Point(29, 67), StartTime = 100, EndTime = 180, StartAngle = 45, EndAngle = 90 },

				new MovingLine { Center = new Point(29, 29), StartTime = 190, EndTime = 270, StartAngle = 0, EndAngle = 45 },
				new MovingLine { Center = new Point(67, 67), StartTime = 190, EndTime = 270, StartAngle = 0, EndAngle = 45 },
				new MovingLine { Center = new Point(67, 29), StartTime = 190, EndTime = 270, StartAngle = 90, EndAngle = 135 },
				new MovingLine { Center = new Point(29, 67), StartTime = 190, EndTime = 270, StartAngle = 90, EndAngle = 135 },

				new MovingLine { Center = new Point(10, 48), StartTime = 280, EndTime = 360, StartAngle = 45, EndAngle = 90 },
				new MovingLine { Center = new Point(86, 48), StartTime = 280, EndTime = 360, StartAngle = 45, EndAngle = 90 },
				new MovingLine { Center = new Point(48, 10), StartTime = 280, EndTime = 360, StartAngle = 135, EndAngle = 180 },
				new MovingLine { Center = new Point(48, 86), StartTime = 280, EndTime = 360, StartAngle = 135, EndAngle = 180 },

				new MovingLine { Center = new Point(10, 48), StartTime = 370, EndTime = 450, StartAngle = 90, EndAngle = 135 },
				new MovingLine { Center = new Point(86, 48), StartTime = 370, EndTime = 450, StartAngle = 90, EndAngle = 135 },
				new MovingLine { Center = new Point(48, 10), StartTime = 370, EndTime = 450, StartAngle = 0, EndAngle = 45 },
				new MovingLine { Center = new Point(48, 86), StartTime = 370, EndTime = 450, StartAngle = 0, EndAngle = 45 },

				new MovingLine { Center = new Point(29, 29), StartTime = 460, EndTime = 540, StartAngle = 45, EndAngle = 90 },
				new MovingLine { Center = new Point(67, 67), StartTime = 460, EndTime = 540, StartAngle = 45, EndAngle = 90 },
				new MovingLine { Center = new Point(67, 29), StartTime = 460, EndTime = 540, StartAngle = 135, EndAngle = 180 },
				new MovingLine { Center = new Point(29, 67), StartTime = 460, EndTime = 540, StartAngle = 135, EndAngle = 180 },

				new MovingLine { Center = new Point(29, 29), StartTime = 550, EndTime = 630, StartAngle = 90, EndAngle = 135 },
				new MovingLine { Center = new Point(67, 67), StartTime = 550, EndTime = 630, StartAngle = 90, EndAngle = 135 },
				new MovingLine { Center = new Point(67, 29), StartTime = 550, EndTime = 630, StartAngle = 0, EndAngle = 45 },
				new MovingLine { Center = new Point(29, 67), StartTime = 550, EndTime = 630, StartAngle = 0, EndAngle = 45 },

				new MovingLine { Center = new Point(0, 0), StartTime = 640, EndTime = 720, StartAngle = 135, EndAngle = 180 },
				new MovingLine { Center = new Point(96, 0), StartTime = 640, EndTime = 720, StartAngle = 45, EndAngle = 90 },
				new MovingLine { Center = new Point(96, 96), StartTime = 640, EndTime = 720, StartAngle = 135, EndAngle = 180 },
				new MovingLine { Center = new Point(0, 96), StartTime = 640, EndTime = 720, StartAngle = 45, EndAngle = 90 },
				new MovingLine { Center = new Point(48, 48), StartTime = 640, EndTime = 720, StartAngle = 45, EndAngle = 90 },
				new MovingLine { Center = new Point(48, 48), StartTime = 640, EndTime = 720, StartAngle = 135, EndAngle = 180 },

				new MovingLine { Center = new Point(0, 0), StartTime = 720, EndTime = 1080, StartAngle = 0, EndAngle = 0 },
				new MovingLine { Center = new Point(96, 0), StartTime = 720, EndTime = 1080, StartAngle = 90, EndAngle = 90 },
				new MovingLine { Center = new Point(96, 96), StartTime = 720, EndTime = 1080, StartAngle = 0, EndAngle = 0 },
				new MovingLine { Center = new Point(0, 96), StartTime = 720, EndTime = 1080, StartAngle = 90, EndAngle = 90 },

				new MovingLine { Center = new Point(48, 48), StartTime = 720, EndTime = 1080, StartAngle = 90, EndAngle = 270 },
				new MovingLine { Center = new Point(48, 48), StartTime = 720, EndTime = 1080, StartAngle = 0, EndAngle = 180 },
			};

			var center = new Point(48, 48);
			var lightDistance = bodyLayout.GetAllLights().ToDictionary(light => light, light => (((bodyLayout.GetLightPosition(light) - center).Length - 9) * 16.9830463869911).Round());
			var lightColor = new LightColor(0, 1000, new List<int> { 0x101010 }, new List<int> { 0x000010, 0x101010, 0x000010 }, new List<int> { 0x001000, 0x101010, 0x001000 }, new List<int> { 0x100000, 0x101010, 0x100000 }, new List<int> { 0x100808, 0x081008, 0x080810 }, new List<int> { 0x100010, 0x001010, 0x100010, 0x001010, 0x100010 });
			var maxTime = lines.Max(line => line.EndTime);
			for (var time = 0; time <= maxTime; time += 5)
			{
				var first = true;

				foreach (var line in lines)
				{
					if ((time < line.StartTime) || (time > line.EndTime))
						continue;

					if (first)
					{
						segment.Clear(time);
						first = false;
					}

					foreach (var light in bodyLayout.GetAllLights())
					{
						var point = bodyLayout.GetLightPosition(light);
						var useAngle = (((double)time - line.StartTime) / (line.EndTime - line.StartTime) * (line.EndAngle - line.StartAngle) + line.StartAngle) * Math.PI / 180;
						var sin = Math.Sin(useAngle);
						var cos = Math.Cos(useAngle);
						var t = (line.Center.Y - point.Y) * sin - (line.Center.X - point.X) * cos;
						var dist = Math.Sqrt(Math.Pow(t * cos + line.Center.X - point.X, 2) + Math.Pow(-t * sin + line.Center.Y - point.Y, 2)).Round();
						if (dist < Width / 2)
							segment.AddLight(light, time, lightColor, lightDistance[light]);
					}
				}
			}
			return segment;
		}

		Segment RotateSquares()
		{
			var center = new Point(48, 48);
			var segment = new Segment();
			var lightColor = new LightColor(0, 2, new List<int> { 0x101010, 0x101010, 0x101010 }, new List<int> { 0x101010, 0x100000, 0x101010 }, new List<int> { 0x100000, 0x101000, 0x000010 }, new List<int> { 0x100010, 0x000010, 0x001010 });
			for (var angle = 0; angle < 360; ++angle)
			{
				segment.Clear(angle);
				var radians = angle * Math.PI / 180;
				var cos1 = Math.Cos(-radians);
				var cos2 = Math.Cos(radians);
				var sin1 = Math.Sin(-radians);
				var sin2 = Math.Sin(radians);

				foreach (var light in bodyLayout.GetAllLights())
				{
					var point = bodyLayout.GetLightPosition(light) - center;
					var point1 = new Point(Math.Abs((point.X * cos1 - point.Y * sin1).Round()), Math.Abs((point.X * sin1 + point.Y * cos1).Round()));
					var point2 = new Point(Math.Abs((point.X * cos2 - point.Y * sin2).Round()), Math.Abs((point.X * sin2 + point.Y * cos2).Round()));

					var add = -1;

					// Outer square
					if ((point1.X >= 0) && (point1.X <= 48) && (point1.Y >= 47) && (point1.Y <= 48))
						add = 0;
					if ((point1.Y >= 0) && (point1.Y <= 48) && (point1.X >= 47) && (point1.X <= 48))
						add = 0;

					// Middle square
					if ((point2.X >= 0) && (point2.X <= 29) && (point2.Y >= 28) && (point2.Y <= 29))
						add = 1;
					if ((point2.Y >= 0) && (point2.Y <= 29) && (point2.X >= 28) && (point2.X <= 29))
						add = 1;

					// Inner square
					if ((point1.X >= 0) && (point1.X <= 10) && (point1.Y >= 9) && (point1.Y <= 10))
						add = 2;
					if ((point1.Y >= 0) && (point1.Y <= 10) && (point1.X >= 9) && (point1.X <= 10))
						add = 2;

					if (add != -1)
						segment.AddLight(light, angle, lightColor, add);
				}
			}
			return segment;
		}

		Segment ColsToRows(out int moveTime, out int stopTime)
		{
			const int PauseTime = 50;
			const int PointCount = 228;
			const string Data =
				"19,20/19,21/19,22/19,23/19,24/19,25/19,26/19,27/19,28/19,29/19,30/19,31/19,32/19,33/19,34/19,35/19,36/19,37/19,38/19,39/19,40/19,41/19,42/19,43/19,44/19,45/19,46/19,47/19,48/19,49/19,50/19,51/19,52/19,53/19,54/19,55/19,56/19,57/19,58/19,59/19,60/19,61/19,62/19,63/19,64/19,65/19,66/19,67/19,68/19,69/19,70/19,71/19,72/19,73/19,74/19,75/19,76/19,77/19,78/19,79/19,80/19,81/19,82/19,83/19,84/19,85/19,86/19,87/19,88/19,89/19,90/19,91/19,92/19,93/19,94/19,95/20,95/21,95/22,95/23,95/24,95/25,95/26,95/27" +
				",95/28,95/29,95/30,95/31,95/32,95/33,95/34,95/35,95/36,95/37,95/38,95/38,94/38,93/38,92/38,91/38,90/38,89/38,88/38,87/38,86/38,85/38,84/38,83/38,82/38,81/38,80/38,79/38,78/38,77/38,76/38,75/38,74/38,73/38,72/38,71/38,70/38,69/38,68/38,67/38,66/38,65/38,64/38,63/38,62/38,61/38,60/38,59/38,58/38,57/38,56/38,55/38,54/38,53/38,52/38,51/38,50/38,49/38,48/38,47/38,46/38,45/38,44/38,43/38,42/38,41/38,40/38,39/38,38/38,37/38,36/38,35/38,34/38,33/38,32/38,31/38,30/38,29/38,28/38,27/38,26/38,25/38,24/38,2" +
				"3/38,22/38,21/38,20/38,19/38,18/38,17/38,16/38,15/38,14/38,13/38,12/38,11/38,10/38,9/38,8/38,7/38,6/38,5/38,4/38,3/38,2/38,1/38,0/37,0/36,0/35,0/34,0/33,0/32,0/31,0/30,0/29,0/28,0/27,0/26,0/25,0/24,0/23,0/22,0/21,0/20,0/19,0/19,1/19,2/19,3/19,4/19,5/19,6/19,7/19,8/19,9/19,10/19,11/19,12/19,13/19,14/19,15/19,16/19,17/19,18/19,19/18,19/17,19/16,19/15,19/14,19/13,19/12,19/11,19/10,19/9,19/8,19/7,19/6,19/5,19/4,19/3,19/2,19/1,19/0,19/0,20/0,21/0,22/0,23/0,24/0,25/0,26/0,27/0,28/0,29/0,30/0,31/0,32/0" +
				",33/0,34/0,35/0,36/0,37/0,38/1,38/2,38/3,38/4,38/5,38/6,38/7,38/8,38/9,38/10,38/11,38/12,38/13,38/14,38/15,38/16,38/17,38/18,38/19,38/20,38/21,38/22,38/23,38/24,38/25,38/26,38/27,38/28,38/29,38/30,38/31,38/32,38/33,38/34,38/35,38/36,38/37,38/38,38/39,38/40,38/41,38/42,38/43,38/44,38/45,38/46,38/47,38/48,38/49,38/50,38/51,38/52,38/53,38/54,38/55,38/56,38/57,38/58,38/59,38/60,38/61,38/62,38/63,38/64,38/65,38/66,38/67,38/68,38/69,38/70,38/71,38/72,38/73,38/74,38/75,38/76,38/77,38/78,38/79,38/80,38/" +
				"81,38/82,38/83,38/84,38/85,38/86,38/87,38/88,38/89,38/90,38/91,38/92,38/93,38/94,38/95,38/95,37/95,36/95,35/95,34/95,33/95,32/95,31/95,30/95,29/95,28/95,27/95,26/95,25/95,24/95,23/95,22/95,21/95,20/95,19/94,19/93,19/92,19/91,19/90,19/89,19/88,19/87,19/86,19/85,19/84,19/83,19/82,19/81,19/80,19/79,19/78,19/77,19/76,19/75,19/74,19/73,19/72,19/71,19/70,19/69,19/68,19/67,19/66,19/65,19/64,19/63,19/62,19/61,19/60,19/59,19/58,19/57,19/56,19/55,19/54,19/53,19/52,19/51,19/50,19/49,19/48,19/47,19/46,19/45" +
				",19/44,19/43,19/42,19/41,19/40,19/39,19/38,19/37,19/36,19/35,19/34,19/33,19/32,19/31,19/30,19/29,19/28,19/27,19/26,19/25,19/24,19/23,19/22,19/21,19/20,19/19,19";
			var lights = Data.Split('/').Select(Point.Parse).Select(point => new List<Point> { point, new Point(95 - point.X, 95 - point.Y) }.SelectMany(p => bodyLayout.GetPositionLights(p, 2, 2)).ToList()).ToList();
			var center = new Point(48, 48);
			var lightDistance = bodyLayout.GetAllLights().ToDictionary(light => light, light => (((bodyLayout.GetLightPosition(light) - center).Length - 10) * 21.7012478066469).Round());

			var lightColor = new LightColor(0, 2000,
				new List<int> { 0x101010, 0x001010, 0x001008, 0x000010, 0x101010, 0x001010, 0x001008, 0x000010, 0x101010 },
				new List<int> { 0x101010, 0x100010, 0x100008, 0x000010, 0x101010, 0x100010, 0x100008, 0x000010, 0x101010 },
				new List<int> { 0x101010, 0x101000, 0x100800, 0x001000, 0x101010, 0x101000, 0x100800, 0x001000, 0x101010 },
				new List<int> { 0x101010, 0x001010, 0x000810, 0x001000, 0x101010, 0x001010, 0x000810, 0x001000, 0x101010 },
				new List<int> { 0x101010, 0x100010, 0x080010, 0x100000, 0x101010, 0x100010, 0x080010, 0x100000, 0x101010 }
			);

			var segment = new Segment();
			moveTime = 0;
			for (var start = 0; start < lights.Count; ++start)
			{
				segment.Clear(moveTime);
				for (var ctr = 0; ctr < PointCount; ++ctr)
					foreach (var light in lights[(start + ctr) % lights.Count])
					{
						var useTime = ((double)moveTime / 554 * 1000 + lightDistance[light]).Round();
						segment.AddLight(light, moveTime, moveTime + 1000, lightColor, useTime, lightColor, useTime + 1000, true);
					}
				if (start % (lights.Count / 2) == 0)
					moveTime += PauseTime;
				else
					++moveTime;
			}

			stopTime = moveTime;
			segment.Clear(stopTime);
			for (var ctr = 0; ctr < PointCount; ++ctr)
				foreach (var light in lights[PointCount + ctr])
				{
					var useTime = ((double)stopTime / 2 / 554 * 1000 + lightDistance[light]).Round();
					segment.AddLight(light, stopTime, stopTime + 1000, lightColor, useTime, lightColor, useTime + 1000, true);
				}
			stopTime += moveTime;

			return segment;
		}

		Segment RotateLines()
		{
			const double FreezeTime = 0.05;
			var color = new LightColor(0, 1, new List<int> { 0x101010, 0x101010 }, new List<int> { 0x100000, 0x101000 }, new List<int> { 0x001000, 0x081008 }, new List<int> { 0x100010, 0x001010 });
			var segment = new Segment();
			for (var time = 0; time < 360; ++time)
			{
				segment.Clear(time);

				var angle = 90 - Math.Max(0, Math.Min((Math.Cos(time * Math.PI / 180) * (1 + FreezeTime * 2) / 2 + 0.5) * 90, 90));
				var cos = Math.Cos(angle * Math.PI / 180);
				var sin = Math.Sin(angle * Math.PI / 180);
				var rects = new List<Tuple<Rect, Point, int>>
				{
					Tuple.Create(new Rect(19.6, -1000, 18.8, 2000), new Point(86, 29), 1),
					Tuple.Create(new Rect(57.6, -1000, 18.8, 2000), new Point(10, 67), 1),
					Tuple.Create(new Rect(38.6, -1000, 18.8, 2000), new Point(48, 48), 0),
				};

				foreach (var light in bodyLayout.GetAllLights())
				{
					var point = bodyLayout.GetLightPosition(light);
					foreach (var rect in rects)
					{
						var newPoint = new Point((point.X - rect.Item2.X) * cos - (point.Y - rect.Item2.Y) * sin + rect.Item2.X, (point.X - rect.Item2.X) * sin + (point.Y - rect.Item2.Y) * cos + rect.Item2.Y);
						if (rect.Item1.Contains(newPoint))
							segment.AddLight(light, time, color, rect.Item3);
					}
				}
			}
			return segment;
		}

		public override Song Render()
		{
			var song = new Song("tinylove.ogg");

			// Shift (1670)
			var shift = Shift(out var shiftTime);
			song.AddSegment(shift, 0, shiftTime, 1670, 2266, 8);
			song.AddPaletteChange(0, 0);
			song.AddPaletteChange(1170, 2170, 1);
			song.AddPaletteChange(5702, 6702, 2);
			song.AddPaletteChange(10234, 11234, 3);
			song.AddPaletteChange(14766, 15766, 4);
			song.AddPaletteChange(19798, 0);

			// Runners (19798)
			var runners = Runners();
			song.AddSegment(runners, 0, 2800, 19798, 15862);
			song.AddPaletteChange(19798, 0);
			song.AddPaletteChange(23830, 24830, 1);
			song.AddPaletteChange(30628, 31628, 2);

			song.AddSegment(runners, 2800, 4400, 35660, 6712);
			song.AddPaletteChange(35160, 36160, 3);
			song.AddPaletteChange(42372, 0);

			// LineStop (42372)
			var lineStop = LineStop();
			song.AddSegment(lineStop, 0, 970 + 100, 42372, 23492);

			// Heart (65864)
			var heart = Heart();
			song.AddSegment(heart, 0, 78, 65864, 3356, 5);
			song.AddPaletteChange(65864, 0);
			song.AddPaletteChange(69020, 69420, 1);
			song.AddPaletteChange(72376, 72776, 2);
			song.AddPaletteChange(75732, 76132, 3);
			song.AddPaletteChange(79088, 79488, 4);
			song.AddPaletteChange(82644, 0);

			// LinesSparkle (82644)
			var linesSparkle = LinesSparkle(out int linesSparkleLength);
			song.AddSegment(linesSparkle, 0, linesSparkleLength * 4 / 5, 82644, 13424);
			song.AddSegmentByVelocity(linesSparkle, linesSparkleLength * 4 / 5, linesSparkleLength, linesSparkleLength / 5, 96068, 5262, linesSparkleLength / 5, 0, 5261);
			song.AddPaletteChange(82644, 0);
			song.AddPaletteChange(86000, 1);
			song.AddPaletteChange(89356, 2);
			song.AddPaletteChange(91034, 3);
			song.AddPaletteChange(92712, 4);
			song.AddPaletteChange(95229, 5);
			song.AddPaletteChange(101330, 0);

			// MoveMelody (101330)
			var moveMelody = MoveMelody();
			song.AddSegment(moveMelody, 0, 800, 101330, 3122);
			song.AddSegment(moveMelody, 800, 1600, 104452, 3031);
			song.AddSegment(moveMelody, 1600, 2400, 107483, 2975);
			song.AddSegment(moveMelody, 2400, 3200, 110458, 2789);
			song.AddSegment(moveMelody, 3200, 4000, 113247, 2809);
			song.AddSegment(moveMelody, 4000, 4800, 116056, 2746);
			song.AddSegment(moveMelody, 4800, 5600, 118802, 2822);
			song.AddSegment(moveMelody, 5600, 6400, 121624, 2758);
			song.AddSegment(moveMelody, 6400, 7200, 124382, 2801);
			song.AddSegment(moveMelody, 7200, 8000, 127183, 2863);
			song.AddSegment(moveMelody, 8000, 8800, 130046, 2821);
			song.AddSegment(moveMelody, 8800, 9600, 132867, 2929);
			song.AddSegment(moveMelody, 9600, 10400, 135796, 2830);
			song.AddSegment(moveMelody, 10400, 11200, 138626, 2863);
			song.AddSegment(moveMelody, 11200, 12000, 141489, 2851);
			song.AddSegment(moveMelody, 12000, 12800, 144340, 3003);

			// MovingLines (147343)
			var movingLines = MovingLines();
			song.AddSegment(movingLines, 0, 180, 147343, 2865);
			song.AddSegment(movingLines, 180, 360, 150208, 2847);
			song.AddSegment(movingLines, 360, 540, 153055, 2779);
			song.AddSegment(movingLines, 540, 720, 155834, 2839);
			song.AddSegment(movingLines, 720, 900, 158673, 2655);
			song.AddSegment(movingLines, 900, 1080, 161328, 2655);
			song.AddPaletteChange(147343, 0);
			song.AddPaletteChange(150008, 150408, 1);
			song.AddPaletteChange(152855, 153255, 2);
			song.AddPaletteChange(155634, 156034, 3);
			song.AddPaletteChange(158473, 158873, 4);
			song.AddPaletteChange(161128, 161528, 5);
			song.AddPaletteChange(163983, 0);

			// RotateSquares (163983)
			var rotateSquares = RotateSquares();
			song.AddSegment(rotateSquares, 0, 180, 163983, 3211);
			song.AddSegment(rotateSquares, 0, 180, 167194, 3302);
			song.AddSegment(rotateSquares, 0, 180, 170496, 3160);
			song.AddSegment(rotateSquares, 0, 180, 173656, 3213);
			song.AddSegment(rotateSquares, 0, 180, 176869, 3582);
			song.AddSegment(rotateSquares, 0, 180, 180451, 3324);
			song.AddSegment(rotateSquares, 0, 180, 183775, 3606);
			song.AddPaletteChange(163983, 0);
			song.AddPaletteChange(170296, 170696, 1);
			song.AddPaletteChange(176669, 177069, 2);
			song.AddPaletteChange(183575, 183975, 3);
			song.AddPaletteChange(187381, 0);

			// ColsToRows (187381)
			var colsToRows = ColsToRows(out var colsToRowsMoveTime, out var colsToRowsStopTime);
			song.AddSegment(colsToRows, 0, colsToRowsMoveTime, 187381, 3204);
			song.AddSegment(colsToRows, 0, colsToRowsMoveTime, 190585, 3352);
			song.AddSegment(colsToRows, 0, colsToRowsMoveTime, 193937, 3373);
			song.AddSegment(colsToRows, 0, colsToRowsMoveTime, 197310, 3365);
			song.AddSegment(colsToRows, 0, colsToRowsMoveTime, 200675, 3681);
			song.AddSegment(colsToRows, 0, colsToRowsMoveTime, 204356, 3274);
			song.AddSegment(colsToRows, 0, colsToRowsMoveTime, 207630, 3352);
			song.AddSegment(colsToRows, 0, colsToRowsMoveTime / 2, 210982, 1846);
			song.AddSegment(colsToRows, colsToRowsMoveTime, colsToRowsStopTime, 212828, 4073);
			song.AddPaletteChange(187381, 0);
			song.AddPaletteChange(193737, 194137, 1);
			song.AddPaletteChange(200475, 200875, 2);
			song.AddPaletteChange(207430, 207830, 3);
			song.AddPaletteChange(212628, 213028, 4);
			song.AddPaletteChange(216901, 0);

			// RotateLines (216901)
			var rotateLines = RotateLines();
			song.AddSegment(rotateLines, 0, 180, 216901, 3382);
			song.AddSegment(rotateLines, 180, 360, 220283, 2192);
			song.AddSegment(rotateLines, 0, 360, 222475, 4735);
			song.AddSegment(rotateLines, 0, 360, 227210, 4965);
			song.AddSegment(rotateLines, 0, 180, 232175, 3500);
			song.AddSegment(rotateLines, 180, 180, 235675, 3458);
			song.AddPaletteChange(216901, 0);
			song.AddPaletteChange(222275, 222675, 1);
			song.AddPaletteChange(227010, 227410, 2);
			song.AddPaletteChange(231975, 232375, 3);
			song.AddPaletteChange(239133, 0);

			return song;
		}
	}
}
