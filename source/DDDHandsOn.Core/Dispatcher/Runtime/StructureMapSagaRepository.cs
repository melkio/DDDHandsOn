using MassTransit;
using MassTransit.Pipeline;
using MassTransit.Saga;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDHandsOn.Core.Dispatcher.Runtime
{
    public class StructureMapSagaRepository<TSaga> : ISagaRepository<TSaga>
        where TSaga : class, ISaga
    {
        private readonly IContainer _container;
        private readonly ISagaRepository<TSaga> _innerRepository;

        public StructureMapSagaRepository(IContainer container, ISagaRepository<TSaga> innerRepository)
        {
            _container = container;
            _innerRepository = innerRepository;
        }

        public IEnumerable<Action<IConsumeContext<TMessage>>> GetSaga<TMessage>(IConsumeContext<TMessage> context, Guid sagaId, InstanceHandlerSelector<TSaga, TMessage> selector, ISagaPolicy<TSaga, TMessage> policy)
            where TMessage : class
        {
            return _innerRepository.GetSaga(context, sagaId, (saga, message) =>
            {
                _container.BuildUp(saga);

                return selector(saga, message);
            }, policy);
        }

        public IEnumerable<Guid> Find(ISagaFilter<TSaga> filter)
        {
            return _innerRepository.Find(filter);
        }

        public IEnumerable<TSaga> Where(ISagaFilter<TSaga> filter)
        {
            return _innerRepository.Where(filter).Select(x =>
            {
                _container.BuildUp(x);

                return x;
            });
        }

        public IEnumerable<TResult> Where<TResult>(ISagaFilter<TSaga> filter, Func<TSaga, TResult> transformer)
        {
            return _innerRepository.Where(filter, x =>
            {
                _container.BuildUp(x);

                return transformer(x);
            });
        }

        public IEnumerable<TResult> Select<TResult>(Func<TSaga, TResult> transformer)
        {
            return _innerRepository.Select(x =>
            {
                _container.BuildUp(x);

                return transformer(x);
            });
        }
    }
}
