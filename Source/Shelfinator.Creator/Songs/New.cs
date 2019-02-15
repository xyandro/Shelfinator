using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class New : SongCreator
	{
		public override int SongNumber => 9;
		public override bool Test => true;

		readonly Layout bodyLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");

		Segment TimeWarp(out int time)
		{
			const int Repeat = 10;
			var frame1 = Enumerable.Range(0, 3).SelectMany(y => Enumerable.Range(0, 3).SelectMany(x => bodyLayout.GetPositionLights(x * 38, y * 38, 21, 21))).ToList();
			var frame2 = Enumerable.Range(0, 2).SelectMany(y => Enumerable.Range(0, 2).SelectMany(x => bodyLayout.GetPositionLights(x * 38 + 19, y * 38 + 19, 21, 21))).ToList();
			var segment = new Segment();
			time = 0;
			var delay = 1 << Repeat;
			for (var ctr = 0; ctr < Repeat; ++ctr)
			{
				segment.Clear(time);
				foreach (var light in frame1)
					segment.AddLight(light, time, 0x101010);
				time += delay;

				segment.Clear(time);
				foreach (var light in frame2)
					segment.AddLight(light, time, 0x101010);
				time += delay;
				delay >>= 1;
			}
			return segment;
		}

		Segment Wipe(out int time)
		{
			var funcs = new List<Tuple<Func<Point, int>, LightColor>>
			{
				new Tuple<Func<Point, int>, LightColor>(p => (((p - Helpers.Center).Length - 9) * 16.9830463869911).Round(), new LightColor(0, 1000, new List<int> { 0x100000, 0x010000 })),
				new Tuple<Func<Point, int>, LightColor>(p => (p.X * 10.4166666666667).Round(), new LightColor(0, 1000, new List<int> { 0x001000, 0x000100 })),
				new Tuple<Func<Point, int>, LightColor>(p => (p.Y * 10.4166666666667).Round(), new LightColor(0, 1000, new List<int> { 0x000010, 0x000001 })),
				new Tuple<Func<Point, int>, LightColor>(p => ((p.X + p.Y) * 5.20833333333333).Round(), new LightColor(0, 1000, new List<int> { 0x100000, 0x001000 })),
				new Tuple<Func<Point, int>, LightColor>(p => ((p.X - p.Y + 96) * 5.20833333333333).Round(), new LightColor(0, 1000, new List<int> { 0x001000, 0x000010 })),
				new Tuple<Func<Point, int>, LightColor>(p => (1000 - ((p - Helpers.Center).Length - 9) * 16.9830463869911).Round(), new LightColor(0, 1000, new List<int> { 0x000010, 0x100000 })),
				new Tuple<Func<Point, int>, LightColor>(p => (1000 - p.X * 10.4166666666667).Round(), new LightColor(0, 1000, new List<int> { 0x100010, 0x001010 })),
				new Tuple<Func<Point, int>, LightColor>(p => (1000 - p.Y * 10.4166666666667).Round(), new LightColor(0, 1000, new List<int> { 0x101010, 0x000000, 0x101010 })),
				new Tuple<Func<Point, int>, LightColor>(p => (1000 - (p.X + p.Y) * 5.20833333333333).Round(), new LightColor(0, 1000, new List<int> { 0x100000, 0x001000, 0x000010 })),
				new Tuple<Func<Point, int>, LightColor>(p => (1000 - (p.X - p.Y + 96) * 5.20833333333333).Round(), new LightColor(0, 1000, Helpers.Rainbow6)),
			};

			var segment = new Segment();
			time = 0;
			foreach (var func in funcs)
			{
				foreach (var light in bodyLayout.GetAllLights())
				{
					var point = bodyLayout.GetLightPosition(light);
					var delay = func.Item1(point);
					segment.AddLight(light, time + delay, func.Item2, delay);
				}
				time += 2000;
			}
			return segment;
		}

		Segment Shift(out int time)
		{
			time = 0;
			var segment = new Segment();
			for (var ctr = 0; ctr < 34; ++ctr)
			//for (var angle = 0; angle < 360; angle += 5)
			{
				segment.Clear(time);
				var shift = 17 - Math.Abs(17 - ctr);
				//var shift = Math.Max(0, Math.Min((21d * (0.5d - Math.Cos(angle / 180d * Math.PI) / 2d) - 2).Round(), 17));
				for (var y = 0; y < 4; ++y)
					for (var x = 0; x < 4; ++x)
					{
						foreach (var light in bodyLayout.GetPositionLights(x * 38 - shift, y * 38, 19, 2))
							segment.AddLight(light, time, 0x101010);
						foreach (var light in bodyLayout.GetPositionLights(x * 38 + 2 + shift, y * 38 + 19, 19, 2))
							segment.AddLight(light, time, 0x101010);
						foreach (var light in bodyLayout.GetPositionLights(x * 38 + 19, y * 38 - shift, 2, 19))
							segment.AddLight(light, time, 0x101010);
						foreach (var light in bodyLayout.GetPositionLights(x * 38, y * 38 + 2 + shift, 2, 19))
							segment.AddLight(light, time, 0x101010);
					}
				++time;
			}
			return segment;
		}

		Segment CircleDot()
		{
			const int Segments = 4;
			const double MinDistance = 9;
			const double MaxDistance = 48;
			const double Width = 5;
			var segment = new Segment();
			for (var angle = 0; angle < 360; angle += 5)
			{
				//var minDistance = (1d - Math.Cos(angle / 180d * Math.PI)) / 2d * (MaxDistance - Width - MinDistance) + MinDistance;
				var minDistance = MaxDistance-Width;
				segment.Clear(angle);
				foreach (var light in bodyLayout.GetAllLights())
				{
					var p = bodyLayout.GetLightPosition(light);
					var distance = (p - Helpers.Center).Length.Round();
					if ((distance < minDistance) || (distance > minDistance + Width))
						continue;
					var pAngle = Helpers.GetAngle(p, Helpers.Center) + 360d - angle / Segments;
					if (pAngle * Segments / 360d % 1d > 0.5)
						continue;
					segment.AddLight(light, angle, 0x101010);
				}
			}
			return segment;
		}

		public override Song Render()
		{
			var song = new Song("new.ogg");

			//// TimeWarp (0)
			//var timeWarp = TimeWarp(out var timeWarpTime);
			//song.AddSegment(timeWarp, 0, timeWarpTime, 0, 10000);

			//// Wipe (0)
			//var wipe = Wipe(out var wipeTime);
			//song.AddSegment(wipe, 0, wipeTime, 0, wipeTime);

			//// Shift (0)
			//var shift = Shift(out var shiftTime);
			//song.AddSegment(shift, 0, shiftTime, 0, 1000, 10);

			// CircleDot (0)
			var circleDot = CircleDot();
			song.AddSegment(circleDot, 0, 360, 0, 1000, 10);

			return song;
		}
	}
}
