using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Squares : ISong
	{
		public int SongNumber => 19;

		void GetSquareLights(out List<List<int>> lights, out List<Point> centers)
		{
			var layout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");
			var squares = new Layout("Shelfinator.Creator.Songs.Layout.Squares.png");
			var positions = squares.GetAllLights().Except(0).OrderBy(light => light).Select(light => squares.GetLightPositions(light)).ToList();
			lights = squares.GetAllLights().Except(0).OrderBy(light => light).Select(light => squares.GetLightPositions(light)).Select(list => list.Select(light => layout.GetPositionLight(light)).ToList()).ToList();
			centers = positions.Select(list => Helpers.GetCenter(list)).ToList();
		}

		public Song Render()
		{
			const double Brightness = 1f / 16;
			const int DisplayTime = 3000;
			const int FadeTime = 1000;

			var song = new Song();
			var segment = new Segment();
			GetSquareLights(out var lights, out var centers);
			var center = Helpers.GetCenter(centers);

			var values = new List<List<int>>();
			values.Add(centers.Select(p => (p - center).Length).Scale(null, null, 0, 100).Round().ToList());
			values.Add(centers.Select(p => (p - center).Length).Scale(null, null, 100, 0).Round().ToList());
			values.Add(centers.Select(p => p.X).Scale(null, null, 0, 100).Round().ToList());
			values.Add(centers.Select(p => p.X).Scale(null, null, 100, 0).Round().ToList());
			values.Add(centers.Select(p => p.Y).Scale(null, null, 0, 100).Round().ToList());
			values.Add(centers.Select(p => p.Y).Scale(null, null, 100, 0).Round().ToList());
			values.Add(centers.Select(p => p.X + p.Y).Scale(null, null, 0, 100).Round().ToList());
			values.Add(centers.Select(p => p.X + p.Y).Scale(null, null, 100, 0).Round().ToList());

			var color = new LightColor(0, 100, Helpers.Rainbow6.Multiply(Brightness).ToList());

			var time = 0;
			for (var paletteCtr = 0; paletteCtr < values.Count; paletteCtr++)
			{
				for (var listCtr = 0; listCtr < lights.Count; listCtr++)
					foreach (var light in lights[listCtr])
						segment.AddLight(light, time, time + FadeTime, null, color, values[paletteCtr][listCtr]);
				song.AddSegment(segment, time, time + DisplayTime);
				time += DisplayTime;
			}

			for (var listCtr = 0; listCtr < lights.Count; listCtr++)
				foreach (var light in lights[listCtr])
					segment.AddLight(light, time, time + FadeTime, null, Segment.Absolute, 0);
			song.AddSegment(segment, time, time + DisplayTime);

			return song;
		}
	}
}
