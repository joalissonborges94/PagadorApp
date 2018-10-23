using System.ComponentModel.DataAnnotations;

namespace PagadorApp.Models
{
    public class Customer
    {
        [Required(ErrorMessage = "O campo Nome Comprador é obrigatório", AllowEmptyStrings = false)]
        [MaxLength(255)]
        [Display(Name = "Nome do comprador")]
        public string Name { get; set; }
    }
}
