using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Bohemian : ISong
	{
		public int SongNumber => 3;

		const double Brightness = 1f / 16;
		readonly Layout headerLayout, bodyLayout, squaresLayout;

		public Bohemian()
		{
			headerLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Header.png");
			bodyLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");
			squaresLayout = new Layout("Shelfinator.Creator.Songs.Layout.Squares.png");
		}

		Segment Spiral()
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

			var segment = new Segment();

			var points = data.Split('/').Select(l => l.Split('&').Select(p => p.Split(',').Select(int.Parse).ToList()).Select(p => new Point(p[0], p[1])).ToList()).ToList();

			var colors = new List<int> { 0xff0000, 0x00ff00, 0x0000ff, 0x800080 }.Multiply(Brightness).ToList();

			for (var pass = 0; pass < 4; ++pass)
			{
				Func<Point, int> GetLight;
				switch (pass)
				{
					case 0: GetLight = point => bodyLayout.GetPositionLight(point); break;
					case 1: GetLight = point => bodyLayout.GetPositionLight(new Point(bodyLayout.Height - 1 - point.Y, point.X)); break;
					case 2: GetLight = point => bodyLayout.GetPositionLight(new Point(bodyLayout.Width - 1 - point.X, bodyLayout.Height - 1 - point.Y)); break;
					case 3: GetLight = point => bodyLayout.GetPositionLight(new Point(point.Y, bodyLayout.Width - 1 - point.X)); break;
					default: throw new Exception("Invalid pass");
				}
				for (var ctr = 0; ctr < points.Count; ++ctr)
				{
					int startTime = 1500 * pass + 900 * ctr / points.Count;
					foreach (var point in points[ctr])
					{
						var light = GetLight(point);
						segment.AddLight(light, startTime, startTime + 300, Segment.Absolute, 0x000000, Segment.Absolute, colors[pass]);
						segment.AddLight(light, startTime + 1200, startTime + 1200 + 300, null, Segment.Absolute, 0x000000);
					}
				}
			}

			return segment;
		}

		Segment RunAway()
		{
			const string PathPoints =
"0,0/1,0/2,0/3,0/4,0/5,0/6,0/7,0/8,0/9,0/10,0/11,0/12,0/13,0/14,0/15,0/16,0/17,0/18,0/19,0/20,0/21,0/22,0/23,0/24,0/25,0/26,0/27,0/28,0/29,0/30,0/31,0/32,0/33,0/34,0/35,0/36,0/37,0/38,0/39,0/40,0/41,0/42,0/43,0/44,0/45,0/46,0/47,0/48,0/49,0/50,0/51,0/52,0/53,0/54,0/55,0/56,0/57,0/58,0/59,0/60,0/61,0/62,0/63,0/64,0/65,0/66,0/67,0/68,0/69,0/70,0/71,0/72,0/73,0/74,0/75,0/76,0/77,0/78,0/79,0/80,0/81,0/82,0/83,0/84,0/85,0/86,0/87,0/88,0/89,0/90,0/91,0/92,0/93,0/94,0/95,0/95,1/95,2/95,3/95,4/95,5/95,6/" +
"95,7/95,8/95,9/95,10/95,11/95,12/95,13/95,14/95,15/95,16/95,17/95,18/95,19/94,19/93,19/92,19/91,19/90,19/89,19/88,19/87,19/86,19/85,19/84,19/83,19/82,19/81,19/80,19/79,19/78,19/77,19/76,19/75,19/74,19/73,19/72,19/71,19/70,19/69,19/68,19/67,19/66,19/65,19/64,19/63,19/62,19/61,19/60,19/59,19/58,19/57,19/56,19/55,19/54,19/53,19/52,19/51,19/50,19/49,19/48,19/47,19/46,19/45,19/44,19/43,19/42,19/41,19/40,19/39,19/38,19/37,19/36,19/35,19/34,19/33,19/32,19/31,19/30,19/29,19/28,19/27,19/26,19/25,19/24,19" +
"/23,19/22,19/21,19/20,19/19,19/18,19/17,19/16,19/15,19/14,19/13,19/12,19/11,19/10,19/9,19/8,19/7,19/6,19/5,19/4,19/3,19/2,19/1,19/0,19/0,20/0,21/0,22/0,23/0,24/0,25/0,26/0,27/0,28/0,29/0,30/0,31/0,32/0,33/0,34/0,35/0,36/0,37/0,38/1,38/2,38/3,38/4,38/5,38/6,38/7,38/8,38/9,38/10,38/11,38/12,38/13,38/14,38/15,38/16,38/17,38/18,38/19,38/20,38/21,38/22,38/23,38/24,38/25,38/26,38/27,38/28,38/29,38/30,38/31,38/32,38/33,38/34,38/35,38/36,38/37,38/38,38/39,38/40,38/41,38/42,38/43,38/44,38/45,38/46,38/47," +
"38/48,38/49,38/50,38/51,38/52,38/53,38/54,38/55,38/56,38/57,38/58,38/59,38/60,38/61,38/62,38/63,38/64,38/65,38/66,38/67,38/68,38/69,38/70,38/71,38/72,38/73,38/74,38/75,38/76,38/77,38/78,38/79,38/80,38/81,38/82,38/83,38/84,38/85,38/86,38/87,38/88,38/89,38/90,38/91,38/92,38/93,38/94,38/95,38/95,39/95,40/95,41/95,42/95,43/95,44/95,45/95,46/95,47/95,48/95,49/95,50/95,51/95,52/95,53/95,54/95,55/95,56/95,57/94,57/93,57/92,57/91,57/90,57/89,57/88,57/87,57/86,57/85,57/84,57/83,57/82,57/81,57/80,57/79,57" +
"/78,57/77,57/76,57/75,57/74,57/73,57/72,57/71,57/70,57/69,57/68,57/67,57/66,57/65,57/64,57/63,57/62,57/61,57/60,57/59,57/58,57/57,57/56,57/55,57/54,57/53,57/52,57/51,57/50,57/49,57/48,57/47,57/46,57/45,57/44,57/43,57/42,57/41,57/40,57/39,57/38,57/37,57/36,57/35,57/34,57/33,57/32,57/31,57/30,57/29,57/28,57/27,57/26,57/25,57/24,57/23,57/22,57/21,57/20,57/19,57/18,57/17,57/16,57/15,57/14,57/13,57/12,57/11,57/10,57/9,57/8,57/7,57/6,57/5,57/4,57/3,57/2,57/1,57/0,57/0,58/0,59/0,60/0,61/0,62/0,63/0,64/" +
"0,65/0,66/0,67/0,68/0,69/0,70/0,71/0,72/0,73/0,74/0,75/0,76/1,76/2,76/3,76/4,76/5,76/6,76/7,76/8,76/9,76/10,76/11,76/12,76/13,76/14,76/15,76/16,76/17,76/18,76/19,76/20,76/21,76/22,76/23,76/24,76/25,76/26,76/27,76/28,76/29,76/30,76/31,76/32,76/33,76/34,76/35,76/36,76/37,76/38,76/39,76/40,76/41,76/42,76/43,76/44,76/45,76/46,76/47,76/48,76/49,76/50,76/51,76/52,76/53,76/54,76/55,76/56,76/57,76/58,76/59,76/60,76/61,76/62,76/63,76/64,76/65,76/66,76/67,76/68,76/69,76/70,76/71,76/72,76/73,76/74,76/75,76" +
"/76,76/77,76/78,76/79,76/80,76/81,76/82,76/83,76/84,76/85,76/86,76/87,76/88,76/89,76/90,76/91,76/92,76/93,76/94,76/95,76/95,77/95,78/95,79/95,80/95,81/95,82/95,83/95,84/95,85/95,86/95,87/95,88/95,89/95,90/95,91/95,92/95,93/95,94/95,95/94,95/93,95/92,95/91,95/90,95/89,95/88,95/87,95/86,95/85,95/84,95/83,95/82,95/81,95/80,95/79,95/78,95/77,95/76,95/75,95/74,95/73,95/72,95/71,95/70,95/69,95/68,95/67,95/66,95/65,95/64,95/63,95/62,95/61,95/60,95/59,95/58,95/57,95/56,95/55,95/54,95/53,95/52,95/51,95/5" +
"0,95/49,95/48,95/47,95/46,95/45,95/44,95/43,95/42,95/41,95/40,95/39,95/38,95/37,95/36,95/35,95/34,95/33,95/32,95/31,95/30,95/29,95/28,95/27,95/26,95/25,95/24,95/23,95/22,95/21,95/20,95/19,95/18,95/17,95/16,95/15,95/14,95/13,95/12,95/11,95/10,95/9,95/8,95/7,95/6,95/5,95/4,95/3,95/2,95/1,95/0,95";

			var segment = new Segment();
			var pathPoints = PathPoints.Split('/').Select(p => Point.Parse(p)).ToList();

			var squares = new Layout("Shelfinator.Creator.Songs.Layout.Squares.png");
			var squareLights = new Dictionary<int, List<int>>();
			var squareCenter = new Dictionary<int, Point>();
			foreach (var square in squares.GetAllLights())
			{
				if (square == 0)
					continue;

				var mappedSquareLights = bodyLayout.GetMappedLights(squares, square);
				squareLights[square] = mappedSquareLights;
				var squareLocations = mappedSquareLights.Select(light => bodyLayout.GetLightPosition(light)).OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
				squareCenter[square] = squareLocations.First() + (squareLocations.Last() - squareLocations.First()) / 2;
			}

			var colors = new List<int> { 0x000080, 0x0000ff, 0x008000, 0x008080, 0x0080ff, 0x00ff00, 0x00ff80, 0x00ffff, 0x800000, 0x800080, 0x8000ff, 0x808000, 0x808080, 0x8080ff, 0x80ff00, 0x80ff80, 0x80ffff, 0xff0000, 0xff0080, 0xff00ff, 0xff8000, 0xff8080, 0xff80ff, 0xffff00, 0xffff80 }.Multiply(Brightness).ToList();
			var time = 0;
			foreach (var pathPoint in pathPoints)
			{
				foreach (var light in bodyLayout.GetPositionLights(pathPoint, 2, 2))
				{
					segment.AddLight(light, time, Segment.Absolute, Helpers.MultiplyColor(0xffffff, Brightness));
					segment.AddLight(light, time + 1, Segment.Absolute, 0x000000);
				}

				foreach (var square in squareLights.Keys)
				{
					var focusAngle = Helpers.GetAngle(squareCenter[square], pathPoint);
					var useLight = squareLights[square].OrderBy(light => Math.Abs(focusAngle - Helpers.GetAngle(bodyLayout.GetLightPosition(light), squareCenter[square]))).First();

					segment.AddLight(useLight, time, Segment.Absolute, colors[square - 1]);
					segment.AddLight(useLight, time + 1, Segment.Absolute, 0x000000);
				}

				++time;
			}
			segment.Clear(time);

			return segment;
		}

		Segment Snake()
		{
			const string data =
"0,0/1,0/2,0/3,0/4,0/5,0/6,0/7,0/8,0/9,0/10,0/11,0/12,0/13,0/14,0/15,0/16,0/17,0/18,0/19,0/20,0/21,0/22,0/23,0/24,0/25,0/26,0/27,0/28,0/29,0/30,0/31,0/32,0/33,0/34,0/35,0/36,0/37,0/38,0/39,0/40,0/41,0/42,0/43,0/44,0/45,0/46,0/47,0/48,0/49,0/50,0/51,0/52,0/53,0/54,0/55,0/56,0/57,0/58,0/59,0/60,0/61,0/62,0/63,0/64,0/65,0/66,0/67,0/68,0/69,0/70,0/71,0/72,0/73,0/74,0/75,0/76,0/77,0/78,0/79,0/80,0/81,0/82,0/83,0/84,0/85,0/86,0/87,0/88,0/89,0/90,0/91,0/92,0/93,0/94,0/95,0/95,1/95,2/95,3/95,4/95,5/95,6/" +
"95,7/95,8/95,9/95,10/95,11/95,12/95,13/95,14/95,15/95,16/95,17/95,18/95,19/95,20/95,21/95,22/95,23/95,24/95,25/95,26/95,27/95,28/95,29/95,30/95,31/95,32/95,33/95,34/95,35/95,36/95,37/95,38/95,39/95,40/95,41/95,42/95,43/95,44/95,45/95,46/95,47/95,48/95,49/95,50/95,51/95,52/95,53/95,54/95,55/95,56/95,57/95,58/95,59/95,60/95,61/95,62/95,63/95,64/95,65/95,66/95,67/95,68/95,69/95,70/95,71/95,72/95,73/95,74/95,75/95,76/95,77/95,78/95,79/95,80/95,81/95,82/95,83/95,84/95,85/95,86/95,87/95,88/95,89/95,90" +
"/95,91/95,92/95,93/95,94/95,95/94,95/93,95/92,95/91,95/90,95/89,95/88,95/87,95/86,95/85,95/84,95/83,95/82,95/81,95/80,95/79,95/78,95/77,95/76,95/76,94/76,93/76,92/76,91/76,90/76,89/76,88/76,87/76,86/76,85/76,84/76,83/76,82/76,81/76,80/76,79/76,78/76,77/76,76/76,75/76,74/76,73/76,72/76,71/76,70/76,69/76,68/76,67/76,66/76,65/76,64/76,63/76,62/76,61/76,60/76,59/76,58/76,57/76,56/76,55/76,54/76,53/76,52/76,51/76,50/76,49/76,48/76,47/76,46/76,45/76,44/76,43/76,42/76,41/76,40/76,39/76,38/76,37/76,36/7" +
"6,35/76,34/76,33/76,32/76,31/76,30/76,29/76,28/76,27/76,26/76,25/76,24/76,23/76,22/76,21/76,20/76,19/75,19/74,19/73,19/72,19/71,19/70,19/69,19/68,19/67,19/66,19/65,19/64,19/63,19/62,19/61,19/60,19/59,19/58,19/57,19/57,20/57,21/57,22/57,23/57,24/57,25/57,26/57,27/57,28/57,29/57,30/57,31/57,32/57,33/57,34/57,35/57,36/57,37/57,38/57,39/57,40/57,41/57,42/57,43/57,44/57,45/57,46/57,47/57,48/57,49/57,50/57,51/57,52/57,53/57,54/57,55/57,56/57,57/57,58/57,59/57,60/57,61/57,62/57,63/57,64/57,65/57,66/57," +
"67/57,68/57,69/57,70/57,71/57,72/57,73/57,74/57,75/57,76/57,77/57,78/57,79/57,80/57,81/57,82/57,83/57,84/57,85/57,86/57,87/57,88/57,89/57,90/57,91/57,92/57,93/57,94/57,95/56,95/55,95/54,95/53,95/52,95/51,95/50,95/49,95/48,95/47,95/46,95/45,95/44,95/43,95/42,95/41,95/40,95/39,95/38,95/38,94/38,93/38,92/38,91/38,90/38,89/38,88/38,87/38,86/38,85/38,84/38,83/38,82/38,81/38,80/38,79/38,78/38,77/38,76/38,75/38,74/38,73/38,72/38,71/38,70/38,69/38,68/38,67/38,66/38,65/38,64/38,63/38,62/38,61/38,60/38,59" +
"/38,58/38,57/38,56/38,55/38,54/38,53/38,52/38,51/38,50/38,49/38,48/38,47/38,46/38,45/38,44/38,43/38,42/38,41/38,40/38,39/38,38/38,37/38,36/38,35/38,34/38,33/38,32/38,31/38,30/38,29/38,28/38,27/38,26/38,25/38,24/38,23/38,22/38,21/38,20/38,19/37,19/36,19/35,19/34,19/33,19/32,19/31,19/30,19/29,19/28,19/27,19/26,19/25,19/24,19/23,19/22,19/21,19/20,19/19,19/19,20/19,21/19,22/19,23/19,24/19,25/19,26/19,27/19,28/19,29/19,30/19,31/19,32/19,33/19,34/19,35/19,36/19,37/19,38/19,39/19,40/19,41/19,42/19,43/1" +
"9,44/19,45/19,46/19,47/19,48/19,49/19,50/19,51/19,52/19,53/19,54/19,55/19,56/19,57/19,58/19,59/19,60/19,61/19,62/19,63/19,64/19,65/19,66/19,67/19,68/19,69/19,70/19,71/19,72/19,73/19,74/19,75/19,76/19,77/19,78/19,79/19,80/19,81/19,82/19,83/19,84/19,85/19,86/19,87/19,88/19,89/19,90/19,91/19,92/19,93/19,94/19,95/18,95/17,95/16,95/15,95/14,95/13,95/12,95/11,95/10,95/9,95/8,95/7,95/6,95/5,95/4,95/3,95/2,95/1,95/0,95/0,94/0,93/0,92/0,91/0,90/0,89/0,88/0,87/0,86/0,85/0,84/0,83/0,82/0,81/0,80/0,79/0,78/" +
"0,77/0,76/0,75/0,74/0,73/0,72/0,71/0,70/0,69/0,68/0,67/0,66/0,65/0,64/0,63/0,62/0,61/0,60/0,59/0,58/0,57/0,56/0,55/0,54/0,53/0,52/0,51/0,50/0,49/0,48/0,47/0,46/0,45/0,44/0,43/0,42/0,41/0,40/0,39/0,38/0,37/0,36/0,35/0,34/0,33/0,32/0,31/0,30/0,29/0,28/0,27/0,26/0,25/0,24/0,23/0,22/0,21/0,20/0,19/0,18/0,17/0,16/0,15/0,14/0,13/0,12/0,11/0,10/0,9/0,8/0,7/0,6/0,5/0,4/0,3/0,2/0,1";

			const double SnakeBrightness = 1f / 16;
			const double PelletBrightness = 1f / 16;
			const int LengthIncrease = 32;
			const int ColorVariation = 1000;

			var segment = new Segment();

			var colors = Helpers.Rainbow7.Multiply(SnakeBrightness).ToList();
			colors.AddRange(colors.AsEnumerable().Reverse().Skip(1));
			var snakeLights = data.Split('/').Select(l => l.Split(',').Select(int.Parse).ToList()).Select(p => bodyLayout.GetPositionLights(p[0], p[1], 2, 2)).ToList();

			var time = 0;
			var busy = snakeLights.Select(b => false).ToArray();
			var snakeStart = 0;
			var snakeEnd = -1;
			var addLen = LengthIncrease;
			var pellet = default(int?);
			var rand = new Random(0xbadf00d);
			var color = new LightColor(0, ColorVariation, colors);
			while (true)
			{
				var busyCount = busy.Count(b => b);

				++snakeStart;
				if (snakeStart >= snakeLights.Count)
					snakeStart -= snakeLights.Count;
				foreach (var light in snakeLights[snakeStart])
					segment.AddLight(light, time, color, time % ColorVariation);

				busy[snakeStart] = true;
				if (snakeStart == pellet)
				{
					pellet = null;
					addLen += LengthIncrease;
				}

				if ((addLen > 0) && (time % 4 == 0))
					--addLen;
				else
				{
					++snakeEnd;
					if (snakeEnd >= snakeLights.Count)
						snakeEnd -= snakeLights.Count;
					foreach (var light in snakeLights[snakeEnd])
						segment.AddLight(light, time, Segment.Absolute, 0x000000);
					busy[snakeEnd] = false;
				}

				if (!pellet.HasValue)
				{
					pellet = busy.Indexes(b => !b).OrderBy(b => rand.Next()).DefaultIfEmpty(-1).First();
					if (pellet == -1)
						break;
					foreach (var light in snakeLights[pellet.Value])
					{
						segment.AddLight(light, time, Segment.Absolute, Helpers.MultiplyColor(0xffffff, PelletBrightness));
					}
				}

				++time;
			}

			return segment;
		}

		const int RunAwayDistance = 20;
		static Random rand = new Random(0xdec0de);

		class Player
		{
			public Player()
			{
				Position = new Point(rand.Next(32), rand.Next(8) - 10);
			}

			public int Color { get; set; }
			public Point Position { get; set; }
			public Point? Destination { get; set; }

			bool tagged = true;
			public bool Tagged
			{
				get => tagged;
				set
				{
					if (tagged == value)
						return;
					tagged = value;
					Destination = null;
				}
			}
		}

		void MoveHunters(List<Player> hunters, List<Player> players)
		{
			while (true)
			{
				hunters = hunters.Where(hunter => !hunter.Tagged).ToList();
				players = players.Where(player => !player.Tagged).ToList();

				var ideas = hunters.SelectMany(hunter => players.Select(player => Tuple.Create(hunter, player, (hunter.Position - player.Position).LengthSquared))).OrderBy(idea => idea.Item3).ToList();
				var tagged = ideas.Where(idea => idea.Item3 <= 1).Select(idea => idea.Item2).ToList();
				if (tagged.Any())
				{
					tagged.ForEach(player => player.Tagged = true);
					continue;
				}

				var busy = new HashSet<Player>();
				foreach (var hunter in ideas.Select(idea => idea.Item1).Distinct())
				{
					var close = ideas.Where(idea => idea.Item1 == hunter).Select(idea => idea.Item2).ToList();
					var closest = close.Where(player => !busy.Contains(player)).Concat(close).First();
					busy.Add(closest);
					hunter.Position = SongHelper.NextLocation(hunter.Position, closest.Position);
				}

				break;
			}
		}

		void MovePlayers(List<Player> hunters, List<Player> players)
		{
			foreach (var player in players)
			{
				if (!player.Tagged)
				{
					var closest = hunters.Where(hunter => (!hunter.Tagged)).OrderBy(hunter => (player.Position - hunter.Position).LengthSquared).FirstOrDefault();
					if ((closest != null) && ((closest.Position - player.Position).Length < RunAwayDistance))
					{
						player.Position = SongHelper.NextLocation(player.Position, closest.Position, true);
						player.Destination = null;
						continue;
					}
				}

				if (player.Destination == null)
					while (true)
					{
						player.Destination = player.Tagged ? new Point(rand.Next(32), rand.Next(8) - 10) : new Point(rand.Next(97), rand.Next(97));
						if ((player.Destination.Value != player.Position) && (SongHelper.GetLight(player.Destination.Value) != null))
							break;
					}

				player.Position = SongHelper.NextLocation(player.Position, player.Destination.Value);
				if (player.Position == player.Destination.Value)
					player.Destination = null;
			}
		}

		bool GameIsOver(List<Player> hunters, List<Player> players)
		{
			if (hunters.Any(hunter => hunter.Tagged))
				return false;
			if (players.Any(player => (!player.Tagged) || (player.Position.Y >= 0)))
				return false;
			return true;
		}

		Segment RenderGame(int numHunters, int numPlayers)
		{
			var segment = new Segment();
			var colors = new List<int> { 0xff0000, 0x00ff00, 0xffff00, 0xff00ff, 0xff8000 };

			const int HunterLockup = 1000;
			const int PlayerLockup = 500;
			const int EndingDelay = 500;

			var hunters = Enumerable.Range(0, numHunters).Select(x => new Player()).ToList();
			var players = Enumerable.Range(0, numPlayers).Select(x => new Player { Color = colors[x % colors.Count] }).ToList();

			var time = 0;
			var stopTime = default(int?);
			while (true)
			{
				if (time == HunterLockup)
					hunters.ForEach(hunter => hunter.Tagged = false);
				if (time == PlayerLockup)
					players.ForEach(player => player.Tagged = false);

				var draw = false;
				if (time % 4 == 0)
				{
					MoveHunters(hunters, players);
					draw = true;
				}
				if (time % 6 == 0)
				{
					MovePlayers(hunters, players);
					draw = true;
				}

				if (draw)
				{
					if (GameIsOver(hunters, players))
						stopTime = stopTime ?? time + EndingDelay;

					segment.Clear(time);
					players.ForEach(player => segment.AddLight(SongHelper.GetLight(player.Position).Value, time, Segment.Absolute, Helpers.MultiplyColor(player.Tagged ? 0x0000ff : player.Color, Brightness)));
					hunters.ForEach(hunter => segment.AddLight(SongHelper.GetLight(hunter.Position).Value, time, Segment.Absolute, Helpers.MultiplyColor(0xffffff, Brightness / (hunter.Tagged ? 3 : 1))));
				}

				++time;
				if (time == stopTime)
					break;
			}

			return segment;
		}

		List<Segment> Tags()
		{
			var segments = new List<Segment>();
			segments.Add(RenderGame(1, 1));
			segments.Add(RenderGame(1, 5));
			segments.Add(RenderGame(2, 32));
			segments.Add(RenderGame(8, 256));
			return segments;
		}

		void LinesSegment(Segment segment, int square1, double startAngle1, double endAngle1, int square2, double startAngle2, double endAngle2, LightColor color, ref int time)
		{
			const double MinWidth = 1;
			const double MaxWidth = 1.5;

			var positions1 = squaresLayout.GetLightPositions(square1);
			var positions2 = squaresLayout.GetLightPositions(square2);
			var center1 = new Point((positions1.Min(p => p.X) + positions1.Max(p => p.X)) / 2, (positions1.Min(p => p.Y) + positions1.Max(p => p.Y)) / 2);
			var center2 = new Point((positions2.Min(p => p.X) + positions2.Max(p => p.X)) / 2, (positions2.Min(p => p.Y) + positions2.Max(p => p.Y)) / 2);
			var count = Math.Abs(endAngle1 - startAngle1) / 2;
			for (var ctr = 0; ctr < count; ++ctr)
			{
				foreach (var light in bodyLayout.GetAllLights())
				{
					var pos = bodyLayout.GetLightPosition(light);
					var percent1 = 0D;
					var percent2 = 0D;

					var useAngle1 = (startAngle1 + (endAngle1 - startAngle1) * ctr / count) * Math.PI / 180;
					var useAngle2 = (startAngle2 + (endAngle2 - startAngle2) * ctr / count) * Math.PI / 180;
					var sin1 = Math.Sin(useAngle1);
					var sin2 = Math.Sin(useAngle2);
					var cos1 = -Math.Cos(useAngle1);
					var cos2 = -Math.Cos(useAngle2);
					var R1 = (pos.X - center1.X) * sin1 + (pos.Y - center1.Y) * cos1;
					var R2 = (pos.X - center2.X) * sin2 + (pos.Y - center2.Y) * cos2;
					var dist1 = Math.Sqrt(Math.Pow(R1 * sin1 + center1.X - pos.X, 2) + Math.Pow(R1 * cos1 + center1.Y - pos.Y, 2));
					var dist2 = Math.Sqrt(Math.Pow(R2 * sin2 + center2.X - pos.X, 2) + Math.Pow(R2 * cos2 + center2.Y - pos.Y, 2));
					if (dist1 < MaxWidth)
						percent1 += Math.Min(1 - (dist1 - MinWidth) / (MaxWidth - MinWidth), 1);
					if (dist2 < MaxWidth)
						percent2 += Math.Min(1 - (dist2 - MinWidth) / (MaxWidth - MinWidth), 1);

					segment.AddLight(light, time, color, ((percent1 + percent2) * 1000).Round());
				}
				++time;
			}
		}

		Segment Lines(out int linesTime)
		{
			var segments = new List<Tuple<Segment, int>>();

			var color = new LightColor(0, 1000,
				new List<int> { 0x000000, 0xffffff }.Multiply(Brightness).ToList(),
				new List<int> { 0x000000, 0x0000ff }.Multiply(Brightness).ToList(),
				new List<int> { 0x000000, 0xff0000 }.Multiply(Brightness).ToList(),
				new List<int> { 0x000000, 0x00ff00 }.Multiply(Brightness).ToList()
			);

			var segment = new Segment();
			linesTime = 0;
			LinesSegment(segment, 8, 45, 135, 9, 90, 0, color, ref linesTime);
			LinesSegment(segment, 14, 135, 225, 19, 180, 90, color, ref linesTime);
			LinesSegment(segment, 18, 45, 135, 17, 90, 0, color, ref linesTime);
			LinesSegment(segment, 12, 135, 225, 7, 180, 90, color, ref linesTime);
			return segment;
		}

		Segment Sine()
		{
			const int Size = 2;

			var segment = new Segment();
			var allLights = bodyLayout.GetAllLights();
			var allLocations = allLights.Select(light => bodyLayout.GetLightPosition(light)).ToList();
			var ordered = allLocations.OrderBy(p => p.X).ThenBy(p => p.Y);

			var color = new LightColor(0, 1,
				new List<int> { 0xff0000, 0x0000ff }.Multiply(Brightness).ToList(),
				new List<int> { 0x0000ff, 0x00ff00 }.Multiply(Brightness).ToList(),
				new List<int> { 0x00ff00, 0xff0000 }.Multiply(Brightness).ToList()
			);

			for (var time = 0; time < 1000; time += 5)
			{
				segment.Clear(time);

				for (var x = 0; x <= 96; x += 19)
				{
					var y = ((Math.Sin(2 * Math.PI * (time / 1000D + x / 96D)) + 1) * (97 - Size) / 2).Round();
					var lights = bodyLayout.GetPositionLights(x, y, 2, 2);
					foreach (var light in lights)
						segment.AddLight(light, time, color, 0);

					y = ((Math.Cos(2 * Math.PI * (time / 1000D + x / 96D)) + 1) * (97 - Size) / 2).Round();
					lights = bodyLayout.GetPositionLights(y, x, 2, 2);
					foreach (var light in lights)
						segment.AddLight(light, time, color, 1);
				}
			}

			return segment;
		}

		Segment Face()
		{
			const string data =
"21,0&21,1/22,0&22,1/23,0&23,1/24,0&24,1/25,0&25,1/26,0&26,1/27,0&27,1/28,0&28,1/29,0&29,1/30,0&30,1/31,0&31,1/32,0&32,1/33,0&33,1/34,0&34,1/35,0&35,1/36,0&36,1/37,0&37,1/38,0/38,1&39,0/39,1/38,2&39,2/38,3&39,3/38,4&39,4/38,5&39,5/38,6&39,6/38,7&39,7/38,8&39,8/38,9&39,9/38,10&39,10/38,11&39,11/38,12&39,12/38,13&39,13/38,14&39,14/38,15&39,15/38,16&39,16/38,17&39,17/38,18&39,18/39,19/38,19&39,20/38,20/37,19&37,20/36,19&36,20/35,19&35,20/34,19&34,20/33,19&33,20/32,19&32,20/31,19&31,20/30,19&30,20/29" +
",19&29,20/28,19&28,20/27,19&27,20/26,19&26,20/25,19&25,20/24,19&24,20/23,19&23,20/22,19&22,20/21,19&21,20/20,20/19,20&20,19/19,19/19,18&20,18/19,17&20,17/19,16&20,16/19,15&20,15/19,14&20,14/19,13&20,13/19,12&20,12/19,11&20,11/19,10&20,10/19,9&20,9/19,8&20,8/19,7&20,7/19,6&20,6/19,5&20,5/19,4&20,4/19,3&20,3/19,2&20,2/19,1/19,0&20,1/20,0P59,0&59,1/60,0&60,1/61,0&61,1/62,0&62,1/63,0&63,1/64,0&64,1/65,0&65,1/66,0&66,1/67,0&67,1/68,0&68,1/69,0&69,1/70,0&70,1/71,0&71,1/72,0&72,1/73,0&73,1/74,0&74,1/75" +
",0&75,1/76,0/76,1&77,0/77,1/76,2&77,2/76,3&77,3/76,4&77,4/76,5&77,5/76,6&77,6/76,7&77,7/76,8&77,8/76,9&77,9/76,10&77,10/76,11&77,11/76,12&77,12/76,13&77,13/76,14&77,14/76,15&77,15/76,16&77,16/76,17&77,17/76,18&77,18/77,19/76,19&77,20/76,20/75,19&75,20/74,19&74,20/73,19&73,20/72,19&72,20/71,19&71,20/70,19&70,20/69,19&69,20/68,19&68,20/67,19&67,20/66,19&66,20/65,19&65,20/64,19&64,20/63,19&63,20/62,19&62,20/61,19&61,20/60,19&60,20/59,19&59,20/58,20/57,20&58,19/57,19/57,18&58,18/57,17&58,17/57,16&58" +
",16/57,15&58,15/57,14&58,14/57,13&58,13/57,12&58,12/57,11&58,11/57,10&58,10/57,9&58,9/57,8&58,8/57,7&58,7/57,6&58,6/57,5&58,5/57,4&58,4/57,3&58,3/57,2&58,2/57,1/57,0&58,1/58,0P48,57&48,58/49,57&49,58/50,57&50,58/51,57&51,58/52,57&52,58/53,57&53,58/54,57&54,58/55,57&55,58/56,57&56,58/57,57&57,58/58,57&58,58/59,57&59,58/60,57&60,58/61,57&61,58/62,57&62,58/63,57&63,58/64,57&64,58/65,57&65,58/66,57&66,58/67,57&67,58/68,57&68,58/69,57&69,58/70,57&70,58/71,57&71,58/72,57&72,58/73,57&73,58/74,57&74,58/" +
"75,57&75,58/76,57&76,58/77,57&77,58/78,57&78,58/79,57&79,58/80,57&80,58/81,57&81,58/82,57&82,58/83,57&83,58/84,57&84,58/85,57&85,58/86,57&86,58/87,57&87,58/88,57&88,58/89,57&89,58/90,57&90,58/91,57&91,58/92,57&92,58/93,57&93,58/94,57&94,58/95,58/95,57&96,58/96,57/95,56&96,56/95,55&96,55/95,54&96,54/95,53&96,53/95,52&96,52/95,51&96,51/95,50&96,50/95,49&96,49/95,48&96,48/95,47&96,47/95,46&96,46/95,45&96,45/95,44&96,44/95,43&96,43/95,42&96,42/95,41&96,41/95,40&96,40/96,39/95,39&96,38/95,38/94,38&94" +
",39/93,38&93,39/92,38&92,39/91,38&91,39/90,38&90,39/89,38&89,39/88,38&88,39/87,38&87,39/86,38&86,39/85,38&85,39/84,38&84,39/83,38&83,39/82,38&82,39/81,38&81,39/80,38&80,39/79,38&79,39/78,38&78,39/77,38/76,38&77,39/76,39/76,40&77,40/76,41&77,41/76,42&77,42/76,43&77,43/76,44&77,44/76,45&77,45/76,46&77,46/76,47&77,47/76,48&77,48/76,49&77,49/76,50&77,50/76,51&77,51/76,52&77,52/76,53&77,53/76,54&77,54/76,55&77,55/76,56&77,56/76,57&77,57/76,58&77,58/76,59&77,59/76,60&77,60/76,61&77,61/76,62&77,62/76,6" +
"3&77,63/76,64&77,64/76,65&77,65/76,66&77,66/76,67&77,67/76,68&77,68/76,69&77,69/76,70&77,70/76,71&77,71/76,72&77,72/76,73&77,73/76,74&77,74/76,75&77,75/77,76/76,76&77,77/76,77/75,76&75,77/74,76&74,77/73,76&73,77/72,76&72,77/71,76&71,77/70,76&70,77/69,76&69,77/68,76&68,77/67,76&67,77/66,76&66,77/65,76&65,77/64,76&64,77/63,76&63,77/62,76&62,77/61,76&61,77/60,76&60,77/59,76&59,77/58,76&58,77/57,76&57,77/56,76&56,77/55,76&55,77/54,76&54,77/53,76&53,77/52,76&52,77/51,76&51,77/50,76&50,77/49,76&49,77/" +
"48,76&48,77/47,76&47,77/46,76&46,77/45,76&45,77/44,76&44,77/43,76&43,77/42,76&42,77/41,76&41,77/40,76&40,77/39,76&39,77/38,76&38,77/37,76&37,77/36,76&36,77/35,76&35,77/34,76&34,77/33,76&33,77/32,76&32,77/31,76&31,77/30,76&30,77/29,76&29,77/28,76&28,77/27,76&27,77/26,76&26,77/25,76&25,77/24,76&24,77/23,76&23,77/22,76&22,77/21,76&21,77/20,77/19,77&20,76/19,76/19,75&20,75/19,74&20,74/19,73&20,73/19,72&20,72/19,71&20,71/19,70&20,70/19,69&20,69/19,68&20,68/19,67&20,67/19,66&20,66/19,65&20,65/19,64&20" +
",64/19,63&20,63/19,62&20,62/19,61&20,61/19,60&20,60/19,59&20,59/19,58&20,58/19,57&20,57/19,56&20,56/19,55&20,55/19,54&20,54/19,53&20,53/19,52&20,52/19,51&20,51/19,50&20,50/19,49&20,49/19,48&20,48/19,47&20,47/19,46&20,46/19,45&20,45/19,44&20,44/19,43&20,43/19,42&20,42/19,41&20,41/19,40&20,40/20,39/19,39&20,38/19,38/18,38&18,39/17,38&17,39/16,38&16,39/15,38&15,39/14,38&14,39/13,38&13,39/12,38&12,39/11,38&11,39/10,38&10,39/9,38&9,39/8,38&8,39/7,38&7,39/6,38&6,39/5,38&5,39/4,38&4,39/3,38&3,39/2,38&2" +
",39/1,38/0,38&1,39/0,39/0,40&1,40/0,41&1,41/0,42&1,42/0,43&1,43/0,44&1,44/0,45&1,45/0,46&1,46/0,47&1,47/0,48&1,48/0,49&1,49/0,50&1,50/0,51&1,51/0,52&1,52/0,53&1,53/0,54&1,54/0,55&1,55/0,56&1,56/0,57/0,58&1,57/1,58/2,57&2,58/3,57&3,58/4,57&4,58/5,57&5,58/6,57&6,58/7,57&7,58/8,57&8,58/9,57&9,58/10,57&10,58/11,57&11,58/12,57&12,58/13,57&13,58/14,57&14,58/15,57&15,58/16,57&16,58/17,57&17,58/18,57&18,58/19,57&19,58/20,57&20,58/21,57&21,58/22,57&22,58/23,57&23,58/24,57&24,58/25,57&25,58/26,57&26,58/27" +
",57&27,58/28,57&28,58/29,57&29,58/30,57&30,58/31,57&31,58/32,57&32,58/33,57&33,58/34,57&34,58/35,57&35,58/36,57&36,58/37,57&37,58/38,57&38,58/39,57&39,58/40,57&40,58/41,57&41,58/42,57&42,58/43,57&43,58/44,57&44,58/45,57&45,58/46,57&46,58/47,57&47,58";

			const int Length = 60;
			const int PassiveColor = 0x040404;

			var segment = new Segment();

			var faceLights = data.Split('P').Select(part => part.Split('/').Select(l => l.Split('&').Select(p => p.Split(',').Select(int.Parse).ToList()).Select(p => bodyLayout.GetPositionLight(p[0], p[1])).ToList()).ToList()).ToList();
			var color = new LightColor(0, 100, new List<List<int>> {
				new List<int> { 0xff6969, 0xd34949, 0xda3232, 0xb80000, 0x620000 }.Multiply(Brightness).ToList(),
				new List<int> { 0x32d0d3, 0x0000ff, 0x66ffff }.Multiply(Brightness).ToList(),
				Helpers.Rainbow6.Multiply(Brightness).ToList()
			});

			foreach (var facePart in faceLights)
				foreach (var lightListOrder in facePart)
					foreach (var light in lightListOrder)
						segment.AddLight(light, 0, Segment.Absolute, PassiveColor);

			for (var time = 0; time < 3120; ++time)
			{
				foreach (var facePart in faceLights)
				{
					var onOffset = time % facePart.Count;
					foreach (var light in facePart[onOffset])
						segment.AddLight(light, time, time + Length, color, 0, color, 100, true);
					var offOffset = (time - Length) % facePart.Count;
					if (offOffset >= 0)
						foreach (var light in facePart[offOffset])
							segment.AddLight(light, time, Segment.Absolute, PassiveColor);
				}
			}

			return segment;
		}

		int[,] CloudsGetPixels(string fileName, double Brightness)
		{
			int[,] pixels;
			using (var stream = typeof(Bohemian).Assembly.GetManifestResourceStream(fileName))
			using (var image = System.Drawing.Image.FromStream(stream))
			using (var bmp = new System.Drawing.Bitmap(image))
			{
				if ((bmp.Width != 97) || (bmp.Height != 97))
					throw new Exception("Invalid image");

				pixels = new int[bmp.Width, bmp.Height];
				var lockBits = bmp.LockBits(new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				var data = new byte[lockBits.Stride * bmp.Height];
				Marshal.Copy(lockBits.Scan0, data, 0, data.Length);
				bmp.UnlockBits(lockBits);

				var lineSkip = lockBits.Stride - bmp.Width * sizeof(int);
				var ofs = 0;
				for (var y = 0; y < bmp.Height; ++y)
				{
					for (var x = 0; x < bmp.Width; ++x)
					{
						pixels[x, y] = Helpers.MultiplyColor(BitConverter.ToInt32(data, ofs), Brightness);
						ofs += sizeof(int);
					}
					ofs += lineSkip;
				}
				var hexChars = Enumerable.Range(0, 16).Select(num => $"{num:x}"[0]).ToArray();
			}

			return pixels;
		}

		Segment Clouds()
		{
			var empty = new int[97, 97];
			var pixels = CloudsGetPixels("Shelfinator.Creator.Songs.Layout.Clouds.png", Brightness);

			var segment = new Segment();

			for (var offset = 0; offset < 97; ++offset)
				for (var y = 0; y < 97; ++y)
					for (var x = 0; x < 97; ++x)
						foreach (var light in bodyLayout.GetPositionLights((x + offset) % 97, (y + offset) % 97, 1, 1))
							segment.AddLight(light, offset, Segment.Absolute, pixels[x, y]);

			return segment;
		}


		class FloodPoint
		{
			const int Length = 100;
			Dictionary<int, int?> points;
			readonly Dictionary<int, List<int>> next;
			readonly int color;

			public FloodPoint(Dictionary<int, List<int>> next, int color, int light)
			{
				this.next = next;
				this.color = color;
				points = next.ToDictionary(pair => pair.Key, pair => default(int?));
				points[light] = 0;
			}

			public bool Increment()
			{
				var result = false;
				var newPoints = points.ToDictionary(pair => pair.Key, pair => pair.Value + 1);
				foreach (var light in points.Keys)
				{
					if ((points[light] ?? 0) < Length)
						result = true;
					if (points[light] == 0)
					{
						foreach (var nextLight in next[light])
							if (!points[nextLight].HasValue)
								newPoints[nextLight] = 0;
					}
				}
				points = newPoints;
				return result;
			}

			public void AddColor(Dictionary<int, int> lights)
			{
				if (lights.Count != next.Count)
					throw new ArgumentException(nameof(lights));
				foreach (var light in lights.Keys.ToList())
					if (points[light].HasValue)
					{
						var mult = (double)points[light].Value;
						if (mult >= Length / 2)
							mult = Length - mult;
						mult = Math.Max(Math.Min(1, mult / 20), 0);
						var thisColor = Helpers.MultiplyColor(color, mult);
						lights[light] = Helpers.AddColor(lights[light], thisColor);
					}
			}
		}

		Segment Flood()
		{
			var lights = bodyLayout.GetAllLights();
			var next = lights.Select(l => new { light = l, point = bodyLayout.GetLightPosition(l) }).Select(o => new { o.light, points = new List<Point> { new Point(o.point.X - 1, o.point.Y), new Point(o.point.X + 1, o.point.Y), new Point(o.point.X, o.point.Y - 1), new Point(o.point.X, o.point.Y + 1) } }).ToDictionary(o => o.light, o => bodyLayout.TryGetPositionLights(o.points));
			var segment = new Segment();

			var points = new List<FloodPoint>();
			var time = 0;
			while (true)
			{
				var newPoints = new List<FloodPoint>();
				foreach (var point in points)
					if (point.Increment())
						newPoints.Add(point);
				points = newPoints;
				if ((!points.Any()) && (time >= 4400))
					break;

				switch (time)
				{
					case 0: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), bodyLayout.GetPositionLight(0, 96))); break;
					case 200: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), bodyLayout.GetPositionLight(0, 0))); break;
					case 400: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), bodyLayout.GetPositionLight(96, 0))); break;
					case 600: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), bodyLayout.GetPositionLight(96, 96))); break;
					case 800:
						points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), bodyLayout.GetPositionLight(0, 96)));
						points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), bodyLayout.GetPositionLight(96, 0)));
						break;
					case 1000:
						points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), bodyLayout.GetPositionLight(0, 0)));
						points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), bodyLayout.GetPositionLight(96, 96)));
						break;
					case 1200:
						points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), bodyLayout.GetPositionLight(0, 96)));
						points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), bodyLayout.GetPositionLight(96, 0)));
						break;
					case 1400: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), bodyLayout.GetPositionLight(58, 94))); break;
					case 1509: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), bodyLayout.GetPositionLight(19, 66))); break;
					case 1614: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), bodyLayout.GetPositionLight(0, 90))); break;
					case 1717: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), bodyLayout.GetPositionLight(51, 95))); break;
					case 1818: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), bodyLayout.GetPositionLight(53, 57))); break;
					case 1915: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), bodyLayout.GetPositionLight(2, 95))); break;
					case 2010: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), bodyLayout.GetPositionLight(19, 32))); break;
					case 2103: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), bodyLayout.GetPositionLight(9, 0))); break;
					case 2193: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), bodyLayout.GetPositionLight(64, 96))); break;
					case 2281: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), bodyLayout.GetPositionLight(36, 76))); break;
					case 2366: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), bodyLayout.GetPositionLight(0, 68))); break;
					case 2449: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), bodyLayout.GetPositionLight(3, 19))); break;
					case 2530: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), bodyLayout.GetPositionLight(77, 2))); break;
					case 2609: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), bodyLayout.GetPositionLight(39, 71))); break;
					case 2685: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), bodyLayout.GetPositionLight(69, 77))); break;
					case 2760: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), bodyLayout.GetPositionLight(19, 69))); break;
					case 2832: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), bodyLayout.GetPositionLight(47, 95))); break;
					case 2903: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), bodyLayout.GetPositionLight(1, 25))); break;
					case 2972: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), bodyLayout.GetPositionLight(57, 81))); break;
					case 3039: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), bodyLayout.GetPositionLight(58, 85))); break;
					case 3104: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), bodyLayout.GetPositionLight(58, 33))); break;
					case 3168: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), bodyLayout.GetPositionLight(6, 39))); break;
					case 3229: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), bodyLayout.GetPositionLight(70, 20))); break;
					case 3290: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), bodyLayout.GetPositionLight(70, 77))); break;
					case 3348: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), bodyLayout.GetPositionLight(20, 94))); break;
					case 3405: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), bodyLayout.GetPositionLight(1, 13))); break;
					case 3461: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), bodyLayout.GetPositionLight(74, 95))); break;
					case 3515: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), bodyLayout.GetPositionLight(38, 10))); break;
					case 3567: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), bodyLayout.GetPositionLight(69, 39))); break;
					case 3619: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), bodyLayout.GetPositionLight(90, 39))); break;
					case 3669: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), bodyLayout.GetPositionLight(21, 20))); break;
					case 3717: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), bodyLayout.GetPositionLight(58, 0))); break;
					case 3764: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), bodyLayout.GetPositionLight(76, 41))); break;
					case 3810: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), bodyLayout.GetPositionLight(77, 19))); break;
					case 3855: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), bodyLayout.GetPositionLight(80, 77))); break;
					case 3899: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), bodyLayout.GetPositionLight(28, 1))); break;
					case 3941: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), bodyLayout.GetPositionLight(0, 0))); break;
					case 3983: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), bodyLayout.GetPositionLight(33, 96))); break;
					case 4023: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0xff0000, Brightness), bodyLayout.GetPositionLight(58, 92))); break;
					case 4062: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x00ff00, Brightness), bodyLayout.GetPositionLight(53, 1))); break;
					case 4100: points.Add(new FloodPoint(next, Helpers.MultiplyColor(0x0000ff, Brightness), bodyLayout.GetPositionLight(64, 39))); break;
				}

				var colors = next.ToDictionary(x => x.Key, x => 0);
				foreach (var point in points)
					point.AddColor(colors);

				foreach (var pair in colors)
					segment.AddLight(pair.Key, time, Segment.Absolute, pair.Value);

				++time;
			}

			return segment;
		}

		Segment Flashy()
		{
			const int BladeCount = 8;
			const int Fade = 100;
			const int Delay = 150;

			var segment = new Segment();

			foreach (var square in squaresLayout.GetAllLights())
			{
				var allLights = bodyLayout.GetMappedLights(squaresLayout, square);
				var allLocations = allLights.Select(light => bodyLayout.GetLightPosition(light)).ToList();
				var topLeft = allLocations.OrderBy(p => p.X).ThenBy(p => p.Y).First();
				var bottomRight = allLocations.OrderBy(p => p.X).ThenBy(p => p.Y).Last();
				var center = new Point((topLeft.X + bottomRight.X) / 2, (topLeft.Y + bottomRight.Y) / 2);

				var angles = allLocations.GetAngles(center).Cycle(0, 360 / BladeCount).AdjustToZero().Scale(0, 360 / BladeCount, 0, 500).Round().ToList();

				var rainbow = Helpers.Rainbow7.Concat(Helpers.Rainbow7[0]).Multiply(Brightness).ToList();
				var useColors = new LightColor(angles.Min(), angles.Max(), rainbow);
				for (var ctr = 0; ctr < allLights.Count; ++ctr)
					for (var repeat = 0; repeat < 9000; repeat += 500)
					{
						segment.AddLight(allLights[ctr], angles[ctr] + repeat, angles[ctr] + repeat + Fade, null, 0, useColors, angles[ctr]);
						segment.AddLight(allLights[ctr], angles[ctr] + repeat + Delay, angles[ctr] + repeat + Delay + Fade, null, Segment.Absolute, 0x000000);
					}
			}
			return segment;
		}


		Segment Clock()
		{
			var segment = new Segment();
			var positions = squaresLayout.GetAllLights().Except(0).OrderBy(light => light).Select(light => squaresLayout.GetLightPositions(light)).ToList();
			var lights = positions.Select(list => list.Select(light => bodyLayout.GetPositionLight(light)).ToList()).ToList();
			var centers = positions.Select(list => Helpers.GetCenter(list)).ToList();
			var rand = new Random();
			var offsets = Enumerable.Range(0, centers.Count).Select(index => index * 360 / centers.Count).OrderBy(x => rand.Next()).ToList();
			var angles = positions.Select((list, index) => list.Select(p => Helpers.GetAngle(p, centers[index]) + offsets[index]).ToList()).ToList();

			var color = new LightColor(0, 20, Helpers.Rainbow6.Select(value => new List<int> { value, 0x000000 }.Multiply(Brightness).ToList()).ToList());

			for (var angle = 0; angle < 360; angle += 5)
				for (var listCtr = 0; listCtr < lights.Count; listCtr++)
				{
					var reverse = listCtr % 2 == 1;

					for (var lightCtr = 0; lightCtr < lights[listCtr].Count; lightCtr++)
					{
						var lightAngle = (int)Math.Abs(Helpers.Cycle((reverse ? -1 : 1) * angle - angles[listCtr][lightCtr], -180, 180));
						segment.AddLight(lights[listCtr][lightCtr], angle, color, lightAngle);
					}
				}

			return segment;
		}

		Segment Runner()
		{
			const int Concurrency = 19;
			const string Song =
"0,0/1,0/2,0/3,0/4,0/5,0/6,0/7,0/8,0/9,0/10,0/11,0/12,0/13,0/14,0/15,0/16,0/17,0/18,0/19,0/20,0/21,0/22,0/23,0/24,0/25,0/26,0/27,0/28,0/29,0/30,0/31,0/32,0/33,0/34,0/35,0/36,0/37,0/38,0/39,0/40,0/41,0/42,0/43,0/44,0/45,0/46,0/47,0/48,0/49,0/50,0/51,0/52,0/53,0/54,0/55,0/56,0/57,0/58,0/59,0/60,0/61,0/62,0/63,0/64,0/65,0/66,0/67,0/68,0/69,0/70,0/71,0/72,0/73,0/74,0/75,0/76,0/77,0/78,0/79,0/80,0/81,0/82,0/83,0/84,0/85,0/86,0/87,0/88,0/89,0/90,0/91,0/92,0/93,0/94,0/95,0/95,1/95,2/95,3/95,4/95,5/95,6/" +
"95,7/95,8/95,9/95,10/95,11/95,12/95,13/95,14/95,15/95,16/95,17/95,18/95,19/95,20/95,21/95,22/95,23/95,24/95,25/95,26/95,27/95,28/95,29/95,30/95,31/95,32/95,33/95,34/95,35/95,36/95,37/95,38/95,39/95,40/95,41/95,42/95,43/95,44/95,45/95,46/95,47/95,48/95,49/95,50/95,51/95,52/95,53/95,54/95,55/95,56/95,57/95,58/95,59/95,60/95,61/95,62/95,63/95,64/95,65/95,66/95,67/95,68/95,69/95,70/95,71/95,72/95,73/95,74/95,75/95,76/95,77/95,78/95,79/95,80/95,81/95,82/95,83/95,84/95,85/95,86/95,87/95,88/95,89/95,90" +
"/95,91/95,92/95,93/95,94/95,95/94,95/93,95/92,95/91,95/90,95/89,95/88,95/87,95/86,95/85,95/84,95/83,95/82,95/81,95/80,95/79,95/78,95/77,95/76,95/75,95/74,95/73,95/72,95/71,95/70,95/69,95/68,95/67,95/66,95/65,95/64,95/63,95/62,95/61,95/60,95/59,95/58,95/57,95/56,95/55,95/54,95/53,95/52,95/51,95/50,95/49,95/48,95/47,95/46,95/45,95/44,95/43,95/42,95/41,95/40,95/39,95/38,95/37,95/36,95/35,95/34,95/33,95/32,95/31,95/30,95/29,95/28,95/27,95/26,95/25,95/24,95/23,95/22,95/21,95/20,95/19,95/18,95/17,95/1" +
"6,95/15,95/14,95/13,95/12,95/11,95/10,95/9,95/8,95/7,95/6,95/5,95/4,95/3,95/2,95/1,95/0,95/0,94/0,93/0,92/0,91/0,90/0,89/0,88/0,87/0,86/0,85/0,84/0,83/0,82/0,81/0,80/0,79/0,78/0,77/0,76/0,75/0,74/0,73/0,72/0,71/0,70/0,69/0,68/0,67/0,66/0,65/0,64/0,63/0,62/0,61/0,60/0,59/0,58/0,57/0,56/0,55/0,54/0,53/0,52/0,51/0,50/0,49/0,48/0,47/0,46/0,45/0,44/0,43/0,42/0,41/0,40/0,39/0,38/0,37/0,36/0,35/0,34/0,33/0,32/0,31/0,30/0,29/0,28/0,27/0,26/0,25/0,24/0,23/0,22/0,21/0,20/0,19/0,18/0,17/0,16/0,15/0,14/0,13" +
"/0,12/0,11/0,10/0,9/0,8/0,7/0,6/0,5/0,4/0,3/0,2/0,1|19,19/19,20/19,21/19,22/19,23/19,24/19,25/19,26/19,27/19,28/19,29/19,30/19,31/19,32/19,33/19,34/19,35/19,36/19,37/19,38/19,39/19,40/19,41/19,42/19,43/19,44/19,45/19,46/19,47/19,48/19,49/19,50/19,51/19,52/19,53/19,54/19,55/19,56/19,57/19,58/19,59/19,60/19,61/19,62/19,63/19,64/19,65/19,66/19,67/19,68/19,69/19,70/19,71/19,72/19,73/19,74/19,75/19,76/20,76/21,76/22,76/23,76/24,76/25,76/26,76/27,76/28,76/29,76/30,76/31,76/32,76/33,76/34,76/35,76/36,7" +
"6/37,76/38,76/39,76/40,76/41,76/42,76/43,76/44,76/45,76/46,76/47,76/48,76/49,76/50,76/51,76/52,76/53,76/54,76/55,76/56,76/57,76/58,76/59,76/60,76/61,76/62,76/63,76/64,76/65,76/66,76/67,76/68,76/69,76/70,76/71,76/72,76/73,76/74,76/75,76/76,76/76,75/76,74/76,73/76,72/76,71/76,70/76,69/76,68/76,67/76,66/76,65/76,64/76,63/76,62/76,61/76,60/76,59/76,58/76,57/76,56/76,55/76,54/76,53/76,52/76,51/76,50/76,49/76,48/76,47/76,46/76,45/76,44/76,43/76,42/76,41/76,40/76,39/76,38/76,37/76,36/76,35/76,34/76,33/" +
"76,32/76,31/76,30/76,29/76,28/76,27/76,26/76,25/76,24/76,23/76,22/76,21/76,20/76,19/75,19/74,19/73,19/72,19/71,19/70,19/69,19/68,19/67,19/66,19/65,19/64,19/63,19/62,19/61,19/60,19/59,19/58,19/57,19/56,19/55,19/54,19/53,19/52,19/51,19/50,19/49,19/48,19/47,19/46,19/45,19/44,19/43,19/42,19/41,19/40,19/39,19/38,19/37,19/36,19/35,19/34,19/33,19/32,19/31,19/30,19/29,19/28,19/27,19/26,19/25,19/24,19/23,19/22,19/21,19/20,19|38,38/39,38/40,38/41,38/42,38/43,38/44,38/45,38/46,38/47,38/48,38/49,38/50,38/51" +
",38/52,38/53,38/54,38/55,38/56,38/57,38/57,39/57,40/57,41/57,42/57,43/57,44/57,45/57,46/57,47/57,48/57,49/57,50/57,51/57,52/57,53/57,54/57,55/57,56/57,57/56,57/55,57/54,57/53,57/52,57/51,57/50,57/49,57/48,57/47,57/46,57/45,57/44,57/43,57/42,57/41,57/40,57/39,57/38,57/38,56/38,55/38,54/38,53/38,52/38,51/38,50/38,49/38,48/38,47/38,46/38,45/38,44/38,43/38,42/38,41/38,40/38,39";

			var results = Song.Split('|').Select(x => x.Split('/').Select(p => bodyLayout.GetPositionLights(new Rect(Point.Parse(p), new Size(2, 2)))).ToList()).ToList();

			var color = new LightColor(0, 3, new List<List<int>> {
				new List<int> { 0xffffff }.Multiply(Brightness).ToList(),
				new List<int> { 0xffffff, 0xff0000, 0xffffff, 0xff0000 }.Multiply(Brightness).ToList(),
				new List<int> { 0x0080ff, 0x01ffff, 0x3f00ff, 0x89d0f0 }.Multiply(Brightness).ToList(),
				new List<int> { 0x17b7ab, 0xe71880, 0xbcd63d, 0xf15a25 }.Multiply(Brightness).ToList(),
			});
			var segment = new Segment();
			foreach (var result in results)
				for (var time = 0; time < Concurrency * 4; ++time)
				{
					foreach (var light in result.SelectMany(x => x))
						segment.AddLight(light, time, Segment.Absolute, 0x000000);
					for (var ctr = 0; ctr < result.Count; ctr += Concurrency)
						foreach (var light in result[(time + ctr) % result.Count])
							segment.AddLight(light, time, color, (ctr / Concurrency) % 4);
				}

			return segment;
		}

		Segment Squares()
		{
			const int DisplayTime = 3000;
			const int FadeTime = 1000;

			var segment = new Segment();
			var squares = new Layout("Shelfinator.Creator.Songs.Layout.Squares.png");
			var positions = squares.GetAllLights().Except(0).OrderBy(light => light).Select(light => squares.GetLightPositions(light)).ToList();
			var lights = squares.GetAllLights().Except(0).OrderBy(light => light).Select(light => squares.GetLightPositions(light)).Select(list => list.Select(light => bodyLayout.GetPositionLight(light)).ToList()).ToList();
			var centers = positions.Select(list => Helpers.GetCenter(list)).ToList();
			var center = Helpers.GetCenter(centers);

			var values = new List<List<int>>();
			values.Add(centers.Select(p => (p - center).Length).Scale(null, null, 0, 100).Round().ToList());
			values.Add(centers.Select(p => (p - center).Length).Scale(null, null, 100, 0).Round().ToList());
			values.Add(centers.Select(p => p.X).Scale(null, null, 0, 100).Round().ToList());
			values.Add(centers.Select(p => p.X).Scale(null, null, 100, 0).Round().ToList());
			values.Add(centers.Select(p => p.Y).Scale(null, null, 0, 100).Round().ToList());
			values.Add(centers.Select(p => p.Y).Scale(null, null, 100, 0).Round().ToList());
			values.Add(centers.Select(p => p.X + p.Y).Scale(null, null, 0, 100).Round().ToList());
			values.Add(centers.Select(p => p.X + p.Y).Scale(null, null, 100, 0).Round().ToList());
			values.Add(centers.Select(p => p.X - p.Y).Scale(null, null, 0, 100).Round().ToList());
			values.Add(centers.Select(p => p.X - p.Y).Scale(null, null, 100, 0).Round().ToList());

			var color = new LightColor(0, 100, Helpers.Rainbow6.Multiply(Brightness).ToList());

			var time = 0;
			for (var paletteCtr = 0; paletteCtr < values.Count; paletteCtr++)
			{
				for (var listCtr = 0; listCtr < lights.Count; listCtr++)
					foreach (var light in lights[listCtr])
						segment.AddLight(light, time, time + FadeTime, null, color, values[paletteCtr][listCtr]);
				time += DisplayTime;
			}

			for (var listCtr = 0; listCtr < lights.Count; listCtr++)
				foreach (var light in lights[listCtr])
					segment.AddLight(light, time, time + FadeTime, null, Segment.Absolute, 0);

			return segment;
		}

		Segment SquareFlash()
		{
			const int Delay = 1000;

			var segment = new Segment();
			var lights = squaresLayout.GetAllLights().Except(0).OrderBy(light => light).Select(light => bodyLayout.GetPositionLights(squaresLayout.GetLightPositions(light))).ToList();

			var color = new LightColor(0, 24, Helpers.Rainbow7.Multiply(Brightness).ToList());

			var rand = new Random(10);
			var order = Enumerable.Range(0, lights.Count).OrderBy(x => rand.Next()).ToList();
			var times = new List<int> { 312120, 313822, 315540, 317238, 319076, 320884, 323201, 325039, 326721, 327721, 328696, 329646, 330571, 331471, 332346, 333196, 334021, 334821, 335596, 336346, 337071, 337771, 338446, 339096, 339721, 340321, 340896, 341446, 341971, 342471, 342946, 343396, 343821, 344221, 344596, 344946, 345271, 345571, 345846, 346096, 346321, 346521, 346696, 346846, 346971, 347071, 347146, 347196, 347221, 347246, 347271, 347296, 347321, 347346, 347371, 347396, 347421, 347446, 347471, 347496, 347521, 347546, 347571, 347596, 347621, 347646, 347671, 347696, 347721, 347746, 347771, 347796, 347821, 347846, 347871, 347896, 347921, 347946, 347971, 347996, 348021, 348046, 348071, 348096, 348121, 348146, 348171, 348196, 348221, 348246, 348271, 348296, 348321, 348346, 348371, 348396, 348421, 348446, 348471, 348496, 348521, 348546, 348571, 348596, 348621, 348646, 348671, 348696, 348721, 348746, 348771, 348796, 348821, 348846, 348871, 348896, 348921, 348946, 348971, 348996, 349021, 349046, 349071, 349096, 349121, 349146, 349171, 349196, 349221, 349246, 349271, 349296, 349321, 349346, 349371, 349396, 349421, 349446, 349471, 349496, 349521, 349546, 349571, 349596, 349621, 349646, 349671, 349696, 349721, 349746, 349771, 349796, 349821, 349846, 349871, 349896, 349921, 349946, 349971, 349996, 350021, 350046, 350071, 350096, 350121, 350146, 350171, 350196, 350221, 350246, 350271, 350296, 350321, 350346, 350371, 350396, 350421, 350446, 350471, 350496, 350521, 350546, 350571, 350596, 350621, 350646, 350671, 350696, 350721, 350746, 350771, 350796, 350821, 350846, 350871, 350896, 350921, 350946, 350971, 350996, 351021, 351046, 351071, 351096, 351121, 351146, 351171, 351196, 351221, 351246, 351271, 351296, 351321, 351346, 351371, 351396, 351421, 351446, 351471, 351496, 351521, 351546, 351571, 351596, 351621, 351646, 351671, 351696, 351721, 351746, 351771, 351796, 351821, 351846, 351871, 351896, 351921, 351946, 351971, 351996, 352021, 352046, 352071, 352096, 352121, 352146, 352171, 352196, 352221, 352246, 352271 };
			for (var ctr = 0; ctr < times.Count - 1; ++ctr)
			{
				foreach (var light in lights[order[ctr % 25]])
					segment.AddLight(light, times[ctr], color, order[ctr % 25]);
				foreach (var light in lights[order[ctr % 25]])
					segment.AddLight(light, times[ctr + 1], times[ctr + 1] + Delay, null, Segment.Absolute, 0x000000);
			}

			return segment;
		}

		public Song Render()
		{
			var song = new Song("bohemian.wav");

			// Spiral (0)
			var spiral = Spiral();
			song.AddSegmentWithRepeat(spiral, 0, 6900, 0, 15081);

			// RunAway (15081)
			var runAway = RunAway();
			song.AddSegmentWithRepeat(runAway, 0, runAway.MaxTime(), 15081, 10391);

			// Snake (25472)
			var snake = Snake();
			song.AddSegmentWithRepeat(snake, 0, snake.MaxTime(), 25472, 28404);
			song.AddSegmentWithRepeat(snake, snake.MaxTime(), snake.MaxTime(), song.MaxTime(), 2000);
			//song.AddSegmentWithRepeat(runAway, 0, runAway.MaxTime(), 25472, 30404);

			// Tag (55876)
			var tags = Tags();
			var tagTotalTime = tags.Sum(seg => seg.MaxTime());
			foreach (var tag in tags)
				song.AddSegmentWithRepeat(tag, 0, tag.MaxTime(), song.MaxTime(), tag.MaxTime() * 86450 / tagTotalTime);

			// Lines (142323)
			var lines = Lines(out var linesTime);
			song.AddSegmentWithRepeat(lines, 0, linesTime, 142323, 6762, 4);
			song.AddPaletteSequence(142323, 0);
			song.AddPaletteSequence(148585, 149585, null, 1);
			song.AddPaletteSequence(155347, 156347, null, 2);
			song.AddPaletteSequence(162109, 163109, null, 3);
			song.AddPaletteSequence(169371, 0);

			// Sine (169371)
			var sine = Sine();
			song.AddSegmentWithRepeat(sine, 0, 1000, 169371, 3380, 4);
			song.AddPaletteSequence(169371, 0);
			song.AddPaletteSequence(170802, 171802, null, 1);
			song.AddPaletteSequence(172734, 173734, null, 2);
			song.AddPaletteSequence(174665, 175665, null, 0);
			song.AddPaletteSequence(176597, 177597, null, 1);
			song.AddPaletteSequence(178528, 179528, null, 2);
			song.AddPaletteSequence(180460, 181460, null, 0);
			song.AddPaletteSequence(182891, 0);

			// Face (182891)
			var face = Face();
			song.AddSegmentWithRepeat(face, 0, 971, 182891, 9920);
			song.AddPaletteSequence(182891, 0);
			song.AddPaletteSequence(185698, 186698, null, 1);
			song.AddPaletteSequence(189004, 190004, null, 2);
			song.AddPaletteSequence(192811, 0);

			// Clouds (192811)
			var clouds = Clouds();
			song.AddSegmentWithRepeat(clouds, 0, 97, 192811, 1680, 6);

			// Flood (202891)
			var flood = Flood();
			song.AddSegmentWithRepeat(flood, 0, 4400, 202891, 4400 / 200 * 1600);

			// Flashy (238104)
			var flashy = Flashy();
			song.AddSegmentWithRepeat(flashy, 0, 10000, 238104);

			// Clock (248104)
			var clock = Clock();
			song.AddSegmentWithRepeat(clock, 0, 360, 248104, 973, 17);
			song.AddPaletteSequence(248104, 0);
			song.AddPaletteSequence(250361, 251361, null, 1);
			song.AddPaletteSequence(253118, 254118, null, 2);
			song.AddPaletteSequence(255875, 256875, null, 3);
			song.AddPaletteSequence(258631, 259631, null, 4);
			song.AddPaletteSequence(261388, 262388, null, 5);
			song.AddPaletteSequence(264645, 0);

			// Runner (264645)
			var runner = Runner();
			song.AddSegmentWithRepeat(runner, 0, 76, 264645, 6924, 4);
			song.RepeatSegmentByVelocity(runner, 0, 76, 292341, 2947, 76, 0, 6924);
			song.AddPaletteSequence(264645, 0);
			song.AddPaletteSequence(271069, 272069, null, 1);
			song.AddPaletteSequence(277993, 278993, null, 2);
			song.AddPaletteSequence(284917, 285917, null, 3);
			song.AddPaletteSequence(295288, 0);

			// Squares (295288)
			var squares = Squares();
			song.AddSegmentWithRepeat(squares, 0, squares.MaxTime(), 295288, 16660);

			// SquareFlash (311948) 355000
			var squareFlash = SquareFlash();
			song.AddSegmentWithRepeat(squareFlash, 311948, 355000, 311948);

			return song;
		}
	}
}
