﻿//using System.Collections.Generic;
//using System.Linq;
//using Shelfinator.Creator.SongData;
//
//namespace Shelfinator.Creator.Songs
//{
//	class Face : ISong
//	{
//		public int SongNumber => 18;
//
//		List<List<int>> GetOrderedLights(List<int> lights) => lights.Select(light => new { light, order = (light & 0xff8) >> 3 }).Concat(lights.Select(light => new { light, order = (light & 0x3fe000) >> 13 })).GroupBy(o => o.order).Where(group => group.Key != 0).OrderBy(group => group.Key).Select(group => group.Select(o => o.light).ToList()).ToList();
//
//		List<List<List<int>>> GetFaceLights()
//		{
//			const string data =
//"21,0&21,1/22,0&22,1/23,0&23,1/24,0&24,1/25,0&25,1/26,0&26,1/27,0&27,1/28,0&28,1/29,0&29,1/30,0&30,1/31,0&31,1/32,0&32,1/33,0&33,1/34,0&34,1/35,0&35,1/36,0&36,1/37,0&37,1/38,0/38,1&39,0/39,1/38,2&39,2/38,3&39,3/38,4&39,4/38,5&39,5/38,6&39,6/38,7&39,7/38,8&39,8/38,9&39,9/38,10&39,10/38,11&39,11/38,12&39,12/38,13&39,13/38,14&39,14/38,15&39,15/38,16&39,16/38,17&39,17/38,18&39,18/39,19/38,19&39,20/38,20/37,19&37,20/36,19&36,20/35,19&35,20/34,19&34,20/33,19&33,20/32,19&32,20/31,19&31,20/30,19&30,20/29" +
//",19&29,20/28,19&28,20/27,19&27,20/26,19&26,20/25,19&25,20/24,19&24,20/23,19&23,20/22,19&22,20/21,19&21,20/20,20/19,20&20,19/19,19/19,18&20,18/19,17&20,17/19,16&20,16/19,15&20,15/19,14&20,14/19,13&20,13/19,12&20,12/19,11&20,11/19,10&20,10/19,9&20,9/19,8&20,8/19,7&20,7/19,6&20,6/19,5&20,5/19,4&20,4/19,3&20,3/19,2&20,2/19,1/19,0&20,1/20,0P59,0&59,1/60,0&60,1/61,0&61,1/62,0&62,1/63,0&63,1/64,0&64,1/65,0&65,1/66,0&66,1/67,0&67,1/68,0&68,1/69,0&69,1/70,0&70,1/71,0&71,1/72,0&72,1/73,0&73,1/74,0&74,1/75" +
//",0&75,1/76,0/76,1&77,0/77,1/76,2&77,2/76,3&77,3/76,4&77,4/76,5&77,5/76,6&77,6/76,7&77,7/76,8&77,8/76,9&77,9/76,10&77,10/76,11&77,11/76,12&77,12/76,13&77,13/76,14&77,14/76,15&77,15/76,16&77,16/76,17&77,17/76,18&77,18/77,19/76,19&77,20/76,20/75,19&75,20/74,19&74,20/73,19&73,20/72,19&72,20/71,19&71,20/70,19&70,20/69,19&69,20/68,19&68,20/67,19&67,20/66,19&66,20/65,19&65,20/64,19&64,20/63,19&63,20/62,19&62,20/61,19&61,20/60,19&60,20/59,19&59,20/58,20/57,20&58,19/57,19/57,18&58,18/57,17&58,17/57,16&58" +
//",16/57,15&58,15/57,14&58,14/57,13&58,13/57,12&58,12/57,11&58,11/57,10&58,10/57,9&58,9/57,8&58,8/57,7&58,7/57,6&58,6/57,5&58,5/57,4&58,4/57,3&58,3/57,2&58,2/57,1/57,0&58,1/58,0P48,57&48,58/49,57&49,58/50,57&50,58/51,57&51,58/52,57&52,58/53,57&53,58/54,57&54,58/55,57&55,58/56,57&56,58/57,57&57,58/58,57&58,58/59,57&59,58/60,57&60,58/61,57&61,58/62,57&62,58/63,57&63,58/64,57&64,58/65,57&65,58/66,57&66,58/67,57&67,58/68,57&68,58/69,57&69,58/70,57&70,58/71,57&71,58/72,57&72,58/73,57&73,58/74,57&74,58/" +
//"75,57&75,58/76,57&76,58/77,57&77,58/78,57&78,58/79,57&79,58/80,57&80,58/81,57&81,58/82,57&82,58/83,57&83,58/84,57&84,58/85,57&85,58/86,57&86,58/87,57&87,58/88,57&88,58/89,57&89,58/90,57&90,58/91,57&91,58/92,57&92,58/93,57&93,58/94,57&94,58/95,58/95,57&96,58/96,57/95,56&96,56/95,55&96,55/95,54&96,54/95,53&96,53/95,52&96,52/95,51&96,51/95,50&96,50/95,49&96,49/95,48&96,48/95,47&96,47/95,46&96,46/95,45&96,45/95,44&96,44/95,43&96,43/95,42&96,42/95,41&96,41/95,40&96,40/96,39/95,39&96,38/95,38/94,38&94" +
//",39/93,38&93,39/92,38&92,39/91,38&91,39/90,38&90,39/89,38&89,39/88,38&88,39/87,38&87,39/86,38&86,39/85,38&85,39/84,38&84,39/83,38&83,39/82,38&82,39/81,38&81,39/80,38&80,39/79,38&79,39/78,38&78,39/77,38/76,38&77,39/76,39/76,40&77,40/76,41&77,41/76,42&77,42/76,43&77,43/76,44&77,44/76,45&77,45/76,46&77,46/76,47&77,47/76,48&77,48/76,49&77,49/76,50&77,50/76,51&77,51/76,52&77,52/76,53&77,53/76,54&77,54/76,55&77,55/76,56&77,56/76,57&77,57/76,58&77,58/76,59&77,59/76,60&77,60/76,61&77,61/76,62&77,62/76,6" +
//"3&77,63/76,64&77,64/76,65&77,65/76,66&77,66/76,67&77,67/76,68&77,68/76,69&77,69/76,70&77,70/76,71&77,71/76,72&77,72/76,73&77,73/76,74&77,74/76,75&77,75/77,76/76,76&77,77/76,77/75,76&75,77/74,76&74,77/73,76&73,77/72,76&72,77/71,76&71,77/70,76&70,77/69,76&69,77/68,76&68,77/67,76&67,77/66,76&66,77/65,76&65,77/64,76&64,77/63,76&63,77/62,76&62,77/61,76&61,77/60,76&60,77/59,76&59,77/58,76&58,77/57,76&57,77/56,76&56,77/55,76&55,77/54,76&54,77/53,76&53,77/52,76&52,77/51,76&51,77/50,76&50,77/49,76&49,77/" +
//"48,76&48,77/47,76&47,77/46,76&46,77/45,76&45,77/44,76&44,77/43,76&43,77/42,76&42,77/41,76&41,77/40,76&40,77/39,76&39,77/38,76&38,77/37,76&37,77/36,76&36,77/35,76&35,77/34,76&34,77/33,76&33,77/32,76&32,77/31,76&31,77/30,76&30,77/29,76&29,77/28,76&28,77/27,76&27,77/26,76&26,77/25,76&25,77/24,76&24,77/23,76&23,77/22,76&22,77/21,76&21,77/20,77/19,77&20,76/19,76/19,75&20,75/19,74&20,74/19,73&20,73/19,72&20,72/19,71&20,71/19,70&20,70/19,69&20,69/19,68&20,68/19,67&20,67/19,66&20,66/19,65&20,65/19,64&20" +
//",64/19,63&20,63/19,62&20,62/19,61&20,61/19,60&20,60/19,59&20,59/19,58&20,58/19,57&20,57/19,56&20,56/19,55&20,55/19,54&20,54/19,53&20,53/19,52&20,52/19,51&20,51/19,50&20,50/19,49&20,49/19,48&20,48/19,47&20,47/19,46&20,46/19,45&20,45/19,44&20,44/19,43&20,43/19,42&20,42/19,41&20,41/19,40&20,40/20,39/19,39&20,38/19,38/18,38&18,39/17,38&17,39/16,38&16,39/15,38&15,39/14,38&14,39/13,38&13,39/12,38&12,39/11,38&11,39/10,38&10,39/9,38&9,39/8,38&8,39/7,38&7,39/6,38&6,39/5,38&5,39/4,38&4,39/3,38&3,39/2,38&2" +
//",39/1,38/0,38&1,39/0,39/0,40&1,40/0,41&1,41/0,42&1,42/0,43&1,43/0,44&1,44/0,45&1,45/0,46&1,46/0,47&1,47/0,48&1,48/0,49&1,49/0,50&1,50/0,51&1,51/0,52&1,52/0,53&1,53/0,54&1,54/0,55&1,55/0,56&1,56/0,57/0,58&1,57/1,58/2,57&2,58/3,57&3,58/4,57&4,58/5,57&5,58/6,57&6,58/7,57&7,58/8,57&8,58/9,57&9,58/10,57&10,58/11,57&11,58/12,57&12,58/13,57&13,58/14,57&14,58/15,57&15,58/16,57&16,58/17,57&17,58/18,57&18,58/19,57&19,58/20,57&20,58/21,57&21,58/22,57&22,58/23,57&23,58/24,57&24,58/25,57&25,58/26,57&26,58/27" +
//",57&27,58/28,57&28,58/29,57&29,58/30,57&30,58/31,57&31,58/32,57&32,58/33,57&33,58/34,57&34,58/35,57&35,58/36,57&36,58/37,57&37,58/38,57&38,58/39,57&39,58/40,57&40,58/41,57&41,58/42,57&42,58/43,57&43,58/44,57&44,58/45,57&45,58/46,57&46,58/47,57&47,58";
//
//			var layout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");
//			return data.Split('P').Select(part => part.Split('/').Select(l => l.Split('&').Select(p => p.Split(',').Select(int.Parse).ToList()).Select(p => layout.GetPositionLight(p[0], p[1])).ToList()).ToList()).ToList();
//		}
//
//		LightColor GetColor(double Brightness)
//		{
//			var rainbow = Helpers.Rainbow6.Multiply(Brightness).ToList();
//			var reds = new List<int> { 0xff6969, 0xd34949, 0xda3232, 0xb80000, 0x620000 }.Multiply(Brightness).ToList();
//			var blues = new List<int> { 0x32d0d3, 0x0000ff, 0x66ffff }.Multiply(Brightness).ToList();
//			var color = new LightColor(0, 100, new List<List<int>> { reds, blues, rainbow });
//			return color;
//		}
//
//		public Song Render()
//		{
//			const double Brightness = 1f / 16;
//			const int Length = 60;
//			const int PassiveColor = 0x040404;
//
//			var segment = new Segment();
//
//			var faceLights = GetFaceLights();
//			var color = GetColor(Brightness);
//
//			foreach (var facePart in faceLights)
//				foreach (var lightListOrder in facePart)
//					foreach (var light in lightListOrder)
//						segment.AddLight(light, 0, Segment.Absolute, PassiveColor);
//
//			for (var time = 0; time < 3120; ++time)
//			{
//				foreach (var facePart in faceLights)
//				{
//					var onOffset = time % facePart.Count;
//					foreach (var light in facePart[onOffset])
//						segment.AddLight(light, time, time + Length, color, 0, color, 100, true);
//					var offOffset = (time - Length) % facePart.Count;
//					if (offOffset >= 0)
//						foreach (var light in facePart[offOffset])
//							segment.AddLight(light, time, Segment.Absolute, PassiveColor);
//				}
//			}
//
//			var song = new Song();
//			song.AddSegment(segment, 0, 3120, 30000);
//
//			song.AddPaletteSequence(10000, 11000, null, 1);
//			song.AddPaletteSequence(20000, 21000, null, 2);
//
//			return song;
//		}
//	}
//}
