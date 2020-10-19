using INTELECTAH.ConsultaFacil.Exception.Implementations;
using System;
using System.Collections.Generic;

namespace INTELECTAH.ConsultaFacil.Domain.Entities
{
    public class Consulta
    {
        public int ConsultaId { get; set; }

        public int PacienteId { get; set; }

        public int ExameId { get; set; }

        public DateTime DataHora { get; set; }

        public string Protocolo { get; private set; }

        public Paciente Paciente { get; set; }

        public Exame Exame { get; set; }

        public void Validate()
        {
            IList<string> exceptions = new List<string>();

            // Validate IdPaciente
            if (PacienteId <= Decimal.Zero)
                exceptions.Add("Paciente é requerido");

            // Validate IdExame
            if (ExameId <= Decimal.Zero)
                exceptions.Add("Exame é requerido");

            // Validate DataHora
            if (DataHora.Equals(DateTime.MinValue))
                exceptions.Add("Data e Hora requerida");

            if (DataHora < DateTime.Now)
                exceptions.Add("A Data e Hora inválida");

            // Validate Protocolo
            if (String.IsNullOrEmpty(Protocolo))
                exceptions.Add("Protocolo é requerido");

            if (exceptions.Count > Decimal.Zero)
                throw new DomainException(exceptions);
        }

        public void GenerateProtocolo()
        {
            Protocolo = $"{DateTime.Now:yyyyMMddHHmmssffff}-{PacienteId}";
        }
    }
}
