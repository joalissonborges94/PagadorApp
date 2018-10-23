using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PagadorApp.Models;
using PagadorApp.Services;

namespace PagadorApp.Controllers

{

    public class PagamentoController : Controller

    {

        private readonly IPagamentoService _pagamento;

        private IConfiguration _configuration;

        public PagamentoController(IPagamentoService pagamento, IConfiguration Configuration)

        {
            _pagamento = pagamento;
            _configuration = Configuration;
        }

        [HttpGet]

        public async Task<IActionResult> Index()

        {
            List<Sale> ListSale = new List<Sale>();
            var Payments = await _pagamento.Listar();

            if (Payments.Payments != null)

            {
                foreach (Payment Pay in Payments.Payments)

                {
                    Sale Ret = await _pagamento.Consultar(Pay.PaymentId.ToString());
                    ListSale.Add(Ret);
                }
            }
            return View(ListSale);
        }

        [HttpGet]

        public IActionResult Pagar()

        {
            ViewBag.MerchantOrderId = _configuration["MerchantOderId"];
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]

        public async Task<IActionResult> Pagar(Sale sale)

        {
            if (ModelState.IsValid)

            {
                var _sale = await _pagamento.Pagar(sale);

                if (_sale == null)

                    return View(sale);
                return RedirectToAction("Visualizar", "Pagamento", new { id = _sale.payment.PaymentId.ToString() });

            }

            ViewBag.MerchantOrderId = sale.MerchantOrderId;
            return View();

        }

        [HttpGet, HttpPost]

        public async Task<IActionResult> Visualizar(String id)

        {
            var _sale = await _pagamento.Consultar(id);
            return View(_sale);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]

        public async Task<IActionResult> Capturar(Sale sale, String id)

        {
            string PaymentId = sale.payment != null ? sale.payment.PaymentId.ToString() : id;
            await _pagamento.Capturar(PaymentId);
            return RedirectToAction("Visualizar", "Pagamento", new { id = PaymentId });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]

        public async Task<IActionResult> Cancelar(Sale sale, String id)

        {
            string PaymentId = sale.payment != null ? sale.payment.PaymentId.ToString() : id;
            await _pagamento.Cancelar(PaymentId);
            return RedirectToAction("Visualizar", "Pagamento", new { id = PaymentId });
        }


        public IActionResult Error()

        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}