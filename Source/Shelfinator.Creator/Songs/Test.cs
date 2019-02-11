using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Test : ISong
	{
		public int SongNumber => 0;

		readonly Layout headerLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Header.png");

		public Song Render()
		{
			var song = new Song("test.ogg");
			song.AddPaletteChange(0, 0);

			var segment = new Segment();
			segment.AddLight(headerLayout.GetPositionLight(0,0), 0, 1, 0x101010, 0x000000);
			song.AddSegment(segment, 0, 1, 1000, 3000);

			return song;
		}
	}
}
