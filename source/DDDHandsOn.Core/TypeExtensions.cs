using System;
using System.Linq;

namespace DDDHandsOn.Core
{
    public static class TypeExtensions
    {
        public static Boolean HasAttribute<TAttribute>(this Type type) where TAttribute : Attribute
        {
            return type.HasAttribute<TAttribute>(false);
        }

        public static Boolean HasAttribute<TAttribute>(this Type type, Boolean inherit) where TAttribute : Attribute
        {
            return type
                .GetCustomAttributes(inherit)
                .OfType<TAttribute>()
                .Any();
        }

        public static TAttribute GetAttribute<TAttribute>(this Type type) where TAttribute : Attribute
        {
            return type.GetAttribute<TAttribute>(false);
        }

        public static TAttribute GetAttribute<TAttribute>(this Type type, Boolean inherit) where TAttribute : Attribute
        {
            return type
                .GetCustomAttributes(inherit)
                .OfType<TAttribute>()
                .Single();
        }
    }
}
