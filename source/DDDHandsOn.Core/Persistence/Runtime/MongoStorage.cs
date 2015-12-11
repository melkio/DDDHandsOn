using DDDHandsOn.Core.Persistence.ComponentModel;
using MongoDB.Driver;
using System.Configuration;

namespace DDDHandsOn.Core.Persistence.Runtime
{
    class MongoStorage : IMongoStorage
    {
        private readonly MongoDatabase _database;

        public MongoStorage()
        {
            _database = SetUpDatabase();
        }

        public MongoDatabase GetDatabase()
        {
            return _database;
        }

        public MongoCollection<T> GetCollectionFor<T>()
        {
            CollectionNameAttribute attribute = typeof(T).GetAttribute<CollectionNameAttribute>();
            return _database.GetCollection<T>(attribute.CollectionName);
        }

        private static MongoDatabase SetUpDatabase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["storage"].ConnectionString;
            var builder = new MongoUrlBuilder(connectionString);
            var url = builder.ToMongoUrl();
            
            var client = new MongoClient(url);
            var server = client.GetServer();
            return server.GetDatabase(url.DatabaseName);
        }
    }
}
