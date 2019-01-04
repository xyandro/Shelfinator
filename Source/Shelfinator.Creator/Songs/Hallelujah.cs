using System;
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
			const double TimeModifier = 0.999782689004043;
			var segment = new Segment();
			var rand = new Random(0x3984753);
			var flakes = new List<SnowFlake>();
			var color = new LightColor(0, 1000, new List<int> { 0xffffff, 0x0000ff, 0x00ffff, 0xab97d2 }.Multiply(Brightness).ToList());
			var fallTime = 0;
			for (var time = 0; time < 20000; ++time)
			{
				if (time >= fallTime)
				{
					var column = rand.Next(0, 97);
					flakes.Add(new SnowFlake(column));
					fallTime = (time + InitialTime * Math.Pow(TimeModifier, time)).Round();
				}

				for (var ctr = 0; ctr < flakes.Count; ++ctr)
				{
					var flake = flakes[ctr];
					if (time % flake.Speed != 0)
						continue;

					if (flake.Y != -1)
						foreach (var light in bodyLayout.GetPositionLights(flake.Point, 1, 1))
							segment.AddLight(light, time, Segment.Absolute, 0x000000);

					++flake.Y;

					if (flake.Y > 96)
						flakes.RemoveAt(ctr--);
					else
						foreach (var light in bodyLayout.GetPositionLights(flake.Point, 1, 1))
							segment.AddLight(light, time, color, flake.Color);
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
			for (var y = 0; y < 97; y += 19)
				for (var x = 0; x < 97; x += 19)
					foreach (var light in bodyLayout.GetPositionLights(x, y, 2, 2))
						segment.AddLight(light, 0, color, ((new Point(x, y) - center).Length * 100).Round());

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
			Emulator.TestPosition = 112700;
			var rotateSquares = RotateSquares(out var rotateSquaresTime);
			song.AddSegmentWithRepeat(rotateSquares, 0, rotateSquaresTime, 112700, 4000, 8);
			song.AddPaletteSequence(112700, 0);
			song.AddPaletteSequence(120200, 121200, null, 1);
			song.AddPaletteSequence(128200, 129200, null, 2);
			song.AddPaletteSequence(136200, 137200, null, 3);
			song.AddPaletteSequence(144700, 0);

			// Next (144700)

			// Fireworks

			return song;
		}
	}
}
