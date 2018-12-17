using System;
using System.Linq;
using Shelfinator.Creator.PatternData;

namespace Shelfinator.Creator.Patterns
{
	class Sine : IPattern
	{
		public int PatternNumber => 12;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			const int Size = 2;

			var segment = new Segment();
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");
			var allLights = layout.GetAllLights();
			var allLocations = allLights.Select(light => layout.GetLightPosition(light)).ToList();
			var ordered = allLocations.OrderBy(p => p.X).ThenBy(p => p.Y);

			var rainbow7 = Helpers.Rainbow7.Multiply(Brightness).ToList();
			for (var time = 0; time < 1000; time += 20)
			{
				segment.Clear(time);

				for (var x = 0; x <= 96; x += 19)
				{
					var y = ((Math.Sin(2 * Math.PI * (time / 1000D + x / 96D)) + 1) * (97 - Size) / 2).Round();
					var lights = layout.GetPositionLights(x, y, 2, 2);
					foreach (var light in lights)
						segment.AddLight(light, time, Segment.Absolute, Helpers.MultiplyColor(0xff0000, Brightness));

					y = ((Math.Cos(2 * Math.PI * (time / 1000D + x / 96D)) + 1) * (97 - Size) / 2).Round();
					lights = layout.GetPositionLights(y, x, 2, 2);
					foreach (var light in lights)
						segment.AddLight(light, time, Segment.Absolute, Helpers.MultiplyColor(0x0000ff, Brightness));
				}
			}

			var pattern = new Pattern();
			pattern.AddSegment(segment, 0, 1000, 2000, 5);
			return pattern;
		}
	}
}
