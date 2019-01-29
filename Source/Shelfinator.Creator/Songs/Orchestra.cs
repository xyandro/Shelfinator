using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Orchestra : ISong
	{
		public int SongNumber => 7;

		readonly Layout bodyLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");

		Segment Beats()
		{
			var segment = new Segment();
			foreach (var light in bodyLayout.GetAllLights())
				segment.AddLight(light, 0, 1, 0x101010, 0x000000);
			return segment;
		}

		public Song Render()
		{
			var song = new Song("orchestra.mp3"); // First sound is at 500; Measures start at 1720, repeat every 1890, and stop at 177490

			// Beats (0)
			var beats = Beats();
			song.AddSegment(beats, 0, 1, 1720, 1890, 93);

			return song;
		}
	}
}
