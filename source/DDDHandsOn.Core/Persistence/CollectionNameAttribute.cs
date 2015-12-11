using System;

namespace DDDHandsOn.Core.Persistence
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CollectionNameAttribute : Attribute
    {
        public String CollectionName { get; private set; }

        public CollectionNameAttribute(String collectionName)
        {
            CollectionName = collectionName;
        }
    }
}
