using System.Collections.Generic;
using System.Linq;

namespace LevelUpCSharp.Linq
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> Odd<T>(this IEnumerable<T> source)
		{
			return source.Where((item, index) => index % 2 == 0);
		}
	}
}