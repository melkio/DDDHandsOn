using DDDHandsOn.Core.DomainModel;
using System;

namespace DDDHandsOn.Web.Domain
{
    public class Reclamo : AggregateRootMappedByConventions
    {
        private String _stato;

        public void Apri(string testo, string categoria, string sistemaOperativo)
        {
            // VALIDAZIONE
            if (String.IsNullOrEmpty(testo))
                throw new ArgumentNullException("testo");

            RaiseEvent<ReclamoAperto>(e =>
            {
                e.Testo = testo;
                e.SistemaOperativo = sistemaOperativo;
                e.Categoria = categoria;
            });
        }

        void Apply(ReclamoAperto domainEvent)
        {
            _stato = "aperto";
        }
    }

    public class ReclamoAperto : DomainEvent
    {
        public String Testo { get; set; }
        public String Categoria { get; set; }
        public String SistemaOperativo { get; set; }
    }
}