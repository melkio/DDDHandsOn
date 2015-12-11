namespace DDDHandsOn.Core.Persistence.ComponentModel
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork CreateUnitOfWork();
    }
}
