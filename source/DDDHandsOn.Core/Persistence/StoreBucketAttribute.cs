using DDDHandsOn.Core.DomainModel;
using System;

namespace DDDHandsOn.Core.Persistence
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
    public class StoreBucketAttribute : Attribute
    {
        public String Name { get; private set; }

        public StoreBucketAttribute(String name)
        {
            Name = name;
        }
    }

    public static class StoreBucketExtensionMethods
    {
        public static String ComposeFor<TAggregate>(this StoreBucketAttribute attribute) where TAggregate : IAggregateRoot
        {
            return attribute.ComposeFor(typeof(TAggregate));
        }

        public static String ComposeFor(this StoreBucketAttribute attribute, Type type) 
        {
            return String.Concat(attribute.Name, "/", type.Name);
        }
    }
}
