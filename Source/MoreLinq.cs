using System.Collections.Generic;

namespace Shelfinator
{
	static class MoreLinq
	{
		static public IEnumerable<T> NonNull<T>(this IEnumerable<T?> items) where T : struct
		{
			foreach (var item in items)
				if (item.HasValue)
					yield return item.Value;
		}

		static public IEnumerable<T> Concat<T>(this IEnumerable<T> items, T addItem)
		{
			foreach (var item in items)
				yield return item;
			yield return addItem;
		}
	}
}
