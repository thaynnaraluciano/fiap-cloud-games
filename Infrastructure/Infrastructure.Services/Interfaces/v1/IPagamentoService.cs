namespace Infrastructure.Data.Interfaces.Pagamento;

public interface IPagamentoService
{
    Task<bool> ProcessarPagamento(decimal valor, string metodoPagamento);
}
