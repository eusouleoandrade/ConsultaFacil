using INTELECTAH.ConsultaFacil.Exception.Implementations;
using System;
using System.Collections.Generic;

namespace INTELECTAH.ConsultaFacil.Domain
{
    public class TipoExame
    {
        public int TipoExameId { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public List<Exame> Exames { get; set; }

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

            // Validate Descrição
            if (!String.IsNullOrEmpty(Descricao))
            {
                if (Descricao.Length > 256)
                    exceptions.Add("Descrição não pode ter mais que 256 caracteres");
            }
            else
            {
                exceptions.Add("Descrição é requerida");
            }

            if (exceptions.Count > Decimal.Zero)
                throw new DomainException(exceptions);
        }
    }
}
