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
			const string PointsData =
"0,0&1,0&2,0&3,0&4,0&5,0&6,0&7,0&8,0&9,0&10,0&11,0&12,0&13,0&14,0&15,0&16,0&17,0&18,0&19,0&19,1&19,2&19,3&19,4&19,5&19,6&19,7&19,8&19,9&19,10&19,11&19,12&19,13&19,14&19,15&19,16&19,17&19,18&19,19&18,19&17,19&16,19&15,19&14,19&13,19&12,19&11,19&10,19&9,19&8,19&7,19&6,19&5,19&4,19&3,19&2,19&1,19&0,19&0,18&0,17&0,16&0,15&0,14&0,13&0,12&0,11&0,10&0,9&0,8&0,7&0,6&0,5&0,4&0,3&0,2&0,1&0,0&0,-1&0,-2&0,-3&0,-4&0,-5&0,-6&0,-7&0,-8&0,-9&0,-10&0,-11&0,-12&0,-13&0,-14&0,-15&0,-16&0,-17&0,-18&0,-19&-1,-19&-2,-" +
"19&-3,-19&-4,-19&-5,-19&-6,-19&-7,-19&-8,-19&-9,-19&-10,-19&-11,-19&-12,-19&-13,-19&-14,-19&-15,-19&-16,-19&-17,-19&-18,-19&-19,-19&-19,-18&-19,-17&-19,-16&-19,-15&-19,-14&-19,-13&-19,-12&-19,-11&-19,-10&-19,-9&-19,-8&-19,-7&-19,-6&-19,-5&-19,-4&-19,-3&-19,-2&-19,-1&-19,0&-19,0&-19,-1&-19,-2&-19,-3&-19,-4&-19,-5&-19,-6&-19,-7&-19,-8&-19,-9&-19,-10&-19,-11&-19,-12&-19,-13&-19,-14&-19,-15&-19,-16&-19,-17&-19,-18&-19,-19&-18,-19&-17,-19&-16,-19&-15,-19&-14,-19&-13,-19&-12,-19&-11,-19&-10,-19&-9,-19" +
"&-8,-19&-7,-19&-6,-19&-5,-19&-4,-19&-3,-19&-2,-19&-1,-19&0,-19&0,-18&0,-17&0,-16&0,-15&0,-14&0,-13&0,-12&0,-11&0,-10&0,-9&0,-8&0,-7&0,-6&0,-5&0,-4&0,-3&0,-2&0,-1&0,0&0,1&0,2&0,3&0,4&0,5&0,6&0,7&0,8&0,9&0,10&0,11&0,12&0,13&0,14&0,15&0,16&0,17&0,18&0,19&1,19&2,19&3,19&4,19&5,19&6,19&7,19&8,19&9,19&10,19&11,19&12,19&13,19&14,19&15,19&16,19&17,19&18,19&19,19&19,18&19,17&19,16&19,15&19,14&19,13&19,12&19,11&19,10&19,9&19,8&19,7&19,6&19,5&19,4&19,3&19,2&19,1&19,0&18,0&17,0&16,0&15,0&14,0&13,0&12,0&11,0" +
"&10,0&9,0&8,0&7,0&6,0&5,0&4,0&3,0&2,0&1,0&0,0&-1,0&-2,0&-3,0&-4,0&-5,0&-6,0&-7,0&-8,0&-9,0&-10,0&-11,0&-12,0&-13,0&-14,0&-15,0&-16,0&-17,0&-18,0&-19,0&-19,-1&-19,-2&-19,-3&-19,-4&-19,-5&-19,-6&-19,-7&-19,-8&-19,-9&-19,-10&-19,-11&-19,-12&-19,-13&-19,-14&-19,-15&-19,-16&-19,-17&-19,-18&-19,-19&-18,-19&-17,-19&-16,-19&-15,-19&-14,-19&-13,-19&-12,-19&-11,-19&-10,-19&-9,-19&-8,-19&-7,-19&-6,-19&-5,-19&-4,-19&-3,-19&-2,-19&-1,-19&0,-19&0,-19&-1,-19&-2,-19&-3,-19&-4,-19&-5,-19&-6,-19&-7,-19&-8,-19&-9," +
"-19&-10,-19&-11,-19&-12,-19&-13,-19&-14,-19&-15,-19&-16,-19&-17,-19&-18,-19&-19,-19&-19,-18&-19,-17&-19,-16&-19,-15&-19,-14&-19,-13&-19,-12&-19,-11&-19,-10&-19,-9&-19,-8&-19,-7&-19,-6&-19,-5&-19,-4&-19,-3&-19,-2&-19,-1&-19,0&-18,0&-17,0&-16,0&-15,0&-14,0&-13,0&-12,0&-11,0&-10,0&-9,0&-8,0&-7,0&-6,0&-5,0&-4,0&-3,0&-2,0&-1,0";
			var points = PointsData.Split('&').Select(Point.Parse).ToList();
			var color = new LightColor(0, 39,
				new List<int> { 0x000000 },
				new List<int> { 0x101010 },
				new List<int> { 0x100000, 0x001000 },
				new List<int> { 0x001000, 0x000010 },
				new List<int> { 0x000010, 0x100000 }
			);
			var funcs = new List<Func<Point, Point>>
			{
				point => new Point(point.X + 19, point.Y + 19),
				point => new Point(-point.Y + 76, point.X + 19),
				point => new Point(-point.X + 76, -point.Y + 76),
				point => new Point(point.Y + 19, -point.X + 76),
			};

			var segment = new Segment();
			var time = 0;
			for (var point = 0; point < points.Count; ++point)
			{
				segment.Clear(time);
				for (var ctr = 0; ctr < Length; ++ctr)
				{
					var add = points[(point + ctr) % points.Count];
					foreach (var func in funcs)
						foreach (var light in bodyLayout.GetPositionLights(func(add), 2, 2))
							segment.AddLight(light, time, color, ctr);
				}
				++time;
			}
			return segment;
		}

		Segment BeatPattern(out int time)
		{
			var beats = new List<int> { 0, 7560, 8269, 8978, 9686, 10395, 10868, 11340, 12049, 12758, 13466, 14175, 14648, 15120, 15829, 16538, 17010, 17719, 18428, 18900, 19609, 20318, 20790, 21499, 22208, 22680, 23389 };
			var segment = new Segment();
			var points = new List<Tuple<Point, Vector>>();
			var colors = new List<LightColor>
			{
				new LightColor(0, 1, new List<int> { 0x010000, 0x100000 }),
				new LightColor(0, 1, new List<int> { 0x000001, 0x000010 }),
				new LightColor(0, 1, new List<int> { 0x000100, 0x001000 }),
			};
			for (var square = 0; square < 49; square += 2)
			{
				var x = square % 7 * 19 - 19;
				var y = square / 7 * 19 - 19;
				points.Add(Tuple.Create(new Point(x, y), new Vector(1, 0)));
				points.Add(Tuple.Create(new Point(x + 19, y), new Vector(0, 1)));
				points.Add(Tuple.Create(new Point(x + 19, y + 19), new Vector(-1, 0)));
				points.Add(Tuple.Create(new Point(x, y + 19), new Vector(0, -1)));
			}
			var center = new Point(47.5, 47.5);
			var distances = bodyLayout.GetAllLights().ToDictionary(light => light, light => ((bodyLayout.GetLightPosition(light) - center).Length - 9.51314879522022).Round());
			for (time = 0; time < 1890 * 15; time += 10)
			{
				var useLights = new HashSet<int>();
				var pointTime = time * 19 / 1890 % 19;
				foreach (var tuple in points)
					foreach (var light in bodyLayout.GetPositionLights(tuple.Item1 + tuple.Item2 * pointTime, 2, 2))
						useLights.Add(light);

				foreach (var light in bodyLayout.GetAllLights())
				{
					var index = beats.BinarySearch(time - distances[light] * 30);
					if (index < 0)
						index = Math.Max(0, ~index - 1);
					segment.AddLight(light, time, colors[index % colors.Count], useLights.Contains(light) ? 1 : 0);
				}
			}
			return segment;
		}

		Segment BeatCircles()
		{
			var beatTimes = new List<double> { 0, 709, 1418, 2126, 2835, 3308, 3780, 4489, 5198, 5906, 6615, 7088, 7560, 8269, 8978, 9686, 10395, 10868, 11340, 12049, 12758, 13230 };
			var points = new List<Point> { new Point(0, 0), new Point(96, 0), new Point(96, 96), new Point(0, 96) };

			var colors = new List<int> { 0xff0000, 0x00ff00, 0x0000ff };
			var distances = bodyLayout.GetAllLights().ToDictionary(light => light, light => points.ToDictionary(point => point, point => (bodyLayout.GetLightPosition(light) - point).Length - 9));
			var segment = new Segment();
			for (var time = 0; time < 15120; time += 10)
				foreach (var light in bodyLayout.GetAllLights())
				{
					var color = 0x000000;
					for (var beatCtr = 0; beatCtr < beatTimes.Count; beatCtr++)
					{
						var lightDist = distances[light][points[beatCtr % points.Count]];
						var dist = time - lightDist * 10 - beatTimes[beatCtr];
						if ((dist >= 0) && (dist < 800))
							color = Helpers.AddColor(color, Helpers.MultiplyColor(colors[beatCtr % colors.Count], 1 - lightDist / 135.764501987817));
					}
					segment.AddLight(light, time, Helpers.MultiplyColor(color, 1d / 16));
				}
			return segment;
		}

		Segment Corners()
		{
			const int Size = 6;

			var color = new LightColor(9, 68, new List<IReadOnlyList<int>>
			{
				new List<int> { 0x101010, 0x010101 },
				new List<int> { 0x100000, 0x000010 },
				new List<int> { 0x000010, 0x001000 },
				new List<int> { 0x001000, 0x100000 },
				new List<int> { 0x001010, 0x100010, 0x001010, 0x100010 },
				Helpers.Rainbow6,
			});
			var segment = new Segment();
			var center = new Point(48, 48);
			var dist = bodyLayout.GetAllLights().ToDictionary(light => light, light => (bodyLayout.GetLightPosition(light) - center).Length.Round());
			for (var time = 0; time < 19; ++time)
			{
				segment.Clear(time);
				for (var ctr1 = 0; ctr1 < 97; ctr1 += 19)
					for (var ctr2 = 0; ctr2 < 7; ++ctr2)
					{
						var lights = new List<int>();
						lights.AddRange(bodyLayout.GetPositionLights(time - 18 - Size / 2 - Size % 2 + ctr2 * 19, ctr1, Size, 1));
						lights.AddRange(bodyLayout.GetPositionLights(1 - time - Size / 2 + ctr2 * 19, ctr1 + 1, Size, 1));
						lights.AddRange(bodyLayout.GetPositionLights(ctr1 + 1, time - 18 - Size / 2 - Size % 2 + ctr2 * 19, 1, Size));
						lights.AddRange(bodyLayout.GetPositionLights(ctr1, 1 - time - Size / 2 + ctr2 * 19, 1, Size));
						foreach (var light in lights)
							segment.AddLight(light, time, color, dist[light]);
					}
			}
			return segment;
		}

		Segment SquareCircles(out int time)
		{
			var segment = new Segment();
			var paths = new List<Tuple<double, double>>
			{
				Tuple.Create(19d, 0d),
				Tuple.Create(53.7401153701776d, 135d),
				Tuple.Create(38d, 0d),
				Tuple.Create(26.8700576850888d, 135d),
			};
			var color = new LightColor(0, 3, new List<IReadOnlyList<int>>
			{
				new List<int> { 0x101010, 0x100000, 0x001000, 0x000010 },
				new List<int> { 0x100000, 0x001000, 0x000010, 0x101010 },
				new List<int> { 0x001000, 0x000010, 0x101010, 0x100000 },
				new List<int> { 0x000010, 0x101010, 0x100000, 0x001000 },
			});
			time = 0;
			double distance, angle;
			for (var pathIndex = 0; pathIndex < paths.Count; ++pathIndex)
			{
				for (var step = 0; step <= 80; ++step)
				{
					var min = paths[pathIndex % paths.Count];
					var max = paths[(pathIndex + 1) % paths.Count];
					distance = (max.Item1 - min.Item1) * step / 80 + min.Item1;
					angle = (max.Item2 - min.Item2) * step / 80 + min.Item2;

					segment.Clear(time);
					for (var circle = 0; circle < 4; ++circle)
					{
						var useAngle = (angle + circle * 90) * Math.PI / 180;
						var center = new Point(distance * Math.Cos(useAngle) + 48, distance * Math.Sin(useAngle) + 48);
						foreach (var light in bodyLayout.GetAllLights())
							if ((bodyLayout.GetLightPosition(light) - center).LengthSquared < 200.5)
								segment.AddLight(light, time, color, circle);
					}
					time += step == 0 ? 20 : 1;
				}
			}

			return segment;
		}

		Segment BeatGrow()
		{
			var times = new List<int> { 0, 3, 6, 9, 12, 14, 16, 19, 22, 25, 28, 30, 32, 35, 38, 41, 44, 46, 48, 51, 54, 56 };

			var lights = bodyLayout.GetAllLights();
			var center = new Point(48, 48);
			var distances = lights.ToDictionary(light => light, light => 800 - (((bodyLayout.GetLightPosition(light) - center).Length - 9) * 13.5864371095928).Round());
			var buckets = distances.GroupBy(pair => Math.Min(pair.Value * times.Count / 800, times.Count - 1)).OrderByDescending(group => group.Key).Select(group => group.Select(x => x.Key).ToList()).ToList();
			buckets = buckets.TakeEach(4).Concat(buckets.Skip(2).TakeEach(4)).Concat(buckets.Skip(1).TakeEach(2)).ToList();

			var segment = new Segment();
			var color = new LightColor(0, 1600, Helpers.Rainbow6.Concat(Helpers.Rainbow6).Concat(Helpers.Rainbow6[0]).ToList());
			var active = new HashSet<int>();
			var timeIndex = 0;
			for (var time = 0; time < 64; ++time)
			{
				if ((timeIndex < times.Count) && (times[timeIndex] == time))
					buckets[timeIndex++].ForEach(light => active.Add(light));

				foreach (var light in lights)
					if (active.Contains(light))
					{
						var colorValue = time % 8 * 100 + distances[light];
						segment.AddLight(light, time, time + 1, color, colorValue, color, colorValue + 100, true);
					}
			}
			return segment;
		}

		Segment Blinds()
		{
			const int Size = 12;
			const int Border = 7;
			const int Speed = 30;
			var segment = new Segment();
			var color = new List<LightColor>
			{
				new LightColor(900, 6788, new List<IReadOnlyList<int>> { new List<int> { 0x101000, 0x001010 }, new List<int> { 0x100000, 0x000010 }, new List<int> { 0x100000, 0x100800, 0x101000, 0x001000, 0x000010, 0x090010 } }),
				new LightColor(900, 6788, new List<IReadOnlyList<int>> { new List<int> { 0x100010, 0x101000 }, new List<int> { 0x001000, 0x100000 }, new List<int> { 0x000010, 0x090010, 0x100000, 0x100800, 0x101000, 0x001000 } }),
				new LightColor(900, 6788, new List<IReadOnlyList<int>> { new List<int> { 0x001010, 0x100010 }, new List<int> { 0x000010, 0x001000 }, new List<int> { 0x101000, 0x001000, 0x000010, 0x090010, 0x100000, 0x100800 } }),
			};
			var center = new Point(48, 48);
			var lights = bodyLayout.GetAllLights();
			var distances = lights.ToDictionary(light => light, light => ((bodyLayout.GetLightPosition(light) - center).Length * 100).Round());
			for (var angle = 0; angle < 360; ++angle)
			{
				var useAngle = angle * Math.PI / 180;
				var cos = Math.Cos(useAngle);
				var sin = Math.Sin(useAngle);
				var dist = Size * 2 * Speed * angle / 360;

				segment.Clear(angle);
				foreach (var light in lights)
				{
					var point = bodyLayout.GetLightPosition(light);
					var xPos = ((point.X - 48) * cos - (point.Y - 48) * sin + 500).Round() + dist;
					if (xPos % (Size + Border) < Size)
						segment.AddLight(light, angle, color[xPos % ((Size + Border) * 6) / (Size + Border) / 2], distances[light]);
					//segment.AddLight(light, angle, color[xPos % (Size * 3) / Size], distances[light]);
				}
			}
			return segment;
		}

		public Song Render()
		{
			var song = new Song("orchestra.mp3"); // First sound is at 500; Measures start at 1720, repeat every 1890, and stop at 177490. Beats appear quantized to 1890/8 = 236.25

			// Lines (intro: 500; measures: 1720)
			var lines = Lines();
			song.AddSegment(lines, 259, 382, 500, 1220);
			song.AddSegment(lines, 0, 382, 1720, 1890 * 2, 4);
			song.AddPaletteChange(500, 0);
			song.AddPaletteChange(500, 1720, 1);
			song.AddPaletteChange(5000, 6000, 2);
			song.AddPaletteChange(8780, 9780, 3);
			song.AddPaletteChange(12560, 13560, 4);
			song.AddPaletteChange(16840, 0);

			// BeatPattern (16840)
			var beatPattern = BeatPattern(out int beatPatternTime);
			song.AddSegment(beatPattern, 0, beatPatternTime, 16840);

			// BeatCircles (45190)
			var beatCircles = BeatCircles();
			song.AddSegment(beatCircles, 0, 15120, 45190);

			// Corners (60310)
			var corners = Corners();
			song.AddSegment(corners, 0, 19, 60310, 1890, 12);
			song.AddPaletteChange(60310, 0);
			song.AddPaletteChange(63590, 64590, 1);
			song.AddPaletteChange(67370, 68370, 2);
			song.AddPaletteChange(71150, 72150, 3);
			song.AddPaletteChange(74930, 75930, 4);
			song.AddPaletteChange(78710, 79710, 5);
			song.AddPaletteChange(82990, 0);

			// SquareCircles (82990)
			var squareCircles = SquareCircles(out var squareCirclesTime);
			song.AddSegment(squareCircles, 100, 400, 82990, 1890 * 3);
			song.AddSegment(squareCircles, 0, 400, song.MaxTime(), 1890 * 4, 2);
			song.AddPaletteChange(82990, 0);
			new List<int> { 0, 709, 1418, 2126, 2835, 3308, 3780, 4489, 5198, 5906, 6615, 7088, 7560, 8269, 8978, 9450, 10159, 10868, 11340, 12049, 12758, 13230, 13939, 14648, 15120, 15829, 16538, 17010, 17719, 18428, 18900 }.Skip(1).Select(x => x + 82990).ForEach((x, index) => song.AddPaletteChange(x - 100, x + 100, (index + 1) % 4));
			song.AddPaletteChange(103780, 0);

			// BeatGrow (103780)
			var beatGrow = BeatGrow();
			song.AddSegment(beatGrow, 0, 64, 103780, 15120);

			// Blinds (118900)
			var blinds = Blinds();
			song.AddSegment(blinds, 0, 360, 118900, 1890 * 6, 2);
			song.AddPaletteChange(118900, 0);
			song.AddPaletteChange(125960, 126960, 1);
			song.AddPaletteChange(133520, 134520, 2);
			song.AddPaletteChange(141580, 0);

			// Next (141580)

			return song;
		}
	}
}
