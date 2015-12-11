using MassTransit;

namespace DDDHandsOn.Core.Dispatcher.ComponentModel
{
    public interface IServiceBusFactory
    {
        IServiceBus Create();
    }
}
