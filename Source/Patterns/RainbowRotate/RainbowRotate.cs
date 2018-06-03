using System;
using System.Linq;
using System.Windows;

namespace Shelfinator.Patterns
{
	class RainbowRotate : IPattern
	{
		public int PatternNumber => 15;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			const int StartTime = 500;
			const int RotateTime = 2000;
			const int RotateIncrement = 10;
			const int RotateCount = 3;

			var pattern = new Pattern();
			var layout = new Layout("Shelfinator.Patterns.Layout.Layout-Body.png");
			var lights = layout.GetAllLights();

			var colors = Helpers.Rainbow7.ToList();
			colors.AddRange(Helpers.Rainbow7.AsEnumerable().Reverse().Skip(1));
			colors.InsertRange(0, Helpers.Rainbow7.Skip(1).Reverse());
			colors = colors.Multiply(Brightness).ToList();

			var center = new Point(48, 48);
			var times = lights.Select(light => layout.GetLightPosition(light)).GetDistance(center).Scale(0, null, 0, StartTime).Round().ToList();
			var lightTimes = lights.ToDictionary(times);
			foreach (var pair in lightTimes)
				pattern.Lights.Add(pair.Key, pair.Value, PixelColor.Gradient(colors, layout.GetLightPosition(pair.Key).X, -96, 192));

			for (var angle = 0; angle <= 360; angle += RotateIncrement)
			{
				var time = StartTime + angle * RotateTime / 360;
				var sin = Math.Sin(angle * Math.PI / 180);
				var cos = Math.Cos(angle * Math.PI / 180);
				foreach (var light in lights)
				{
					var location = layout.GetLightPosition(light);
					var newX = (location.X - 48) * cos - (location.Y - 48) * sin + 48;
					var newColor = PixelColor.Gradient(colors, newX, -96, 192);
					pattern.Lights.Add(light, time, newColor);
				}
			}

			foreach (var pair in lightTimes)
				pattern.Lights.Add(pair.Key, StartTime + RotateTime + StartTime - pair.Value, 0x000000);

			pattern.Sequences.Add(new Sequence(0, StartTime));
			pattern.Sequences.Add(new Sequence(StartTime, StartTime + RotateTime, RotateCount));
			pattern.Sequences.Add(new Sequence(StartTime + RotateTime, StartTime + RotateTime + StartTime));
			return pattern;
		}
	}
}
