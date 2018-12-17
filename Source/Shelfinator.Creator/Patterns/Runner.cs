﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Shelfinator.Creator.PatternData;

namespace Shelfinator.Creator.Patterns
{
	class Runner : IPattern
	{
		public int PatternNumber => 29;

		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			const int Concurrency = 19;
			const string Pattern =
"0,0/1,0/2,0/3,0/4,0/5,0/6,0/7,0/8,0/9,0/10,0/11,0/12,0/13,0/14,0/15,0/16,0/17,0/18,0/19,0/20,0/21,0/22,0/23,0/24,0/25,0/26,0/27,0/28,0/29,0/30,0/31,0/32,0/33,0/34,0/35,0/36,0/37,0/38,0/39,0/40,0/41,0/42,0/43,0/44,0/45,0/46,0/47,0/48,0/49,0/50,0/51,0/52,0/53,0/54,0/55,0/56,0/57,0/58,0/59,0/60,0/61,0/62,0/63,0/64,0/65,0/66,0/67,0/68,0/69,0/70,0/71,0/72,0/73,0/74,0/75,0/76,0/77,0/78,0/79,0/80,0/81,0/82,0/83,0/84,0/85,0/86,0/87,0/88,0/89,0/90,0/91,0/92,0/93,0/94,0/95,0/95,1/95,2/95,3/95,4/95,5/95,6/" +
"95,7/95,8/95,9/95,10/95,11/95,12/95,13/95,14/95,15/95,16/95,17/95,18/95,19/95,20/95,21/95,22/95,23/95,24/95,25/95,26/95,27/95,28/95,29/95,30/95,31/95,32/95,33/95,34/95,35/95,36/95,37/95,38/95,39/95,40/95,41/95,42/95,43/95,44/95,45/95,46/95,47/95,48/95,49/95,50/95,51/95,52/95,53/95,54/95,55/95,56/95,57/95,58/95,59/95,60/95,61/95,62/95,63/95,64/95,65/95,66/95,67/95,68/95,69/95,70/95,71/95,72/95,73/95,74/95,75/95,76/95,77/95,78/95,79/95,80/95,81/95,82/95,83/95,84/95,85/95,86/95,87/95,88/95,89/95,90" +
"/95,91/95,92/95,93/95,94/95,95/94,95/93,95/92,95/91,95/90,95/89,95/88,95/87,95/86,95/85,95/84,95/83,95/82,95/81,95/80,95/79,95/78,95/77,95/76,95/75,95/74,95/73,95/72,95/71,95/70,95/69,95/68,95/67,95/66,95/65,95/64,95/63,95/62,95/61,95/60,95/59,95/58,95/57,95/56,95/55,95/54,95/53,95/52,95/51,95/50,95/49,95/48,95/47,95/46,95/45,95/44,95/43,95/42,95/41,95/40,95/39,95/38,95/37,95/36,95/35,95/34,95/33,95/32,95/31,95/30,95/29,95/28,95/27,95/26,95/25,95/24,95/23,95/22,95/21,95/20,95/19,95/18,95/17,95/1" +
"6,95/15,95/14,95/13,95/12,95/11,95/10,95/9,95/8,95/7,95/6,95/5,95/4,95/3,95/2,95/1,95/0,95/0,94/0,93/0,92/0,91/0,90/0,89/0,88/0,87/0,86/0,85/0,84/0,83/0,82/0,81/0,80/0,79/0,78/0,77/0,76/0,75/0,74/0,73/0,72/0,71/0,70/0,69/0,68/0,67/0,66/0,65/0,64/0,63/0,62/0,61/0,60/0,59/0,58/0,57/0,56/0,55/0,54/0,53/0,52/0,51/0,50/0,49/0,48/0,47/0,46/0,45/0,44/0,43/0,42/0,41/0,40/0,39/0,38/0,37/0,36/0,35/0,34/0,33/0,32/0,31/0,30/0,29/0,28/0,27/0,26/0,25/0,24/0,23/0,22/0,21/0,20/0,19/0,18/0,17/0,16/0,15/0,14/0,13" +
"/0,12/0,11/0,10/0,9/0,8/0,7/0,6/0,5/0,4/0,3/0,2/0,1|19,19/19,20/19,21/19,22/19,23/19,24/19,25/19,26/19,27/19,28/19,29/19,30/19,31/19,32/19,33/19,34/19,35/19,36/19,37/19,38/19,39/19,40/19,41/19,42/19,43/19,44/19,45/19,46/19,47/19,48/19,49/19,50/19,51/19,52/19,53/19,54/19,55/19,56/19,57/19,58/19,59/19,60/19,61/19,62/19,63/19,64/19,65/19,66/19,67/19,68/19,69/19,70/19,71/19,72/19,73/19,74/19,75/19,76/20,76/21,76/22,76/23,76/24,76/25,76/26,76/27,76/28,76/29,76/30,76/31,76/32,76/33,76/34,76/35,76/36,7" +
"6/37,76/38,76/39,76/40,76/41,76/42,76/43,76/44,76/45,76/46,76/47,76/48,76/49,76/50,76/51,76/52,76/53,76/54,76/55,76/56,76/57,76/58,76/59,76/60,76/61,76/62,76/63,76/64,76/65,76/66,76/67,76/68,76/69,76/70,76/71,76/72,76/73,76/74,76/75,76/76,76/76,75/76,74/76,73/76,72/76,71/76,70/76,69/76,68/76,67/76,66/76,65/76,64/76,63/76,62/76,61/76,60/76,59/76,58/76,57/76,56/76,55/76,54/76,53/76,52/76,51/76,50/76,49/76,48/76,47/76,46/76,45/76,44/76,43/76,42/76,41/76,40/76,39/76,38/76,37/76,36/76,35/76,34/76,33/" +
"76,32/76,31/76,30/76,29/76,28/76,27/76,26/76,25/76,24/76,23/76,22/76,21/76,20/76,19/75,19/74,19/73,19/72,19/71,19/70,19/69,19/68,19/67,19/66,19/65,19/64,19/63,19/62,19/61,19/60,19/59,19/58,19/57,19/56,19/55,19/54,19/53,19/52,19/51,19/50,19/49,19/48,19/47,19/46,19/45,19/44,19/43,19/42,19/41,19/40,19/39,19/38,19/37,19/36,19/35,19/34,19/33,19/32,19/31,19/30,19/29,19/28,19/27,19/26,19/25,19/24,19/23,19/22,19/21,19/20,19|38,38/39,38/40,38/41,38/42,38/43,38/44,38/45,38/46,38/47,38/48,38/49,38/50,38/51" +
",38/52,38/53,38/54,38/55,38/56,38/57,38/57,39/57,40/57,41/57,42/57,43/57,44/57,45/57,46/57,47/57,48/57,49/57,50/57,51/57,52/57,53/57,54/57,55/57,56/57,57/56,57/55,57/54,57/53,57/52,57/51,57/50,57/49,57/48,57/47,57/46,57/45,57/44,57/43,57/42,57/41,57/40,57/39,57/38,57/38,56/38,55/38,54/38,53/38,52/38,51/38,50/38,49/38,48/38,47/38,46/38,45/38,44/38,43/38,42/38,41/38,40/38,39";

			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");
			var results = Pattern.Split('|').Select(x => x.Split('/').Select(p => layout.GetPositionLights(new Rect(Point.Parse(p), new Size(2, 2)))).ToList()).ToList();

			var color = new LightColor(0, 3, new List<List<int>> {
				new List<int> { 0x000000 }.Multiply(Brightness).ToList(),
				new List<int> { 0xffffff }.Multiply(Brightness).ToList(),
				new List<int> { 0xffffff, 0xff0000, 0xffffff, 0xff0000 }.Multiply(Brightness).ToList(),
				new List<int> { 0x0080ff, 0x01ffff, 0x3f00ff, 0x89d0f0 }.Multiply(Brightness).ToList(),
				new List<int> { 0x17b7ab, 0xe71880, 0xbcd63d, 0xf15a25 }.Multiply(Brightness).ToList(),
			});
			var pattern = new Pattern();
			foreach (var result in results)
				for (var time = 0; time < Concurrency * 4; ++time)
				{
					foreach (var light in result.SelectMany(x => x))
						pattern.AddLight(light, time, pattern.Absolute, 0x000000);
					for (var ctr = 0; ctr < result.Count; ctr += Concurrency)
						foreach (var light in result[(time + ctr) % result.Count])
							pattern.AddLight(light, time, color, (ctr / Concurrency) % 4);
				}

			const int Speed = 1500;
			const int Repeat = 2;

			// Fade in to 1
			pattern.AddLightSequence(Concurrency * 3, Concurrency * 3, Speed);
			pattern.AddPaletteSequence(0, Speed, null, 1);

			// Palette 1
			pattern.AddLightSequence(Concurrency * 3, Concurrency * 4, 0, Concurrency, Speed);
			pattern.AddLightSequence(0, Concurrency * 4, Speed * 4, Repeat);

			// Fade to 2
			var stopTime = pattern.MaxLightSequenceTime();
			pattern.AddPaletteSequence(stopTime - Speed / 2, stopTime + Speed / 2, null, 2);

			// Palette 2
			pattern.AddLightSequence(0, Concurrency * 4, Speed * 4, Repeat);
			pattern.AddLightSequence(0, Concurrency, Concurrency, 0, Speed);

			// Fade to 3
			stopTime = pattern.MaxLightSequenceTime();
			pattern.AddLightSequence(Concurrency, Concurrency, Speed);
			pattern.AddPaletteSequence(stopTime, stopTime + Speed, null, 3);

			// Palette 3
			pattern.AddLightSequence(Concurrency, 0, 0, -Concurrency, Speed);
			pattern.AddLightSequence(Concurrency * 4, 0, Speed * 4, Repeat);

			// Fade to 4
			stopTime = pattern.MaxLightSequenceTime();
			pattern.AddPaletteSequence(stopTime - Speed / 2, stopTime + Speed / 2, null, 4);

			// Palette 4
			pattern.AddLightSequence(Concurrency * 4, 0, Speed * 4, Repeat);
			pattern.AddLightSequence(Concurrency * 4, Concurrency * 3, -Concurrency, 0, Speed);

			// Fade out to 0
			stopTime = pattern.MaxLightSequenceTime();
			pattern.AddLightSequence(Concurrency * 3, Concurrency * 3, Speed);
			pattern.AddPaletteSequence(stopTime, stopTime + Speed, null, 0);

			return pattern;
		}
	}
}
