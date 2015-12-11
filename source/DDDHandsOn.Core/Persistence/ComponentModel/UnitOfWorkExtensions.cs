using DDDHandsOn.Core.Security.ComponentModel;
using System;

namespace DDDHandsOn.Core.Persistence.ComponentModel
{
    public static class UnitOfWorkExtensions
    {
        public static void Commit(this IUnitOfWork unitOfWork, IOperationContext context)
        {
            unitOfWork.Commit(Guid.Empty, context);
        }
    }
}
