using PagadorApp.Models;
using PagadorApp.Models.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace PagadorApp.Models
{
    public class CreditCard
    {
        [Required(ErrorMessage = "O campo Número Do Cartão é obrigatório", AllowEmptyStrings = false)]
        [MaxLength(16)]
        [Display(Name = "Numero do cartão")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "O campo Nome Impresso No Cartão é obrigatório", AllowEmptyStrings = false)]
        [MaxLength(25)]
        [Display(Name = "Nome impresso no cartão")]
        public string Holder { get; set; }

        [Required(ErrorMessage = "O campo Validade Do Cartão é obrigatório", AllowEmptyStrings = false)]
        [MaxLength(16)]
        [Display(Name = "Validade do cartão")]
        public string ExpirationDate { get; set; }

        [Required(ErrorMessage = "O campo Código De Segurança é obrigatório", AllowEmptyStrings = false)]
        [MaxLength(16)]
        [Display(Name = "Código de Segurança")]
        public string SecurityCode { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [Required(ErrorMessage = "O campo Bandeira Do Cartão é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Bandeira do Cartão")]
        public BrandEnum Brand { get; set; }

    }
}
