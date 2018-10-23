using PagadorApp.Models;
using System.Threading.Tasks;

namespace PagadorApp.Services
{
    public interface IPagamentoService
    {
        Task<Payment> Listar();
        Task<Sale> Pagar(Sale sale);
        Task<Sale> Consultar(string PaymentId);
        Task Capturar(string PaymentId);
        Task Cancelar(string PaymentId);
    }
}
