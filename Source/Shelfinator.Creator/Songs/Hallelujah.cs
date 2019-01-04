﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Hallelujah : ISong
	{
		public int SongNumber => 6;

		const double Brightness = 1f / 16;
		readonly Layout bodyLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");

		Segment Bounce()
		{
			const int Length = 750;
			var segment = new Segment();
			var points = new List<Point> { new Point(0, 0), new Point(0, 95), new Point(95, 95), new Point(95, 0) };
			var direction = new List<Vector> { new Vector(0, 1), new Vector(1, 0), new Vector(0, -1), new Vector(-1, 0) };
			var color = new List<int> { 0xff0000, 0x00ffff, 0x0000ff, 0x00ff00 };

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
								segment.AddLight(light, time, Segment.Absolute, Helpers.MultiplyColor(color[(pointCtr + 4 - dir) % 4], Brightness));
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

		Segment FourCircle()
		{
			var segment = new Segment();
			var color = new LightColor(0, 0, new List<List<int>>
			{
				new List<int> { 0xffffff }.Multiply(Brightness).ToList(),
				new List<int> { 0xffff00 }.Multiply(Brightness).ToList(),
				new List<int> { 0x00ffff }.Multiply(Brightness).ToList(),
				new List<int> { 0xff00ff }.Multiply(Brightness).ToList(),
			});
			for (var time = 0; time < 38; ++time)
				for (var x = 0; x < 97; ++x)
					for (var y = 0; y < 97; y += 38)
					{
						var useColor = ((x + 38 - time) % 38) <= 20 ? color : Segment.Absolute;
						foreach (var light in bodyLayout.GetPositionLights(x, y, 1, 2))
							segment.AddLight(light, time, useColor);
						foreach (var light in bodyLayout.GetPositionLights(96 - x, y + 19, 1, 2))
							segment.AddLight(light, time, useColor);
						foreach (var light in bodyLayout.GetPositionLights(y, x, 2, 1))
							segment.AddLight(light, time, useColor);
						foreach (var light in bodyLayout.GetPositionLights(y + 19, 96 - x, 2, 1))
							segment.AddLight(light, time, useColor);
					}
			return segment;
		}

		Segment SpinColor()
		{
			const int RotateDist = 97 / 4;
			const int ColorDist = RotateDist - 5;
			var color = new LightColor(0, 360, new List<List<int>>
			{
				new List<int> { 0xffffff }.Multiply(Brightness).ToList(),
				new List<int> { 0x00ff00 }.Multiply(Brightness).ToList(),
				new List<int> { 0x00ffff, 0xff00ff, 0x00ffff }.Multiply(Brightness).ToList(),
				Helpers.Rainbow6.Concat(Helpers.Rainbow6.Take(1)).Multiply(Brightness).ToList(),
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
			var color = new LightColor(0, 1000, new List<int> { 0xffffff, 0x0000ff, 0x00ffff, 0xab97d2 }.Multiply(Brightness).ToList());
			var fallTime = 0;
			var columnMax = Enumerable.Repeat(96, 97).ToList();
			for (var time = 0; time < 20000; ++time)
			{
				if (time >= fallTime)
				{
					var column = rand.Next(0, 97);
					flakes.Add(new SnowFlake(column));
					fallTime = (time + InitialTime * Math.Pow(TimeModifier, time)).Round();
				}

				foreach (var flake in flakes)
				{
					if (time % flake.Speed != 0)
						continue;

					if (flake.Y < columnMax[flake.X])
						foreach (var light in bodyLayout.GetPositionLights(flake.Point, 1, 1))
							segment.AddLight(light, time, Segment.Absolute, 0x000000);
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
			var color = new LightColor(0, 5, Helpers.Rainbow6.Multiply(Brightness).ToList());

			for (var ctr = 0; ctr < notes.Count; ctr++)
			{
				var y = 19 * notes[ctr];
				for (var x = 0; x < 97; ++x)
					foreach (var light in bodyLayout.GetPositionLights(x, y, 1, 2).Concat(bodyLayout.GetPositionLights(y, x, 2, 1)))
						segment.AddLight(light, times[ctr] + Math.Abs(48 - x), times[ctr + 1], color, ctr % 6, Segment.Absolute, Helpers.MultiplyColor(0x000000, Brightness));
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
				new List<int> { 0xffffff, 0xffffff, 0x000000, 0x000000 }.Multiply(Brightness).ToList(),
				new List<int> { 0x00ff00 }.Multiply(Brightness).ToList(),
				new List<int> { 0xffff00, 0xff00ff }.Multiply(Brightness).ToList(),
				Helpers.Rainbow6.Multiply(Brightness).ToList()
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
							segment.AddLight(light, time, Segment.Absolute, Helpers.MultiplyColor(0x000000, Brightness));
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
			var color = new List<int> { 0x0000ff, 0x00ff00, 0xff0000 };
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
						colorMap[lineColor[line]] = new LightColor(0, 96, new List<int> { Helpers.MultiplyColor(lineColor[line], Brightness / 2), Helpers.MultiplyColor(lineColor[line], Brightness), Helpers.MultiplyColor(lineColor[line], Brightness / 4) });
					for (var x = 0; x < 97; ++x)
						foreach (var light in bodyLayout.GetPositionLights(x, line, 1, 1))
							segment.AddLight(light, angle, colorMap[lineColor[line]], x);
				}
			}
			return segment;
		}

		Segment SunSet(out int time)
		{
			const double HillStartY = 66;
			const double HillMiddleX = 37;
			const double HillMiddleY = 60;

			const double SunStartY = 90;
			const double SunMiddleX = 86;
			const double SunMiddleY = 10;
			const double SunRadius = 10;

			const double RedPercent = 0.5;
			const double FadePercent = 0.3;

			const double HillA = (HillStartY - HillMiddleY) / (HillMiddleX * HillMiddleX);
			const double SunA = (SunStartY - SunMiddleY) / (SunMiddleX * SunMiddleX);

			var segment = new Segment();

			var sunColor = new LightColor(new List<int> { Helpers.MultiplyColor(0xffff00, Brightness) }, new List<int> { 0x000000 });
			var blues = new List<int> { 0x0000ff, 0x0000ff, 0x000080, 0x000080 };
			var reds = new List<int> { 0xff0000, 0xff7f00, 0x0000ff, 0x000080 };
			var green = new LightColor(0, 10000, new List<int> { 0x00ff00, 0x00ff00, 0x008000 }.Multiply(Brightness).ToList(), new List<int> { 0x000000 });

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
				var blue = new LightColor(0, 10000, blues.Zip(reds, (r, b) => Helpers.GradientColor(r, b, redPercent)).Multiply(Brightness).ToList(), new List<int> { 0x000000 });
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
							segment.AddLight(light, time, sunColor);
				}

				// Ground
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

		public Song Render()
		{
			// First measure starts at 700, measures are 2000 throughout the song until the end
			var song = new Song("hallelujah.wav");

			// Bounce (0)
			var bounce = Bounce();
			song.AddSegmentWithRepeat(bounce, 0, 0, 0, 700);
			song.AddSegmentWithRepeat(bounce, 0, 750 * 4, 700, 16000);

			// FourCircle (16700)
			var fourCircle = FourCircle();
			song.AddSegmentWithRepeat(fourCircle, 0, 38, 16700, 4000, 4);
			song.AddPaletteSequence(16700, 0);
			song.AddPaletteSequence(20200, 21200, null, 1);
			song.AddPaletteSequence(24200, 25200, null, 2);
			song.AddPaletteSequence(28200, 29200, null, 3);
			song.AddPaletteSequence(32700, 0);

			// SpinColor (32700)
			var spinColor = SpinColor();
			song.AddSegmentWithRepeat(spinColor, 0, 360, 32700, 8000, 2);
			song.AddPaletteSequence(32700, 0);
			song.AddPaletteSequence(36200, 37200, null, 1);
			song.AddPaletteSequence(40200, 41200, null, 2);
			song.AddPaletteSequence(44200, 45200, null, 3);
			song.AddPaletteSequence(48700, 0);

			// SnowFall (48700)
			var snowFall = SnowFall();
			song.AddSegmentWithRepeat(snowFall, 0, 20000, 48700, 39000);

			// SyncFlash (87700)
			var syncFlash = SyncFlash();
			song.AddSegmentWithRepeat(syncFlash, 0, 25000, 87700, 25000);

			// RotateSquares (112700)
			var rotateSquares = RotateSquares(out var rotateSquaresTime);
			song.AddSegmentWithRepeat(rotateSquares, 0, rotateSquaresTime, 112700, 4000, 8);
			song.AddPaletteSequence(112700, 0);
			song.AddPaletteSequence(120200, 121200, null, 1);
			song.AddPaletteSequence(128200, 129200, null, 2);
			song.AddPaletteSequence(136200, 137200, null, 3);
			song.AddPaletteSequence(144700, 0);

			// SineMix (144700)
			var sineMix = SineMix();
			song.AddSegmentWithRepeat(sineMix, 0, 360, 144700, 4000, 4);

			// Next (160700)

			// SunSet (240700)
			Emulator.TestPosition = 240700;
			var sunSet = SunSet(out var sunTime);
			song.AddSegmentWithRepeat(sunSet, 0, sunTime, 240700, 22000);
			song.AddPaletteSequence(240700, 0);
			song.AddPaletteSequence(262200, 262700, null, 1);
			song.AddPaletteSequence(262700, 0);

			// End (262700)

			// Fireworks?

			return song;
		}
	}
}
