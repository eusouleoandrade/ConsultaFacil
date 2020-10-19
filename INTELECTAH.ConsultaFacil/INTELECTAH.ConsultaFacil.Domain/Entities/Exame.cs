using INTELECTAH.ConsultaFacil.Domain.Entities;
using INTELECTAH.ConsultaFacil.Exception.Implementations;
using System;
using System.Collections.Generic;

namespace INTELECTAH.ConsultaFacil.Domain
{
    public class Exame
    {
        public int ExameId { get; set; }

        public string Nome { get; set; }

        public string Observacao { get; set; }

        public int TipoExameId { get; set; }

        public TipoExame TipoExame { get; set; }

        public List<Consulta> Consultas { get; set; }

        public void Validate()
        {
            IList<string> exceptions = new List<string>();

            // Validate Nome
            if (!String.IsNullOrEmpty(Nome))
            {
                if (Nome.Length > 100)
                    exceptions.Add("Nome não pode ter mais que 100 caracteres");
            }
            else
            {
                exceptions.Add("Nome é requerido");
            }

            // Validate Observação
            if (!String.IsNullOrEmpty(Observacao))
            {
                if (Nome.Length > 1000)
                    exceptions.Add("Observação não pode ter mais que 1000 caracteres");
            }
            else
            {
                exceptions.Add("Observação é requerida");
            }

            // Validate TipoExameId
            if (TipoExameId <= Decimal.Zero)
                exceptions.Add("Tipo Exame é requerido");

            if (exceptions.Count > Decimal.Zero)
                throw new DomainException(exceptions);
        }
    }
}
