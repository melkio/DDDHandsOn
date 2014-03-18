using DDDHandsOn.Core.Dispatcher;
using DDDHandsOn.Core.DomainModel;
using DDDHandsOn.Core.Persistence.ComponentModel;
using DDDHandsOn.Core.Persistence.Runtime;
using MongoDB.Bson.Serialization;
using NEventStore;
using NEventStore.Dispatcher;
using NEventStore.Serialization;
using StructureMap.Configuration.DSL;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DDDHandsOn.Core.Persistence.Configuration
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            For<IAggregateRootBuilder>()
                .Singleton()
                .Use<DefaultAggregateRootBuilder>();

            For<IDispatchCommits>()
                .Singleton()
                .Use<MassTransitDispatchCommits>();

            For<IRepository>()
                .Transient()
                .Use<EventStoreRepository>();

            For<IUnitOfWork>()
                .Transient()
                .Use<UnitOfWork>();

            For<IUnitOfWorkFactory>()
                .Singleton()
                .Use<UnitOfWorkFactory>();
                

            For<IMongoStorage>()
                .Transient()
                .Use<MongoStorage>();

            For<IStoreEvents>()
                .Singleton()
                .Use(c =>
                        {
                            var bootstrapper = c.GetInstance<Bootstrapper>();

                            var types = Directory.GetFiles(bootstrapper.Directory)
                                .Where(f => Path.GetFileName(f).Contains("DDDHandsOn") && (f.EndsWith(".dll") || f.EndsWith(".exe")))
                                .Select(f => Assembly.LoadFrom(f))
                                .SelectMany(a => a.GetTypes())
                                .Where(t => t.IsSubclassOf(typeof(DomainEvent)))
                                .ToArray();
                            Array.ForEach(types, t => BsonClassMap.LookupClassMap(t));

                            var dispatcher = c.GetInstance<IDispatchCommits>();
                            var store = Wireup.Init()
                                            .UsingMongoPersistence("eventStore", new DocumentObjectSerializer())
                                            .UsingSynchronousDispatchScheduler(dispatcher)
                                            .Build();
                            return store;
                        });
        }
    }
}
