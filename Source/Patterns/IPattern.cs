namespace Shelfinator.Patterns
{
	interface IPattern
	{
		int PatternNumber { get; }
		Lights Render();
	}
}
