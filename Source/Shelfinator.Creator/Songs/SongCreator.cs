using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	abstract class SongCreator
	{
		public abstract int SongNumber { get; }
		public virtual bool Test => false;
		public abstract Song Render();
	}
}
