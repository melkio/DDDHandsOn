using Agatha.Common;
using System;

namespace DDDHandsOn.Core.Command
{
    public class BaseResponse : Response
    {
        public Guid CorrelationId { get; set; }
    }
}
