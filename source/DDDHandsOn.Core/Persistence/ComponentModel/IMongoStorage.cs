using System;
using DDDHandsOn.Core.Security.ComponentModel;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace DDDHandsOn.Core.Persistence.ComponentModel
{
    public interface IMongoStorage
    {
        MongoDatabase GetDatabase();
        MongoCollection<T> GetCollectionFor<T>();
    }
}
