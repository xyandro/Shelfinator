using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Orchestra : ISong
	{
		public int SongNumber => 7;

		readonly Layout bodyLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");

		Segment Lines()
		{
			const int Length = 39;
			var points = new List<Point> { new Point(19, 0), new Point(19, 1), new Point(19, 2), new Point(19, 3), new Point(19, 4), new Point(19, 5), new Point(19, 6), new Point(19, 7), new Point(19, 8), new Point(19, 9), new Point(19, 10), new Point(19, 11), new Point(19, 12), new Point(19, 13), new Point(19, 14), new Point(19, 15), new Point(19, 16), new Point(19, 17), new Point(19, 18), new Point(19, 19), new Point(18, 19), new Point(17, 19), new Point(16, 19), new Point(15, 19), new Point(14, 19), new Point(13, 19), new Point(12, 19), new Point(11, 19), new Point(10, 19), new Point(9, 19), new Point(8, 19), new Point(7, 19), new Point(6, 19), new Point(5, 19), new Point(4, 19), new Point(3, 19), new Point(2, 19), new Point(1, 19), new Point(0, 19), new Point(0, 18), new Point(0, 17), new Point(0, 16), new Point(0, 15), new Point(0, 14), new Point(0, 13), new Point(0, 12), new Point(0, 11), new Point(0, 10), new Point(0, 9), new Point(0, 8), new Point(0, 7), new Point(0, 6), new Point(0, 5), new Point(0, 4), new Point(0, 3), new Point(0, 2), new Point(0, 1), new Point(0, 0), new Point(0, -1), new Point(0, -2), new Point(0, -3), new Point(0, -4), new Point(0, -5), new Point(0, -6), new Point(0, -7), new Point(0, -8), new Point(0, -9), new Point(0, -10), new Point(0, -11), new Point(0, -12), new Point(0, -13), new Point(0, -14), new Point(0, -15), new Point(0, -16), new Point(0, -17), new Point(0, -18), new Point(0, -19), new Point(-1, -19), new Point(-2, -19), new Point(-3, -19), new Point(-4, -19), new Point(-5, -19), new Point(-6, -19), new Point(-7, -19), new Point(-8, -19), new Point(-9, -19), new Point(-10, -19), new Point(-11, -19), new Point(-12, -19), new Point(-13, -19), new Point(-14, -19), new Point(-15, -19), new Point(-16, -19), new Point(-17, -19), new Point(-18, -19), new Point(-19, -19), new Point(-19, -18), new Point(-19, -17), new Point(-19, -16), new Point(-19, -15), new Point(-19, -14), new Point(-19, -13), new Point(-19, -12), new Point(-19, -11), new Point(-19, -10), new Point(-19, -9), new Point(-19, -8), new Point(-19, -7), new Point(-19, -6), new Point(-19, -5), new Point(-19, -4), new Point(-19, -3), new Point(-19, -2), new Point(-19, -1), new Point(-19, 0), new Point(-18, 0), new Point(-17, 0), new Point(-16, 0), new Point(-15, 0), new Point(-14, 0), new Point(-13, 0), new Point(-12, 0), new Point(-11, 0), new Point(-10, 0), new Point(-9, 0), new Point(-8, 0), new Point(-7, 0), new Point(-6, 0), new Point(-5, 0), new Point(-4, 0), new Point(-3, 0), new Point(-2, 0), new Point(-1, 0), new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(3, 0), new Point(4, 0), new Point(5, 0), new Point(6, 0), new Point(7, 0), new Point(8, 0), new Point(9, 0), new Point(10, 0), new Point(11, 0), new Point(12, 0), new Point(13, 0), new Point(14, 0), new Point(15, 0), new Point(16, 0), new Point(17, 0), new Point(18, 0) };
			var lightColor = new LightColor(0, 39,
				new List<int> { 0x000000 },
				new List<int> { 0x101010 },
				new List<int> { 0x100000, 0x001000 },
				new List<int> { 0x001000, 0x000010 },
				new List<int> { 0x000010, 0x100000 }
			);
			var parts = new List<Tuple<int, int, int, bool, bool, LightColor>>
			{
				Tuple.Create(0, 19, 19, false, false, lightColor),
				Tuple.Create(0, 76, 76, true, true, lightColor),
				Tuple.Create(76, 76, 19, false, true, lightColor),
				Tuple.Create(76, 19, 76, true, false, lightColor),
			};

			var segment = new Segment();
			var time = 0;
			for (var point = 0; point < points.Count; ++point)
			{
				segment.Clear(time);
				for (var ctr = 0; ctr < Length; ++ctr)
					foreach (var part in parts)
					{
						var add = points[(point + part.Item1 + ctr) % points.Count];
						foreach (var light in bodyLayout.GetPositionLights((part.Item4 ? -1 : 1) * add.X + part.Item2, (part.Item5 ? -1 : 1) * add.Y + part.Item3, 2, 2))
							segment.AddLight(light, time, part.Item6, ctr);
					}
				++time;
			}
			return segment;
		}

		Segment VertHoriz()
		{
			var segment = new Segment();
			var points = new List<Tuple<int, int, Point, Vector>>();
			for (var square = 0; square < 49; ++square)
			{
				var x = square % 7 * 19 - 19;
				var y = square / 7 * 19 - 19;
				if (square % 2 == 0)
				{
					points.Add(Tuple.Create(0, 19, new Point(x, y), new Vector(1, 0)));
					points.Add(Tuple.Create(19, 19, new Point(x + 19, y), new Vector(0, 1)));
					points.Add(Tuple.Create(0, 19, new Point(x + 19, y + 19), new Vector(-1, 0)));
					points.Add(Tuple.Create(19, 19, new Point(x, y + 19), new Vector(0, -1)));
				}
				else
				{
					points.Add(Tuple.Create(0, 5, new Point(x, y + 13), new Vector(0, 1)));
					points.Add(Tuple.Create(6, 13, new Point(x, y + 19), new Vector(1, 0)));

					points.Add(Tuple.Create(19, 5, new Point(x + 13, y + 19), new Vector(1, 0)));
					points.Add(Tuple.Create(25, 13, new Point(x + 19, y + 19), new Vector(0, -1)));

					points.Add(Tuple.Create(0, 5, new Point(x + 19, y + 6), new Vector(0, -1)));
					points.Add(Tuple.Create(6, 13, new Point(x + 19, y), new Vector(-1, 0)));

					points.Add(Tuple.Create(19, 5, new Point(x + 6, y), new Vector(-1, 0)));
					points.Add(Tuple.Create(25, 13, new Point(x, y), new Vector(0, 1)));
				}
			}
			for (var time = 0; time < 38; ++time)
			{
				segment.Clear(time);

				foreach (var point in points)
					if (point.Item1 == 0)
						foreach (var light in bodyLayout.GetPositionLights(point.Item3, 2, 2))
							segment.AddLight(light, time, 0x101010);

				points = points.Select(tuple => Tuple.Create(Math.Max(0, tuple.Item1 - 1), tuple.Item2 - (tuple.Item1 == 0 ? 1 : 0), tuple.Item3 + (tuple.Item1 == 0 ? tuple.Item4 : default(Vector)), tuple.Item4)).Where(tuple => tuple.Item2 >= 0).ToList();
			}
			return segment;
		}

		Segment Beats()
		{
			var times = new List<double> { 0, 708.75, 1417.5, 2126.25, 2835, 3307.5, 3780, 4488.75, 5197.5, 5906.25, 6615, 7087.5, 7560, 8268.75, 8977.5, 9450, 10158.75, 10867.5, 11340, 12048.75, 12757.5, 13230, 13938.75, 14647.5, 15120, 15828.75 }.Select(x => (int)x).ToList();
			var segment = new Segment();
			foreach (var time in times)
			{
				segment.Clear(time);
				foreach (var light in bodyLayout.GetAllLights())
					segment.AddLight(light, time, time + 500, 0x101010, 0x000000);
			}
			return segment;
		}

		public Song Render()
		{
			var song = new Song("orchestra.mp3"); // First sound is at 500; Measures start at 1720, repeat every 1890, and stop at 177490

			// Lines (1720)
			var lines = Lines();
			song.AddSegment(lines, 54, 152, 500, 1220);
			song.AddSegment(lines, 0, 152, 1720, 1890, 8);
			song.AddPaletteChange(500, 0);
			song.AddPaletteChange(500, 1720, 1);
			song.AddPaletteChange(5000, 6000, 2);
			song.AddPaletteChange(8780, 9780, 3);
			song.AddPaletteChange(12560, 13560, 4);
			song.AddPaletteChange(16840, 0);

			// VertHoriz (16840)
			var vertHoriz = VertHoriz();
			song.AddSegment(vertHoriz, 0, 38, 16840, 1890, 4);

			// Beats (24400)
			var beats = Beats();
			song.AddSegment(beats, 0, 177490, 24400);
			Emulator.TestPosition = 24400;

			return song;
		}
	}
}
