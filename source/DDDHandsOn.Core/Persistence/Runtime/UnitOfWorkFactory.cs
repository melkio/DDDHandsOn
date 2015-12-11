using DDDHandsOn.Core.Persistence.ComponentModel;
using System;

namespace DDDHandsOn.Core.Persistence.Runtime
{
    class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWorkFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            return (IUnitOfWork)_serviceProvider.GetService(typeof(IUnitOfWork));    
        }
    }
}
