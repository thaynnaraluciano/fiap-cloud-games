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
            return await _context.Usuarios.FindAsync(id);
        }

        public UsuarioModel? ObterPorEmailAsync(string email)
        {
            if (email != null)
                return _context.Usuarios.FirstOrDefault(x => string.Equals(x.Email, email));

            return null;
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

        public int ObterQtdUsuariosCadastradosAsync()
        {
            return _context.Usuarios.Count();
        }

        public async Task<bool> AlterarStatusUsuario(UsuarioModel userModel)
        {
            var user = await _context.Usuarios.Where(x => x.Id == userModel.Id).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado");
            }
            if (user.Ativo != userModel.Ativo)
            {
                user.Ativo = userModel.Ativo;
                await _context.SaveChangesAsync();
            }
            return user.Ativo;
        }
    }
}
