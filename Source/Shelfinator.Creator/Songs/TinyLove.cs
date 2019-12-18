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
		public override bool Test => true;

		readonly Layout bodyLayout;

		public TinyLove()
		{
			bodyLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");
		}

		Segment Shift()
		{
			var segment = new Segment();

			var color = new LightColor(0, 1000, new List<int> { 0x000000 }, new List<int> { 0x101010 }, new List<int> { 0x100000, 0x100800, 0x101000 }, new List<int> { 0x101010, 0x100000, 0x101010 }, new List<int> { 0x000010, 0x001010, 0x000010 });

			var center = new Point(48, 48);
			var lightColor = bodyLayout.GetAllLights().ToDictionary(light => light, light => (((bodyLayout.GetLightPosition(light) - center).Length - 9) * 16.9830463869911).Round());

			var time = 0;
			for (var ctr = 0; ctr < 42; ++ctr)
			{
				segment.Clear(time);
				var shift = Math.Max(0, Math.Min(19 - Math.Abs(21 - ctr), 17));
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
			var segment = new Segment();
			var aroundSquare = new Dictionary<int, List<Vector>>
			{
				[4] = new List<Vector> { new Vector(0, 0), new Vector(0, 1), new Vector(0, 2), new Vector(0, 3), new Vector(0, 4), new Vector(0, 5), new Vector(0, 6), new Vector(0, 7), new Vector(0, 8), new Vector(0, 9), new Vector(0, 10), new Vector(0, 11), new Vector(0, 12), new Vector(0, 13), new Vector(0, 14), new Vector(0, 15), new Vector(0, 16), new Vector(0, 17), new Vector(0, 18), new Vector(0, 19), new Vector(1, 19), new Vector(2, 19), new Vector(3, 19), new Vector(4, 19), new Vector(5, 19), new Vector(6, 19), new Vector(7, 19), new Vector(8, 19), new Vector(9, 19), new Vector(10, 19), new Vector(11, 19), new Vector(12, 19), new Vector(13, 19), new Vector(14, 19), new Vector(15, 19), new Vector(16, 19), new Vector(17, 19), new Vector(18, 19), new Vector(19, 19), new Vector(19, 18), new Vector(19, 17), new Vector(19, 16), new Vector(19, 15), new Vector(19, 14), new Vector(19, 13), new Vector(19, 12), new Vector(19, 11), new Vector(19, 10), new Vector(19, 9), new Vector(19, 8), new Vector(19, 7), new Vector(19, 6), new Vector(19, 5), new Vector(19, 4), new Vector(19, 3), new Vector(19, 2), new Vector(19, 1), new Vector(19, 0), new Vector(18, 0), new Vector(17, 0), new Vector(16, 0), new Vector(15, 0), new Vector(14, 0), new Vector(13, 0), new Vector(12, 0), new Vector(11, 0), new Vector(10, 0), new Vector(9, 0), new Vector(8, 0), new Vector(7, 0), new Vector(6, 0), new Vector(5, 0), new Vector(4, 0), new Vector(3, 0), new Vector(2, 0), new Vector(1, 0) },
				[6] = new List<Vector> { new Vector(0, 0), new Vector(0, 1), new Vector(0, 2), new Vector(0, 3), new Vector(0, 4), new Vector(0, 5), new Vector(0, 6), new Vector(0, 7), new Vector(0, 8), new Vector(0, 9), new Vector(0, 10), new Vector(0, 11), new Vector(0, 12), new Vector(0, 13), new Vector(0, 14), new Vector(0, 15), new Vector(0, 16), new Vector(0, 17), new Vector(0, 18), new Vector(0, 19), new Vector(0, 20), new Vector(0, 21), new Vector(0, 22), new Vector(0, 23), new Vector(0, 24), new Vector(0, 25), new Vector(0, 26), new Vector(0, 27), new Vector(0, 28), new Vector(0, 29), new Vector(0, 30), new Vector(0, 31), new Vector(0, 32), new Vector(0, 33), new Vector(0, 34), new Vector(0, 35), new Vector(0, 36), new Vector(0, 37), new Vector(0, 38), new Vector(0, 39), new Vector(0, 40), new Vector(0, 41), new Vector(0, 42), new Vector(0, 43), new Vector(0, 44), new Vector(0, 45), new Vector(0, 46), new Vector(0, 47), new Vector(0, 48), new Vector(0, 49), new Vector(0, 50), new Vector(0, 51), new Vector(0, 52), new Vector(0, 53), new Vector(0, 54), new Vector(0, 55), new Vector(0, 56), new Vector(0, 57), new Vector(1, 57), new Vector(2, 57), new Vector(3, 57), new Vector(4, 57), new Vector(5, 57), new Vector(6, 57), new Vector(7, 57), new Vector(8, 57), new Vector(9, 57), new Vector(10, 57), new Vector(11, 57), new Vector(12, 57), new Vector(13, 57), new Vector(14, 57), new Vector(15, 57), new Vector(16, 57), new Vector(17, 57), new Vector(18, 57), new Vector(19, 57), new Vector(20, 57), new Vector(21, 57), new Vector(22, 57), new Vector(23, 57), new Vector(24, 57), new Vector(25, 57), new Vector(26, 57), new Vector(27, 57), new Vector(28, 57), new Vector(29, 57), new Vector(30, 57), new Vector(31, 57), new Vector(32, 57), new Vector(33, 57), new Vector(34, 57), new Vector(35, 57), new Vector(36, 57), new Vector(37, 57), new Vector(38, 57), new Vector(39, 57), new Vector(40, 57), new Vector(41, 57), new Vector(42, 57), new Vector(43, 57), new Vector(44, 57), new Vector(45, 57), new Vector(46, 57), new Vector(47, 57), new Vector(48, 57), new Vector(49, 57), new Vector(50, 57), new Vector(51, 57), new Vector(52, 57), new Vector(53, 57), new Vector(54, 57), new Vector(55, 57), new Vector(56, 57), new Vector(57, 57), new Vector(57, 56), new Vector(57, 55), new Vector(57, 54), new Vector(57, 53), new Vector(57, 52), new Vector(57, 51), new Vector(57, 50), new Vector(57, 49), new Vector(57, 48), new Vector(57, 47), new Vector(57, 46), new Vector(57, 45), new Vector(57, 44), new Vector(57, 43), new Vector(57, 42), new Vector(57, 41), new Vector(57, 40), new Vector(57, 39), new Vector(57, 38), new Vector(57, 37), new Vector(57, 36), new Vector(57, 35), new Vector(57, 34), new Vector(57, 33), new Vector(57, 32), new Vector(57, 31), new Vector(57, 30), new Vector(57, 29), new Vector(57, 28), new Vector(57, 27), new Vector(57, 26), new Vector(57, 25), new Vector(57, 24), new Vector(57, 23), new Vector(57, 22), new Vector(57, 21), new Vector(57, 20), new Vector(57, 19), new Vector(57, 18), new Vector(57, 17), new Vector(57, 16), new Vector(57, 15), new Vector(57, 14), new Vector(57, 13), new Vector(57, 12), new Vector(57, 11), new Vector(57, 10), new Vector(57, 9), new Vector(57, 8), new Vector(57, 7), new Vector(57, 6), new Vector(57, 5), new Vector(57, 4), new Vector(57, 3), new Vector(57, 2), new Vector(57, 1), new Vector(57, 0), new Vector(56, 0), new Vector(55, 0), new Vector(54, 0), new Vector(53, 0), new Vector(52, 0), new Vector(51, 0), new Vector(50, 0), new Vector(49, 0), new Vector(48, 0), new Vector(47, 0), new Vector(46, 0), new Vector(45, 0), new Vector(44, 0), new Vector(43, 0), new Vector(42, 0), new Vector(41, 0), new Vector(40, 0), new Vector(39, 0), new Vector(38, 0), new Vector(37, 0), new Vector(36, 0), new Vector(35, 0), new Vector(34, 0), new Vector(33, 0), new Vector(32, 0), new Vector(31, 0), new Vector(30, 0), new Vector(29, 0), new Vector(28, 0), new Vector(27, 0), new Vector(26, 0), new Vector(25, 0), new Vector(24, 0), new Vector(23, 0), new Vector(22, 0), new Vector(21, 0), new Vector(20, 0), new Vector(19, 0), new Vector(18, 0), new Vector(17, 0), new Vector(16, 0), new Vector(15, 0), new Vector(14, 0), new Vector(13, 0), new Vector(12, 0), new Vector(11, 0), new Vector(10, 0), new Vector(9, 0), new Vector(8, 0), new Vector(7, 0), new Vector(6, 0), new Vector(5, 0), new Vector(4, 0), new Vector(3, 0), new Vector(2, 0), new Vector(1, 0) },
				[8] = new List<Vector> { new Vector(0, 0), new Vector(0, 1), new Vector(0, 2), new Vector(0, 3), new Vector(0, 4), new Vector(0, 5), new Vector(0, 6), new Vector(0, 7), new Vector(0, 8), new Vector(0, 9), new Vector(0, 10), new Vector(0, 11), new Vector(0, 12), new Vector(0, 13), new Vector(0, 14), new Vector(0, 15), new Vector(0, 16), new Vector(0, 17), new Vector(0, 18), new Vector(0, 19), new Vector(0, 20), new Vector(0, 21), new Vector(0, 22), new Vector(0, 23), new Vector(0, 24), new Vector(0, 25), new Vector(0, 26), new Vector(0, 27), new Vector(0, 28), new Vector(0, 29), new Vector(0, 30), new Vector(0, 31), new Vector(0, 32), new Vector(0, 33), new Vector(0, 34), new Vector(0, 35), new Vector(0, 36), new Vector(0, 37), new Vector(0, 38), new Vector(1, 38), new Vector(2, 38), new Vector(3, 38), new Vector(4, 38), new Vector(5, 38), new Vector(6, 38), new Vector(7, 38), new Vector(8, 38), new Vector(9, 38), new Vector(10, 38), new Vector(11, 38), new Vector(12, 38), new Vector(13, 38), new Vector(14, 38), new Vector(15, 38), new Vector(16, 38), new Vector(17, 38), new Vector(18, 38), new Vector(19, 38), new Vector(20, 38), new Vector(21, 38), new Vector(22, 38), new Vector(23, 38), new Vector(24, 38), new Vector(25, 38), new Vector(26, 38), new Vector(27, 38), new Vector(28, 38), new Vector(29, 38), new Vector(30, 38), new Vector(31, 38), new Vector(32, 38), new Vector(33, 38), new Vector(34, 38), new Vector(35, 38), new Vector(36, 38), new Vector(37, 38), new Vector(38, 38), new Vector(38, 37), new Vector(38, 36), new Vector(38, 35), new Vector(38, 34), new Vector(38, 33), new Vector(38, 32), new Vector(38, 31), new Vector(38, 30), new Vector(38, 29), new Vector(38, 28), new Vector(38, 27), new Vector(38, 26), new Vector(38, 25), new Vector(38, 24), new Vector(38, 23), new Vector(38, 22), new Vector(38, 21), new Vector(38, 20), new Vector(38, 19), new Vector(38, 18), new Vector(38, 17), new Vector(38, 16), new Vector(38, 15), new Vector(38, 14), new Vector(38, 13), new Vector(38, 12), new Vector(38, 11), new Vector(38, 10), new Vector(38, 9), new Vector(38, 8), new Vector(38, 7), new Vector(38, 6), new Vector(38, 5), new Vector(38, 4), new Vector(38, 3), new Vector(38, 2), new Vector(38, 1), new Vector(38, 0), new Vector(37, 0), new Vector(36, 0), new Vector(35, 0), new Vector(34, 0), new Vector(33, 0), new Vector(32, 0), new Vector(31, 0), new Vector(30, 0), new Vector(29, 0), new Vector(28, 0), new Vector(27, 0), new Vector(26, 0), new Vector(25, 0), new Vector(24, 0), new Vector(23, 0), new Vector(22, 0), new Vector(21, 0), new Vector(20, 0), new Vector(19, 0), new Vector(18, 0), new Vector(17, 0), new Vector(16, 0), new Vector(15, 0), new Vector(14, 0), new Vector(13, 0), new Vector(12, 0), new Vector(11, 0), new Vector(10, 0), new Vector(9, 0), new Vector(8, 0), new Vector(7, 0), new Vector(6, 0), new Vector(5, 0), new Vector(4, 0), new Vector(3, 0), new Vector(2, 0), new Vector(1, 0) },
				[19] = new List<Vector> { new Vector(0, 0), new Vector(0, 1), new Vector(0, 2), new Vector(0, 3), new Vector(0, 4), new Vector(0, 5), new Vector(0, 6), new Vector(0, 7), new Vector(0, 8), new Vector(0, 9), new Vector(0, 10), new Vector(0, 11), new Vector(0, 12), new Vector(0, 13), new Vector(0, 14), new Vector(0, 15), new Vector(0, 16), new Vector(0, 17), new Vector(0, 18), new Vector(0, 19), new Vector(1, 19), new Vector(2, 19), new Vector(3, 19), new Vector(4, 19), new Vector(5, 19), new Vector(6, 19), new Vector(7, 19), new Vector(8, 19), new Vector(9, 19), new Vector(10, 19), new Vector(11, 19), new Vector(12, 19), new Vector(13, 19), new Vector(14, 19), new Vector(15, 19), new Vector(16, 19), new Vector(17, 19), new Vector(18, 19), new Vector(19, 19), new Vector(19, 18), new Vector(19, 17), new Vector(19, 16), new Vector(19, 15), new Vector(19, 14), new Vector(19, 13), new Vector(19, 12), new Vector(19, 11), new Vector(19, 10), new Vector(19, 9), new Vector(19, 8), new Vector(19, 7), new Vector(19, 6), new Vector(19, 5), new Vector(19, 4), new Vector(19, 3), new Vector(19, 2), new Vector(19, 1), new Vector(19, 0), new Vector(18, 0), new Vector(17, 0), new Vector(16, 0), new Vector(15, 0), new Vector(14, 0), new Vector(13, 0), new Vector(12, 0), new Vector(11, 0), new Vector(10, 0), new Vector(9, 0), new Vector(8, 0), new Vector(7, 0), new Vector(6, 0), new Vector(5, 0), new Vector(4, 0), new Vector(3, 0), new Vector(2, 0), new Vector(1, 0) },
			};
			var useTuples = new Queue<Tuple<Point, int>>(new List<Tuple<Point, int>> { Tuple.Create(new Point(0, 0), 6), null, Tuple.Create(new Point(38, 38), 6), null, Tuple.Create(new Point(38, 0), 6), null, null, null, Tuple.Create(new Point(0, 38), 6), null, Tuple.Create(new Point(19, 19), 4), null, Tuple.Create(new Point(57, 57), 4), null, Tuple.Create(new Point(57, 19), 4), null, Tuple.Create(new Point(19, 57), 4), null, Tuple.Create(default(Point), 0), null, Tuple.Create(default(Point), 0), null, null, null, Tuple.Create(default(Point), 0), null, Tuple.Create(default(Point), 0), null, Tuple.Create(new Point(0, 0), 8), Tuple.Create(default(Point), 0), Tuple.Create(new Point(57, 57), 8), Tuple.Create(default(Point), 0), Tuple.Create(new Point(57, 0), 8), Tuple.Create(default(Point), 0), Tuple.Create(new Point(0, 57), 8), Tuple.Create(default(Point), 0), Tuple.Create(new Point(38, 38), 19) });
			var lightSize = new Size(2, 2);
			var curTuples = new Queue<Tuple<Point, int>>();
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
					var shift = time / 10;
					if (tuple.Item2 == 19)
						shift = time * tuple.Item2 / 200;
					for (var around = 0; around < aroundSquare[tuple.Item2].Count; ++around)
						if ((around + shift) % tuple.Item2 == 0)
						{
							var lights = bodyLayout.GetPositionLights(tuple.Item1 + aroundSquare[tuple.Item2][around], lightSize);
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

				for (var ctr = 0; ctr < lineStopPoints.Count; ++ctr)
				{
					var lineStopPoint = lineStopPoints[ctr];
					if (lineStopPoint.Point == lineStopPoint.Dest)
						lineStopPoint.Direction = new Vector(0, 0);

					var newPoint = lineStopPoint.Point + lineStopPoint.Direction;
					var light = bodyLayout.TryGetPositionLight(newPoint) ?? int.MinValue;
					if (light != int.MinValue)
					{
						lineStopPoint.Point = newPoint;
						segment.AddLight(light, time, lineStopPoint.Color);
					}
				}
			}
			return segment;
		}

		Segment Heart()
		{
			var segment = new Segment();

			var pixels = Helpers.LoadImage("Shelfinator.Creator.Songs.Layout.Heart.png", 1d / 16);
			int x = (97 - pixels.GetLength(1)) / 2, y = 0, xOfs = 1, yOfs = 1;
			for (var time = 0; time <= 77; ++time)
			{
				segment.Clear(time);
				Helpers.DrawImage(pixels, bodyLayout, segment, time, x, y);
				if ((x + xOfs < 0) || (x + pixels.GetLength(0) + xOfs > 97))
					xOfs = -xOfs;
				else
					x += xOfs;
				if ((y + yOfs < 0) || (y + pixels.GetLength(1) + yOfs > 97))
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
			var isHorizes = new List<bool?> { true, true, false, false, true, true, false, false, true, false, true, false, true, false, false, false, null, null, null, null };
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
					segment.AddLight(lights[ctr], lightTime, lightTime + Duration, 0x101010, 0x000000);
				}
				time += Timing;
			}
			return segment;
		}

		Segment MoveMelody()
		{
			const int BeatMoveBefore = 10;
			const int BeatMoveTime = 20;
			const int Width = 21;
			const int Height = 21;
			var beats = new List<MidiNote>
			{
				new MidiNote(MidiNote.PianoNote.G, 4, 100, 0),
				new MidiNote(MidiNote.PianoNote.G, 4, 200, 0),
				new MidiNote(MidiNote.PianoNote.G, 4, 300, 0),
				new MidiNote(MidiNote.PianoNote.G, 4, 350, 0),
				new MidiNote(MidiNote.PianoNote.F, 4, 500, 0),
				new MidiNote(MidiNote.PianoNote.E, 4, 600, 0),
				new MidiNote(MidiNote.PianoNote.D, 4, 700, 0),

				new MidiNote(MidiNote.PianoNote.E, 4, 900, 0),
				new MidiNote(MidiNote.PianoNote.E, 4, 1000, 0),
				new MidiNote(MidiNote.PianoNote.E, 4, 1050, 0),
				new MidiNote(MidiNote.PianoNote.E, 4, 1150, 0),
				new MidiNote(MidiNote.PianoNote.D, 4, 1300, 0),
				new MidiNote(MidiNote.PianoNote.C, 4, 1400, 0),
				new MidiNote(MidiNote.PianoNote.B, 3, 1500, 0),

				new MidiNote(MidiNote.PianoNote.A, 3, 1700, 0),
				new MidiNote(MidiNote.PianoNote.C, 4, 1800, 0),
				new MidiNote(MidiNote.PianoNote.F, 4, 1900, 0),
				new MidiNote(MidiNote.PianoNote.E, 4, 2000, 0),
				new MidiNote(MidiNote.PianoNote.D, 4, 2100, 0),
				new MidiNote(MidiNote.PianoNote.C, 4, 2200, 0),
				new MidiNote(MidiNote.PianoNote.A, 3, 2300, 0),
				new MidiNote(MidiNote.PianoNote.C, 4, 2400, 0),
				new MidiNote(MidiNote.PianoNote.C, 4, 2600, 0),
				new MidiNote(MidiNote.PianoNote.G, 3, 2800, 0),

				new MidiNote(MidiNote.PianoNote.G, 4, 3300, 0),
				new MidiNote(MidiNote.PianoNote.G, 4, 3400, 0),
				new MidiNote(MidiNote.PianoNote.G, 4, 3550, 0),
				new MidiNote(MidiNote.PianoNote.G, 4, 3600, 0),
				new MidiNote(MidiNote.PianoNote.F, 4, 3650, 0),
				new MidiNote(MidiNote.PianoNote.E, 4, 3750, 0),
				new MidiNote(MidiNote.PianoNote.D, 4, 3900, 0),

				new MidiNote(MidiNote.PianoNote.E, 4, 4150, 0),
				new MidiNote(MidiNote.PianoNote.E, 4, 4200, 0),
				new MidiNote(MidiNote.PianoNote.E, 4, 4350, 0),
				new MidiNote(MidiNote.PianoNote.E, 4, 4400, 0),
				new MidiNote(MidiNote.PianoNote.D, 4, 4500, 0),
				new MidiNote(MidiNote.PianoNote.C, 4, 4550, 0),
				new MidiNote(MidiNote.PianoNote.B, 3, 4700, 0),

				new MidiNote(MidiNote.PianoNote.A, 3, 4900, 0),
				new MidiNote(MidiNote.PianoNote.C, 4, 5000, 0),
				new MidiNote(MidiNote.PianoNote.F, 4, 5100, 0),
				new MidiNote(MidiNote.PianoNote.E, 4, 5200, 0),
				new MidiNote(MidiNote.PianoNote.D, 4, 5300, 0),
				new MidiNote(MidiNote.PianoNote.C, 4, 5400, 0),
				new MidiNote(MidiNote.PianoNote.A, 3, 5500, 0),
				new MidiNote(MidiNote.PianoNote.C, 4, 5600, 0),
				new MidiNote(MidiNote.PianoNote.C, 4, 5800, 0),
				new MidiNote(MidiNote.PianoNote.G, 3, 6000, 0),

				new MidiNote(MidiNote.PianoNote.G, 4, 6500, 0),
				new MidiNote(MidiNote.PianoNote.G, 4, 6600, 0),
				new MidiNote(MidiNote.PianoNote.G, 4, 6700, 0),
				new MidiNote(MidiNote.PianoNote.G, 4, 6750, 0),
				new MidiNote(MidiNote.PianoNote.E, 4, 7000, 0),
				new MidiNote(MidiNote.PianoNote.F, 4, 7050, 0),
				new MidiNote(MidiNote.PianoNote.G, 4, 7150, 0),
				new MidiNote(MidiNote.PianoNote.F, 4, 7300, 0),
				new MidiNote(MidiNote.PianoNote.E, 4, 7400, 0),
				new MidiNote(MidiNote.PianoNote.D, 4, 7450, 0),
				new MidiNote(MidiNote.PianoNote.DFlat, 4, 7550, 0),

				new MidiNote(MidiNote.PianoNote.F, 4, 8100, 0),
				new MidiNote(MidiNote.PianoNote.F, 4, 8200, 0),
				new MidiNote(MidiNote.PianoNote.F, 4, 8300, 0),
				new MidiNote(MidiNote.PianoNote.F, 4, 8350, 0),
				new MidiNote(MidiNote.PianoNote.D, 4, 8600, 0),
				new MidiNote(MidiNote.PianoNote.E, 4, 8700, 0),
				new MidiNote(MidiNote.PianoNote.F, 4, 8750, 0),
				new MidiNote(MidiNote.PianoNote.G, 4, 8900, 0),
				new MidiNote(MidiNote.PianoNote.F, 4, 8950, 0),
				new MidiNote(MidiNote.PianoNote.E, 4, 9100, 0),
				new MidiNote(MidiNote.PianoNote.D, 4, 9150, 0),

				new MidiNote(MidiNote.PianoNote.G, 4, 9700, 0),
				new MidiNote(MidiNote.PianoNote.G, 4, 9750, 0),
				new MidiNote(MidiNote.PianoNote.G, 4, 9850, 0),
				new MidiNote(MidiNote.PianoNote.G, 4, 9950, 0),
				new MidiNote(MidiNote.PianoNote.E, 4, 10200, 0),
				new MidiNote(MidiNote.PianoNote.F, 4, 10250, 0),
				new MidiNote(MidiNote.PianoNote.G, 4, 10350, 0),
				new MidiNote(MidiNote.PianoNote.F, 4, 10500, 0),
				new MidiNote(MidiNote.PianoNote.E, 4, 10550, 0),
				new MidiNote(MidiNote.PianoNote.D, 4, 10650, 0),
				new MidiNote(MidiNote.PianoNote.DFlat, 4, 10750, 0),

				new MidiNote(MidiNote.PianoNote.D, 4, 11000, 0),
				new MidiNote(MidiNote.PianoNote.E, 4, 11050, 0),
				new MidiNote(MidiNote.PianoNote.F, 4, 11250, 0),
				new MidiNote(MidiNote.PianoNote.F, 4, 11350, 0),
				new MidiNote(MidiNote.PianoNote.F, 4, 11500, 0),
				new MidiNote(MidiNote.PianoNote.F, 4, 11550, 0),

				new MidiNote(MidiNote.PianoNote.F, 4, 12050, 0),
				new MidiNote(MidiNote.PianoNote.F, 4, 12150, 0),
				new MidiNote(MidiNote.PianoNote.F, 4, 12300, 0),
				new MidiNote(MidiNote.PianoNote.F, 4, 12350, 0),
				new MidiNote(MidiNote.PianoNote.E, 4, 12400, 0),
				new MidiNote(MidiNote.PianoNote.D, 4, 12450, 0),
			};

			var notes = beats.Select(beat => beat.NoteValue).Distinct().OrderByDescending(x => x).ToList();
			var pos = Enumerable.Range(0, notes.Count).ToDictionary(index => notes[index], index => index * (97 - Height) / (notes.Count - 1));
			var segment = new Segment();
			int curX = -1, curY = -1;
			for (var beatCtr = 0; beatCtr < beats.Count; ++beatCtr)
			{
				MidiNote beat = beats[beatCtr];
				var newX = (4 - Math.Abs(beatCtr % 8 - 4)) * 19;
				var newY = pos[beat.NoteValue];
				if (curX == -1)
					curX = newX;
				if (curY == -1)
					curY = newY;
				for (var time = 0; time <= BeatMoveTime; ++time)
				{
					var x = (newX - curX) * time / 20 + curX;
					var y = (newY - curY) * time / 20 + curY;
					var useTime = beat.StartTime - BeatMoveBefore + time;
					segment.Clear(useTime);
					foreach (var light in bodyLayout.GetPositionLights(x, y, Width, Height))
						segment.AddLight(light, useTime, 0x101010);
				}
				curX = newX;
				curY = newY;
			}
			return segment;
		}

		Segment Shrink(out int totalTime)
		{
			const int DelayTime = 10;
			totalTime = 100 + DelayTime * 2 + 1;

			var center = new Point(48, 48);
			var segment = new Segment();
			var colors = Helpers.Rainbow6;
			for (var time = 0; time < totalTime; ++time)
			{
				segment.Clear(time);
				var percent = Math.Max(0, Math.Min((double)(time - DelayTime) / 100, 1));
				foreach (var light in bodyLayout.GetAllLights())
				{
					var point = bodyLayout.GetLightPosition(light);
					var xn = ((point.X - (center.X - 9) * percent) / (1 - percent * 13 / 16)).Round();
					var yn = ((point.Y - (center.Y - 9) * percent) / (1 - percent * 13 / 16)).Round();
					if ((xn < 0) || (yn < 0) || (xn > 96) || (yn > 96))
						continue;

					var col = (xn + 18) / 19;
					var row = (yn + 18) / 19;
					var color = colors[(col + row) % colors.Count];
					segment.AddLight(light, time, color);
				}
			}

			return segment;
		}

		public override Song Render()
		{
			var song = new Song("tinylove.ogg");

			//// Shift (1670)
			//var shift = Shift();
			//song.AddSegment(shift, 0, 4300, 1670, 2266, 8);
			//song.AddPaletteChange(0, 0);
			//song.AddPaletteChange(1170, 2170, 1);
			//song.AddPaletteChange(5702, 6702, 2);
			//song.AddPaletteChange(10234, 11234, 3);
			//song.AddPaletteChange(14766, 15766, 4);
			//song.AddPaletteChange(19798, 0);

			//// Runners (19798)
			//var runners = Runners();
			//song.AddSegment(runners, 0, 2800, 19798, 15862);
			//song.AddPaletteChange(19798, 0);
			//song.AddPaletteChange(23830, 24830, 1);
			//song.AddPaletteChange(30628, 31628, 2);

			//song.AddSegment(runners, 2800, 4400, 35660, 6712);
			//song.AddPaletteChange(35160, 36160, 3);
			//song.AddPaletteChange(42372, 0);

			//// LineStop (42372)
			//var lineStop = LineStop();
			//song.AddSegment(lineStop, 0, 970 + 100, 42372, 23492);

			//// Heart (65864)
			//var heart = Heart();
			//song.AddSegment(heart, 0, 78, 65864, 3356);
			//song.AddSegment(heart, 78, 0, 69220, 3356);
			//song.AddSegment(heart, 0, 78, 72576, 3356);
			//song.AddSegment(heart, 78, 0, 75932, 3356);
			//song.AddSegment(heart, 0, 78, 79288, 3356);

			//// LinesSparkle (82644)
			//var linesSparkle = LinesSparkle(out int linesSparkleLength);
			//song.AddSegment(linesSparkle, 0, linesSparkleLength * 4 / 5, 82644, 13424);
			//song.AddSegmentByVelocity(linesSparkle, linesSparkleLength * 4 / 5, linesSparkleLength, linesSparkleLength / 5, 96068, 5261, linesSparkleLength / 5, 0, 5261);

			//// MoveMelody (101330)
			//var moveMelody = MoveMelody();
			//song.AddSegment(moveMelody, 0, 800, 101330, 3122);
			//song.AddSegment(moveMelody, 800, 1600, 104452, 3031);
			//song.AddSegment(moveMelody, 1600, 2400, 107483, 2975);
			//song.AddSegment(moveMelody, 2400, 3200, 110458, 2789);
			//song.AddSegment(moveMelody, 3200, 4000, 113247, 2809);
			//song.AddSegment(moveMelody, 4000, 4800, 116056, 2746);
			//song.AddSegment(moveMelody, 4800, 5600, 118802, 2822);
			//song.AddSegment(moveMelody, 5600, 6400, 121624, 2758);
			//song.AddSegment(moveMelody, 6400, 7200, 124382, 2801);
			//song.AddSegment(moveMelody, 7200, 8000, 127183, 2863);
			//song.AddSegment(moveMelody, 8000, 8800, 130046, 2821);
			//song.AddSegment(moveMelody, 8800, 9600, 132867, 2929);
			//song.AddSegment(moveMelody, 9600, 10400, 135796, 2830);
			//song.AddSegment(moveMelody, 10400, 11200, 138626, 2863);
			//song.AddSegment(moveMelody, 11200, 12000, 141489, 2851);
			//song.AddSegment(moveMelody, 12000, 12800, 144340, 3003);

			// Shrink (147343)
			var shrink = Shrink(out var shrinkTime);
			song.AddSegment(shrink, 0, shrinkTime, 147343, 2865);
			song.AddSegment(shrink, shrinkTime, 0, 150208, 2847);
			song.AddSegment(shrink, 0, shrinkTime, 153055, 2779);
			song.AddSegment(shrink, shrinkTime, 0, 155834, 2839);
			song.AddSegment(shrink, 0, shrinkTime, 158673, 2655);
			song.AddSegment(shrink, shrinkTime, 0, 161328, 2655);
			Emulator.TestPosition = 147343;

			// Next (163983)

			return song;
		}
	}
}
