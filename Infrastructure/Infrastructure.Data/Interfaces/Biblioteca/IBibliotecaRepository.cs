using Infrastructure.Data.Models.Biblioteca;

namespace Infrastructure.Data.Interfaces.Biblioteca
{
    public interface IBibliotecaRepository
    {
        Task<IEnumerable<BibliotecaModel>> BuscaBibliotecaUser(Guid idUsuario);
        Task<bool> UsuarioPossuiJogoAsync(Guid idUsuario, Guid idJogo);
        Task<BibliotecaModel> AdicionarJogoAsync(BibliotecaModel compra);
    }
}
