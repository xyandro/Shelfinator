using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Test : SongCreator
	{
		public override int SongNumber => 0;

		readonly Layout headerLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Header.png");

		public override Song Render()
		{
			var song = new Song("test.ogg");

			song.AddMeasure(1000); // Empty lead-in time
			song.AddMeasure(2000); // Measure

			song.AddPaletteChange(0, 0);

			var segment = new Segment();
			segment.AddLight(headerLayout.GetPositionLight(0,0), 0, 1, 0x101010, 0x000000);
			song.AddSegment(segment, 0, 1, 1, 1);

			return song;
		}
	}
}
