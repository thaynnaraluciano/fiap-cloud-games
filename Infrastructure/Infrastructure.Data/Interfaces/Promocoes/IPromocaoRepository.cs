using Infrastructure.Data.Models.Promocao;

namespace Infrastructure.Data.Interfaces.Promocoes
{
    public interface IPromocaoRepository
    {
        Task AdicionarAsync(PromocaoModel promocao);
        Task<PromocaoModel?> BuscarPorIdAsync(Guid id);
        Task<IEnumerable<PromocaoModel>> ObterTodosAsync();
        Task<PromocaoModel?> ObterPromocaoAtivaPorJogoAsync(Guid jogoId);
        Task AtualizarAsync(PromocaoModel promocao);
        Task RemoverAsync(PromocaoModel promocao);
        Task<bool> JogoEstaEmOutraPromocaoAsync(Guid jogoId);
        Task<bool> ExistemJogosComPromocaoAsync(IEnumerable<Guid> jogosIds, Guid? promocaoIdAtual = null);
    }
}
