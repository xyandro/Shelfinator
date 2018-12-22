using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Popular : ISong
	{
		public int SongNumber => 5;

		const double Brightness = 1f / 16;
		readonly Layout bodyLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");
		readonly Layout squaresLayout = new Layout("Shelfinator.Creator.Songs.Layout.Squares.png");
		readonly Layout headerLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Header.png");

		Segment PulseSquares()
		{
			var squares = Enumerable.Range(1, 25).Select(square => squaresLayout.GetLightPositions(square)).ToList();
			var centers = squares.Select(square => new Point((square.Min(p => p.X) + square.Max(p => p.X)) / 2, (square.Min(p => p.Y) + square.Max(p => p.Y)) / 2)).ToList();

			var colorData = new List<List<List<int>>>
			{
				new List<List<int>>
				{
					new List<int> { Helpers.MultiplyColor(0xff0000, Brightness), Helpers.MultiplyColor(0xff0000, Brightness / 4) },
					new List<int> { Helpers.MultiplyColor(0x0000ff, Brightness), Helpers.MultiplyColor(0x0000ff, Brightness / 4) },
					new List<int> { Helpers.MultiplyColor(0x00ff00, Brightness), Helpers.MultiplyColor(0x00ff00, Brightness / 4) },
					new List<int> { Helpers.MultiplyColor(0x0000ff, Brightness), Helpers.MultiplyColor(0x0000ff, Brightness / 4) },
				},
				new List<List<int>>
				{
					new List<int> { Helpers.MultiplyColor(0x0000ff, Brightness), Helpers.MultiplyColor(0x0000ff, Brightness / 4) },
					new List<int> { Helpers.MultiplyColor(0xff0000, Brightness), Helpers.MultiplyColor(0xff0000, Brightness / 4) },
					new List<int> { Helpers.MultiplyColor(0x0000ff, Brightness), Helpers.MultiplyColor(0x0000ff, Brightness / 4) },
					new List<int> { Helpers.MultiplyColor(0x00ff00, Brightness), Helpers.MultiplyColor(0x00ff00, Brightness / 4) }
				},
			};

			var colors = colorData.Select(li => new LightColor(0, 200, li.Select(l => l.Concat(l).Concat(l.Take(1)).ToList()).ToList())).ToList();

			var segment = new Segment();
			for (var square = 0; square < squares.Count; ++square)
				foreach (var point in squares[square])
				{
					var dist = (((point - centers[square]).Length - 9) * 26.8245951374785).Round();
					var color = colors[square % 2];
					segment.AddLight(bodyLayout.GetPositionLight(point), 0, 100, color, dist, color, dist + 100, true);
				}

			return segment;
		}

		class MoveSquaresItem
		{
			public Point position, destination;
			public bool horizontal;

			public MoveSquaresItem(Point position, Point destination, bool horizontal)
			{
				this.position = position;
				this.destination = destination;
				this.horizontal = horizontal;
			}

			public void Move(List<int> movePhase)
			{
				if (!horizontal)
				{
					position = new Point(position.Y, position.X);
					destination = new Point(destination.Y, destination.X);
				}
				DoMove(movePhase);
				if (!horizontal)
				{
					position = new Point(position.Y, position.X);
					destination = new Point(destination.Y, destination.X);
				}
			}

			void DoMove(List<int> movePhase)
			{
				if (Done)
					return;
				if (position.Y != destination.Y)
				{
					var moveOffset = movePhase[position.X.Round()];
					if (moveOffset != 0)
					{
						position = new Point(position.X + moveOffset, position.Y);
						return;
					}
					position = new Point(position.X, position.Y + (destination.Y > position.Y ? 1 : -1));
					return;
				}
				position = new Point(position.X + (destination.X > position.X ? 1 : -1), position.Y);
				return;
			}

			public bool Done => position == destination;
		}

		void MoveSquaresAddPoints(List<List<MoveSquaresItem>> phases, Point point1, Point point2, Point point3, Size size, bool horizontal)
		{
			for (var x = 0; x < size.Width.Round(); ++x)
				for (var y = 0; y < size.Height.Round(); ++y)
				{
					phases[0].Add(new MoveSquaresItem(new Point(point1.X + x, point1.Y + y), new Point(point2.X + x, point2.Y + y), horizontal));
					phases[1].Add(new MoveSquaresItem(new Point(point2.X + x, point2.Y + y), new Point(point3.X + x, point3.Y + y), horizontal));
				}
		}

		List<Segment> MoveSquares()
		{
			var movePhase = new List<List<int>>
			{
				new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
				new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
			};

			var result = new List<Segment>();
			var phases = new List<List<MoveSquaresItem>> { new List<MoveSquaresItem>(), new List<MoveSquaresItem>() };

			MoveSquaresAddPoints(phases, new Point(0, 0), new Point(19, 19), new Point(38, 38), new Size(19, 1), true);
			MoveSquaresAddPoints(phases, new Point(20, 0), new Point(20, 19), new Point(39, 38), new Size(18, 1), true);
			MoveSquaresAddPoints(phases, new Point(39, 0), new Point(39, 19), new Point(39, 38), new Size(18, 1), true);
			MoveSquaresAddPoints(phases, new Point(58, 0), new Point(58, 19), new Point(39, 38), new Size(18, 1), true);
			MoveSquaresAddPoints(phases, new Point(77, 0), new Point(58, 19), new Point(39, 38), new Size(19, 1), true);
			MoveSquaresAddPoints(phases, new Point(0, 95), new Point(19, 76), new Point(38, 57), new Size(19, 1), true);
			MoveSquaresAddPoints(phases, new Point(20, 95), new Point(20, 76), new Point(39, 57), new Size(18, 1), true);
			MoveSquaresAddPoints(phases, new Point(39, 95), new Point(39, 76), new Point(39, 57), new Size(18, 1), true);
			MoveSquaresAddPoints(phases, new Point(58, 95), new Point(58, 76), new Point(39, 57), new Size(18, 1), true);
			MoveSquaresAddPoints(phases, new Point(77, 95), new Point(58, 76), new Point(39, 57), new Size(19, 1), true);
			MoveSquaresAddPoints(phases, new Point(0, 0), new Point(19, 19), new Point(38, 38), new Size(1, 19), false);
			MoveSquaresAddPoints(phases, new Point(0, 20), new Point(19, 20), new Point(38, 39), new Size(1, 18), false);
			MoveSquaresAddPoints(phases, new Point(0, 39), new Point(19, 39), new Point(38, 39), new Size(1, 18), false);
			MoveSquaresAddPoints(phases, new Point(0, 58), new Point(19, 58), new Point(38, 39), new Size(1, 18), false);
			MoveSquaresAddPoints(phases, new Point(0, 77), new Point(19, 58), new Point(38, 39), new Size(1, 19), false);
			MoveSquaresAddPoints(phases, new Point(95, 0), new Point(76, 19), new Point(57, 38), new Size(1, 19), false);
			MoveSquaresAddPoints(phases, new Point(95, 20), new Point(76, 20), new Point(57, 39), new Size(1, 18), false);
			MoveSquaresAddPoints(phases, new Point(95, 39), new Point(76, 39), new Point(57, 39), new Size(1, 18), false);
			MoveSquaresAddPoints(phases, new Point(95, 58), new Point(76, 58), new Point(57, 39), new Size(1, 18), false);
			MoveSquaresAddPoints(phases, new Point(95, 77), new Point(76, 58), new Point(57, 39), new Size(1, 19), false);

			var color = new LightColor(
				new List<int> { 0xffffff }.Multiply(Brightness).ToList(),
				new List<int> { 0x995f3d }.Multiply(Brightness).ToList(),
				new List<int> { 0xc73b32 }.Multiply(Brightness).ToList(),
				new List<int> { 0xcc66cc }.Multiply(Brightness).ToList(),
				new List<int> { 0x0ac832 }.Multiply(Brightness).ToList(),
				new List<int> { 0x32b4ff }.Multiply(Brightness).ToList(),
				new List<int> { 0xffb400 }.Multiply(Brightness).ToList(),
				new List<int> { 0xffe6d2 }.Multiply(Brightness).ToList()
			);

			for (var phase = 0; phase < phases.Count; phase++)
			{
				var segment = new Segment();
				var time = 0;
				while (true)
				{
					var done = true;
					foreach (var point in phases[phase])
					{
						if (!point.Done)
							done = false;

						var lights = bodyLayout.GetPositionLights(point.position, 2, 2);
						foreach (var light in lights)
						{
							segment.AddLight(light, time, color);
							segment.AddLight(light, time + 1, Segment.Absolute, 0x000000);
						}
						point.Move(movePhase[phase]);
					}
					++time;

					if (done)
						break;
				}

				result.Add(segment);
			}

			return result;
		}

		Segment SyncSquares()
		{
			var segment = new Segment();
			var color = new LightColor(0, 5, Helpers.Rainbow6.Multiply(Brightness).ToList());
			var order = new List<int> { 1, 5, 25, 21, 8, 14, 18, 12, 3, 15, 23, 11, 7, 9, 19, 17, 2, 10, 24, 16, 4, 20, 22, 6, 13 };
			var times = new List<int> { 29588, 30194, 30649, 30800, 31103, 31406, 31709, 32012, 32618, 33073, 33224, 33527, 33830, 34133, 34436, 35042, 35497, 35648, 35951, 36254, 36557, 36860, 37466, 37769, 38224, 38375, 38678, 38981, 39284, 39890, 40496, 40799, 41102, 41405, 41708, 42314, 42920, 43223, 43526, 43829, 44132, 44738, 45193, 45344, 45647, 45950, 46253, 46556, 47162, 47465, 47920, 48071 };
			for (var ctr = 0; ctr < times.Count; ctr++)
			{
				var lights = bodyLayout.GetMappedLights(squaresLayout, order[ctr % 25]);
				foreach (var light in lights)
					segment.AddLight(light, times[ctr], times[ctr] + 4848, color, ctr % 6, Segment.Absolute, 0x000000);
			}
			return segment;
		}

		Segment RainbowSquare()
		{
			var segment = new Segment();
			var color = new LightColor(0, 192,
				new List<int> { 0xff0000, 0x00ff00, 0xff0000, 0x00ff00, 0xff0000, 0x00ff00, 0xff0000, 0x00ff00, 0xff0000 }.Multiply(Brightness).ToList(),
				new List<int> { 0x00ffff, 0x00ffff, 0xff00ff, 0xff00ff, 0x00ffff, 0x00ffff, 0xff00ff, 0xff00ff, 0x00ffff }.Multiply(Brightness).ToList(),
				Helpers.Rainbow6.Concat(Helpers.Rainbow6).Concat(Helpers.Rainbow6.Take(1)).Multiply(Brightness).ToList()
			);
			for (var ctr = 0; ctr < 97; ++ctr)
			{
				foreach (var light in bodyLayout.GetPositionLights(19, ctr, 2, 1))
					segment.AddLight(light, 0, 1000, color, ctr, color, ctr + 96, true);
				foreach (var light in bodyLayout.GetPositionLights(ctr, 19, 1, 2))
					segment.AddLight(light, 0, 1000, color, ctr, color, ctr + 96, true);
				foreach (var light in bodyLayout.GetPositionLights(76, 96 - ctr, 2, 1))
					segment.AddLight(light, 0, 1000, color, ctr, color, ctr + 96, true);
				foreach (var light in bodyLayout.GetPositionLights(96 - ctr, 76, 1, 2))
					segment.AddLight(light, 0, 1000, color, ctr, color, ctr + 96, true);

				if (((ctr < 17) || (ctr >= 23)) && ((ctr < 74) || (ctr >= 80)))
				{
					foreach (var light in bodyLayout.GetPositionLights(96 - ctr, 0, 1, 2))
						segment.AddLight(light, 0, 1000, color, ctr, color, ctr + 96, true);
					foreach (var light in bodyLayout.GetPositionLights(0, 96 - ctr, 2, 1))
						segment.AddLight(light, 0, 1000, color, ctr, color, ctr + 96, true);
					foreach (var light in bodyLayout.GetPositionLights(ctr, 95, 1, 2))
						segment.AddLight(light, 0, 1000, color, ctr, color, ctr + 96, true);
					foreach (var light in bodyLayout.GetPositionLights(95, ctr, 2, 1))
						segment.AddLight(light, 0, 1000, color, ctr, color, ctr + 96, true);
				}

				if ((ctr >= 23) && (ctr < 74))
				{
					foreach (var light in bodyLayout.GetPositionLights(38, 96 - ctr, 2, 1))
						segment.AddLight(light, 0, 1000, color, ctr, color, ctr + 96, true);
					foreach (var light in bodyLayout.GetPositionLights(96 - ctr, 38, 1, 2))
						segment.AddLight(light, 0, 1000, color, ctr, color, ctr + 96, true);
					foreach (var light in bodyLayout.GetPositionLights(57, ctr, 2, 1))
						segment.AddLight(light, 0, 1000, color, ctr, color, ctr + 96, true);
					foreach (var light in bodyLayout.GetPositionLights(ctr, 57, 1, 2))
						segment.AddLight(light, 0, 1000, color, ctr, color, ctr + 96, true);
				}

				if (ctr < 17)
				{
					foreach (var light in bodyLayout.GetPositionLights(ctr, 38, 1, 2))
						segment.AddLight(light, 0, 1000, color, ctr + 57, color, ctr + 96 + 57, true);
					foreach (var light in bodyLayout.GetPositionLights(ctr, 57, 1, 2))
						segment.AddLight(light, 0, 1000, color, ctr + 38, color, ctr + 96 + 38, true);
					foreach (var light in bodyLayout.GetPositionLights(96 - ctr, 38, 1, 2))
						segment.AddLight(light, 0, 1000, color, ctr + 38, color, ctr + 96 + 38, true);
					foreach (var light in bodyLayout.GetPositionLights(96 - ctr, 57, 1, 2))
						segment.AddLight(light, 0, 1000, color, ctr + 57, color, ctr + 96 + 57, true);
					foreach (var light in bodyLayout.GetPositionLights(38, ctr, 2, 1))
						segment.AddLight(light, 0, 1000, color, ctr + 57, color, ctr + 96 + 57, true);
					foreach (var light in bodyLayout.GetPositionLights(57, ctr, 2, 1))
						segment.AddLight(light, 0, 1000, color, ctr + 38, color, ctr + 96 + 38, true);
					foreach (var light in bodyLayout.GetPositionLights(38, 96 - ctr, 2, 1))
						segment.AddLight(light, 0, 1000, color, ctr + 38, color, ctr + 96 + 38, true);
					foreach (var light in bodyLayout.GetPositionLights(57, 96 - ctr, 2, 1))
						segment.AddLight(light, 0, 1000, color, ctr + 57, color, ctr + 96 + 57, true);
				}
			}

			return segment;
		}

		List<Tuple<int, int, bool>> CalculateMaze(Random rand)
		{
			var walls = new List<Tuple<int, int>>();
			for (var y = 0; y < 6; ++y)
				for (var x = 0; x < 6; ++x)
				{
					if (x != 5)
						walls.Add(Tuple.Create(x + y * 6, x + 1 + y * 6)); // Horizontal
					if (y != 5)
						walls.Add(Tuple.Create(x + y * 6, x + (y + 1) * 6)); // Vertical
				}


			var cellWalls = Enumerable.Range(0, 36).Select(i => new List<Tuple<int, int>>()).ToList();
			foreach (var wall in walls)
			{
				cellWalls[wall.Item1].Add(wall);
				cellWalls[wall.Item2].Add(wall);
			}

			var visited = Enumerable.Repeat(false, 36).ToList();
			var wallList = new HashSet<Tuple<int, int>>();
			var broken = new List<Tuple<int, int>>();
			var first = true;
			while ((first) || (wallList.Any()))
			{
				int cell;
				if (first)
				{
					cell = rand.Next(36);
					first = false;
				}
				else
				{
					var wall = wallList.OrderBy(x => rand.Next()).First();
					wallList.Remove(wall);
					if (visited[wall.Item1] == visited[wall.Item2])
						continue;

					broken.Add(wall);
					cell = visited[wall.Item1] ? wall.Item2 : wall.Item1;
				}

				visited[cell] = true;
				foreach (var cellWall in cellWalls[cell])
					wallList.Add(cellWall);
			}

			return walls.Select(wall => Tuple.Create(wall.Item1, wall.Item2, broken.Contains(wall))).ToList();
		}

		bool rSolveMaze(int cell, Dictionary<int, List<int>> nextCells, List<int> path)
		{
			path.Add(cell);

			if (cell == 35)
				return true;

			foreach (var nextCell in nextCells[cell])
				if (!path.Contains(nextCell))
					if (rSolveMaze(nextCell, nextCells, path))
						return true;

			path.RemoveAt(path.Count - 1);
			return false;
		}

		List<int> SolveMaze(List<Tuple<int, int, bool>> walls)
		{
			var nextCells = walls.Where(tuple => tuple.Item3).SelectMany(tuple => new List<Tuple<int, int>> { Tuple.Create(tuple.Item1, tuple.Item2), Tuple.Create(tuple.Item2, tuple.Item1) }).Where(tuple => tuple.Item1 != tuple.Item2).GroupBy(tuple => tuple.Item1).ToDictionary(group => group.Key, group => group.Select(tuple => tuple.Item2).ToList());
			var path = new List<int>();
			rSolveMaze(0, nextCells, path);
			return path;
		}

		Segment Maze(int seed)
		{
			var segment = new Segment();

			// Header
			const string header = "0,0&1,0&5,0&6,0&0,1&1,1&2,1&4,1&5,1&6,1&0,2&1,2&2,2&3,2&4,2&5,2&6,2&0,3&1,3&3,3&5,3&6,3&0,4&1,4&5,4&6,4&0,5&1,5&5,5&6,5&0,6&1,6&5,6&6,6&0,7&1,7&5,7&6,7&11,0&10,1&11,1&12,1&9,2&10,2&11,2&12,2&13,2&8,3&9,3&13,3&14,3&8,4&9,4&10,4&11,4&12,4&13,4&14,4&8,5&9,5&10,5&11,5&12,5&13,5&14,5&8,6&9,6&13,6&14,6&8,7&9,7&13,7&14,7&16,0&17,0&18,0&19,0&20,0&21,0&16,1&17,1&18,1&19,1&20,1&21,1&19,2&20,2&18,3&19,3&17,4&18,4&16,5&17,5&16,6&17,6&18,6&19,6&20,6&21,6&16,7&17,7&18,7&19,7&20,7&21,7&23,0&24,0&25,0&26,0&27,0&28,0&23,1&24,1&25,1&26,1&27,1&28,1&23,2&24,2&23,3&24,3&25,3&26,3&23,4&24,4&25,4&26,4&23,5&24,5&23,6&24,6&25,6&26,6&27,6&28,6&23,7&24,7&25,7&26,7&27,7&28,7&30,0&31,0&30,1&31,1&30,2&31,2&30,3&31,3&30,4&31,4&30,6&31,6&30,7&31,7";
			var headerColor = new LightColor(70, 1589, new List<int> { 0x00ffff, 0x0000ff }.Multiply(Brightness).ToList());
			var headerCenter = new Point(15.5, 3.5);
			header.Split('&').Select(Point.Parse).ForEach(point => segment.AddLight(headerLayout.GetPositionLight(point), 0, headerColor, ((point - headerCenter).Length * 100).Round()));

			foreach (var light in bodyLayout.GetAllLights())
				segment.AddLight(light, 0, Segment.Absolute, 0x010101);

			var rand = new Random(seed);
			var walls = CalculateMaze(rand);
			foreach (var wall in walls)
			{
				if (wall.Item3)
					continue;

				var wallX = (wall.Item1 % 6) * 19;
				var wallY = (wall.Item1 / 6) * 19;
				if (wall.Item1 + 1 == wall.Item2)
					wallX += rand.Next(2, 17);
				else
					wallY += rand.Next(2, 17);

				foreach (var light in bodyLayout.GetPositionLights(wallX, wallY, 2, 2))
					segment.AddLight(light, 0, Segment.Absolute, Helpers.MultiplyColor(0xff0000, Brightness));
			}
			foreach (var light in bodyLayout.GetPositionLights(0, 0, 2, 2))
				segment.AddLight(light, 0, Segment.Absolute, Helpers.MultiplyColor(0x00ff00, Brightness));
			foreach (var light in bodyLayout.GetPositionLights(95, 95, 2, 2))
				segment.AddLight(light, 0, Segment.Absolute, Helpers.MultiplyColor(0x0000ff, Brightness));

			int time = 1, x = 0, y = 0;
			var solution = SolveMaze(walls);
			foreach (var step in solution)
			{
				var newX = (step % 6) * 19;
				var newY = (step / 6) * 19;
				var xOfs = newX.CompareTo(x);
				var yOfs = newY.CompareTo(y);
				while ((x != newX) || (y != newY))
				{
					x += xOfs;
					y += yOfs;
					foreach (var light in bodyLayout.GetPositionLights(x, y, 2, 2))
						segment.AddLight(light, time, Segment.Absolute, Helpers.MultiplyColor(0x00ff00, Brightness));
					++time;
				}
			}

			return segment;
		}

		class CrowdPerson
		{
			public Point Position { get; set; }
			public Point Destination { get; }
			public int Color { get; }

			public CrowdPerson(Point position, int color)
			{
				Position = position;
				Destination = position;
				Color = Helpers.MultiplyColor(color, Brightness);
			}
		}

		Segment Crowd()
		{
			const int Loops = 4;
			const int LoopDelay = 250;
			const int StartDelay = LoopDelay / 8;
			var colors = new List<int> { 0x80ff40, 0x0000ff, 0xffc000, 0x00ff80, 0xc040ff, 0xff8000, 0x0040ff, 0x80ff80, 0xffff00, 0x8000ff, 0xff00c0, 0xff4080, 0xc0ffff, 0x80c0ff, 0x40ffff, 0xff4000, 0x4040ff, 0x80ffff, 0x40ff00, 0xff00ff, 0xc080ff, 0xffc080, 0x40c0ff, 0xff40c0, 0xff80c0, 0xc0ff00, 0x00ff40, 0xc0ff40, 0x80ff00, 0x8080ff, 0xffff40, 0xc0ffc0, 0x00ffff, 0xff80ff, 0x40ff40, 0xffc0ff };
			var color = 0;
			var correct = colors.ToDictionary(c => c, c => new List<Point>());
			var people = new List<CrowdPerson>();
			for (var y = 0; y < 97; y += 19)
				for (var x = 0; x < 97; x += 19)
				{
					for (var height = 0; height < 2; ++height)
						for (var width = 0; width < 2; ++width)
							people.Add(new CrowdPerson(new Point(x + width, y + height), colors[color]));
					++color;
				}

			var segment = new Segment();
			var rand = new Random(0xf00d);
			var time = 0;
			for (var loop = 0; loop < Loops; ++loop)
			{
				var newPositions = people.Select(x => x.Position).OrderBy(x => rand.Next()).ToList();

				for (var ctr = 0; ctr < people.Count; ctr++)
					people[ctr].Position = newPositions[ctr];

				var next = StartDelay;
				while (true)
				{
					segment.Clear(time);

					var done = true;
					foreach (var person in people)
					{
						segment.AddLight(bodyLayout.GetPositionLight(person.Position), time, Segment.Absolute, person.Color);
						if (person.Position != person.Destination)
						{
							done = false;
							person.Position = SongHelper.NextLocation(person.Position, person.Destination);
						}
						segment.AddLight(bodyLayout.GetPositionLight(person.Position), time + next, Segment.Absolute, person.Color);
					}
					time += next;
					next = 1;
					if (done)
						break;
				}

				time = (time + LoopDelay - 1) / LoopDelay * LoopDelay;
			}
			return segment;
		}

		Segment Stairs()
		{
			var segment = new Segment();
			var squares = Enumerable.Range(1, 25).Select(square => squaresLayout.GetLightPositions(square)).ToList();
			var centers = squares.Select(square => new Point((square.Min(p => p.X) + square.Max(p => p.X)) / 2, (square.Min(p => p.Y) + square.Max(p => p.Y)) / 2)).ToList();
			var color = new LightColor(-75, 75,
				new List<int> { 0x000000, 0xffffff, 0x000000 }.Multiply(Brightness).ToList(),
				new List<int> { 0x000000, 0xff0080, 0xff8000, 0xff8000, 0xff0080, 0x000000 }.Multiply(Brightness).ToList(),
				new List<int> { 0x000000, 0x00ffff, 0xff00ff, 0xff00ff, 0x00ffff, 0x000000 }.Multiply(Brightness).ToList(),
				new List<int> { 0x000000 }.Multiply(Brightness).ToList()
			);
			for (var angle = 0; angle < 360; angle += 5)
				for (var squareCtr = 0; squareCtr < squares.Count; ++squareCtr)
					foreach (var point in squares[squareCtr])
					{
						var p = point - centers[squareCtr];
						var squareOffset = squareCtr % 2 == 0 ? 180 : 0;
						var atAngle = Math.Atan2(p.Y, p.X) / Math.PI * 180 - angle + squareOffset;
						while (atAngle <= -180)
							atAngle += 360;
						while (atAngle >= 180)
							atAngle -= 360;
						segment.AddLight(bodyLayout.GetPositionLight(point), angle, color, atAngle.Round());
					}
			return segment;
		}

		public Song Render()
		{
			var song = new Song("popular.wav");

			// PulseSquares (500)
			Emulator.TestPosition = 500;
			var pulseSquares = PulseSquares();
			song.AddSegmentWithRepeat(pulseSquares, 0, 100, 500, 2424, 4);
			song.AddPaletteSequence(500, 0);
			song.AddPaletteSequence(2924, 1);
			song.AddPaletteSequence(5348, 2);
			song.AddPaletteSequence(7772, 3);
			song.AddPaletteSequence(10196, 0);

			// MoveSquares (10196)
			var moveSquares = MoveSquares();
			song.AddSegmentWithRepeat(moveSquares[0], 0, moveSquares[0].MaxTime(), 10196, 2424);
			song.AddSegmentWithRepeat(moveSquares[1], 0, moveSquares[1].MaxTime(), song.MaxTime(), 2424);
			song.AddSegmentWithRepeat(moveSquares[1], moveSquares[1].MaxTime(), 0, song.MaxTime(), 2424);
			song.AddSegmentWithRepeat(moveSquares[0], moveSquares[0].MaxTime(), 0, song.MaxTime(), 2424);
			song.AddSegmentWithRepeat(moveSquares[0], 0, moveSquares[0].MaxTime(), song.MaxTime(), 2424);
			song.AddSegmentWithRepeat(moveSquares[1], 0, moveSquares[1].MaxTime(), song.MaxTime(), 2424);
			song.AddSegmentWithRepeat(moveSquares[1], moveSquares[1].MaxTime(), 0, song.MaxTime(), 2424);
			song.AddSegmentWithRepeat(moveSquares[0], moveSquares[0].MaxTime(), 0, song.MaxTime(), 2424);
			song.AddPaletteSequence(10196, 0);
			song.AddPaletteSequence(12120, 13120, null, 1);
			song.AddPaletteSequence(14544, 15544, null, 2);
			song.AddPaletteSequence(16968, 17968, null, 3);
			song.AddPaletteSequence(19392, 20392, null, 4);
			song.AddPaletteSequence(21816, 22816, null, 5);
			song.AddPaletteSequence(24240, 25240, null, 6);
			song.AddPaletteSequence(26664, 27664, null, 7);
			song.AddPaletteSequence(29588, 0);

			// SyncSquares (29588)
			var syncSquares = SyncSquares();
			song.AddSegmentWithRepeat(syncSquares, 29588, 48980, 29588, 19392);

			// RainbowSquare (48980)
			var rainbowSquare = RainbowSquare();
			song.AddSegmentWithRepeat(rainbowSquare, 0, 1000, 48980, 2424, 12);
			song.AddPaletteSequence(48980, 0);
			song.AddPaletteSequence(58176, 59176, null, 1);
			song.AddPaletteSequence(67872, 68872, null, 2);
			song.AddPaletteSequence(78068, 0);

			// Maze (78068)
			var maze = Maze(0); // Solution length: 17
			song.AddSegmentWithRepeat(maze, 0, 0, 78068, 7272);
			song.AddSegmentWithRepeat(maze, 1, maze.MaxTime() + 1, song.MaxTime(), 2424);
			song.AddSegmentWithRepeat(maze, maze.MaxTime() + 1, maze.MaxTime() + 1, song.MaxTime(), 2424);
			maze = Maze(2003); // Solution length: 19
			song.AddSegmentWithRepeat(maze, 0, 0, song.MaxTime(), 7272);
			song.AddSegmentWithRepeat(maze, 1, maze.MaxTime() + 1, song.MaxTime(), 2424);
			song.AddSegmentWithRepeat(maze, maze.MaxTime() + 1, maze.MaxTime() + 1, song.MaxTime(), 2424);
			maze = Maze(32648); // Solution length: 23
			song.AddSegmentWithRepeat(maze, 0, 0, song.MaxTime(), 7272);
			song.AddSegmentWithRepeat(maze, 1, maze.MaxTime() + 1, song.MaxTime(), 2424);
			song.AddSegmentWithRepeat(maze, maze.MaxTime() + 1, maze.MaxTime() + 1, song.MaxTime(), 2424);
			maze = Maze(3062193); // Solution length: 25
			song.AddSegmentWithRepeat(maze, 0, 0, song.MaxTime(), 7272);
			song.AddSegmentWithRepeat(maze, 1, maze.MaxTime() + 1, song.MaxTime(), 2424);
			song.AddSegmentWithRepeat(maze, maze.MaxTime() + 1, maze.MaxTime() + 1, song.MaxTime(), 2424);

			// Crowd (126548)
			var crowd = Crowd();
			song.AddSegmentWithRepeat(crowd, 0, 250 * 4, 126548, 2424 * 4 * 4);

			// Stairs (165332)
			var stairs = Stairs();
			song.AddSegmentWithRepeat(stairs, 0, 360, 165332, 2424, 13);
			song.AddPaletteSequence(165332, 0);
			song.AddPaletteSequence(174528, 175528, null, 1);
			song.AddPaletteSequence(184224, 185224, null, 2);
			song.AddPaletteSequence(195026, 195632, null, 3);
			song.AddPaletteSequence(196844, 0);

			return song;
		}
	}
}
