using System;

namespace DDDHandsOn.Core.DomainModel.ComponentModel
{
    public interface IHaveSnapshot
    {
        IAggregateSnapshot GetSnapshot();
    }

    public interface IHaveSnapshotOfType<TSnapshot> : IHaveSnapshot 
        where TSnapshot : IAggregateSnapshot
    {
        TSnapshot GetSnapshot();
    }
}
