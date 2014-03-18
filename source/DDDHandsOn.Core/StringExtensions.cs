using System;
using System.ComponentModel;

namespace DDDHandsOn.Core
{
    public static class StringExtensions
    {
        public static T GetEnumFromDescription<T>(this String description)
        {
            var type = typeof(T);
            if (!type.IsEnum) 
                throw new InvalidOperationException();
            
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
        }
    }
}
