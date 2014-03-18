using DDDHandsOn.Core.Persistence;
using DDDHandsOn.Core.Persistence.ComponentModel;
using DDDHandsOn.Core.Security.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Linq;
using System.Threading;

namespace DDDHandsOn.Core.Security.Runtime
{
    class AgathaOperationContext : IOperationContext
    {
        private readonly IMongoStorage _storage;

        public AgathaOperationContext(IMongoStorage storage)
        {
            _storage = storage;
        }

        public Identity GetCurrentUser()
        {
            var currentUserName = Thread.CurrentPrincipal.Identity.Name;
            var collection = _storage.GetCollectionFor<MongoIdentity>();
            var identity = collection.FindOneById(new BsonString(currentUserName));

            return identity ?? Identity.Guest;
        }

        [CollectionName("identities")]
        private class MongoIdentity : Identity
        {
            [BsonId]
            private String _name;
            [BsonElement("roles")]
            private String[] _roles;
            [BsonElement("denied_ops")]
            private String[] _deniedOperations;

            [BsonIgnore]
            public override String Name 
            {
                get { return _name; }
            }

            [BsonIgnore]
            public override String[] Roles
            {
                get { return _roles ?? new String[0]; }
            }

            public override Boolean CanExecute<TRequest>()
            {
                var operationType = typeof(TRequest).FullName;
                return _deniedOperations == null || !_deniedOperations.Contains(operationType);
            }
        }
    }
}
