namespace Shelfinator.Patterns
{
	interface IPattern
	{
		int PatternNumber { get; }
		Pattern Render();
	}
}
