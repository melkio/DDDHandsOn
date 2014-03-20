using DDDHandsOn.Core.Command;
using System;

namespace DDDHandsOn.Web.Commands
{
    public class EchoRequest : BaseRequest
    {
        public String Value { get; set; }
    }

    public class EchoResponse : BaseResponse
    {
        public String Value { get; set; }
    }
}