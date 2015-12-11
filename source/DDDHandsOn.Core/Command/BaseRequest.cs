using Agatha.Common;
using System;

namespace DDDHandsOn.Core.Command
{
    public abstract class BaseRequest : Request
    {
        public Guid CorrelationId { get; set; }

        protected BaseRequest()
        {
            CorrelationId = Guid.NewGuid();
        }
    }
}
