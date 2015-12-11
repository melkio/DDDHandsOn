using DDDHandsOn.Core.Persistence;
using DDDHandsOn.Core.Persistence.ComponentModel;
using DDDHandsOn.Web.Domain;
using MassTransit;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Builders;
using System;

namespace DDDHandsOn.Web.Denormalizers
{
    public class EchoReadModel : Consumes<EchoExecuted>.All
    {
        private readonly IMongoStorage _storage;

        public EchoReadModel(IMongoStorage storage)
        {
            _storage = storage;
        }

        public void Consume(EchoExecuted message)
        {
            var collection = _storage.GetCollectionFor<Item>();
            var item = collection.FindOne(Query.EQ("_id", message.Value));
            if (item == null)
                item = new Item { Value = message.Value, Count = 0 };
            item.Count++;

            collection.Save(item);
        }

        [CollectionName("Echos")]
        public class Item
        {
            [BsonId]
            public String Value { get; set; }
            public Int32 Count { get; set; }
        }
    }
}