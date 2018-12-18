using System.Collections.Generic;
using System.Linq;
using System;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Flash : ISong
	{
		public int SongNumber => 20;

		List<int> GetLights()
		{
			var layout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");
			var rand = new Random();
			return layout.GetAllLights().OrderBy(x => rand.Next()).ToList();
		}

		LightColor GetColor(double Brightness)
		{
			var rainbow = Helpers.Rainbow6.Multiply(Brightness).ToList();
			var reds = new List<int> { 0xff6969, 0xd34949, 0xda3232, 0xb80000, 0x620000 }.Multiply(Brightness).ToList();
			var blues = new List<int> { 0x32d0d3, 0x0000ff, 0x66ffff }.Multiply(Brightness).ToList();
			var color = new LightColor(0, 100, new List<List<int>> { reds, blues, rainbow });
			return color;
		}

		public Song Render()
		{
			const double Brightness = 1f / 16;
			const int Concurrency = 25;

			var color = new LightColor(0, 6, new List<List<int>> { new List<int> { Helpers.MultiplyColor(0xffffff, 1f / 8) }, Helpers.Rainbow6.Multiply(Brightness).ToList() });

			var segment = new Segment();
			var lights = GetLights();
			for (var time = 0; time < lights.Count; ++time)
			{
				segment.AddLight(lights[time], time, color, time % 7);
				segment.AddLight(lights[time], time + Concurrency, Segment.Absolute, 0x000000);
			}

			var song = new Song();
			song.AddSegment(segment, 0, lights.Count + Concurrency, 2000, 8);

			song.AddPaletteSequence(7000, 8000, null, 1);

			return song;
		}
	}
}
