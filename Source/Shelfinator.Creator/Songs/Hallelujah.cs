using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Hallelujah : SongCreator
	{
		public override int SongNumber => 6;

		readonly Layout bodyLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");

		Segment Bounce()
		{
			const int Length = 750;
			var segment = new Segment();
			var points = new List<Point> { new Point(0, 0), new Point(0, 95), new Point(95, 95), new Point(95, 0) };
			var direction = new List<Vector> { new Vector(0, 1), new Vector(1, 0), new Vector(0, -1), new Vector(-1, 0) };
			var color = new List<int> { 0x100000, 0x001010, 0x000010, 0x001000 };

			var time = 0;
			for (var dir = 0; dir < 4; ++dir)
			{
				var velocity = 0d;
				var pos = 0d;
				while (true)
				{
					var duration = (95d / 2d * (-velocity + Math.Sqrt(Math.Pow(velocity, 2d) - 4d / 95d * (pos - 95d)))).Round();
					// Tweak velocity so bottom falls on an integer
					velocity = (95d - pos) / duration - duration / 95d;
					for (var ctr = 0; ctr < duration; ++ctr)
					{
						segment.Clear(time);
						var mult = Math.Pow(ctr, 2) / 95d + velocity * ctr + pos;
						for (var pointCtr = 0; pointCtr < points.Count; ++pointCtr)
						{
							var newPosition = points[pointCtr] + direction[pointCtr] * mult;
							foreach (var light in bodyLayout.GetPositionLights(newPosition.X.Round(), newPosition.Y.Round(), 2, 2))
								segment.AddLight(light, time, color[(pointCtr + 4 - dir) % 4]);
						}
						++time;
					}
					velocity = -3d / 4d * (2d * duration / 95d + velocity);
					pos = 95d;
					if (velocity > -0.05)
						break;
				}
				time = (time + Length - 1) / Length * Length;
			}
			return segment;
		}

		Segment LinesSquares()
		{
			var segment = new Segment();
			var color = new LightColor(0, 0, new List<IReadOnlyList<int>>
			{
				new List<int> { 0x101010 },
				new List<int> { 0x101000 },
				new List<int> { 0x001010 },
				new List<int> { 0x100010 },
			});
			for (var time = 0; time < 38; ++time)
			{
				segment.Clear(time);
				for (var x = 0; x < 97; ++x)
					for (var y = 0; y < 97; y += 38)
					{
						if (((x + 38 - time) % 38) > 20)
							continue;
						foreach (var light in bodyLayout.GetPositionLights(x, y, 1, 2))
							segment.AddLight(light, time, color, 0);
						foreach (var light in bodyLayout.GetPositionLights(96 - x, y + 19, 1, 2))
							segment.AddLight(light, time, color, 0);
						foreach (var light in bodyLayout.GetPositionLights(y, x, 2, 1))
							segment.AddLight(light, time, color, 0);
						foreach (var light in bodyLayout.GetPositionLights(y + 19, 96 - x, 2, 1))
							segment.AddLight(light, time, color, 0);
					}
			}
			return segment;
		}

		Segment FourCircle()
		{
			const int RotateDist = 97 / 4;
			const int ColorDist = RotateDist - 5;
			var color = new LightColor(0, 360, new List<IReadOnlyList<int>>
			{
				new List<int> { 0x101010 },
				new List<int> { 0x001000 },
				new List<int> { 0x001010, 0x100010, 0x001010 },
				Helpers.Rainbow6.Concat(Helpers.Rainbow6.Take(1)).ToList(),
			});
			var segment = new Segment();
			for (var addAngle = 0; addAngle < 360; ++addAngle)
			{
				segment.Clear(addAngle);
				for (var centerAngle = 0; centerAngle < 360; centerAngle += 90)
				{
					var useAngle = (centerAngle + addAngle) * Math.PI / 180;
					var cos = Math.Cos(useAngle);
					var sin = Math.Sin(useAngle);
					var center = new Point(RotateDist * cos - RotateDist * sin + 48, RotateDist * cos + RotateDist * sin + 48);

					foreach (var light in bodyLayout.GetAllLights())
					{
						var diff = bodyLayout.GetLightPosition(light) - center;
						if (diff.LengthSquared >= Math.Pow(ColorDist, 2))
							continue;

						var angle = (Math.Atan2(diff.Y, diff.X) / Math.PI * 180).Round() + centerAngle;
						while (angle < 0)
							angle += 360;
						while (angle >= 360)
							angle -= 360;
						segment.AddLight(light, addAngle, color, angle);
					}
				}
			}
			return segment;
		}

		class SnowFlake
		{
			static Random rand = new Random(0xdeadb33);
			public int X { get; set; }
			public int Y { get; set; } = -1;
			public int Speed { get; set; } = rand.Next(6, 11);
			public int Color { get; set; } = rand.Next(0, 1000);

			public SnowFlake(int x) => X = x;
			public Point Point => new Point(X, Y);
		}

		Segment SnowFall()
		{
			const double InitialTime = 50;
			const double TimeModifier = 0.9995;
			var segment = new Segment();
			var rand = new Random(0x3984753);
			var flakes = new List<SnowFlake>();
			var color = new LightColor(0, 1000, new List<int> { 0x101010, 0x000010, 0x001010, 0x0b090d });
			var fallTime = 0;
			var columnMax = Enumerable.Repeat(96, 97).ToList();
			var columnOrder = new List<int> { 20, 69, 89, 84, 72, 59, 43, 21, 13, 70, 32, 8, 31, 47, 94, 85, 6, 96, 42, 52, 91, 62, 35, 14, 24, 45, 27, 67, 1, 28, 25, 68, 37, 74, 5, 40, 4, 23, 33, 63, 53, 57, 75, 41, 71, 60, 78, 56, 95, 58, 48, 7, 82, 50, 83, 65, 87, 80, 46, 49, 77, 54, 18, 29, 39, 81, 90, 51, 55, 34, 36, 88, 93, 92, 12, 22, 66, 38, 19, 0, 10, 61, 9, 76, 17, 44, 15, 26, 86, 2, 16, 64, 3, 30, 11, 79, 73 };
			var column = 0;
			for (var time = 0; time < 16000; ++time)
			{
				if (time >= fallTime)
				{
					flakes.Add(new SnowFlake(columnOrder[column++ % columnOrder.Count]));
					fallTime = (time + InitialTime * Math.Pow(TimeModifier, time)).Round();
				}

				foreach (var flake in flakes)
				{
					if (time % flake.Speed != 0)
						continue;

					if (flake.Y < columnMax[flake.X])
						foreach (var light in bodyLayout.GetPositionLights(flake.Point, 1, 1))
							segment.AddLight(light, time, 0x000000);
				}

				for (var ctr = 0; ctr < flakes.Count; ++ctr)
				{
					var flake = flakes[ctr];
					if (time % flake.Speed != 0)
						continue;

					++flake.Y;

					var remove = flake.Y >= columnMax[flake.X];
					if (remove)
						flake.Y = columnMax[flake.X]--;

					foreach (var light in bodyLayout.GetPositionLights(flake.Point, 1, 1))
						segment.AddLight(light, time, color, flake.Color);

					if (remove)
						flakes.RemoveAt(ctr--);
				}
			}

			return segment;
		}

		Segment SyncFlash()
		{
			var times = new List<int> { 0, 750, 1000, 2000, 4000, 4750, 5000, 6000, 8000, 8750, 9000, 10000, 12000, 12750, 13000, 14500, 14750, 15000, 16750, 17000, 17333, 17667, 18000, 18333, 18667, 19000, 19333, 19667, 20000, 20333, 20667, 21000, 21333, 21667, 22000, 22333, 22667, 23000, 23333, 23667, 24000, 24333, 24667, 25000 };
			var notes = new List<int> { 3, 1, 0, 0, 0, 1, 3, 3, 3, 1, 0, 0, 0, 1, 3, 2, 3, 4, 5, 5, 2, 1, 0, 3, 4, 5, 2, 1, 0, 5, 4, 3, 2, 1, 0, 3, 4, 5, 2, 1, 0, 5, 4 };
			var segment = new Segment();
			var color = new LightColor(0, 5, Helpers.Rainbow6);

			for (var ctr = 0; ctr < notes.Count; ctr++)
			{
				var y = 19 * notes[ctr];
				for (var x = 0; x < 97; ++x)
					foreach (var light in bodyLayout.GetPositionLights(x, y, 1, 2).Concat(bodyLayout.GetPositionLights(y, x, 2, 1)))
						segment.AddLight(light, times[ctr] + Math.Abs(48 - x), times[ctr + 1], color, ctr % 6, 0x000000);
			}

			return segment;
		}

		Segment RotateSquares(out int time)
		{
			var segment = new Segment();
			var attached = new List<List<int>>
			{
				new List<int> { 0, 2, 4, 10, 12, 14, 20, 22, 24 },
				new List<int> { 5, 7, 9, 15, 17, 19 },
				new List<int> { 6, 8, 16, 18 },
				new List<int> { 1, 3, 11, 13, 21, 23 },
			};

			var color = new LightColor(900, 6788,
				new List<int> { 0x101010, 0x101010, 0x000000, 0x000000 },
				new List<int> { 0x001000 },
				new List<int> { 0x101000, 0x100010 },
				Helpers.Rainbow6
			);

			var center = new Point(48, 48);
			var create = new List<Vector> { new Vector(0, 0), new Vector(19, 0), new Vector(19, 19), new Vector(0, 19) };
			var dirs = new List<Vector> { new Vector(1, 0), new Vector(0, 1), new Vector(-1, 0), new Vector(0, -1) };
			time = 0;
			foreach (var set in attached)
			{
				var points = set.Select(square => new Point(square % 5 * 19, square / 5 * 19)).SelectMany(point => create.Select(dir => point + dir)).ToList();
				for (var x = 0; x < 19; ++x)
				{
					for (var ctr = 0; ctr < points.Count; ctr++)
					{
						foreach (var light in bodyLayout.GetPositionLights(points[ctr], 2, 2))
							segment.AddLight(light, time, 0x000000);
						points[ctr] += dirs[ctr % 4];
						foreach (var light in bodyLayout.GetPositionLights(points[ctr], 2, 2))
							segment.AddLight(light, time, color, ((points[ctr] - center).Length * 100).Round());
					}
					++time;
				}
			}

			return segment;
		}

		Segment SineMix()
		{
			const int Size = 21;
			const int MidPoint = (Size - 1) / 2;
			var segment = new Segment();
			var color = new List<int> { 0x000010, 0x001000, 0x100000 };
			var colorMap = new Dictionary<int, LightColor>();

			var geoIncrement = Math.Pow(0.5, 1.0 / MidPoint);
			var lineMult = Enumerable.Range(0, Size).Select(line => 1.5 - Math.Pow(geoIncrement, MidPoint - Math.Abs(MidPoint - line))).ToList();

			for (var angle = 0; angle < 360; ++angle)
			{
				var lineColor = Enumerable.Repeat(0x000000, 97).ToList();
				for (var bar = 0; bar < 3; ++bar)
				{
					var startLine = ((Math.Sin((angle + bar * 60) * Math.PI / 180) + 1d) / 2d * (97 - Size)).Round();
					for (var line = 0; line < Size; ++line)
						lineColor[startLine + line] += Helpers.MultiplyColor(color[bar], lineMult[line]);
				}

				segment.Clear(angle);
				for (var line = 0; line < lineColor.Count; ++line)
				{
					if (!colorMap.ContainsKey(lineColor[line]))
						colorMap[lineColor[line]] = new LightColor(0, 96, new List<int> { Helpers.MultiplyColor(lineColor[line], 1d / 2), lineColor[line], Helpers.MultiplyColor(lineColor[line], 1d / 4) });
					for (var x = 0; x < 97; ++x)
						foreach (var light in bodyLayout.GetPositionLights(x, line, 1, 1))
							segment.AddLight(light, angle, colorMap[lineColor[line]], x);
				}
			}
			return segment;
		}

		Segment WalkAround()
		{
			const double CenterSize = 20;
			const double Size = 48;

			var segment = new Segment();
			var useColors = new List<List<int>>
			{
				new List<int> { 0x010101 },
				new List<int> { 0x010101 },
				new List<int> { 0x000000, 0x101010, 0x000000, 0x101010, 0x000000 },
				new List<int> { 0x000000, 0x101010, 0x000000, 0x101010, 0x000000 },
				new List<int> { 0x000000, 0x100000, 0x000000, 0x101000, 0x000000 },
				new List<int> { 0x000000, 0x100000, 0x000000, 0x101000, 0x000000 },
				new List<int> { 0x000000, 0x001010, 0x000000, 0x100010, 0x000000 },
				new List<int> { 0x000000, 0x001010, 0x000000, 0x100010, 0x000000 },
				new List<int> { 0x000000, 0x100000, 0x000000, 0x100800, 0x000000, 0x101000, 0x000000, 0x001000, 0x000000, 0x000010, 0x000000, 0x090010, 0x000000 },
				new List<int> { 0x000000, 0x100000, 0x000000, 0x100800, 0x000000, 0x101000, 0x000000, 0x001000, 0x000000, 0x000010, 0x000000, 0x090010, 0x000000 },
			};
			var color = Enumerable.Range(0, 3).Select(index => new LightColor(0, (Size * 100).Round(), useColors.Skip(index).Take(useColors.Count - 2))).ToList();
			for (var angle = 0; angle < 360; ++angle)
			{
				var center = new Point(48, 48) + new Vector(Math.Sin(angle * Math.PI / 180), Math.Cos(angle * Math.PI / 180)) * CenterSize;

				foreach (var light in bodyLayout.GetAllLights())
				{
					var point = bodyLayout.GetLightPosition(light);
					var distance = 2 * Size + Size * angle / 360 - (point - center).Length;
					var useColor = (int)Math.Floor(distance / Size);
					distance -= useColor * Size;
					segment.AddLight(light, angle, color[useColor], (distance * 100).Round());
				}
			}
			return segment;
		}

		Segment AllSquares(out int time)
		{
			const string RectData = "19,0,59,59&19,57,40,40&0,57,21,21&19,0,78,78&19,19,40,40&38,57,21,21&19,38,59,59&57,38,40,40&0,76,21,21&57,57,40,40&0,19,78,78&57,0,40,40&0,19,59,59&19,19,21,21&0,38,59,59&0,0,78,78&38,19,59,59&57,0,21,21&19,19,78,78&38,0,21,21&0,0,40,40&0,0,21,21&76,38,21,21&19,57,21,21&76,0,21,21&38,38,21,21&0,19,40,40&19,0,21,21&0,57,40,40&76,76,21,21&76,19,21,21&38,76,21,21&38,38,59,59&38,38,40,40&38,19,21,21&0,0,59,59&38,0,59,59&57,76,21,21&0,0,97,97&57,57,21,21&19,19,59,59&0,38,40,40&19,38,40,40&38,19,40,40&76,57,21,21&19,76,21,21&0,38,21,21&19,38,21,21&57,38,21,21&38,0,40,40&38,57,40,40&19,0,40,40&0,19,21,21&57,19,40,40&57,19,21,21";
			var segment = new Segment();
			time = 0;
			var rects = RectData.Split('&').Select(Rect.Parse).ToList();
			var color = new LightColor(0, 1000,
				new List<int> { 0x040404, 0x101010 },
				new List<int> { 0x100000, 0x100010 },
				new List<int> { 0x000010, 0x001010 },
				Helpers.Rainbow6
			);
			foreach (var rect in rects)
			{
				var center = rect.Location + new Vector(rect.Width, rect.Height) / 2;
				var min = (new Point(rect.X + rect.Width / 2, rect.Y + 1) - center).Length;
				var max = (rect.Location - center).Length;
				foreach (var light in bodyLayout.GetPositionLights(rect).Except(bodyLayout.GetPositionLights(rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 4)))
				{
					segment.AddLight(light, time, color, (((bodyLayout.GetLightPosition(light) - center).Length - min) / (max - min) * 1000).Round());
					segment.AddLight(light, time + 1, 0x000000);
				}
				++time;
			}
			return segment;
		}

		class FireworkPart
		{
			public Point Point { get; set; }
			public int Color { get; set; }

			readonly static Random rand = new Random(0x876aec);
			readonly Point startPoint;
			readonly Vector direction;
			int time = 0;

			public FireworkPart(Point point, int angle, int color)
			{
				startPoint = Point = point;
				direction = new Vector(Math.Sin(angle * Math.PI / 180), Math.Cos(angle * Math.PI / 180));
				Color = color;
			}

			public void Move()
			{
				++time;
				var gravity = new Vector(0, 0.01 * time * time);
				Point = startPoint + direction * time + gravity;
			}

			public static IEnumerable<FireworkPart> Create(int position)
			{
				const int Parts = 20;

				var x = (position % 4 + 1) * 19;
				var y = (position / 4 + 1) * 19;
				var point = new Point(x, y);
				var color = rand.Next(0, 7);
				for (var ctr = 0; ctr < Parts; ++ctr)
					yield return new FireworkPart(point, ctr * 360 / Parts, color);
			}
		}

		Segment Fireworks(out int time)
		{
			var launchTimes = new List<int> { 0, 750, 1000, 2000, 4000, 4750, 5000, 6000, 8000, 8750, 9000, 10000, 12000, 13000, 13250, 14250, 15083, 16167, 16833, 17167, 18167, 20083, 20750, 21083, 21583, 21667, 22000, 24083, 24667, 25083, 26083, 28083, 28667, 29083, 29167, 29250, 30417, 30750, 31083, 32417, 32750, 33083, 33125, 33167, 33208, 33250, 33292, 33333, 33375, 33417, 33458, 33500, 33542, 33583, 33625, 33667, 33708 };
			var positions = new List<int> { 4, 13, 0, 15, 2, 3, 6, 1, 8, 12, 7, 9, 10, 11, 5, 14, 4, 13, 0, 15, 2, 3, 6, 1, 8, 12, 7, 9, 10, 11, 5, 14, 4, 13, 0, 15, 2, 3, 6, 1, 8, 2, 15, 9, 0, 1, 4, 10, 3, 12, 7, 14, 11, 6, 5, 8, 13 };

			var parts = new List<FireworkPart>();
			var color = new LightColor(0, 6, Helpers.Rainbow6);
			var segment = new Segment();
			var bounds = new Rect(0, 0, 97, 97);
			time = 0;
			var launchIndex = 0;
			while (true)
			{
				while ((launchIndex < launchTimes.Count) && (time == launchTimes[launchIndex]))
				{
					parts.AddRange(FireworkPart.Create(positions[launchIndex]));
					++launchIndex;
				}

				if (time % 50 == 0)
				{
					foreach (var part in parts)
						foreach (var light in bodyLayout.GetPositionLights(part.Point, 1, 1))
							segment.AddLight(light, time, 0x000000);

					for (var ctr = 0; ctr < parts.Count; ctr++)
					{
						parts[ctr].Move();
						if (!bounds.Contains(parts[ctr].Point))
							parts.RemoveAt(ctr--);
					}

					if (((launchIndex == launchTimes.Count)) && (!parts.Any()))
						break;

					foreach (var part in parts)
						foreach (var light in bodyLayout.GetPositionLights(part.Point, 1, 1))
							segment.AddLight(light, time, color, part.Color);
				}

				++time;
			}

			return segment;
		}

		Segment Sunset(out int time)
		{
			const double HillStartY = 66;
			const double HillMiddleX = 37;
			const double HillMiddleY = 60;

			const double SunStartY = 105;
			const double SunMiddleX = 86;
			const double SunMiddleY = 10;
			const double SunRadius = 10;

			const double RedPercent = 0.5;
			const double FadePercent = 0.25;

			const double HillA = (HillStartY - HillMiddleY) / (HillMiddleX * HillMiddleX);
			const double SunA = (SunStartY - SunMiddleY) / (SunMiddleX * SunMiddleX);

			var segment = new Segment();

			var sunColor = new LightColor(new List<int> { 0x000000 }, new List<int> { 0x101000 }, new List<int> { 0x000000 });
			var blues = new List<int> { 0x000010, 0x000010, 0x000010, 0x000010 };
			var blueReds = new List<int> { 0x100000, 0x100000, 0x100800, 0x000001 };
			var greens = new List<int> { 0x001000, 0x001000, 0x001000, 0x001000 };
			var greenReds = new List<int> { 0x001000, 0x001000, 0x001000, 0x000100 };

			var ground = Enumerable.Range(0, 97).Select(x => (HillA * Math.Pow(x - HillMiddleX, 2) + HillMiddleY).Round()).ToList();

			for (time = 0; time < 137; ++time)
			{
				segment.Clear(time);

				var sunX = 86 - time;
				var sunY = (SunA * Math.Pow(sunX - SunMiddleX, 2) + SunMiddleY).Round();
				var sunPoint = new Point(sunX, sunY);

				var percent = (double)time / 136;

				// Sky
				double redPercent;
				if (percent >= RedPercent)
					redPercent = 1;
				else if (percent >= FadePercent)
					redPercent = (percent - FadePercent) / (RedPercent - FadePercent);
				else
					redPercent = 0;
				var blue = new LightColor(0, 10000, new List<int> { 0x000000 }, blues.Zip(blueReds, (r, b) => Helpers.GradientColor(r, b, redPercent)).ToList(), new List<int> { 0x000000 });
				for (var x = 0; x < 97; x++)
					for (var y = 0; y < ground[x]; ++y)
					{
						var point = new Point(x, y);
						foreach (var light in bodyLayout.GetPositionLights(point, 1, 1))
							segment.AddLight(light, time, blue, ((point - sunPoint).Length * 100).Round());
					}

				// Sun
				for (var x = -SunRadius; x <= SunRadius; ++x)
				{
					var val = (Math.Sqrt(Math.Pow(SunRadius, 2) - Math.Pow(x, 2))).Round();
					var yStart = sunY - val;
					var yEnd = sunY + val;
					for (var y = yStart; y <= yEnd; ++y)
						foreach (var light in bodyLayout.GetPositionLights(x + sunX, y, 1, 1))
							segment.AddLight(light, time, sunColor, 0);
				}

				// Ground
				var green = new LightColor(0, 10000, new List<int> { 0x000000 }, greens.Zip(greenReds, (r, b) => Helpers.GradientColor(r, b, redPercent)).ToList(), new List<int> { 0x000000 });
				for (var x = 0; x < 97; x++)
					for (var y = ground[x]; y < 97; ++y)
					{
						var point = new Point(x, y);
						foreach (var light in bodyLayout.GetPositionLights(point, 1, 1))
							segment.AddLight(light, time, green, ((point - sunPoint).Length * 100).Round());
					}
			}

			return segment;
		}

		public override Song Render()
		{
			// First measure starts at 1400, measures are 2000 throughout the song until the end
			var song = new Song("hallelujah.ogg");

			// Bounce (0)
			var bounce = Bounce();
			song.AddSegment(bounce, 0, 0, 0, 1400);
			song.AddSegment(bounce, 0, 750 * 4, 1400, 16000);

			// FourCircle (17400)
			var spinColor = FourCircle();
			song.AddSegment(spinColor, 0, 360, 17400, 8000, 2);
			song.AddPaletteChange(17400, 0);
			song.AddPaletteChange(20900, 21900, 1);
			song.AddPaletteChange(24900, 25900, 2);
			song.AddPaletteChange(28900, 29900, 3);
			song.AddPaletteChange(33400, 0);

			// LinesSquares (33400)
			var fourCircle = LinesSquares();
			song.AddSegment(fourCircle, 0, 38, 33400, 4000, 4);
			song.AddPaletteChange(33400, 0);
			song.AddPaletteChange(36900, 37900, 1);
			song.AddPaletteChange(40900, 41900, 2);
			song.AddPaletteChange(44900, 45900, 3);
			song.AddPaletteChange(49400, 0);

			// SnowFall (49400)
			var snowFall = SnowFall();
			song.AddSegment(snowFall, 0, 16000, 49400, 39000);

			// SyncFlash (88400)
			var syncFlash = SyncFlash();
			song.AddSegment(syncFlash, 0, 25000, 88400, 25000);

			// RotateSquares (113400)
			var rotateSquares = RotateSquares(out var rotateSquaresTime);
			song.AddSegment(rotateSquares, 0, rotateSquaresTime, 113400, 4000, 8);
			song.AddPaletteChange(113400, 0);
			song.AddPaletteChange(120900, 121900, 1);
			song.AddPaletteChange(128900, 129900, 2);
			song.AddPaletteChange(136900, 137900, 3);
			song.AddPaletteChange(145400, 0);

			// SineMix (145400)
			var sineMix = SineMix();
			song.AddSegment(sineMix, 0, 360, 145400, 4000, 4);

			// WalkAround (161400)
			var walkAround = WalkAround();
			song.AddSegment(walkAround, 0, 360, 161400, 4000, 8);
			song.AddPaletteChange(161400, 0);
			song.AddPaletteChange(165400, 1);
			song.AddPaletteChange(169400, 2);
			song.AddPaletteChange(173400, 3);
			song.AddPaletteChange(177400, 4);
			song.AddPaletteChange(181400, 5);
			song.AddPaletteChange(185400, 6);
			song.AddPaletteChange(189400, 7);
			song.AddPaletteChange(193400, 0);

			// AllSquares (193400)
			var allSquares = AllSquares(out var moveSquaresTime);
			song.AddSegment(allSquares, 0, moveSquaresTime, 193400, 8000);
			song.AddSegment(allSquares, 0, moveSquaresTime * 7 / 8, song.MaxTime(), 7000);
			song.AddPaletteChange(193400, 0);
			song.AddPaletteChange(197400, 1);
			song.AddPaletteChange(201400, 2);
			song.AddPaletteChange(205400, 3);
			song.AddPaletteChange(209400, 0);

			// Fireworks (208400)
			var fireworks = Fireworks(out var fireworksTime);
			song.AddSegment(fireworks, 0, fireworksTime, 208400);

			// Sunset (249400)
			var sunset = Sunset(out var sunTime);
			song.AddSegment(sunset, 0, sunTime, 249400, 14000);
			song.AddPaletteChange(249400, 0);
			song.AddPaletteChange(249400, 250400, 1);
			song.AddPaletteChange(262900, 263400, 2);
			song.AddPaletteChange(263400, 0);

			// End (263400)

			return song;
		}
	}
}
