using DDDHandsOn.Core.Command;
using System;

namespace DDDHandsOn.Web.Commands
{
    public class ApriReclamoRequest : BaseRequest
    {
        public String Testo { get; set; }
        public String Categoria { get; set; }
        public String SistemaOperativo { get; set; }
    }

    public class ApriReclamoResponse : BaseResponse
    {
        public String IdReclamo { get; set; }
    }
}