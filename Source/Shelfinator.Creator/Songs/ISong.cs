using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	interface ISong
	{
		int SongNumber { get; }
		Song Render();
	}
}
