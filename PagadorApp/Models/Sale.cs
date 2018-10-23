using System.ComponentModel.DataAnnotations;
namespace PagadorApp.Models
{
    public class Sale
    {
        [Required(ErrorMessage = "O campo Numero Do Pedido é obrigatório")]

        [Display(Name = "Numero do Pedido")]

        public string MerchantOrderId { get; set; }

        public Customer customer { get; set; }

        public Payment payment { get; set; }
    }

}
