using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDHandsOn.Core
{
    public static class DictionaryExtensions
    {
        public static void CopyTo<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> destination)
        {
            Array.ForEach(source.ToArray(), p => destination[p.Key] = p.Value);
        }

        public static Boolean ContainsAllHeaders(this IDictionary<String, Object> headers)
        {
            var isInvalid = HeaderKeys.Mandatory.Any(x => !headers.ContainsKey(x));
            return !isInvalid;
        }
    }
}
