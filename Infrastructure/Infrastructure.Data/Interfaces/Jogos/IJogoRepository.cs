using Infrastructure.Data.Models.Jogos;

namespace Infrastructure.Data.Interfaces.Jogos
{
    public interface IJogoRepository
    {
        Task<IEnumerable<JogoModel>> ObterTodosAsync();
        Task<JogoModel> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(JogoModel jogo);
        Task AtualizarAsync(JogoModel jogo);
        Task RemoverAsync(JogoModel jogo);
    }
}
