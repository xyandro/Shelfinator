using Shelfinator.Creator.PatternData;

namespace Shelfinator.Creator.Patterns
{
	interface IPattern
	{
		int PatternNumber { get; }
		Pattern Render();
	}
}
