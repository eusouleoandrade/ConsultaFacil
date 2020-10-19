using INTELECTAH.ConsultaFacil.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace INTELECTAH.ConsultaFacil.ViewModel.Implementations
{
    public class ExameViewModel
    {
        [DisplayName("Cod.")]
        public int ExameId { get; set; }

        [MaxLength(100, ErrorMessage = "Nome não pode ter mais que 100 caracteres")]
        [Required(ErrorMessage = "Nome é requerido")]
        [DisplayName("Exame")]
        public string Nome { get; set; }

        [MaxLength(1000, ErrorMessage = "Observação não pode ter mais que 1000 caracteres")]
        [Required(ErrorMessage = "Observação é requerida")]
        [DisplayName("Observação")]
        public string Observacao { get; set; }

        [Required(ErrorMessage = "Tipo exame é requerido")]
        public int TipoExameId { get; set; }

        [DisplayName("Tipo de exame")]
        public TipoExame TipoExame { get; set; }
    }
}
