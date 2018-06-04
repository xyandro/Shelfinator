using System;
using System.Linq;
using System.Windows;

namespace Shelfinator.Patterns
{
	class Snake : IPattern
	{
		public int PatternNumber => 16;

		public Pattern Render()
		{
			const double SnakeBrightness = 1f / 16;
			const double PelletBrightness = 1f / 16;
			const int LengthIncrease = 32;
			const int ColorLength = 100;

			var pattern = new Pattern();
			var layout = new Layout("Shelfinator.Patterns.Layout.Layout-Body.png");
			var snake = new Layout("Shelfinator.Patterns.Snake.Snake.png");

			var colors = Helpers.Rainbow7.Multiply(SnakeBrightness).ToList();
			colors.AddRange(colors.AsEnumerable().Reverse().Skip(1));
			var snakeLights = snake.GetAllLights().OrderBy(light => light).Select(light => snake.GetLightPosition(light)).Select(p => layout.GetPositionLights(new Rect(p, new Size(2, 2)))).ToList();

			var time = 0;
			var busy = snakeLights.Select(b => false).ToArray();
			var snakeStart = -1;
			var snakeEnd = -1;
			var addLen = LengthIncrease;
			var pellet = default(int?);
			var rand = new Random(0xbadf00d);
			while (true)
			{
				var busyCount = busy.Count(b => b);

				++snakeStart;
				if (snakeStart >= snakeLights.Count)
					snakeStart -= snakeLights.Count;
				var color = PixelColor.Gradient(colors, time % ColorLength, 0, 99);
				foreach (var light in snakeLights[snakeStart])
					pattern.Lights.Add(light, time, color);

				busy[snakeStart] = true;
				if (snakeStart == pellet)
				{
					pellet = null;
					addLen += LengthIncrease;
				}

				if ((addLen > 0) && (time % 4 == 0))
					--addLen;
				else
				{
					++snakeEnd;
					if (snakeEnd >= snakeLights.Count)
						snakeEnd -= snakeLights.Count;
					foreach (var light in snakeLights[snakeEnd])
						pattern.Lights.Add(light, time, 0x000000);
					busy[snakeEnd] = false;
				}

				if (!pellet.HasValue)
				{
					pellet = busy.Indexes(b => !b).OrderBy(b => rand.Next()).DefaultIfEmpty(-1).First();
					if (pellet == -1)
						break;
					foreach (var light in snakeLights[pellet.Value])
						pattern.Lights.Add(light, time, new PixelColor(0xffffff) * PelletBrightness);
				}

				++time;
			}

			pattern.Sequences.Add(new Sequence(0, time, duration: 30000));
			pattern.Sequences.Add(new Sequence(time, time, duration: 2000));

			return pattern;
		}
	}
}
