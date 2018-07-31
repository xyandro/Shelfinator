using System;
using System.Linq;
using System.Windows;

namespace Shelfinator.Creator.Patterns
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
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");
			var lights = layout.GetAllLights();

			var colors = Helpers.Rainbow7.ToList();
			colors.AddRange(Helpers.Rainbow7.AsEnumerable().Reverse().Skip(1));
			colors.InsertRange(0, Helpers.Rainbow7.Skip(1).Reverse());
			colors = colors.Multiply(Brightness).ToList();
			var useColors = new LightColor(-96, 192, colors);

			var center = new Point(48, 48);
			var times = lights.Select(light => layout.GetLightPosition(light)).GetDistance(center).Scale(0, null, 0, StartTime).Round().ToList();
			var lightTimes = lights.ToDictionary(times);
			foreach (var pair in lightTimes)
				pattern.AddLight(pair.Key, pair.Value, useColors, layout.GetLightPosition(pair.Key).X.Round());

			for (var angle = 0; angle <= 360; angle += RotateIncrement)
			{
				var time = StartTime + angle * RotateTime / 360;
				var nextTime = StartTime + (angle + RotateIncrement) * RotateTime / 360;
				var sin = Math.Sin(angle * Math.PI / 180);
				var cos = Math.Cos(angle * Math.PI / 180);
				foreach (var light in lights)
				{
					var location = layout.GetLightPosition(light);
					var newX = (location.X - 48) * cos - (location.Y - 48) * sin + 48;
					pattern.AddLight(light, time, nextTime, null, 0, useColors, newX.Round());
				}
			}

			foreach (var pair in lightTimes)
				pattern.AddLight(pair.Key, StartTime + RotateTime + StartTime - pair.Value, pattern.Absolute, 0x000000);

			pattern.AddLightSequence(0, StartTime);
			pattern.AddLightSequence(StartTime, StartTime + RotateTime, repeat: RotateCount);
			pattern.AddLightSequence(StartTime + RotateTime, StartTime + RotateTime + StartTime);
			return pattern;
		}
	}
}
