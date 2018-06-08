using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Shelfinator.Patterns
{
	class Watching : IPattern
	{
		public int PatternNumber => 17;

		public Pattern Render()
		{
			const double WatchingBrightness = 1f / 16;

			var pattern = new Pattern();
			var layout = new Layout("Shelfinator.Patterns.Layout.Layout-Body.png");
			var watching = new Layout("Shelfinator.Patterns.Watching.Watching.png");

			var values = watching.GetAllLights().Select(light => new { light, value = light & 0xfff }).Concat(watching.GetAllLights().Select(light => new { light, value = (light & 0xfff000) >> 12 })).Where(obj => obj.value != 0).OrderBy(obj => obj.value).Select(obj => obj.light).ToList();
			var watchingLights = values.Select(light => watching.GetLightPosition(light)).Select(p => layout.GetPositionLights(new Rect(p, new Size(2, 2)))).ToList();

			var squares = new Layout("Shelfinator.Patterns.Layout.Squares.png");
			var lightSquare = new Dictionary<int, int>();
			var squareLights2 = new Dictionary<int, List<int>>();
			var squareCenter = new Dictionary<int, Point>();
			foreach (var square in squares.GetAllLights())
			{
				if (square == 0)
					continue;

				var squareLights = layout.GetMappedLights(squares, square);
				squareLights2[square] = squareLights;
				squareLights.ForEach(light => lightSquare[light] = square);
				var squareLocations = squareLights.Select(light => layout.GetLightPosition(light)).ToList();
				var topLeft = squareLocations.OrderBy(p => p.X).ThenBy(p => p.Y).First();
				var bottomRight = squareLocations.OrderBy(p => p.X).ThenBy(p => p.Y).Last();
				squareCenter[square] = new Point((topLeft.X + bottomRight.X) / 2, (topLeft.Y + bottomRight.Y) / 2);
			}
			var lights = lightSquare.Keys.ToList();

			var colors = new List<int> { 0x000080, 0x0000ff, 0x008000, 0x008080, 0x0080ff, 0x00ff00, 0x00ff80, 0x00ffff, 0x800000, 0x800080, 0x8000ff, 0x808000, 0x808080, 0x8080ff, 0x80ff00, 0x80ff80, 0x80ffff, 0xff0000, 0xff0080, 0xff00ff, 0xff8000, 0xff8080, 0xff80ff, 0xffff00, 0xffff80 }.Multiply(WatchingBrightness).ToList();
			var useColors = colors.Select(color => new LightColor(-15, 15, new List<int> { 0x000000, color, 0x000000 })).ToList();
			var time = 0;
			foreach (var list in watchingLights)
			{
				var focusCenter = layout.GetLightPosition(list[0]) + new Vector(0.5, 0.5);

				foreach (var square in squareLights2.Keys)
				{
					var center = squareCenter[square];
					var focusAngle = Helpers.GetAngle(center, focusCenter);
					foreach (var light in squareLights2[square])
					{
						var lightAngle = Helpers.GetAngle(center, layout.GetLightPosition(light));
						var diff = Math.Abs(focusAngle - lightAngle);
						pattern.AddLight(light, time, useColors[square - 1], (int)diff);
					}
				}

				++time;
			}
			pattern.Clear(time);

			pattern.AddLightSequence(0, time, 30000);
			pattern.AddLightSequence(time, time, 2000);

			return pattern;
		}
	}
}
