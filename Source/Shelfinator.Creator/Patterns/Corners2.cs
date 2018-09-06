﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Shelfinator.Creator.Patterns
{
	class Corners2 : IPattern
	{
		public int PatternNumber => 31;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			const string lightLocations =
"1,1/2,1/3,1/4,1/5,1/6,1/7,1/8,1/9,1/10,1/11,1/12,1/13,1/14,1/15,1/16,1/17,1/18,1/19,1/19,2/19,3/19,4/19,5/19,6/19,7/19,8/19,9/19,10/19,11/19,12/19,13/19,14/19,15/19,16/19,17/19,18/19,19/18,19/17,19/16,19/15,19/14,19/13,19/12,19/11,19/10,19/9,19/8,19/7,19/6,19/5,19/4,19/3,19/2,19/1,19/1,18/1,17/1,16/1,15/1,14/1,13/1,12/1,11/1,10/1,9/1,8/1,7/1,6/1,5/1,4/1,3/1,2|" +
"20,1/20,2/20,3/20,4/20,5/20,6/20,7/20,8/20,9/20,10/20,11/20,12/20,13/20,14/20,15/20,16/20,17/20,18/20,19/21,19/22,19/23,19/24,19/25,19/26,19/27,19/28,19/29,19/30,19/31,19/32,19/33,19/34,19/35,19/36,19/37,19/38,19/38,18/38,17/38,16/38,15/38,14/38,13/38,12/38,11/38,10/38,9/38,8/38,7/38,6/38,5/38,4/38,3/38,2/38,1/37,1/36,1/35,1/34,1/33,1/32,1/31,1/30,1/29,1/28,1/27,1/26,1/25,1/24,1/23,1/22,1/21,1|" +
"39,1/40,1/41,1/42,1/43,1/44,1/45,1/46,1/47,1/48,1/49,1/50,1/51,1/52,1/53,1/54,1/55,1/56,1/57,1/57,2/57,3/57,4/57,5/57,6/57,7/57,8/57,9/57,10/57,11/57,12/57,13/57,14/57,15/57,16/57,17/57,18/57,19/56,19/55,19/54,19/53,19/52,19/51,19/50,19/49,19/48,19/47,19/46,19/45,19/44,19/43,19/42,19/41,19/40,19/39,19/39,18/39,17/39,16/39,15/39,14/39,13/39,12/39,11/39,10/39,9/39,8/39,7/39,6/39,5/39,4/39,3/39,2|" +
"58,1/58,2/58,3/58,4/58,5/58,6/58,7/58,8/58,9/58,10/58,11/58,12/58,13/58,14/58,15/58,16/58,17/58,18/58,19/59,19/60,19/61,19/62,19/63,19/64,19/65,19/66,19/67,19/68,19/69,19/70,19/71,19/72,19/73,19/74,19/75,19/76,19/76,18/76,17/76,16/76,15/76,14/76,13/76,12/76,11/76,10/76,9/76,8/76,7/76,6/76,5/76,4/76,3/76,2/76,1/75,1/74,1/73,1/72,1/71,1/70,1/69,1/68,1/67,1/66,1/65,1/64,1/63,1/62,1/61,1/60,1/59,1|" +
"77,1/78,1/79,1/80,1/81,1/82,1/83,1/84,1/85,1/86,1/87,1/88,1/89,1/90,1/91,1/92,1/93,1/94,1/95,1/95,2/95,3/95,4/95,5/95,6/95,7/95,8/95,9/95,10/95,11/95,12/95,13/95,14/95,15/95,16/95,17/95,18/95,19/94,19/93,19/92,19/91,19/90,19/89,19/88,19/87,19/86,19/85,19/84,19/83,19/82,19/81,19/80,19/79,19/78,19/77,19/77,18/77,17/77,16/77,15/77,14/77,13/77,12/77,11/77,10/77,9/77,8/77,7/77,6/77,5/77,4/77,3/77,2|" +
"1,20/1,21/1,22/1,23/1,24/1,25/1,26/1,27/1,28/1,29/1,30/1,31/1,32/1,33/1,34/1,35/1,36/1,37/1,38/2,38/3,38/4,38/5,38/6,38/7,38/8,38/9,38/10,38/11,38/12,38/13,38/14,38/15,38/16,38/17,38/18,38/19,38/19,37/19,36/19,35/19,34/19,33/19,32/19,31/19,30/19,29/19,28/19,27/19,26/19,25/19,24/19,23/19,22/19,21/19,20/18,20/17,20/16,20/15,20/14,20/13,20/12,20/11,20/10,20/9,20/8,20/7,20/6,20/5,20/4,20/3,20/2,20|" +
"20,20/21,20/22,20/23,20/24,20/25,20/26,20/27,20/28,20/29,20/30,20/31,20/32,20/33,20/34,20/35,20/36,20/37,20/38,20/38,21/38,22/38,23/38,24/38,25/38,26/38,27/38,28/38,29/38,30/38,31/38,32/38,33/38,34/38,35/38,36/38,37/38,38/37,38/36,38/35,38/34,38/33,38/32,38/31,38/30,38/29,38/28,38/27,38/26,38/25,38/24,38/23,38/22,38/21,38/20,38/20,37/20,36/20,35/20,34/20,33/20,32/20,31/20,30/20,29/20,28/20,27/20,26/20,25/20,24/20,23/20,22/20,21|" +
"39,20/39,21/39,22/39,23/39,24/39,25/39,26/39,27/39,28/39,29/39,30/39,31/39,32/39,33/39,34/39,35/39,36/39,37/39,38/40,38/41,38/42,38/43,38/44,38/45,38/46,38/47,38/48,38/49,38/50,38/51,38/52,38/53,38/54,38/55,38/56,38/57,38/57,37/57,36/57,35/57,34/57,33/57,32/57,31/57,30/57,29/57,28/57,27/57,26/57,25/57,24/57,23/57,22/57,21/57,20/56,20/55,20/54,20/53,20/52,20/51,20/50,20/49,20/48,20/47,20/46,20/45,20/44,20/43,20/42,20/41,20/40,20|" +
"58,20/59,20/60,20/61,20/62,20/63,20/64,20/65,20/66,20/67,20/68,20/69,20/70,20/71,20/72,20/73,20/74,20/75,20/76,20/76,21/76,22/76,23/76,24/76,25/76,26/76,27/76,28/76,29/76,30/76,31/76,32/76,33/76,34/76,35/76,36/76,37/76,38/75,38/74,38/73,38/72,38/71,38/70,38/69,38/68,38/67,38/66,38/65,38/64,38/63,38/62,38/61,38/60,38/59,38/58,38/58,37/58,36/58,35/58,34/58,33/58,32/58,31/58,30/58,29/58,28/58,27/58,26/58,25/58,24/58,23/58,22/58,21|" +
"77,20/77,21/77,22/77,23/77,24/77,25/77,26/77,27/77,28/77,29/77,30/77,31/77,32/77,33/77,34/77,35/77,36/77,37/77,38/78,38/79,38/80,38/81,38/82,38/83,38/84,38/85,38/86,38/87,38/88,38/89,38/90,38/91,38/92,38/93,38/94,38/95,38/95,37/95,36/95,35/95,34/95,33/95,32/95,31/95,30/95,29/95,28/95,27/95,26/95,25/95,24/95,23/95,22/95,21/95,20/94,20/93,20/92,20/91,20/90,20/89,20/88,20/87,20/86,20/85,20/84,20/83,20/82,20/81,20/80,20/79,20/78,20|" +
"1,39/2,39/3,39/4,39/5,39/6,39/7,39/8,39/9,39/10,39/11,39/12,39/13,39/14,39/15,39/16,39/17,39/18,39/19,39/19,40/19,41/19,42/19,43/19,44/19,45/19,46/19,47/19,48/19,49/19,50/19,51/19,52/19,53/19,54/19,55/19,56/19,57/18,57/17,57/16,57/15,57/14,57/13,57/12,57/11,57/10,57/9,57/8,57/7,57/6,57/5,57/4,57/3,57/2,57/1,57/1,56/1,55/1,54/1,53/1,52/1,51/1,50/1,49/1,48/1,47/1,46/1,45/1,44/1,43/1,42/1,41/1,40|" +
"20,39/20,40/20,41/20,42/20,43/20,44/20,45/20,46/20,47/20,48/20,49/20,50/20,51/20,52/20,53/20,54/20,55/20,56/20,57/21,57/22,57/23,57/24,57/25,57/26,57/27,57/28,57/29,57/30,57/31,57/32,57/33,57/34,57/35,57/36,57/37,57/38,57/38,56/38,55/38,54/38,53/38,52/38,51/38,50/38,49/38,48/38,47/38,46/38,45/38,44/38,43/38,42/38,41/38,40/38,39/37,39/36,39/35,39/34,39/33,39/32,39/31,39/30,39/29,39/28,39/27,39/26,39/25,39/24,39/23,39/22,39/21,39|" +
"39,39/40,39/41,39/42,39/43,39/44,39/45,39/46,39/47,39/48,39/49,39/50,39/51,39/52,39/53,39/54,39/55,39/56,39/57,39/57,40/57,41/57,42/57,43/57,44/57,45/57,46/57,47/57,48/57,49/57,50/57,51/57,52/57,53/57,54/57,55/57,56/57,57/56,57/55,57/54,57/53,57/52,57/51,57/50,57/49,57/48,57/47,57/46,57/45,57/44,57/43,57/42,57/41,57/40,57/39,57/39,56/39,55/39,54/39,53/39,52/39,51/39,50/39,49/39,48/39,47/39,46/39,45/39,44/39,43/39,42/39,41/39,40|" +
"58,39/58,40/58,41/58,42/58,43/58,44/58,45/58,46/58,47/58,48/58,49/58,50/58,51/58,52/58,53/58,54/58,55/58,56/58,57/59,57/60,57/61,57/62,57/63,57/64,57/65,57/66,57/67,57/68,57/69,57/70,57/71,57/72,57/73,57/74,57/75,57/76,57/76,56/76,55/76,54/76,53/76,52/76,51/76,50/76,49/76,48/76,47/76,46/76,45/76,44/76,43/76,42/76,41/76,40/76,39/75,39/74,39/73,39/72,39/71,39/70,39/69,39/68,39/67,39/66,39/65,39/64,39/63,39/62,39/61,39/60,39/59,39|" +
"77,39/78,39/79,39/80,39/81,39/82,39/83,39/84,39/85,39/86,39/87,39/88,39/89,39/90,39/91,39/92,39/93,39/94,39/95,39/95,40/95,41/95,42/95,43/95,44/95,45/95,46/95,47/95,48/95,49/95,50/95,51/95,52/95,53/95,54/95,55/95,56/95,57/94,57/93,57/92,57/91,57/90,57/89,57/88,57/87,57/86,57/85,57/84,57/83,57/82,57/81,57/80,57/79,57/78,57/77,57/77,56/77,55/77,54/77,53/77,52/77,51/77,50/77,49/77,48/77,47/77,46/77,45/77,44/77,43/77,42/77,41/77,40|" +
"1,58/1,59/1,60/1,61/1,62/1,63/1,64/1,65/1,66/1,67/1,68/1,69/1,70/1,71/1,72/1,73/1,74/1,75/1,76/2,76/3,76/4,76/5,76/6,76/7,76/8,76/9,76/10,76/11,76/12,76/13,76/14,76/15,76/16,76/17,76/18,76/19,76/19,75/19,74/19,73/19,72/19,71/19,70/19,69/19,68/19,67/19,66/19,65/19,64/19,63/19,62/19,61/19,60/19,59/19,58/18,58/17,58/16,58/15,58/14,58/13,58/12,58/11,58/10,58/9,58/8,58/7,58/6,58/5,58/4,58/3,58/2,58|" +
"20,58/21,58/22,58/23,58/24,58/25,58/26,58/27,58/28,58/29,58/30,58/31,58/32,58/33,58/34,58/35,58/36,58/37,58/38,58/38,59/38,60/38,61/38,62/38,63/38,64/38,65/38,66/38,67/38,68/38,69/38,70/38,71/38,72/38,73/38,74/38,75/38,76/37,76/36,76/35,76/34,76/33,76/32,76/31,76/30,76/29,76/28,76/27,76/26,76/25,76/24,76/23,76/22,76/21,76/20,76/20,75/20,74/20,73/20,72/20,71/20,70/20,69/20,68/20,67/20,66/20,65/20,64/20,63/20,62/20,61/20,60/20,59|" +
"39,58/39,59/39,60/39,61/39,62/39,63/39,64/39,65/39,66/39,67/39,68/39,69/39,70/39,71/39,72/39,73/39,74/39,75/39,76/40,76/41,76/42,76/43,76/44,76/45,76/46,76/47,76/48,76/49,76/50,76/51,76/52,76/53,76/54,76/55,76/56,76/57,76/57,75/57,74/57,73/57,72/57,71/57,70/57,69/57,68/57,67/57,66/57,65/57,64/57,63/57,62/57,61/57,60/57,59/57,58/56,58/55,58/54,58/53,58/52,58/51,58/50,58/49,58/48,58/47,58/46,58/45,58/44,58/43,58/42,58/41,58/40,58|" +
"58,58/59,58/60,58/61,58/62,58/63,58/64,58/65,58/66,58/67,58/68,58/69,58/70,58/71,58/72,58/73,58/74,58/75,58/76,58/76,59/76,60/76,61/76,62/76,63/76,64/76,65/76,66/76,67/76,68/76,69/76,70/76,71/76,72/76,73/76,74/76,75/76,76/75,76/74,76/73,76/72,76/71,76/70,76/69,76/68,76/67,76/66,76/65,76/64,76/63,76/62,76/61,76/60,76/59,76/58,76/58,75/58,74/58,73/58,72/58,71/58,70/58,69/58,68/58,67/58,66/58,65/58,64/58,63/58,62/58,61/58,60/58,59|" +
"77,58/77,59/77,60/77,61/77,62/77,63/77,64/77,65/77,66/77,67/77,68/77,69/77,70/77,71/77,72/77,73/77,74/77,75/77,76/78,76/79,76/80,76/81,76/82,76/83,76/84,76/85,76/86,76/87,76/88,76/89,76/90,76/91,76/92,76/93,76/94,76/95,76/95,75/95,74/95,73/95,72/95,71/95,70/95,69/95,68/95,67/95,66/95,65/95,64/95,63/95,62/95,61/95,60/95,59/95,58/94,58/93,58/92,58/91,58/90,58/89,58/88,58/87,58/86,58/85,58/84,58/83,58/82,58/81,58/80,58/79,58/78,58|" +
"1,77/2,77/3,77/4,77/5,77/6,77/7,77/8,77/9,77/10,77/11,77/12,77/13,77/14,77/15,77/16,77/17,77/18,77/19,77/19,78/19,79/19,80/19,81/19,82/19,83/19,84/19,85/19,86/19,87/19,88/19,89/19,90/19,91/19,92/19,93/19,94/19,95/18,95/17,95/16,95/15,95/14,95/13,95/12,95/11,95/10,95/9,95/8,95/7,95/6,95/5,95/4,95/3,95/2,95/1,95/1,94/1,93/1,92/1,91/1,90/1,89/1,88/1,87/1,86/1,85/1,84/1,83/1,82/1,81/1,80/1,79/1,78|" +
"20,77/20,78/20,79/20,80/20,81/20,82/20,83/20,84/20,85/20,86/20,87/20,88/20,89/20,90/20,91/20,92/20,93/20,94/20,95/21,95/22,95/23,95/24,95/25,95/26,95/27,95/28,95/29,95/30,95/31,95/32,95/33,95/34,95/35,95/36,95/37,95/38,95/38,94/38,93/38,92/38,91/38,90/38,89/38,88/38,87/38,86/38,85/38,84/38,83/38,82/38,81/38,80/38,79/38,78/38,77/37,77/36,77/35,77/34,77/33,77/32,77/31,77/30,77/29,77/28,77/27,77/26,77/25,77/24,77/23,77/22,77/21,77|" +
"39,77/40,77/41,77/42,77/43,77/44,77/45,77/46,77/47,77/48,77/49,77/50,77/51,77/52,77/53,77/54,77/55,77/56,77/57,77/57,78/57,79/57,80/57,81/57,82/57,83/57,84/57,85/57,86/57,87/57,88/57,89/57,90/57,91/57,92/57,93/57,94/57,95/56,95/55,95/54,95/53,95/52,95/51,95/50,95/49,95/48,95/47,95/46,95/45,95/44,95/43,95/42,95/41,95/40,95/39,95/39,94/39,93/39,92/39,91/39,90/39,89/39,88/39,87/39,86/39,85/39,84/39,83/39,82/39,81/39,80/39,79/39,78|" +
"58,77/58,78/58,79/58,80/58,81/58,82/58,83/58,84/58,85/58,86/58,87/58,88/58,89/58,90/58,91/58,92/58,93/58,94/58,95/59,95/60,95/61,95/62,95/63,95/64,95/65,95/66,95/67,95/68,95/69,95/70,95/71,95/72,95/73,95/74,95/75,95/76,95/76,94/76,93/76,92/76,91/76,90/76,89/76,88/76,87/76,86/76,85/76,84/76,83/76,82/76,81/76,80/76,79/76,78/76,77/75,77/74,77/73,77/72,77/71,77/70,77/69,77/68,77/67,77/66,77/65,77/64,77/63,77/62,77/61,77/60,77/59,77|" +
"77,77/78,77/79,77/80,77/81,77/82,77/83,77/84,77/85,77/86,77/87,77/88,77/89,77/90,77/91,77/92,77/93,77/94,77/95,77/95,78/95,79/95,80/95,81/95,82/95,83/95,84/95,85/95,86/95,87/95,88/95,89/95,90/95,91/95,92/95,93/95,94/95,95/94,95/93,95/92,95/91,95/90,95/89,95/88,95/87,95/86,95/85,95/84,95/83,95/82,95/81,95/80,95/79,95/78,95/77,95/77,94/77,93/77,92/77,91/77,90/77,89/77,88/77,87/77,86/77,85/77,84/77,83/77,82/77,81/77,80/77,79/77,78";
			var startTime = new List<int> { 0, 50, 0, 50, 0, 150, 100, 150, 100, 150, 0, 50, 0, 50, 0, 150, 100, 150, 100, 150, 0, 50, 0, 50, 0 };
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");
			var squareLights = lightLocations.Split('|').Select(l => l.Split('/').Select(p => layout.GetPositionLight(Point.Parse(p))).ToList()).ToList();

			var pattern = new Pattern();
			var color = new LightColor(0, 3, new List<List<int>>
			{
				new List<int> { 0x000000 }.Multiply(Brightness).ToList(),
				new List<int> { 0xffffff }.Multiply(Brightness).ToList(),
				new List<int> { 0x0000ff, 0x00ff00, 0xff0000, 0x800080 }.Multiply(Brightness).ToList(),
			});
			for (var squareCtr = 0; squareCtr < squareLights.Count; squareCtr++)
			{
				var light = default(int?);
				var colorIndex = 0;
				var time = startTime[squareCtr];

				for (var pass = 0; pass < 2; ++pass)
					for (var ctr = 0; ctr < 72; ++ctr)
					{
						if (light.HasValue)
							pattern.AddLight(light.Value, time, pattern.Absolute, 0x000000);
						light = squareLights[squareCtr][ctr];
						if (ctr % 18 == 0)
						{
							var newColorIndex = (colorIndex + 1) % 4;
							pattern.AddLight(light.Value, time, time + 33, color, colorIndex, color, newColorIndex);
							colorIndex = newColorIndex;
							time += 33;
						}
						else
						{
							pattern.AddLight(light.Value, time, color, colorIndex);
							++time;
						}
					}
			}

			pattern.AddLightSequence(200, 400, 4000, 10);
			pattern.AddPaletteSequence(0, 1000, null, 1);
			pattern.AddPaletteSequence(9500, 10500, null, 2);
			pattern.AddPaletteSequence(39000, 40000, null, 0);

			return pattern;
		}
	}
}
