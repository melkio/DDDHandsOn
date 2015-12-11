using DDDHandsOn.Core.DomainModel.ComponentModel;
using DDDHandsOn.Core.Persistence;
using DDDHandsOn.Core.Persistence.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Builders;
using System;

namespace DDDHandsOn.Core.DomainModel.Runtime
{
    class IdentityProvider : IIdentityProvider
    {
        private readonly IMongoStorage _storage;

        public IdentityProvider(IMongoStorage storage)
        {
            _storage = storage;
        }

        public Int32 CreateNewOneFor<T>()
        {
            var collection = _storage.GetCollectionFor<AggregateIdentity>();

            var type = typeof(T).FullName;
            var result = collection.FindAndModify(Query.EQ("_id", new BsonString(type)), null, Update.Inc("Value", 1), true, true);
            var value = result.ModifiedDocument["Value"].AsInt32;

            return value;
        }

        [CollectionName("settings")]
        private class AggregateIdentity
        {
            public String Id { get; set; }
            public Int32 Value { get; set; }
        }
    }
}
