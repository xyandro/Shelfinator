using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Shelfinator.Patterns
{
	class Squares : IPattern
	{
		public int PatternNumber => 19;

		List<List<int>> GetOrderedLights(List<int> lights) => lights.Select(light => new { light, order = (light & 0xff8) >> 3 }).Concat(lights.Select(light => new { light, order = (light & 0x3fe000) >> 13 })).GroupBy(o => o.order).Where(group => group.Key != 0).OrderBy(group => group.Key).Select(group => group.Select(o => o.light).ToList()).ToList();

		void GetSquareLights(out List<List<int>> lights, out List<Point> centers)
		{
			var layout = new Layout("Shelfinator.Patterns.Layout.Layout-Body.png");
			var squares = new Layout("Shelfinator.Patterns.Layout.Squares.png");
			var positions = squares.GetAllLights().Except(0).OrderBy(light => light).Select(light => squares.GetLightPositions(light)).ToList();
			lights = squares.GetAllLights().Except(0).OrderBy(light => light).Select(light => squares.GetLightPositions(light)).Select(list => list.Select(light => layout.GetPositionLight(light)).ToList()).ToList();
			centers = positions.Select(list => Helpers.GetCenter(list)).ToList();
		}

		LightColor GetColor(double Brightness)
		{
			var rainbow = Helpers.Rainbow6.Multiply(Brightness).ToList();
			var reds = new List<int> { 0xff6969, 0xd34949, 0xda3232, 0xb80000, 0x620000 }.Multiply(Brightness).ToList();
			var blues = new List<int> { 0x32d0d3, 0x0000ff, 0x66ffff }.Multiply(Brightness).ToList();
			var color = new LightColor(0, 100, new List<List<int>> { reds, blues, rainbow });
			return color;
		}

		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			const int DisplayTime = 3000;
			const int FadeTime = 1000;

			var pattern = new Pattern();
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
						pattern.AddLight(light, time, time + FadeTime, null, color, values[paletteCtr][listCtr]);
				pattern.AddLightSequence(time, time + DisplayTime);
				time += DisplayTime;
			}

			for (var listCtr = 0; listCtr < lights.Count; listCtr++)
				foreach (var light in lights[listCtr])
					pattern.AddLight(light, time, time + FadeTime, null, pattern.Absolute, 0);
			pattern.AddLightSequence(time, time + DisplayTime);

			return pattern;
		}
	}
}
