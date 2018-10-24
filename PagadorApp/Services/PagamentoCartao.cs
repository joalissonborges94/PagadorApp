using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using PagadorApp.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;



namespace PagadorApp.Services

{

    public class PagamentoCartao : IPagamentoService

    {

        private readonly HttpClient Client;

        private IConfiguration _configuration;



        public PagamentoCartao(IConfiguration Configuration)

        {

            _configuration = Configuration;

            Client = new HttpClient();

            AddHeaders();

        }



        public async Task<Payment> Listar()

        {

            List<Sale> ListSale = new List<Sale>();

            Client.BaseAddress = new Uri(_configuration["https://apiquerysandbox.braspag.com.br/"]);

            var response = await Client.GetAsync("v2/sales?merchantOrderId=" + _configuration["merchantOrderId"]);

            if (response.IsSuccessStatusCode)

            {

                var retorno = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Payment>(retorno);

            }

            return new Payment();

        }



        public async Task<Sale> Consultar(String PaymentId)

        {

            if (Client.BaseAddress == null)

                Client.BaseAddress = new Uri(_configuration["https://apiquerysandbox.braspag.com.br/"]);



            var response = await Client.GetAsync("v2/sales/" + PaymentId);

            if (response.IsSuccessStatusCode)

            {

                var retorno = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Sale>(retorno);

            }

            return new Sale();

        }



        public async Task<Sale> Pagar(Sale sale)

        {

            Client.BaseAddress = new Uri(_configuration["https://apisandbox.braspag.com.br/"]);



            var response = await Client.PostAsync("v2/sales/", ConverObject(sale));

            if (response.IsSuccessStatusCode)

            {

                var retorno = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Sale>(retorno);

            }

            return new Sale();

        }



        public async Task Capturar(String PaymentId)

        {

            Client.BaseAddress = new Uri(_configuration["https://apisandbox.braspag.com.br/"]);

            var response = await Client.PutAsync("v2/sales/" + PaymentId.ToString() + "/capture", null);

            if (response.IsSuccessStatusCode)

            {

                await response.Content.ReadAsStringAsync();



            }


        }



        public async Task Cancelar(string PaymentId)

        {

            Client.BaseAddress = new Uri(_configuration["https://apisandbox.braspag.com.br/"]);

            var response = await Client.PutAsync("v2/sales/" + PaymentId.ToString() + "/void", null);

            if (response.IsSuccessStatusCode)

            {

                await response.Content.ReadAsStringAsync();


            }
        }



        private void AddHeaders()

        {

            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Client.DefaultRequestHeaders.Add("MerchantId", _configuration["41fba699-8a20-4aa8-8e82-14c971b0289f"]);

            Client.DefaultRequestHeaders.Add("MerchantKey", _configuration["EKQVRLVBUYODSBHBXPBBLNCJCTFDOECQTLLBZHCQ"]);

        }



        private static StringContent ConverObject(Sale sale)

        {

            var json = JsonConvert.SerializeObject(sale);

            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            return stringContent;

        }

    }

}