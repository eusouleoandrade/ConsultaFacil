using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace INTELECTAH.ConsultaFacil.ViewModel
{
    public class PacienteViewModel
    {
        [DisplayName("Cod.")]
        public int PacienteId { get; set; }

        [Required(ErrorMessage = "Nome é requerido")]
        [MaxLength(100, ErrorMessage = "Nome não pode ter mais que 100 caracteres")]
        [DisplayName("Paciente")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "CPF é requerido")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Inserir apenas os números. Ex: 46480549031")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Data Nascimento é requerida")]
        [DisplayName("Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Sexo é requerido")]
        public string Sexo { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("([0-9\\s]+)", ErrorMessage = "Inserir apenas os números. Ex: 81 999999999")]
        [Required(ErrorMessage = "Telefone é requerido")]
        public string Telefone { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "E-mail é requerido")]
        [DisplayName("E-mail")]
        public string Email { get; set; }
    }
}
