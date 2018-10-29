﻿using PagadorApp.Models.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PagadorApp.Models
{
    public class Payment
    {
        public Payment()
        {
            Amount = 0;
            Installments = 0;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        [Required(ErrorMessage = "O Provedor é obrigatório")]
        [Display(Name = "Provedor")]
        public ProviderEnum Provider { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public string Type { get; set; }

        [Required(ErrorMessage = "O Valor da compra é obrigatório")]
        [Range(1, 9223372036854775807, ErrorMessage = "Valor da compra inválido!")]
        [Display(Name = "Valor da compra")]
        public long Amount { get; set; }

        [Required(ErrorMessage = "O Valor da compra é obrigatório")]
        [Range(1, 922337203685477.58, ErrorMessage = "O Valor da compra deverá ser entre 1 e 92233720368547758,07.")]
        private decimal amountD;
        public decimal AmountD

        {
            get
            {
                return amountD;
            }
            set
            {
                amountD = value;
                Amount = (long)(value * 100);
            }
        }

        [Required(ErrorMessage = "O Numero de parcelas é obrigatório")]
        [Range(1, 99, ErrorMessage = "Número de parcelas inválido! .")]
        [Display(Name = "Nº de parcelas")]

        public int Installments { get; set; }
        public CreditCard creditCard { get; set; }

        [Display(Name = "Número do Comprovante de Venda")]
        public string ProofOfSale { get; set; }

        [Display(Name = "Id da transação no provedor de meio de pagamento")]
        public string AcquirerTransactionId { get; set; }

        [Display(Name = "Código de autorização")]
        public string AuthorizationCode { get; set; }

        [Display(Name = "Identificador do Pedido")]
        public Guid PaymentId { get; set; }

        [Display(Name = "Data do Recebimento pela Braspag")]
        public DateTime ReceivedDate { get; set; }

        [Display(Name = "Código de retorno da Operação")]
        public byte ReasonCode { get; set; }

        [Display(Name = "Mensagem de retorno da Operação")]
        public string ReasonMessage { get; set; }

        [Display(Name = "Status")]
        public byte Status { get; set; }

        public string MsgStatus
        {
            get
            {
                return DescisaoEnum.GetDescription("StatusTransacaoEnum", Convert.ToInt16(Status));
            }
        }

        [Display(Name = "Código retornado pelo provedor")]
        public string ProviderReturnCode { get; set; }



        [Display(Name = "Mensagem retornada pelo provedor")]
        public string ProviderReturnMessage { get; set; }

        public string TransformAmount
        {
            get
            {
                return ((decimal)Amount / 100).ToString("c");
            }
        }

        public List<Link> Links { get; set; }
        public List<Payment> Payments { get; set; }
    }
}