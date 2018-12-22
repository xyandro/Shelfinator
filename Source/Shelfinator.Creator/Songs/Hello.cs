﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Hello : ISong
	{
		public int SongNumber => 1;

		Segment GetHello()
		{
			const double Brightness = 1f / 16;
			const string HeaderHelloLights = "2,1/2,2/2,3/2,4/2,5/3,1/3,2/3,3/3,4/3,5/4,3/5,3/6,1/6,2/6,3/6,4/6,5/7,1/7,2/7,3/7,4/7,5/9,1/9,2/9,3/9,4/9,5/10,1/10,2/10,3/10,4/10,5/11,1/11,3/11,5/12,1/12,3/12,5/14,1/14,2/14,3/14,4/14,5/15,1/15,2/15,3/15,4/15,5/16,5/17,5/19,1/19,2/19,3/19,4/19,5/20,1/20,2/20,3/20,4/20,5/21,5/22,5/24,2/24,3/24,4/25,1/25,2/25,3/25,4/25,5/26,1/26,5/27,1/27,5/28,1/28,2/28,3/28,4/28,5/29,2/29,3/29,4";
			const string BodyHelloLights =
"0,0/1,0/2,0/3,0/4,0/5,0/6,0/7,0/8,0/9,0/10,0/11,0/12,0/13,0/14,0/15,0/16,0/17,0/18,0/19,0/20,0/38,0/39,0/40,0/41,0/42,0/43,0/44,0/45,0/46,0/47,0/48,0/49,0/50,0/51,0/52,0/53,0/54,0/55,0/56,0/57,0/58,0/76,0/77,0/78,0/79,0/80,0/81,0/82,0/83,0/84,0/85,0/86,0/87,0/88,0/89,0/90,0/91,0/92,0/93,0/94,0/95,0/96,0/0,1/1,1/2,1/3,1/4,1/5,1/6,1/7,1/8,1/9,1/10,1/11,1/12,1/13,1/14,1/15,1/16,1/17,1/18,1/19,1/20,1/38,1/39,1/40,1/41,1/42,1/43,1/44,1/45,1/46,1/47,1/48,1/49,1/50,1/51,1/52,1/53,1/54,1/55,1/56,1/57,1/" +
"58,1/76,1/77,1/78,1/79,1/80,1/81,1/82,1/83,1/84,1/85,1/86,1/87,1/88,1/89,1/90,1/91,1/92,1/93,1/94,1/95,1/96,1/0,2/1,2/19,2/20,2/38,2/39,2/57,2/58,2/76,2/77,2/95,2/96,2/0,3/1,3/19,3/20,3/38,3/39,3/57,3/58,3/76,3/77,3/95,3/96,3/0,4/1,4/19,4/20,4/38,4/39,4/57,4/58,4/76,4/77,4/95,4/96,4/0,5/1,5/19,5/20,5/38,5/39,5/57,5/58,5/76,5/77,5/95,5/96,5/0,6/1,6/19,6/20,6/38,6/39,6/57,6/58,6/76,6/77,6/95,6/96,6/0,7/1,7/19,7/20,7/38,7/39,7/57,7/58,7/76,7/77,7/95,7/96,7/0,8/1,8/19,8/20,8/38,8/39,8/57,8/58,8/76,8" +
"/77,8/95,8/96,8/0,9/1,9/19,9/20,9/38,9/39,9/57,9/58,9/76,9/77,9/95,9/96,9/0,10/1,10/19,10/20,10/38,10/39,10/57,10/58,10/76,10/77,10/95,10/96,10/0,11/1,11/19,11/20,11/38,11/39,11/57,11/58,11/76,11/77,11/95,11/96,11/0,12/1,12/19,12/20,12/38,12/39,12/57,12/58,12/76,12/77,12/95,12/96,12/0,13/1,13/19,13/20,13/38,13/39,13/57,13/58,13/76,13/77,13/95,13/96,13/0,14/1,14/19,14/20,14/38,14/39,14/57,14/58,14/76,14/77,14/95,14/96,14/0,15/1,15/19,15/20,15/38,15/39,15/57,15/58,15/76,15/77,15/95,15/96,15/0,16/1" +
",16/19,16/20,16/38,16/39,16/57,16/58,16/76,16/77,16/95,16/96,16/0,17/1,17/19,17/20,17/38,17/39,17/57,17/58,17/76,17/77,17/95,17/96,17/0,18/1,18/19,18/20,18/38,18/39,18/57,18/58,18/76,18/77,18/95,18/96,18/0,19/1,19/19,19/20,19/38,19/39,19/57,19/58,19/76,19/77,19/95,19/96,19/0,20/1,20/19,20/20,20/38,20/39,20/57,20/58,20/76,20/77,20/95,20/96,20/0,21/1,21/19,21/20,21/38,21/39,21/57,21/58,21/76,21/77,21/95,21/96,21/0,22/1,22/19,22/20,22/38,22/39,22/57,22/58,22/76,22/77,22/95,22/96,22/0,23/1,23/19,23/" +
"20,23/38,23/39,23/57,23/58,23/76,23/77,23/95,23/96,23/0,24/1,24/19,24/20,24/38,24/39,24/57,24/58,24/76,24/77,24/95,24/96,24/0,25/1,25/19,25/20,25/38,25/39,25/57,25/58,25/76,25/77,25/95,25/96,25/0,26/1,26/19,26/20,26/38,26/39,26/57,26/58,26/76,26/77,26/95,26/96,26/0,27/1,27/19,27/20,27/38,27/39,27/57,27/58,27/76,27/77,27/95,27/96,27/0,28/1,28/19,28/20,28/38,28/39,28/57,28/58,28/76,28/77,28/95,28/96,28/0,29/1,29/19,29/20,29/38,29/39,29/57,29/58,29/76,29/77,29/95,29/96,29/0,30/1,30/19,30/20,30/38,3" +
"0/39,30/57,30/58,30/76,30/77,30/95,30/96,30/0,31/1,31/19,31/20,31/38,31/39,31/57,31/58,31/76,31/77,31/95,31/96,31/0,32/1,32/19,32/20,32/38,32/39,32/57,32/58,32/76,32/77,32/95,32/96,32/0,33/1,33/19,33/20,33/38,33/39,33/57,33/58,33/76,33/77,33/95,33/96,33/0,34/1,34/19,34/20,34/38,34/39,34/57,34/58,34/76,34/77,34/95,34/96,34/0,35/1,35/19,35/20,35/38,35/39,35/57,35/58,35/76,35/77,35/95,35/96,35/0,36/1,36/19,36/20,36/38,36/39,36/57,36/58,36/76,36/77,36/95,36/96,36/0,37/1,37/19,37/20,37/38,37/39,37/57" +
",37/58,37/76,37/77,37/95,37/96,37/0,38/1,38/19,38/20,38/21,38/22,38/23,38/24,38/25,38/26,38/27,38/28,38/29,38/30,38/31,38/32,38/33,38/34,38/35,38/36,38/37,38/38,38/39,38/57,38/58,38/76,38/77,38/95,38/96,38/0,39/1,39/19,39/20,39/21,39/22,39/23,39/24,39/25,39/26,39/27,39/28,39/29,39/30,39/31,39/32,39/33,39/34,39/35,39/36,39/37,39/38,39/39,39/57,39/58,39/76,39/77,39/95,39/96,39/0,40/1,40/57,40/58,40/76,40/77,40/95,40/96,40/0,41/1,41/57,41/58,41/76,41/77,41/95,41/96,41/0,42/1,42/57,42/58,42/76,42/77" +
",42/95,42/96,42/0,43/1,43/57,43/58,43/76,43/77,43/95,43/96,43/0,44/1,44/57,44/58,44/76,44/77,44/95,44/96,44/0,45/1,45/57,45/58,45/76,45/77,45/95,45/96,45/0,46/1,46/57,46/58,46/76,46/77,46/95,46/96,46/0,47/1,47/57,47/58,47/76,47/77,47/95,47/96,47/0,48/1,48/57,48/58,48/76,48/77,48/95,48/96,48/0,49/1,49/57,49/58,49/76,49/77,49/95,49/96,49/0,50/1,50/57,50/58,50/76,50/77,50/95,50/96,50/0,51/1,51/57,51/58,51/76,51/77,51/95,51/96,51/0,52/1,52/57,52/58,52/76,52/77,52/95,52/96,52/0,53/1,53/57,53/58,53/76" +
",53/77,53/95,53/96,53/0,54/1,54/57,54/58,54/76,54/77,54/95,54/96,54/0,55/1,55/57,55/58,55/76,55/77,55/95,55/96,55/0,56/1,56/57,56/58,56/76,56/77,56/95,56/96,56/0,57/1,57/19,57/20,57/21,57/22,57/23,57/24,57/25,57/26,57/27,57/28,57/29,57/30,57/31,57/32,57/33,57/34,57/35,57/36,57/37,57/38,57/39,57/57,57/58,57/76,57/77,57/95,57/96,57/0,58/1,58/19,58/20,58/21,58/22,58/23,58/24,58/25,58/26,58/27,58/28,58/29,58/30,58/31,58/32,58/33,58/34,58/35,58/36,58/37,58/38,58/39,58/57,58/58,58/76,58/77,58/95,58/96" +
",58/0,59/1,59/19,59/20,59/38,59/39,59/57,59/58,59/76,59/77,59/95,59/96,59/0,60/1,60/19,60/20,60/38,60/39,60/57,60/58,60/76,60/77,60/95,60/96,60/0,61/1,61/19,61/20,61/38,61/39,61/57,61/58,61/76,61/77,61/95,61/96,61/0,62/1,62/19,62/20,62/38,62/39,62/57,62/58,62/76,62/77,62/95,62/96,62/0,63/1,63/19,63/20,63/38,63/39,63/57,63/58,63/76,63/77,63/95,63/96,63/0,64/1,64/19,64/20,64/38,64/39,64/57,64/58,64/76,64/77,64/95,64/96,64/0,65/1,65/19,65/20,65/38,65/39,65/57,65/58,65/76,65/77,65/95,65/96,65/0,66/1" +
",66/19,66/20,66/38,66/39,66/57,66/58,66/76,66/77,66/95,66/96,66/0,67/1,67/19,67/20,67/38,67/39,67/57,67/58,67/76,67/77,67/95,67/96,67/0,68/1,68/19,68/20,68/38,68/39,68/57,68/58,68/76,68/77,68/95,68/96,68/0,69/1,69/19,69/20,69/38,69/39,69/57,69/58,69/76,69/77,69/95,69/96,69/0,70/1,70/19,70/20,70/38,70/39,70/57,70/58,70/76,70/77,70/95,70/96,70/0,71/1,71/19,71/20,71/38,71/39,71/57,71/58,71/76,71/77,71/95,71/96,71/0,72/1,72/19,72/20,72/38,72/39,72/57,72/58,72/76,72/77,72/95,72/96,72/0,73/1,73/19,73/" +
"20,73/38,73/39,73/57,73/58,73/76,73/77,73/95,73/96,73/0,74/1,74/19,74/20,74/38,74/39,74/57,74/58,74/76,74/77,74/95,74/96,74/0,75/1,75/19,75/20,75/38,75/39,75/57,75/58,75/76,75/77,75/95,75/96,75/0,76/1,76/19,76/20,76/38,76/39,76/57,76/58,76/76,76/77,76/95,76/96,76/0,77/1,77/19,77/20,77/38,77/39,77/57,77/58,77/76,77/77,77/95,77/96,77/0,78/1,78/19,78/20,78/38,78/39,78/57,78/58,78/76,78/77,78/95,78/96,78/0,79/1,79/19,79/20,79/38,79/39,79/57,79/58,79/76,79/77,79/95,79/96,79/0,80/1,80/19,80/20,80/38,8" +
"0/39,80/57,80/58,80/76,80/77,80/95,80/96,80/0,81/1,81/19,81/20,81/38,81/39,81/57,81/58,81/76,81/77,81/95,81/96,81/0,82/1,82/19,82/20,82/38,82/39,82/57,82/58,82/76,82/77,82/95,82/96,82/0,83/1,83/19,83/20,83/38,83/39,83/57,83/58,83/76,83/77,83/95,83/96,83/0,84/1,84/19,84/20,84/38,84/39,84/57,84/58,84/76,84/77,84/95,84/96,84/0,85/1,85/19,85/20,85/38,85/39,85/57,85/58,85/76,85/77,85/95,85/96,85/0,86/1,86/19,86/20,86/38,86/39,86/57,86/58,86/76,86/77,86/95,86/96,86/0,87/1,87/19,87/20,87/38,87/39,87/57" +
",87/58,87/76,87/77,87/95,87/96,87/0,88/1,88/19,88/20,88/38,88/39,88/57,88/58,88/76,88/77,88/95,88/96,88/0,89/1,89/19,89/20,89/38,89/39,89/57,89/58,89/76,89/77,89/95,89/96,89/0,90/1,90/19,90/20,90/38,90/39,90/57,90/58,90/76,90/77,90/95,90/96,90/0,91/1,91/19,91/20,91/38,91/39,91/57,91/58,91/76,91/77,91/95,91/96,91/0,92/1,92/19,92/20,92/38,92/39,92/57,92/58,92/76,92/77,92/95,92/96,92/0,93/1,93/19,93/20,93/38,93/39,93/57,93/58,93/76,93/77,93/95,93/96,93/0,94/1,94/19,94/20,94/38,94/39,94/57,94/58,94/" +
"76,94/77,94/95,94/96,94/0,95/1,95/2,95/3,95/4,95/5,95/6,95/7,95/8,95/9,95/10,95/11,95/12,95/13,95/14,95/15,95/16,95/17,95/18,95/19,95/20,95/38,95/39,95/40,95/41,95/42,95/43,95/44,95/45,95/46,95/47,95/48,95/49,95/50,95/51,95/52,95/53,95/54,95/55,95/56,95/57,95/58,95/76,95/77,95/78,95/79,95/80,95/81,95/82,95/83,95/84,95/85,95/86,95/87,95/88,95/89,95/90,95/91,95/92,95/93,95/94,95/95,95/96,95/0,96/1,96/2,96/3,96/4,96/5,96/6,96/7,96/8,96/9,96/10,96/11,96/12,96/13,96/14,96/15,96/16,96/17,96/18,96/19,9" +
"6/20,96/38,96/39,96/40,96/41,96/42,96/43,96/44,96/45,96/46,96/47,96/48,96/49,96/50,96/51,96/52,96/53,96/54,96/55,96/56,96/57,96/58,96/76,96/77,96/78,96/79,96/80,96/81,96/82,96/83,96/84,96/85,96/86,96/87,96/88,96/89,96/90,96/91,96/92,96/93,96/94,96/95,96/96,96";

			var headerLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Header.png");
			var headerLights = headerLayout.GetAllLights();
			var headerHelloLights = HeaderHelloLights.Split('/').Select(p => Point.Parse(p)).Select(p => headerLayout.GetPositionLight(p)).ToList();
			var headerLocations = headerLights.Select(light => headerLayout.GetLightPosition(light)).ToList();
			var headerOrdered = headerLocations.OrderBy(p => p.X).ThenBy(p => p.Y);
			var headerTopLeft = headerOrdered.First();
			var headerBottomRight = headerOrdered.Last();
			var headerCenter = new Point((headerTopLeft.X + headerBottomRight.X) / 2, (headerTopLeft.Y + headerBottomRight.Y) / 2);
			var headerDistances = headerLocations.Select(p => (p - headerCenter)).Select(p => new Vector(p.X / 32, p.Y / 8).Length).ToList();
			var headerDistanceInts = headerDistances.Select(l => Helpers.Scale(l, headerDistances.Min(), headerDistances.Max(), 0, 2000).Round()).ToList();

			var bodyLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");
			var bodyLights = bodyLayout.GetAllLights();
			var bodyHelloLights = new HashSet<int>(BodyHelloLights.Split('/').Select(p => Point.Parse(p)).Select(p => bodyLayout.GetPositionLight(p)));
			var bodyLocations = bodyLights.Select(light => bodyLayout.GetLightPosition(light)).ToList();
			var bodyOrdered = bodyLocations.OrderBy(p => p.X).ThenBy(p => p.Y);
			var bodyTopLeft = bodyOrdered.First();
			var bodyBottomRight = bodyOrdered.Last();
			var bodyCenter = new Point((bodyTopLeft.X + bodyBottomRight.X) / 2, (bodyTopLeft.Y + bodyBottomRight.Y) / 2);
			var bodyDistances = bodyLocations.Select(p => (p - bodyCenter).Length).ToList();
			var bodyDistanceInts = bodyDistances.Select(l => Helpers.Scale(l, bodyDistances.Min(), bodyDistances.Max(), 0, 2000).Round()).ToList();

			var segment = new Segment();
			var color = new LightColor(0, 1000, Helpers.Rainbow7.Multiply(Brightness).ToList(), new List<int> { 0xffffff }.Multiply(Brightness).ToList(), new List<int> { 0x000000 });
			for (var ctr = 0; ctr < headerLights.Count; ++ctr)
			{
				segment.AddLight(headerLights[ctr], headerDistanceInts[ctr], headerDistanceInts[ctr] + 400, null, color, headerLocations[ctr].X.Round() * 1000 / headerBottomRight.X.Round());
				if (!headerHelloLights.Contains(headerLights[ctr]))
					segment.AddLight(headerLights[ctr], headerDistanceInts[ctr] + 1000, headerDistanceInts[ctr] + 1400, null, Segment.Absolute, 0x000000);
			}

			for (var ctr = 0; ctr < bodyLights.Count; ++ctr)
			{
				segment.AddLight(bodyLights[ctr], bodyDistanceInts[ctr], bodyDistanceInts[ctr] + 400, null, color, bodyLocations[ctr].X.Round() * 1000 / bodyBottomRight.X.Round());
				if (!bodyHelloLights.Contains(bodyLights[ctr]))
					segment.AddLight(bodyLights[ctr], bodyDistanceInts[ctr] + 1000, bodyDistanceInts[ctr] + 1400, null, Segment.Absolute, 0x000000);
			}

			return segment;
		}

		public Song Render()
		{
			var song = new Song("hello.wav");
			var hello = GetHello();
			song.AddSegmentWithRepeat(hello, 0, 0, 0, 1500);
			song.AddSegmentWithRepeat(hello, 0, 8000, song.MaxTime());

			song.AddPaletteSequence(0, 0);
			song.AddPaletteSequence(5000, 6000, null, 1);
			song.AddPaletteSequence(7000, 8000, null, 2);
			return song;
		}
	}
}
