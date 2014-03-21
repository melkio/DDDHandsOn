using Agatha.Common;
using DDDHandsOn.Core.Command;
using DDDHandsOn.Core.Persistence.ComponentModel;
using DDDHandsOn.Core.Security.ComponentModel;
using DDDHandsOn.Web.Commands;
using DDDHandsOn.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDDHandsOn.Web.CommandHandlers
{
    public class ApriReclamoCommandHandler : BaseRequestHandler<ApriReclamoRequest, ApriReclamoResponse>
    {
        public ApriReclamoCommandHandler(IOperationContext context, IUnitOfWorkFactory factory)
            : base(context, factory)
        {

        }

        public override Response Handle(ApriReclamoRequest request)
        {
            var reclamo = new Reclamo();
            reclamo.Apri(request.Testo, request.SistemaOperativo, request.Categoria);
            Add(reclamo);

            var response = CreateTypedResponse();
            response.IdReclamo = reclamo.Id;
            return response;
        }
    }
}