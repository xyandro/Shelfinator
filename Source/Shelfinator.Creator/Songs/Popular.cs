using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Popular : SongCreator
	{
		public override int SongNumber => 5;

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
					new List<int> { 0x100000, 0x020000 },
					new List<int> { 0x000010, 0x000002 },
					new List<int> { 0x001000, 0x000200 },
					new List<int> { 0x000010, 0x000002 },
				},
				new List<List<int>>
				{
					new List<int> { 0x000010, 0x000002 },
					new List<int> { 0x100000, 0x020000 },
					new List<int> { 0x000010, 0x000002 },
					new List<int> { 0x001000, 0x000200 }
				},
			};

			var colors = colorData.Select(li => new LightColor(0, 200, li.Select(l => l.Concat(l).Concat(l.Take(1)).ToList()).Cast<IReadOnlyList<int>>().ToList())).ToList();

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
				new List<int> { 0x101010 },
				new List<int> { 0x0a0604 },
				new List<int> { 0x0c0403 },
				new List<int> { 0x0d060d },
				new List<int> { 0x010d03 },
				new List<int> { 0x030b10 },
				new List<int> { 0x100b00 },
				new List<int> { 0x100e0d }
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
							segment.AddLight(light, time, color, 0);
							segment.AddLight(light, time + 1, 0x000000);
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
			var color = new LightColor(0, 5, Helpers.Rainbow6);
			var order = "1,5,25,21,8,14,18,12,3,15,23,11,7,9,19,17,2,10,24,16,4,20,22,6,13,1,5,25,21,8,14,18,12,3,15,23,11,7,9,19,17,2,10,24,16,4,20,22,6,13,1&5&21&25".Split(',').Select(x => x.Split('&').Select(int.Parse).ToList()).ToList();
			var times = "29588,30194,30649,30800,31103,31406,31709,32012,32618,33073,33224,33527,33830,34133,34436,35042,35497,35648,35951,36254,36557,36860,37466,37769,38224,38375,38678,38981,39284,39890,40496,40799,41102,41405,41708,42314,42920,43223,43526,43829,44132,44738,45344,45647,45950,46253,46556,47162,47465,47920,48071".Split(',').Select(int.Parse).ToList();
			var onColor = -1;
			for (var ctr = 0; ctr < times.Count; ctr++)
				foreach (var square in order[ctr])
				{
					++onColor;
					var lights = bodyLayout.GetMappedLights(squaresLayout, square);
					foreach (var light in lights)
						segment.AddLight(light, times[ctr], times[ctr] + 606, color, onColor % 6, 0x000000);
				}
			return segment;
		}

		Segment RainbowSquare()
		{
			var segment = new Segment();
			var color = new LightColor(0, 192,
				new List<int> { 0x000010, 0x000004, 0x000004, 0x000010, 0x000004, 0x000004, 0x000010 },
				new List<int> { 0x100000, 0x001000, 0x100000, 0x001000, 0x100000, 0x001000, 0x100000, 0x001000, 0x100000 },
				new List<int> { 0x100010, 0x001010, 0x100010, 0x001010, 0x100010, 0x001010, 0x100010, 0x001010, 0x100010 },
				Helpers.Rainbow6.Concat(Helpers.Rainbow6).Concat(Helpers.Rainbow6.Take(1)).ToList()
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

		Segment SineShine()
		{
			var segment = new Segment();
			var lightColor = new LightColor(0, 291, new List<IReadOnlyList<int>> {
				new List<int> { 0x001000, 0x101010, 0x001000, 0x101010, 0x001000, 0x101010, 0x001000 },
				new List<int> { 0x101000, 0x000010, 0x101000, 0x000010, 0x101000, 0x000010, 0x101000 },
				Helpers.Rainbow6.Concat(Helpers.Rainbow6).Concat(Helpers.Rainbow6).Concat(Helpers.Rainbow6.Take(1)).ToList(),
			});
			for (var x = 0; x < 97; ++x)
				for (var y = 0; y < 97; ++y)
				{
					foreach (var light in bodyLayout.GetPositionLights(x, y, 1, 1))
					{
						var yVal = (Math.Abs(48 - x) + Math.Sin(y * Math.PI / 180 * 360 / 96) * 48).Round();
						segment.AddLight(light, 0, 1, lightColor, yVal + 97 * 2, lightColor, yVal + 97, true);
					}
				}
			return segment;
		}

		Segment Wavey()
		{
			var segment = new Segment();
			var colors = new List<int> { 0x101010, 0x001000, 0x100010, 0x100000, 0x001010, 0x000010, 0x101000, 0x0f0008 };
			var lightColor = new LightColor(0, 291, colors.Select(x => new List<int> { x, 0x000000, 0x000000, 0x000000, x, 0x000000, 0x000000, 0x000000, x, 0x000000, 0x000000, 0x000000, x }).Cast<IReadOnlyList<int>>().ToList());
			for (var x = 0; x < 97; ++x)
				for (var y = 0; y < 97; y += 19)
				{
					segment.AddLight(bodyLayout.GetPositionLight(x, y), 0, 1, lightColor, x + y, lightColor, x + y + 97, true);
					segment.AddLight(bodyLayout.GetPositionLight(x, y + 1), 0, 1, lightColor, 97 - x + y, lightColor, 194 - x + y, true);
				}
			for (var y = 0; y < 97; ++y)
				for (var x = 0; x < 97; x += 19)
				{
					if ((y + 1) % 19 - 1 < 3)
						continue;
					segment.AddLight(bodyLayout.GetPositionLight(x, y), 0, 1, lightColor, x + y, lightColor, x + y + 97, true);
					segment.AddLight(bodyLayout.GetPositionLight(x + 1, y), 0, 1, lightColor, 97 + x - y, lightColor, 194 + x - y, true);
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
				Color = color;
			}
		}

		Segment Crowd()
		{
			const int Loops = 3;
			const int LoopDelay = 250;
			const int StartDelay = LoopDelay / 8;
			var colors = new List<int> { 0x081004, 0x000010, 0x100c00, 0x001008, 0x0c0410, 0x100800, 0x000410, 0x081008, 0x101000, 0x080010, 0x10000c, 0x100408, 0x0c1010, 0x080c10, 0x041010, 0x100400, 0x040410, 0x081010, 0x041000, 0x100010, 0x0c0810, 0x100c08, 0x040c10, 0x10040c, 0x10080c, 0x0c1000, 0x001004, 0x0c1004, 0x081000, 0x080810, 0x101004, 0x0c100c, 0x001010, 0x100810, 0x041004, 0x100c10 };
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
						segment.AddLight(bodyLayout.GetPositionLight(person.Position), time, person.Color);
						if (person.Position != person.Destination)
						{
							done = false;
							person.Position = SongHelper.NextLocation(person.Position, person.Destination);
						}
						segment.AddLight(bodyLayout.GetPositionLight(person.Position), time + next, person.Color);
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
				new List<int> { 0x000000, 0x101010, 0x000000 },
				new List<int> { 0x000000, 0x100008, 0x100800, 0x100800, 0x100008, 0x000000 },
				new List<int> { 0x000000, 0x001010, 0x100010, 0x100010, 0x001010, 0x000000 },
				new List<int> { 0x000000 }
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

		public override Song Render()
		{
			var song = new Song("popular.ogg");

			song.AddMeasure(1000); // Empty lead-in time
			song.AddMeasure(2424, 81); // Measures

			// PulseSquares (1)
			var pulseSquares = PulseSquares();
			song.AddSegment(pulseSquares, 0, 100, 1, 1, 4);
			song.AddPaletteChange(1, 0);
			song.AddPaletteChange(2, 1);
			song.AddPaletteChange(3, 2);
			song.AddPaletteChange(4, 3);
			song.AddPaletteChange(5, 0);

			// MoveSquares (5)
			var moveSquares = MoveSquares();
			song.AddSegment(moveSquares[0], 0, moveSquares[0].MaxTime(), 5, 1);
			song.AddSegment(moveSquares[1], 0, moveSquares[1].MaxTime(), 6, 1);
			song.AddSegment(moveSquares[1], moveSquares[1].MaxTime(), 0, 7, 1);
			song.AddSegment(moveSquares[0], moveSquares[0].MaxTime(), 0, 8, 1);
			song.AddSegment(moveSquares[0], 0, moveSquares[0].MaxTime(), 9, 1);
			song.AddSegment(moveSquares[1], 0, moveSquares[1].MaxTime(), 10, 1);
			song.AddSegment(moveSquares[1], moveSquares[1].MaxTime(), 0, 11, 1);
			song.AddSegment(moveSquares[0], moveSquares[0].MaxTime(), 0, 12, 1);
			song.AddPaletteChange(5, 0);
			song.AddPaletteChange(5.75, 6.25, 1);
			song.AddPaletteChange(6.75, 7.25, 2);
			song.AddPaletteChange(7.75, 8.25, 3);
			song.AddPaletteChange(8.75, 9.25, 4);
			song.AddPaletteChange(9.75, 10.25, 5);
			song.AddPaletteChange(10.75, 11.25, 6);
			song.AddPaletteChange(11.75, 12.25, 7);
			song.AddPaletteChange(13, 0);

			// SyncSquares (13)
			var syncSquares = SyncSquares();
			song.AddSegment(syncSquares, 29588, 48980, 13, 8);

			// SineShine (21)
			var sineShine = SineShine();
			song.AddSegment(sineShine, 0, 1, 21, 1, 12);
			song.AddPaletteChange(21, 0);
			song.AddPaletteChange(24.75, 25.25, 1);
			song.AddPaletteChange(28.75, 29.25, 2);
			song.AddPaletteChange(33, 0);

			// Crowd (33)
			var crowd = Crowd();
			song.AddSegment(crowd, 0, 250 * 3, 33, 12);

			// Wavey (45)
			var wavey = Wavey();
			song.AddSegment(wavey, 0, 1, 45, 1, 8);
			song.AddPaletteChange(45, 0);
			song.AddPaletteChange(45.9, 46.1, 1);
			song.AddPaletteChange(46.9, 47.1, 2);
			song.AddPaletteChange(47.9, 48.1, 3);
			song.AddPaletteChange(48.9, 49.1, 4);
			song.AddPaletteChange(49.9, 50.1, 5);
			song.AddPaletteChange(50.9, 51.1, 6);
			song.AddPaletteChange(51.9, 52.1, 7);
			song.AddPaletteChange(53, 0);

			// RainbowSquare (53)
			var rainbowSquare = RainbowSquare();
			song.AddSegment(rainbowSquare, 0, 1000, 53, 1, 16);
			song.AddPaletteChange(53, 0);
			song.AddPaletteChange(56.75, 57.25, 1);
			song.AddPaletteChange(60.75, 61.25, 2);
			song.AddPaletteChange(64.75, 65.25, 3);
			song.AddPaletteChange(69, 0);

			// Stairs (69)
			var stairs = Stairs();
			song.AddSegment(stairs, 0, 360, 69, 1, 13);
			song.AddPaletteChange(69, 0);
			song.AddPaletteChange(72.75, 73.25, 1);
			song.AddPaletteChange(76.75, 77.25, 2);
			song.AddPaletteChange(81.25, 81.5, 3);
			song.AddPaletteChange(82, 0);

			return song;
		}
	}
}
