using System;
using System.Collections.Generic;
using System.Linq;

namespace Shelfinator.Creator.Patterns
{
	class SquareFlash : IPattern
	{
		public int PatternNumber => 24;

		List<List<int>> GetLights()
		{
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");
			var squares = new Layout("Shelfinator.Creator.Patterns.Layout.Squares.png");
			return squares.GetAllLights().Except(0).OrderBy(light => light).Select(light => layout.GetPositionLights(squares.GetLightPositions(light))).ToList();
		}

		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			const int Count = 100;

			var pattern = new Pattern();
			var lights = GetLights();

			var color = new LightColor(0, 24, Helpers.Rainbow7.Multiply(Brightness).ToList());

			var rand = new Random(10);
			var order = Enumerable.Range(0, lights.Count).OrderBy(x => rand.Next()).ToList();
			var time = 0;
			var timeOffset = 1000;
			for (var ctr = 0; ctr < Count; ++ctr)
			{
				foreach (var light in lights[order[ctr % 25]])
					pattern.AddLight(light, time, color, order[ctr % 25]);
				time += timeOffset;
				foreach (var light in lights[order[ctr % 25]])
					pattern.AddLight(light, time, time + 300, null, pattern.Absolute, 0x000000);
				timeOffset = Math.Max(50, timeOffset - Math.Min(25, timeOffset * 19 / 20));
			}
			pattern.Clear(time);

			pattern.AddLightSequence(0, time);
			pattern.AddLightSequence(time, time, 1000);

			return pattern;
		}
	}
}
