﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Shelfinator.Creator.Patterns
{
	class Spiral : IPattern
	{
		public int PatternNumber => 14;

		List<List<Point>> GetLights()
		{
			const string data =
"0,0&0,1/1,0&1,1/2,0&2,1/3,0&3,1/4,0&4,1/5,0&5,1/6,0&6,1/7,0&7,1/8,0&8,1/9,0&9,1/10,0&10,1/11,0&11,1/12,0&12,1/13,0&13,1/14,0&14,1/15,0&15,1/16,0&16,1/17,0&17,1/18,0&18,1/19,0&19,1/20,0&20,1/21,0&21,1/22,0&22,1/23,0&23,1/24,0&24,1/25,0&25,1/26,0&26,1/27,0&27,1/28,0&28,1/29,0&29,1/30,0&30,1/31,0&31,1/32,0&32,1/33,0&33,1/34,0&34,1/35,0&35,1/36,0&36,1/37,0&37,1/38,0&38,1/39,0&39,1/40,0&40,1/41,0&41,1/42,0&42,1/43,0&43,1/44,0&44,1/45,0&45,1/46,0&46,1/47,0&47,1/48,0&48,1/49,0&49,1/50,0&50,1/51,0&51,1/" +
"52,0&52,1/53,0&53,1/54,0&54,1/55,0&55,1/56,0&56,1/57,0&57,1/58,0&58,1/59,0&59,1/60,0&60,1/61,0&61,1/62,0&62,1/63,0&63,1/64,0&64,1/65,0&65,1/66,0&66,1/67,0&67,1/68,0&68,1/69,0&69,1/70,0&70,1/71,0&71,1/72,0&72,1/73,0&73,1/74,0&74,1/75,0&75,1/76,0&76,1/77,0&77,1/78,0&78,1/79,0&79,1/80,0&80,1/81,0&81,1/82,0&82,1/83,0&83,1/84,0&84,1/85,0&85,1/86,0&86,1/87,0&87,1/88,0&88,1/89,0&89,1/90,0&90,1/91,0&91,1/92,0&92,1/93,0&93,1/94,0&94,1/95,0&95,1&96,0&96,1/95,2&96,2/95,3&96,3/95,4&96,4/95,5&96,5/95,6&96,6/" +
"95,7&96,7/95,8&96,8/95,9&96,9/95,10&96,10/95,11&96,11/95,12&96,12/95,13&96,13/95,14&96,14/95,15&96,15/95,16&96,16/95,17&96,17/95,18&96,18/95,19&96,19/95,20&96,20/95,21&96,21/95,22&96,22/95,23&96,23/95,24&96,24/95,25&96,25/95,26&96,26/95,27&96,27/95,28&96,28/95,29&96,29/95,30&96,30/95,31&96,31/95,32&96,32/95,33&96,33/95,34&96,34/95,35&96,35/95,36&96,36/95,37&96,37/95,38&96,38/95,39&96,39/95,40&96,40/95,41&96,41/95,42&96,42/95,43&96,43/95,44&96,44/95,45&96,45/95,46&96,46/95,47&96,47/95,48&96,48/95" +
",49&96,49/95,50&96,50/95,51&96,51/95,52&96,52/95,53&96,53/95,54&96,54/95,55&96,55/95,56&96,56/95,57&96,57/95,58&96,58/95,59&96,59/95,60&96,60/95,61&96,61/95,62&96,62/95,63&96,63/95,64&96,64/95,65&96,65/95,66&96,66/95,67&96,67/95,68&96,68/95,69&96,69/95,70&96,70/95,71&96,71/95,72&96,72/95,73&96,73/95,74&96,74/95,75&96,75/95,76&96,76/95,77&96,77/95,78&96,78/95,79&96,79/95,80&96,80/95,81&96,81/95,82&96,82/95,83&96,83/95,84&96,84/95,85&96,85/95,86&96,86/95,87&96,87/95,88&96,88/95,89&96,89/95,90&96,9" +
"0/95,91&96,91/95,92&96,92/95,93&96,93/95,94&96,94/95,95&95,96&96,95&96,96/94,95&94,96/93,95&93,96/92,95&92,96/91,95&91,96/90,95&90,96/89,95&89,96/88,95&88,96/87,95&87,96/86,95&86,96/85,95&85,96/84,95&84,96/83,95&83,96/82,95&82,96/81,95&81,96/80,95&80,96/79,95&79,96/78,95&78,96/77,95&77,96/76,95&76,96/75,95&75,96/74,95&74,96/73,95&73,96/72,95&72,96/71,95&71,96/70,95&70,96/69,95&69,96/68,95&68,96/67,95&67,96/66,95&66,96/65,95&65,96/64,95&64,96/63,95&63,96/62,95&62,96/61,95&61,96/60,95&60,96/59,95&" +
"59,96/58,95&58,96/57,95&57,96/56,95&56,96/55,95&55,96/54,95&54,96/53,95&53,96/52,95&52,96/51,95&51,96/50,95&50,96/49,95&49,96/48,95&48,96/47,95&47,96/46,95&46,96/45,95&45,96/44,95&44,96/43,95&43,96/42,95&42,96/41,95&41,96/40,95&40,96/39,95&39,96/38,95&38,96/37,95&37,96/36,95&36,96/35,95&35,96/34,95&34,96/33,95&33,96/32,95&32,96/31,95&31,96/30,95&30,96/29,95&29,96/28,95&28,96/27,95&27,96/26,95&26,96/25,95&25,96/24,95&24,96/23,95&23,96/22,95&22,96/21,95&21,96/20,95&20,96/19,95&19,96/18,95&18,96/17" +
",95&17,96/16,95&16,96/15,95&15,96/14,95&14,96/13,95&13,96/12,95&12,96/11,95&11,96/10,95&10,96/9,95&9,96/8,95&8,96/7,95&7,96/6,95&6,96/5,95&5,96/4,95&4,96/3,95&3,96/2,95&2,96/0,95&0,96&1,95&1,96/0,94&1,94/0,93&1,93/0,92&1,92/0,91&1,91/0,90&1,90/0,89&1,89/0,88&1,88/0,87&1,87/0,86&1,86/0,85&1,85/0,84&1,84/0,83&1,83/0,82&1,82/0,81&1,81/0,80&1,80/0,79&1,79/0,78&1,78/0,77&1,77/0,76&1,76/0,75&1,75/0,74&1,74/0,73&1,73/0,72&1,72/0,71&1,71/0,70&1,70/0,69&1,69/0,68&1,68/0,67&1,67/0,66&1,66/0,65&1,65/0,64&1" +
",64/0,63&1,63/0,62&1,62/0,61&1,61/0,60&1,60/0,59&1,59/0,58&1,58/0,57&1,57/0,56&1,56/0,55&1,55/0,54&1,54/0,53&1,53/0,52&1,52/0,51&1,51/0,50&1,50/0,49&1,49/0,48&1,48/0,47&1,47/0,46&1,46/0,45&1,45/0,44&1,44/0,43&1,43/0,42&1,42/0,41&1,41/0,40&1,40/0,39&1,39/0,38&1,38/0,37&1,37/0,36&1,36/0,35&1,35/0,34&1,34/0,33&1,33/0,32&1,32/0,31&1,31/0,30&1,30/0,29&1,29/0,28&1,28/0,27&1,27/0,26&1,26/0,25&1,25/0,24&1,24/0,23&1,23/0,22&1,22/0,21&1,21/0,19&0,20&1,19&1,20/2,19&2,20/3,19&3,20/4,19&4,20/5,19&5,20/6,19&6" +
",20/7,19&7,20/8,19&8,20/9,19&9,20/10,19&10,20/11,19&11,20/12,19&12,20/13,19&13,20/14,19&14,20/15,19&15,20/16,19&16,20/17,19&17,20/18,19&18,20/19,19&19,20/20,19&20,20/21,19&21,20/22,19&22,20/23,19&23,20/24,19&24,20/25,19&25,20/26,19&26,20/27,19&27,20/28,19&28,20/29,19&29,20/30,19&30,20/31,19&31,20/32,19&32,20/33,19&33,20/34,19&34,20/35,19&35,20/36,19&36,20/37,19&37,20/38,19&38,20/39,19&39,20/40,19&40,20/41,19&41,20/42,19&42,20/43,19&43,20/44,19&44,20/45,19&45,20/46,19&46,20/47,19&47,20/48,19&48,2" +
"0/49,19&49,20/50,19&50,20/51,19&51,20/52,19&52,20/53,19&53,20/54,19&54,20/55,19&55,20/56,19&56,20/57,19&57,20/58,19&58,20/59,19&59,20/60,19&60,20/61,19&61,20/62,19&62,20/63,19&63,20/64,19&64,20/65,19&65,20/66,19&66,20/67,19&67,20/68,19&68,20/69,19&69,20/70,19&70,20/71,19&71,20/72,19&72,20/73,19&73,20/74,19&74,20/75,19&75,20/76,19&76,20&77,19&77,20/76,21&77,21/76,22&77,22/76,23&77,23/76,24&77,24/76,25&77,25/76,26&77,26/76,27&77,27/76,28&77,28/76,29&77,29/76,30&77,30/76,31&77,31/76,32&77,32/76,33&" +
"77,33/76,34&77,34/76,35&77,35/76,36&77,36/76,37&77,37/76,38&77,38/76,39&77,39/76,40&77,40/76,41&77,41/76,42&77,42/76,43&77,43/76,44&77,44/76,45&77,45/76,46&77,46/76,47&77,47/76,48&77,48/76,49&77,49/76,50&77,50/76,51&77,51/76,52&77,52/76,53&77,53/76,54&77,54/76,55&77,55/76,56&77,56/76,57&77,57/76,58&77,58/76,59&77,59/76,60&77,60/76,61&77,61/76,62&77,62/76,63&77,63/76,64&77,64/76,65&77,65/76,66&77,66/76,67&77,67/76,68&77,68/76,69&77,69/76,70&77,70/76,71&77,71/76,72&77,72/76,73&77,73/76,74&77,74/76" +
",75&77,75/76,76&76,77&77,76&77,77/75,76&75,77/74,76&74,77/73,76&73,77/72,76&72,77/71,76&71,77/70,76&70,77/69,76&69,77/68,76&68,77/67,76&67,77/66,76&66,77/65,76&65,77/64,76&64,77/63,76&63,77/62,76&62,77/61,76&61,77/60,76&60,77/59,76&59,77/58,76&58,77/57,76&57,77/56,76&56,77/55,76&55,77/54,76&54,77/53,76&53,77/52,76&52,77/51,76&51,77/50,76&50,77/49,76&49,77/48,76&48,77/47,76&47,77/46,76&46,77/45,76&45,77/44,76&44,77/43,76&43,77/42,76&42,77/41,76&41,77/40,76&40,77/39,76&39,77/38,76&38,77/37,76&37,7" +
"7/36,76&36,77/35,76&35,77/34,76&34,77/33,76&33,77/32,76&32,77/31,76&31,77/30,76&30,77/29,76&29,77/28,76&28,77/27,76&27,77/26,76&26,77/25,76&25,77/24,76&24,77/23,76&23,77/22,76&22,77/21,76&21,77/19,76&19,77&20,76&20,77/19,75&20,75/19,74&20,74/19,73&20,73/19,72&20,72/19,71&20,71/19,70&20,70/19,69&20,69/19,68&20,68/19,67&20,67/19,66&20,66/19,65&20,65/19,64&20,64/19,63&20,63/19,62&20,62/19,61&20,61/19,60&20,60/19,59&20,59/19,58&20,58/19,57&20,57/19,56&20,56/19,55&20,55/19,54&20,54/19,53&20,53/19,52&" +
"20,52/19,51&20,51/19,50&20,50/19,49&20,49/19,48&20,48/19,47&20,47/19,46&20,46/19,45&20,45/19,44&20,44/19,43&20,43/19,42&20,42/19,41&20,41/19,40&20,40/19,38&19,39&20,38&20,39/21,38&21,39/22,38&22,39/23,38&23,39/24,38&24,39/25,38&25,39/26,38&26,39/27,38&27,39/28,38&28,39/29,38&29,39/30,38&30,39/31,38&31,39/32,38&32,39/33,38&33,39/34,38&34,39/35,38&35,39/36,38&36,39/37,38&37,39/38,38&38,39/39,38&39,39/40,38&40,39/41,38&41,39/42,38&42,39/43,38&43,39/44,38&44,39/45,38&45,39/46,38&46,39/47,38&47,39/48" +
",38&48,39/49,38&49,39/50,38&50,39/51,38&51,39/52,38&52,39/53,38&53,39/54,38&54,39/55,38&55,39/56,38&56,39/57,38&57,39&58,38&58,39/57,40&58,40/57,41&58,41/57,42&58,42/57,43&58,43/57,44&58,44/57,45&58,45/57,46&58,46/57,47&58,47/57,48&58,48/57,49&58,49/57,50&58,50/57,51&58,51/57,52&58,52/57,53&58,53/57,54&58,54/57,55&58,55/57,56&58,56/57,57&57,58&58,57&58,58/56,57&56,58/55,57&55,58/54,57&54,58/53,57&53,58/52,57&52,58/51,57&51,58/50,57&50,58/49,57&49,58/48,57&48,58/47,57&47,58/46,57&46,58/45,57&45,5" +
"8/44,57&44,58/43,57&43,58/42,57&42,58/41,57&41,58/40,57&40,58/39,57&39,58/38,57&38,58";

			return data.Split('/').Select(l => l.Split('&').Select(p => p.Split(',').Select(int.Parse).ToList()).Select(p => new Point(p[0], p[1])).ToList()).ToList();
		}

		public Pattern Render()
		{
			const double Brightness = 1f / 16;

			var pattern = new Pattern();
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");

			var points = GetLights();

			var colors = new List<int> { 0xff0000, 0x00ff00, 0x0000ff, 0x800080 }.Multiply(Brightness).ToList();

			for (var pass = 0; pass < 4; ++pass)
			{
				Func<Point, int> GetLight;
				switch (pass)
				{
					case 0: GetLight = point => layout.GetPositionLight(point); break;
					case 1: GetLight = point => layout.GetPositionLight(new Point(layout.Height - 1 - point.Y, point.X)); break;
					case 2: GetLight = point => layout.GetPositionLight(new Point(layout.Width - 1 - point.X, layout.Height - 1 - point.Y)); break;
					case 3: GetLight = point => layout.GetPositionLight(new Point(point.Y, layout.Width - 1 - point.X)); break;
					default: throw new Exception("Invalid pass");
				}
				for (var ctr = 0; ctr < points.Count; ++ctr)
				{
					int startTime = 1500 * pass + 900 * ctr / points.Count;
					foreach (var point in points[ctr])
					{
						var light = GetLight(point);
						pattern.AddLight(light, startTime, startTime + 300, pattern.Absolute, 0x000000, pattern.Absolute, colors[pass]);
						pattern.AddLight(light, startTime + 1200, startTime + 1200 + 300, null, pattern.Absolute, 0x000000);
					}
				}
			}
			pattern.AddLightSequence(0, 6900, 20000);

			return pattern;
		}
	}
}