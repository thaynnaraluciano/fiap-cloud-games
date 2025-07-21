using Infrastructure.Data.Interfaces.Usuarios;
using Infrastructure.Data.Models.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories.Usuarios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(UsuarioModel usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(UsuarioModel usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<UsuarioModel> ObterPorIdAsync(Guid id)
        {
            //TO DO tratar retorno null?
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<IEnumerable<UsuarioModel>> ObterTodosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task RemoverAsync(UsuarioModel usuario)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
