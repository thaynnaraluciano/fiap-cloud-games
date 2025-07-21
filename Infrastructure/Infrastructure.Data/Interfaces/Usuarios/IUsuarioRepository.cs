using Infrastructure.Data.Models.Usuarios;

namespace Infrastructure.Data.Interfaces.Usuarios
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<UsuarioModel>> ObterTodosAsync();
        Task<UsuarioModel> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(UsuarioModel usuario);
        Task AtualizarAsync(UsuarioModel usuario);
        Task RemoverAsync(UsuarioModel usuario);
    }
}
