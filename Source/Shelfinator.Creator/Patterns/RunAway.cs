﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Shelfinator.Creator.PatternData;

namespace Shelfinator.Creator.Patterns
{
	class RunAway : IPattern
	{
		public int PatternNumber => 17;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			const string PathPoints =
"0,0/1,0/2,0/3,0/4,0/5,0/6,0/7,0/8,0/9,0/10,0/11,0/12,0/13,0/14,0/15,0/16,0/17,0/18,0/19,0/20,0/21,0/22,0/23,0/24,0/25,0/26,0/27,0/28,0/29,0/30,0/31,0/32,0/33,0/34,0/35,0/36,0/37,0/38,0/39,0/40,0/41,0/42,0/43,0/44,0/45,0/46,0/47,0/48,0/49,0/50,0/51,0/52,0/53,0/54,0/55,0/56,0/57,0/58,0/59,0/60,0/61,0/62,0/63,0/64,0/65,0/66,0/67,0/68,0/69,0/70,0/71,0/72,0/73,0/74,0/75,0/76,0/77,0/78,0/79,0/80,0/81,0/82,0/83,0/84,0/85,0/86,0/87,0/88,0/89,0/90,0/91,0/92,0/93,0/94,0/95,0/95,1/95,2/95,3/95,4/95,5/95,6/" +
"95,7/95,8/95,9/95,10/95,11/95,12/95,13/95,14/95,15/95,16/95,17/95,18/95,19/94,19/93,19/92,19/91,19/90,19/89,19/88,19/87,19/86,19/85,19/84,19/83,19/82,19/81,19/80,19/79,19/78,19/77,19/76,19/75,19/74,19/73,19/72,19/71,19/70,19/69,19/68,19/67,19/66,19/65,19/64,19/63,19/62,19/61,19/60,19/59,19/58,19/57,19/56,19/55,19/54,19/53,19/52,19/51,19/50,19/49,19/48,19/47,19/46,19/45,19/44,19/43,19/42,19/41,19/40,19/39,19/38,19/37,19/36,19/35,19/34,19/33,19/32,19/31,19/30,19/29,19/28,19/27,19/26,19/25,19/24,19" +
"/23,19/22,19/21,19/20,19/19,19/18,19/17,19/16,19/15,19/14,19/13,19/12,19/11,19/10,19/9,19/8,19/7,19/6,19/5,19/4,19/3,19/2,19/1,19/0,19/0,20/0,21/0,22/0,23/0,24/0,25/0,26/0,27/0,28/0,29/0,30/0,31/0,32/0,33/0,34/0,35/0,36/0,37/0,38/1,38/2,38/3,38/4,38/5,38/6,38/7,38/8,38/9,38/10,38/11,38/12,38/13,38/14,38/15,38/16,38/17,38/18,38/19,38/20,38/21,38/22,38/23,38/24,38/25,38/26,38/27,38/28,38/29,38/30,38/31,38/32,38/33,38/34,38/35,38/36,38/37,38/38,38/39,38/40,38/41,38/42,38/43,38/44,38/45,38/46,38/47," +
"38/48,38/49,38/50,38/51,38/52,38/53,38/54,38/55,38/56,38/57,38/58,38/59,38/60,38/61,38/62,38/63,38/64,38/65,38/66,38/67,38/68,38/69,38/70,38/71,38/72,38/73,38/74,38/75,38/76,38/77,38/78,38/79,38/80,38/81,38/82,38/83,38/84,38/85,38/86,38/87,38/88,38/89,38/90,38/91,38/92,38/93,38/94,38/95,38/95,39/95,40/95,41/95,42/95,43/95,44/95,45/95,46/95,47/95,48/95,49/95,50/95,51/95,52/95,53/95,54/95,55/95,56/95,57/94,57/93,57/92,57/91,57/90,57/89,57/88,57/87,57/86,57/85,57/84,57/83,57/82,57/81,57/80,57/79,57" +
"/78,57/77,57/76,57/75,57/74,57/73,57/72,57/71,57/70,57/69,57/68,57/67,57/66,57/65,57/64,57/63,57/62,57/61,57/60,57/59,57/58,57/57,57/56,57/55,57/54,57/53,57/52,57/51,57/50,57/49,57/48,57/47,57/46,57/45,57/44,57/43,57/42,57/41,57/40,57/39,57/38,57/37,57/36,57/35,57/34,57/33,57/32,57/31,57/30,57/29,57/28,57/27,57/26,57/25,57/24,57/23,57/22,57/21,57/20,57/19,57/18,57/17,57/16,57/15,57/14,57/13,57/12,57/11,57/10,57/9,57/8,57/7,57/6,57/5,57/4,57/3,57/2,57/1,57/0,57/0,58/0,59/0,60/0,61/0,62/0,63/0,64/" +
"0,65/0,66/0,67/0,68/0,69/0,70/0,71/0,72/0,73/0,74/0,75/0,76/1,76/2,76/3,76/4,76/5,76/6,76/7,76/8,76/9,76/10,76/11,76/12,76/13,76/14,76/15,76/16,76/17,76/18,76/19,76/20,76/21,76/22,76/23,76/24,76/25,76/26,76/27,76/28,76/29,76/30,76/31,76/32,76/33,76/34,76/35,76/36,76/37,76/38,76/39,76/40,76/41,76/42,76/43,76/44,76/45,76/46,76/47,76/48,76/49,76/50,76/51,76/52,76/53,76/54,76/55,76/56,76/57,76/58,76/59,76/60,76/61,76/62,76/63,76/64,76/65,76/66,76/67,76/68,76/69,76/70,76/71,76/72,76/73,76/74,76/75,76" +
"/76,76/77,76/78,76/79,76/80,76/81,76/82,76/83,76/84,76/85,76/86,76/87,76/88,76/89,76/90,76/91,76/92,76/93,76/94,76/95,76/95,77/95,78/95,79/95,80/95,81/95,82/95,83/95,84/95,85/95,86/95,87/95,88/95,89/95,90/95,91/95,92/95,93/95,94/95,95/94,95/93,95/92,95/91,95/90,95/89,95/88,95/87,95/86,95/85,95/84,95/83,95/82,95/81,95/80,95/79,95/78,95/77,95/76,95/75,95/74,95/73,95/72,95/71,95/70,95/69,95/68,95/67,95/66,95/65,95/64,95/63,95/62,95/61,95/60,95/59,95/58,95/57,95/56,95/55,95/54,95/53,95/52,95/51,95/5" +
"0,95/49,95/48,95/47,95/46,95/45,95/44,95/43,95/42,95/41,95/40,95/39,95/38,95/37,95/36,95/35,95/34,95/33,95/32,95/31,95/30,95/29,95/28,95/27,95/26,95/25,95/24,95/23,95/22,95/21,95/20,95/19,95/18,95/17,95/16,95/15,95/14,95/13,95/12,95/11,95/10,95/9,95/8,95/7,95/6,95/5,95/4,95/3,95/2,95/1,95/0,95/0,94/0,93/0,92/0,91/0,90/0,89/0,88/0,87/0,86/0,85/0,84/0,83/0,82/0,81/0,80/0,79/0,78/0,77/0,76/0,75/0,74/0,73/0,72/0,71/0,70/0,69/0,68/0,67/0,66/0,65/0,64/0,63/0,62/0,61/0,60/0,59/0,58/0,57/0,56/0,55/0,54/" +
"0,53/0,52/0,51/0,50/0,49/0,48/0,47/0,46/0,45/0,44/0,43/0,42/0,41/0,40/0,39/0,38/0,37/0,36/0,35/0,34/0,33/0,32/0,31/0,30/0,29/0,28/0,27/0,26/0,25/0,24/0,23/0,22/0,21/0,20/0,19/0,18/0,17/0,16/0,15/0,14/0,13/0,12/0,11/0,10/0,9/0,8/0,7/0,6/0,5/0,4/0,3/0,2/0,1/0,0/1,0/2,0/3,0/4,0/5,0/6,0/7,0/8,0/9,0/10,0/11,0/12,0/13,0/14,0/15,0/16,0/17,0/18,0/19,0/19,1/19,2/19,3/19,4/19,5/19,6/19,7/19,8/19,9/19,10/19,11/19,12/19,13/19,14/19,15/19,16/19,17/19,18/19,19/19,20/19,21/19,22/19,23/19,24/19,25/19,26/19,27/1" +
"9,28/19,29/19,30/19,31/19,32/19,33/19,34/19,35/19,36/19,37/19,38/19,39/19,40/19,41/19,42/19,43/19,44/19,45/19,46/19,47/19,48/19,49/19,50/19,51/19,52/19,53/19,54/19,55/19,56/19,57/19,58/19,59/19,60/19,61/19,62/19,63/19,64/19,65/19,66/19,67/19,68/19,69/19,70/19,71/19,72/19,73/19,74/19,75/19,76/19,77/19,78/19,79/19,80/19,81/19,82/19,83/19,84/19,85/19,86/19,87/19,88/19,89/19,90/19,91/19,92/19,93/19,94/19,95/20,95/21,95/22,95/23,95/24,95/25,95/26,95/27,95/28,95/29,95/30,95/31,95/32,95/33,95/34,95/35," +
"95/36,95/37,95/38,95/38,94/38,93/38,92/38,91/38,90/38,89/38,88/38,87/38,86/38,85/38,84/38,83/38,82/38,81/38,80/38,79/38,78/38,77/38,76/38,75/38,74/38,73/38,72/38,71/38,70/38,69/38,68/38,67/38,66/38,65/38,64/38,63/38,62/38,61/38,60/38,59/38,58/38,57/38,56/38,55/38,54/38,53/38,52/38,51/38,50/38,49/38,48/38,47/38,46/38,45/38,44/38,43/38,42/38,41/38,40/38,39/38,38/38,37/38,36/38,35/38,34/38,33/38,32/38,31/38,30/38,29/38,28/38,27/38,26/38,25/38,24/38,23/38,22/38,21/38,20/38,19/38,18/38,17/38,16/38,15" +
"/38,14/38,13/38,12/38,11/38,10/38,9/38,8/38,7/38,6/38,5/38,4/38,3/38,2/38,1/38,0/39,0/40,0/41,0/42,0/43,0/44,0/45,0/46,0/47,0/48,0/49,0/50,0/51,0/52,0/53,0/54,0/55,0/56,0/57,0/57,1/57,2/57,3/57,4/57,5/57,6/57,7/57,8/57,9/57,10/57,11/57,12/57,13/57,14/57,15/57,16/57,17/57,18/57,19/57,20/57,21/57,22/57,23/57,24/57,25/57,26/57,27/57,28/57,29/57,30/57,31/57,32/57,33/57,34/57,35/57,36/57,37/57,38/57,39/57,40/57,41/57,42/57,43/57,44/57,45/57,46/57,47/57,48/57,49/57,50/57,51/57,52/57,53/57,54/57,55/57," +
"56/57,57/57,58/57,59/57,60/57,61/57,62/57,63/57,64/57,65/57,66/57,67/57,68/57,69/57,70/57,71/57,72/57,73/57,74/57,75/57,76/57,77/57,78/57,79/57,80/57,81/57,82/57,83/57,84/57,85/57,86/57,87/57,88/57,89/57,90/57,91/57,92/57,93/57,94/57,95/58,95/59,95/60,95/61,95/62,95/63,95/64,95/65,95/66,95/67,95/68,95/69,95/70,95/71,95/72,95/73,95/74,95/75,95/76,95/76,94/76,93/76,92/76,91/76,90/76,89/76,88/76,87/76,86/76,85/76,84/76,83/76,82/76,81/76,80/76,79/76,78/76,77/76,76/76,75/76,74/76,73/76,72/76,71/76,70" +
"/76,69/76,68/76,67/76,66/76,65/76,64/76,63/76,62/76,61/76,60/76,59/76,58/76,57/76,56/76,55/76,54/76,53/76,52/76,51/76,50/76,49/76,48/76,47/76,46/76,45/76,44/76,43/76,42/76,41/76,40/76,39/76,38/76,37/76,36/76,35/76,34/76,33/76,32/76,31/76,30/76,29/76,28/76,27/76,26/76,25/76,24/76,23/76,22/76,21/76,20/76,19/76,18/76,17/76,16/76,15/76,14/76,13/76,12/76,11/76,10/76,9/76,8/76,7/76,6/76,5/76,4/76,3/76,2/76,1/76,0/77,0/78,0/79,0/80,0/81,0/82,0/83,0/84,0/85,0/86,0/87,0/88,0/89,0/90,0/91,0/92,0/93,0/94,0" +
"/95,0/95,1/95,2/95,3/95,4/95,5/95,6/95,7/95,8/95,9/95,10/95,11/95,12/95,13/95,14/95,15/95,16/95,17/95,18/95,19/95,20/95,21/95,22/95,23/95,24/95,25/95,26/95,27/95,28/95,29/95,30/95,31/95,32/95,33/95,34/95,35/95,36/95,37/95,38/95,39/95,40/95,41/95,42/95,43/95,44/95,45/95,46/95,47/95,48/95,49/95,50/95,51/95,52/95,53/95,54/95,55/95,56/95,57/95,58/95,59/95,60/95,61/95,62/95,63/95,64/95,65/95,66/95,67/95,68/95,69/95,70/95,71/95,72/95,73/95,74/95,75/95,76/95,77/95,78/95,79/95,80/95,81/95,82/95,83/95,84" +
"/95,85/95,86/95,87/95,88/95,89/95,90/95,91/95,92/95,93/95,94/95,95";

			var segment = new Segment();
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");
			var pathPoints = PathPoints.Split('/').Select(p => Point.Parse(p)).ToList();

			var squares = new Layout("Shelfinator.Creator.Patterns.Layout.Squares.png");
			var squareLights = new Dictionary<int, List<int>>();
			var squareCenter = new Dictionary<int, Point>();
			foreach (var square in squares.GetAllLights())
			{
				if (square == 0)
					continue;

				var mappedSquareLights = layout.GetMappedLights(squares, square);
				squareLights[square] = mappedSquareLights;
				var squareLocations = mappedSquareLights.Select(light => layout.GetLightPosition(light)).OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
				squareCenter[square] = squareLocations.First() + (squareLocations.Last() - squareLocations.First()) / 2;
			}

			var colors = new List<int> { 0x000080, 0x0000ff, 0x008000, 0x008080, 0x0080ff, 0x00ff00, 0x00ff80, 0x00ffff, 0x800000, 0x800080, 0x8000ff, 0x808000, 0x808080, 0x8080ff, 0x80ff00, 0x80ff80, 0x80ffff, 0xff0000, 0xff0080, 0xff00ff, 0xff8000, 0xff8080, 0xff80ff, 0xffff00, 0xffff80 }.Multiply(Brightness).ToList();
			var time = 0;
			foreach (var pathPoint in pathPoints)
			{
				foreach (var light in layout.GetPositionLights(pathPoint, 2, 2))
				{
					segment.AddLight(light, time, Segment.Absolute, Helpers.MultiplyColor(0xffffff, Brightness));
					segment.AddLight(light, time + 1, Segment.Absolute, 0x000000);
				}

				foreach (var square in squareLights.Keys)
				{
					var focusAngle = Helpers.GetAngle(squareCenter[square], pathPoint);
					var useLight = squareLights[square].OrderBy(light => Math.Abs(focusAngle - Helpers.GetAngle(layout.GetLightPosition(light), squareCenter[square]))).First();

					segment.AddLight(useLight, time, Segment.Absolute, colors[square - 1]);
					segment.AddLight(useLight, time + 1, Segment.Absolute, 0x000000);
				}

				++time;
			}
			segment.Clear(time);

			var pattern = new Pattern();
			pattern.AddSegment(segment, 0, time, 15000);
			pattern.AddSegment(segment, time, time, 2000);
			return pattern;
		}
	}
}
