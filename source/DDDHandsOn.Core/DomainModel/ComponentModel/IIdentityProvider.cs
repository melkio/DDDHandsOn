using DDDHandsOn.Core.DomainModel;
using System;

namespace DDDHandsOn.Core.DomainModel.ComponentModel
{
    public interface IIdentityProvider
    {
        Int32 CreateNewOneFor<T>();
    }
}
