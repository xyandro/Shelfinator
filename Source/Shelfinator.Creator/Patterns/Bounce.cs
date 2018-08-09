using System.Collections.Generic;
using System.Linq;

namespace Shelfinator.Creator.Patterns
{
	class Bounce : IPattern
	{
		public int PatternNumber => 23;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;

			var pattern = new Pattern();

			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");
			var columns = new List<int> { 0, 19, 38, 57, 76, 95 };
			var columnTime = new List<int> { 48, 64, 0, 32, 80, 16 };

			var paddleDest = columnTime.Select((val, index) => new { time = val, column = columns[index] }).OrderBy(o => o.time).Select(o => o.column).ToList();

			var color = new LightColor(0, 95, Helpers.Rainbow6.Multiply(Brightness).ToList());

			double paddlePos = 0;
			int paddleIndex = 0, paddleSteps = 0;
			for (var time = 0; time <= 437; ++time)
			{
				pattern.Clear(time);
				for (var columnCtr = 0; columnCtr < columns.Count; columnCtr++)
				{
					int y;
					var useTime = time - columnTime[columnCtr];
					if (time < 96)
					{
						y = 95 - time;
						foreach (var light in layout.GetPositionLights(columns[columnCtr], y + 2, 2, 1))
							pattern.AddLight(light, time, pattern.Absolute, Helpers.MultiplyColor(0xffffff, Brightness));
					}
					else if (useTime < 116)
						y = 0;
					else if (useTime == 116)
					{
						y = 0;
						foreach (var light in layout.GetPositionLights(columns[columnCtr], 2, 2, 1))
						{
							pattern.AddLight(light, 96, pattern.Absolute, Helpers.MultiplyColor(0xffffff, Brightness));
							pattern.AddLight(light, time - 20, time, null, pattern.Absolute, 0x000000);
						}
					}
					else if (useTime <= 356)
					{
						var yTime = Helpers.Cycle(useTime - 68, 0, 96).Round();
						y = (31d / 768 * yTime * yTime - 31d / 8 * yTime + 93).Round();
					}
					else
						continue;

					foreach (var light in layout.GetPositionLights(columns[columnCtr], y, 2, 2))
						pattern.AddLight(light, time, color, columns[columnCtr]);
				}

				if (time < 148)
				{
				}
				else if (time == 148)
				{
					foreach (var light in layout.GetPositionLights(0, 95, 97, 2))
					{
						var x = layout.GetLightPosition(light).X.Round();
						pattern.AddLight(light, 5, 48, null, color, x);
						if ((x <= 43) || (x > 53))
							pattern.AddLight(light, 48, 96, null, pattern.Absolute, 0x000000);
					}
					paddlePos = 48;
					paddleSteps = 164 - time;
				}
				else
				{
					if (paddleSteps == 0)
					{
						++paddleIndex;
						if (paddleIndex >= paddleDest.Count)
							paddleIndex = 0;
						paddleSteps = time > 340 ? 64 : 16;
					}

					var dest = time > 340 ? -10 : paddleDest[paddleIndex];
					paddlePos += (dest - paddlePos) / paddleSteps;
					--paddleSteps;

					for (var y = 0; y < 2; ++y)
						for (var x = 0; x < 10; ++x)
						{
							var pos = paddlePos.Round();
							var light = layout.TryGetPositionLight(x + pos - 4, y + 95);
							if (light.HasValue)
								pattern.AddLight(light.Value, time, color, x + pos);
						}
				}
			}

			pattern.AddLightSequence(0, 96, 4000);
			pattern.AddLightSequence(96, 196, 2083);
			pattern.AddLightSequence(196, 292, 2000, 9);
			pattern.AddLightSequence(292, 437, 3021);
			pattern.AddLightSequence(437, 437, 1000);

			return pattern;
		}
	}
}
