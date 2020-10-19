using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace INTELECTAH.ConsultaFacil.ViewModel.Implementations
{
    public class TipoExameViewModel
    {
        [DisplayName("Cod.")]
        public int TipoExameId { get; set; }

        [MaxLength(100, ErrorMessage = "Nome não pode ter mais que 100 caracteres")]
        [Required(ErrorMessage = "Nome é requerido")]
        [DisplayName("Tipo de exame")]
        public string Nome { get; set; }

        [MaxLength(256, ErrorMessage = "Descrição não pode ter mais que 256 caracteres")]
        [Required(ErrorMessage = "Descrição é requerida")]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
    }
}
