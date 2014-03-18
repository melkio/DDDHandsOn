using System;
using System.Collections.Generic;

namespace DDDHandsOn.Core
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var current in source)
                action(current);
        }
    }
}
