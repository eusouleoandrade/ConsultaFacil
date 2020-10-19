using INTELECTAH.ConsultaFacil.Domain.Entities;
using INTELECTAH.ConsultaFacil.Exception.Implementations;
using System;
using System.Collections.Generic;

namespace INTELECTAH.ConsultaFacil.Domain
{
    public class Paciente
    {
        public int PacienteId { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        public DateTime DataNascimento { get; set; }

        public String Sexo { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

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

            // Validate CPF
            if (String.IsNullOrEmpty(CPF))
                exceptions.Add("CPF é requerido");

            // Validate Data Nascimento
            if (DataNascimento.Equals(DateTime.MinValue))
                exceptions.Add("Data de Nascimento é requerida");

            if (DataNascimento > DateTime.Now)
                exceptions.Add("Data de Nascimento inválida");

            // Validate Sexo
            if (String.IsNullOrEmpty(Sexo))
                exceptions.Add("Sexo é requerido");

            // Validate Telefone
            if (String.IsNullOrEmpty(Telefone))
                exceptions.Add("Telefone é requerido");

            // Validate E-mail
            if (String.IsNullOrEmpty(Email))
                exceptions.Add("E-mail é requerido");

            if (exceptions.Count > Decimal.Zero)
                throw new DomainException(exceptions);
        }
    }
}
