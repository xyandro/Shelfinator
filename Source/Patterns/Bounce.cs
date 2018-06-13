using System;
using System.Collections.Generic;
using System.Linq;

namespace Shelfinator.Patterns
{
	class Bounce : IPattern
	{
		public int PatternNumber => 23;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;

			var pattern = new Pattern();

			var layout = new Layout("Shelfinator.Patterns.Layout.Layout-Body.png");
			var columns = new List<int> { 0, 19, 38, 57, 76, 95 };
			var rand = new Random(7);
			var columnTime = columns.Select((col, index) => index * 16).OrderBy(x => rand.Next()).ToList();

			var paddleTime = columnTime.Select((val, index) => new { time = val, column = columns[index] }).OrderBy(o => o.time).ToList();
			paddleTime.Add(paddleTime[0]);
			var paddlePos = paddleTime.Take(paddleTime.Count - 1).SelectMany((o, index) => MoreLinq.Range(o.column, (double)(paddleTime[index + 1].column - o.column) / 16, 16)).Round().ToList();

			var color = new LightColor(0, 95, Helpers.Rainbow6.Multiply(Brightness).ToList());

			for (var time = 0; time < 288; ++time)
			{
				pattern.Clear(time);
				for (var columnCtr = 0; columnCtr < columns.Count; columnCtr++)
				{
					var useTime = time - columnTime[columnCtr];
					if ((useTime < 0) || (useTime >= 192))
						continue;
					useTime = Helpers.Cycle(useTime, 0, 96).Round();
					var y = (31d / 768 * useTime * useTime - 31d / 8 * useTime + 93).Round();
					foreach (var light in layout.GetPositionLights(columns[columnCtr], y, 2, 2))
						pattern.AddLight(light, time, color, columns[columnCtr]);
				}
				for (var y = 0; y < 2; ++y)
					for (var x = 0; x < 10; ++x)
					{
						var useTime = time % 96;
						var light = layout.TryGetPositionLight(x + paddlePos[useTime] - 4, y + 95);
						if (light.HasValue)
							pattern.AddLight(light.Value, time, color, x + paddlePos[useTime]);
					}
			}

			pattern.AddLightSequence(0, 96, 2000);
			pattern.AddLightSequence(96, 192, 2000, 8);
			pattern.AddLightSequence(192, 288, 2000);

			return pattern;
		}
	}
}
