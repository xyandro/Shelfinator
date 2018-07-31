namespace Shelfinator.Creator.Patterns
{
	interface IPattern
	{
		int PatternNumber { get; }
		Pattern Render();
	}
}
