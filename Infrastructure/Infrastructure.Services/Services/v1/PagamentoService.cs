using Infrastructure.Data.Interfaces.Pagamento;

namespace Infrastructure.Services.Services.v1
{
    public class PagamentoService : IPagamentoService
    {
        public async Task<bool> ProcessarPagamento(decimal valor, string metodoPagamento)
        {
            await Task.Delay(1500); // simula tempo de processamento
            return true; // pagamento aprovado
        }
    }
}