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
			const int Length = 1500;
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
					velocity = -7d / 8d * (2d * duration / 95d + velocity);
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
				new List<int> { 0x00ffff }.Multiply(Brightness).ToList(),
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

		public Song Render()
		{
			// First measure starts at 700, measures are 2000 throughout the song until the end
			var song = new Song("hallelujah.wav");

			// Bounce (0)
			var bounce = Bounce();
			song.AddSegmentWithRepeat(bounce, 0, 0, 0, 700);
			song.AddSegmentWithRepeat(bounce, 0, 1500 * 4, 700, 16000);

			// Lines (16700)
			var linesSquares = LinesSquares();
			song.AddSegmentWithRepeat(linesSquares, 0, 38, 16700, 4000, 4);
			song.AddPaletteSequence(16700, 0);
			song.AddPaletteSequence(20200, 21200, null, 1);
			song.AddPaletteSequence(24200, 25200, null, 2);
			song.AddPaletteSequence(28200, 29200, null, 3);
			song.AddPaletteSequence(32700, 0);

			// SpinColor (32700)
			Emulator.TestPosition = 32700;
			var spinColor = SpinColor();
			song.AddSegmentWithRepeat(spinColor, 0, 360, 32700, 8000, 2);
			song.AddPaletteSequence(32700, 0);
			song.AddPaletteSequence(36200, 37200, null, 1);
			song.AddPaletteSequence(40200, 41200, null, 2);
			song.AddPaletteSequence(44200, 45200, null, 3);
			song.AddPaletteSequence(48700, 0);

			// Next (48700)

			// Snowfall accumulating
			// Fireworks

			return song;
		}
	}
}
