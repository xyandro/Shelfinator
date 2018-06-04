using System;
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

		static public IEnumerable<T> Concat<T>(this IEnumerable<T> items, params T[] addItems)
		{
			foreach (var item in items)
				yield return item;
			foreach (var item in addItems)
				yield return item;
		}

		static public Dictionary<T1, T2> ToDictionary<T1, T2>(this IEnumerable<T1> items1, IEnumerable<T2> items2)
		{
			var result = new Dictionary<T1, T2>();
			using (var enum1 = items1.GetEnumerator())
			using (var enum2 = items2.GetEnumerator())
				while (true)
				{
					var move1 = enum1.MoveNext();
					if (move1 != enum2.MoveNext())
						throw new Exception("Enumerables must have the same number of elements");
					if (!move1)
						break;
					result[enum1.Current] = enum2.Current;
				}
			return result;
		}

		static public IEnumerable<int> Indexes<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
		{
			var index = 0;
			foreach (var item in source)
			{
				if (predicate(item))
					yield return index;
				++index;
			}
		}
	}
}
