using INTELECTAH.ConsultaFacil.Domain;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace INTELECTAH.ConsultaFacil.ViewModel.Implementations
{
    public class ConsultaViewModel
    {
        [DisplayName("Cod.")]
        public int ConsultaId { get; set; }

        [Required(ErrorMessage = "Paciente é requerido")]
        public int PacienteId { get; set; }

        [Required(ErrorMessage = "Exame é requerido")]
        public int ExameId { get; set; }

        [Required(ErrorMessage = "Data e hora é requerida")]
        [DisplayName("Data Hora")]
        public DateTime? DataHora { get; set; }

        public string Protocolo { get; set; }

        public Paciente Paciente { get; set; }

        public Exame Exame { get; set; }
    }
}
