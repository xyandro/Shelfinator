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

		public override Song Render()
		{
			var song = new Song("new.ogg");

			//// TimeWarp (0)
			//var timeWarp = TimeWarp(out var timeWarpTime);
			//song.AddSegment(timeWarp, 0, timeWarpTime, 0, 10000);

			// Wipe (0)
			var wipe = Wipe(out var wipeTime);
			song.AddSegment(wipe, 0, wipeTime, 0, wipeTime);

			return song;
		}
	}
}
