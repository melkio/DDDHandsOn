using System;

namespace DDDHandsOn.Core.Security.ComponentModel
{
    public interface IOperationContext
    {
        Identity GetCurrentUser();
    }
}
