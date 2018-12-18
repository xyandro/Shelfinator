using System.Collections.Generic;
using System.Linq;
using System;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Clock : ISong
	{
		public int SongNumber => 21;

		void GetSquareLights(out List<List<int>> lights, out List<List<double>> angles)
		{
			var layout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");
			var squares = new Layout("Shelfinator.Creator.Songs.Layout.Squares.png");
			var positions = squares.GetAllLights().Except(0).OrderBy(light => light).Select(light => squares.GetLightPositions(light)).ToList();
			lights = positions.Select(list => list.Select(light => layout.GetPositionLight(light)).ToList()).ToList();
			var centers = positions.Select(list => Helpers.GetCenter(list)).ToList();
			var rand = new Random();
			var offsets = Enumerable.Range(0, centers.Count).Select(index => index * 360 / centers.Count).OrderBy(x => rand.Next()).ToList();
			angles = positions.Select((list, index) => list.Select(p => Helpers.GetAngle(p, centers[index]) + offsets[index]).ToList()).ToList();
		}

		public Song Render()
		{
			const double Brightness = 1f / 16;

			var segment = new Segment();
			GetSquareLights(out var lights, out var angles);

			var color = new LightColor(0, 20, Helpers.Rainbow6.Select(value => new List<int> { value, 0x000000 }.Multiply(Brightness).ToList()).ToList());

			for (var angle = 0; angle < 360; angle += 5)
				for (var listCtr = 0; listCtr < lights.Count; listCtr++)
				{
					var reverse = listCtr % 2 == 1;

					for (var lightCtr = 0; lightCtr < lights[listCtr].Count; lightCtr++)
					{
						var lightAngle = (int)Math.Abs(Helpers.Cycle((reverse ? -1 : 1) * angle - angles[listCtr][lightCtr], -180, 180));
						segment.AddLight(lights[listCtr][lightCtr], angle, color, lightAngle);
					}
				}

			var song = new Song();
			song.AddSegment(segment, 0, 360, 1000, 17);

			song.AddPaletteSequence(2000, 3000, null, 1);
			song.AddPaletteSequence(5000, 6000, null, 2);
			song.AddPaletteSequence(8000, 9000, null, 3);
			song.AddPaletteSequence(11000, 12000, null, 4);
			song.AddPaletteSequence(14000, 15000, null, 5);

			return song;
		}
	}
}
