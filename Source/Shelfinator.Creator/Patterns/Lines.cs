using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Shelfinator.Creator.Patterns
{
	class Lines : IPattern
	{
		public int PatternNumber => 44;
		const double Brightness = 1f / 16;
		const double MinWidth = 1;
		const double MaxWidth = 1.5;
		const int TimeMultiplier = 50;

		public Pattern Render()
		{
			const int RepeatCount = 3;

			var pattern = new Pattern();
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");
			var squares = new Layout("Shelfinator.Creator.Patterns.Layout.Squares.png");

			var time = 0;
			var color = new LightColor(0, 1000,
				new List<int> { 0x000000, 0x000000 }.Multiply(Brightness).ToList(),
				new List<int> { 0x000000, 0xffffff }.Multiply(Brightness).ToList(),
				new List<int> { 0x000000, 0x0000ff }.Multiply(Brightness).ToList(),
				new List<int> { 0x000000, 0xff0000 }.Multiply(Brightness).ToList(),
				new List<int> { 0x000000, 0x00ff00 }.Multiply(Brightness).ToList()
			);

			AddPointData(13, 0, 180, ref time, pattern, squares, layout, color, 3);
			AddPointData(8, 0, 45, ref time, pattern, squares, layout, color);
			for (var ctr = 0; ctr < 3; ++ctr)
			{
				AddPointData(8, 45, 135, ref time, pattern, squares, layout, color);
				AddPointData(14, 135, 225, ref time, pattern, squares, layout, color);
				AddPointData(18, 45, 135, ref time, pattern, squares, layout, color);
				AddPointData(12, 135, 225, ref time, pattern, squares, layout, color);
			}
			AddPointData(8, 45, 90, ref time, pattern, squares, layout, color);
			for (var ctr = 0; ctr < 3; ++ctr)
			{
				AddPointData(9, 90, 180, ref time, pattern, squares, layout, color);
				AddPointData(19, 0, 90, ref time, pattern, squares, layout, color);
				AddPointData(17, 90, 180, ref time, pattern, squares, layout, color);
				AddPointData(7, 0, 90, ref time, pattern, squares, layout, color);
			}
			AddPointData(8, 90, 180, ref time, pattern, squares, layout, color);
			AddPointData(13, 0, 180, ref time, pattern, squares, layout, color, 3);

			time = pattern.MaxLightSequenceTime();
			pattern.AddPaletteSequence(0, 1000, 0, 1);
			pattern.AddPaletteSequence(time * 1 / 4 - 500, time * 1 / 4 + 500, null, 2);
			pattern.AddPaletteSequence(time * 2 / 4 - 500, time * 2 / 4 + 500, null, 3);
			pattern.AddPaletteSequence(time * 3 / 4 - 500, time * 3 / 4 + 500, null, 4);
			pattern.AddPaletteSequence(time - 1000, time, null, 0);

			return pattern;
		}

		void AddPointData(int square, double startAngle, double endAngle, ref int time, Pattern pattern, Layout squares, Layout layout, LightColor color, int repeat = 1)
		{
			var positions = squares.GetLightPositions(square);
			var center = new Point((positions.Min(p => p.X) + positions.Max(p => p.X)) / 2, (positions.Min(p => p.Y) + positions.Max(p => p.Y)) / 2);
			var startTime = time;
			var count = Math.Abs(endAngle - startAngle) / 5;
			for (var ctr = 0; ctr < count; ++ctr)
			{
				foreach (var light in layout.GetAllLights())
				{
					var pos = layout.GetLightPosition(light);
					var percent = 0D;

					var useAngle = (startAngle + (endAngle - startAngle) * ctr / count) * Math.PI / 180;
					var sin = Math.Sin(useAngle);
					var cos = -Math.Cos(useAngle);
					var R = (pos.X - center.X) * sin + (pos.Y - center.Y) * cos;
					var dist = Math.Sqrt(Math.Pow(R * sin + center.X - pos.X, 2) + Math.Pow(R * cos + center.Y - pos.Y, 2));
					if (dist < MaxWidth)
						percent += Math.Min(1 - (dist - MinWidth) / (MaxWidth - MinWidth), 1);

					pattern.AddLight(light, time, color, (percent * 1000).Round());
				}
				++time;
			}
			pattern.AddLightSequence(startTime, time, (time - startTime) * TimeMultiplier, repeat);
		}
	}
}
