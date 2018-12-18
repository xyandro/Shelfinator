using System.Linq;
using System;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Flex : ISong
	{
		public int SongNumber => 33;

		public Song Render()
		{
			const double Brightness = 1f / 16;

			var layout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");
			var segment = new Segment();
			var color = new LightColor(0, 5, Helpers.Rainbow6.Multiply(Brightness).ToList());

			for (var pass = 0; pass < 8; ++pass)
			{
				for (var time = 0; time < 96; ++time)
				{
					layout.GetAllLights().ForEach(light => segment.AddLight(light, time + pass * 142, Segment.Absolute, 0x000000));
					for (var xPosition = 0; xPosition < 6; ++xPosition)
						for (var yPosition = 0; yPosition < 6; ++yPosition)
						{
							int x, y;
							switch (pass)
							{
								case 0: x = time * xPosition / 5; y = 0; break;
								case 1: x = 19 * xPosition; y = time * yPosition / 5; break;
								case 2: x = (95 - time) * xPosition / 5 + time; y = 19 * yPosition; break;
								case 3: x = 95; y = (95 - time) * yPosition / 5 + time; break;
								case 4: x = 95 - time * (5 - xPosition) / 5; y = 95; break;
								case 5: x = 19 * xPosition; y = 95 - time * (5 - yPosition) / 5; break;
								case 6: x = (95 - time) * xPosition / 5; y = 19 * yPosition; break;
								case 7: x = 0; y = (95 - time) * yPosition / 5; break;
								default: throw new Exception("Invalid");
							}
							foreach (var light in layout.GetPositionLights(x, y, 2, 2))
								segment.AddLight(light, time + pass * 142, color, (xPosition + yPosition) % 6);
						}
				}
			}

			var song = new Song();
			song.AddSegment(segment, 0, 1136, 16000);

			return song;
		}
	}
}
