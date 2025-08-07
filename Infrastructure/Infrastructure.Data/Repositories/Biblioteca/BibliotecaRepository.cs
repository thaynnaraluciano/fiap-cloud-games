using Infrastructure.Data.Interfaces.Biblioteca;
using Infrastructure.Data.Models.Biblioteca;
using Infrastructure.Data.Models.Jogos;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories.Biblioteca
{
    public class BibliotecaRepository : IBibliotecaRepository
    {
        private readonly AppDbContext _context;
        public BibliotecaRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BibliotecaModel>> BuscaBibliotecaUser(Guid idUsuario)
        {
            return await _context.Biblioteca
                .Include(b => b.Jogo)
                .Where(b => b.IdUsuario == idUsuario)
                .ToListAsync();
        }

        public async Task<bool> UsuarioPossuiJogoAsync(Guid idUsuario, Guid idJogo)
        {
            return await _context.Biblioteca
                .AnyAsync(b => b.IdUsuario == idUsuario && b.IdJogo == idJogo);
        }

        public async Task<BibliotecaModel> AdicionarJogoAsync(BibliotecaModel compra)
        {
            _context.Biblioteca.Add(compra);
            await _context.SaveChangesAsync();
            return compra;
        }
    }
}
