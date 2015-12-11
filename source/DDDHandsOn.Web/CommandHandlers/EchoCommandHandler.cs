using Agatha.Common;
using Agatha.ServiceLayer;
using DDDHandsOn.Core.Command;
using DDDHandsOn.Core.Persistence.ComponentModel;
using DDDHandsOn.Core.Security.ComponentModel;
using DDDHandsOn.Web.Commands;
using DDDHandsOn.Web.Domain;
using System;

namespace DDDHandsOn.Web.CommandHandlers
{
    class EchoCommandHandler : BaseRequestHandler<EchoRequest, EchoResponse>
    {
        public EchoCommandHandler(IOperationContext context, IUnitOfWorkFactory factory)
            : base(context, factory)
        {

        }

        public override Response Handle(EchoRequest request)
        {
            var response = CreateTypedResponse();

            var echo = new Echo();
            echo.Execute(request.Value);

            Add(echo);

            response.Value = request.Value;

            return response;
        }
    }
}