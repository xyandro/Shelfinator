using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Shelfinator.Creator.Patterns
{
	class Watching : IPattern
	{
		public int PatternNumber => 17;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			const string LightData =
"0,0/1,0/2,0/3,0/4,0/5,0/6,0/7,0/8,0/9,0/10,0/11,0/12,0/13,0/14,0/15,0/16,0/17,0/18,0/19,0/20,0/21,0/22,0/23,0/24,0/25,0/26,0/27,0/28,0/29,0/30,0/31,0/32,0/33,0/34,0/35,0/36,0/37,0/38,0/39,0/40,0/41,0/42,0/43,0/44,0/45,0/46,0/47,0/48,0/49,0/50,0/51,0/52,0/53,0/54,0/55,0/56,0/57,0/58,0/59,0/60,0/61,0/62,0/63,0/64,0/65,0/66,0/67,0/68,0/69,0/70,0/71,0/72,0/73,0/74,0/75,0/76,0/77,0/78,0/79,0/80,0/81,0/82,0/83,0/84,0/85,0/86,0/87,0/88,0/89,0/90,0/91,0/92,0/93,0/94,0/95,0/96,0/96,1/96,2/96,3/96,4/96,5/" +
"96,6/96,7/96,8/96,9/96,10/96,11/96,12/96,13/96,14/96,15/96,16/96,17/96,18/96,19/96,20/96,21/96,22/96,23/96,24/96,25/96,26/96,27/96,28/96,29/96,30/96,31/96,32/96,33/96,34/96,35/96,36/96,37/96,38/96,39/96,40/96,41/96,42/96,43/96,44/96,45/96,46/96,47/96,48/96,49/96,50/96,51/96,52/96,53/96,54/96,55/96,56/96,57/96,58/96,59/96,60/96,61/96,62/96,63/96,64/96,65/96,66/96,67/96,68/96,69/96,70/96,71/96,72/96,73/96,74/96,75/96,76/96,77/96,78/96,79/96,80/96,81/96,82/96,83/96,84/96,85/96,86/96,87/96,88/96,89/" +
"96,90/96,91/96,92/96,93/96,94/96,95/96,96/95,96/94,96/93,96/92,96/91,96/90,96/89,96/88,96/87,96/86,96/85,96/84,96/83,96/82,96/81,96/80,96/79,96/78,96/77,96/76,96/75,96/74,96/73,96/72,96/71,96/70,96/69,96/68,96/67,96/66,96/65,96/64,96/63,96/62,96/61,96/60,96/59,96/58,96/57,96/56,96/55,96/54,96/53,96/52,96/51,96/50,96/49,96/48,96/47,96/46,96/45,96/44,96/43,96/42,96/41,96/40,96/39,96/38,96/37,96/36,96/35,96/34,96/33,96/32,96/31,96/30,96/29,96/28,96/27,96/26,96/25,96/24,96/23,96/22,96/21,96/20,96/19" +
",96/18,96/17,96/16,96/15,96/14,96/13,96/12,96/11,96/10,96/9,96/8,96/7,96/6,96/5,96/4,96/3,96/2,96/1,96/0,96/0,95/0,94/0,93/0,92/0,91/0,90/0,89/0,88/0,87/0,86/0,85/0,84/0,83/0,82/0,81/0,80/0,79/0,78/0,77/0,76/0,75/0,74/0,73/0,72/0,71/0,70/0,69/0,68/0,67/0,66/0,65/0,64/0,63/0,62/0,61/0,60/0,59/0,58/0,57/0,56/0,55/0,54/0,53/0,52/0,51/0,50/0,49/0,48/0,47/0,46/0,45/0,44/0,43/0,42/0,41/0,40/0,39/0,38/0,37/0,36/0,35/0,34/0,33/0,32/0,31/0,30/0,29/0,28/0,27/0,26/0,25/0,24/0,23/0,22/0,21/0,20/0,19/0,18/0," +
"17/0,16/0,15/0,14/0,13/0,12/0,11/0,10/0,9/0,8/0,7/0,6/0,5/0,4/0,3/0,2/0,1";

			var pattern = new Pattern();
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");
			var watchingLights = LightData.Split('/').Select(p => layout.GetPositionLight(Point.Parse(p))).ToList();

			var squares = new Layout("Shelfinator.Creator.Patterns.Layout.Squares.png");
			var lightSquare = new Dictionary<int, int>();
			var squareLightsDict = new Dictionary<int, List<int>>();
			var squareCenter = new Dictionary<int, Point>();
			foreach (var square in squares.GetAllLights())
			{
				if (square == 0)
					continue;

				var squareLights = layout.GetMappedLights(squares, square);
				squareLightsDict[square] = squareLights;
				squareLights.ForEach(light => lightSquare[light] = square);
				var squareLocations = squareLights.Select(light => layout.GetLightPosition(light)).ToList();
				var topLeft = squareLocations.OrderBy(p => p.X).ThenBy(p => p.Y).First();
				var bottomRight = squareLocations.OrderBy(p => p.X).ThenBy(p => p.Y).Last();
				squareCenter[square] = new Point((topLeft.X + bottomRight.X) / 2, (topLeft.Y + bottomRight.Y) / 2);
			}
			var lights = lightSquare.Keys.ToList();

			var colors = new List<int> { 0x000080, 0x0000ff, 0x008000, 0x008080, 0x0080ff, 0x00ff00, 0x00ff80, 0x00ffff, 0x800000, 0x800080, 0x8000ff, 0x808000, 0x808080, 0x8080ff, 0x80ff00, 0x80ff80, 0x80ffff, 0xff0000, 0xff0080, 0xff00ff, 0xff8000, 0xff8080, 0xff80ff, 0xffff00, 0xffff80 }.Multiply(Brightness).ToList();
			var useColors = colors.Select(color => new LightColor(-5, 5, new List<int> { 0x000000, color, 0x000000 })).ToList();
			var time = 0;
			foreach (var watchingLight in watchingLights)
			{
				pattern.AddLight(watchingLight, time, pattern.Absolute, Helpers.MultiplyColor(0xffffff, Brightness));
				pattern.AddLight(watchingLight, time + 1, pattern.Absolute, 0x000000);
				var focusCenter = layout.GetLightPosition(watchingLight);

				foreach (var square in squareLightsDict.Keys)
				{
					var center = squareCenter[square];
					var focusAngle = Helpers.GetAngle(center, focusCenter);
					foreach (var light in squareLightsDict[square])
					{
						var lightAngle = Helpers.GetAngle(center, layout.GetLightPosition(light));
						var diff = Math.Abs(focusAngle - lightAngle);
						pattern.AddLight(light, time, useColors[square - 1], (int)diff);
					}
				}

				++time;
			}
			pattern.Clear(time);

			pattern.AddLightSequence(0, time, 5000, 3);
			pattern.AddLightSequence(time, time, 2000);

			return pattern;
		}
	}
}
