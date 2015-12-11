using System;

namespace DDDHandsOn.Core
{
    public static class ObjectExtensions
    {
        public static T As<T>(this Object source)
            where T : class
        {
            var target = source as T;
            if (target == null)
            {
                var message = String.Format("Isn't possible to cast {0} type to {1} type", source.GetType().FullName, typeof(T).FullName);
                throw new InvalidCastException(message);
            }

            return target;
        }
    }
}
