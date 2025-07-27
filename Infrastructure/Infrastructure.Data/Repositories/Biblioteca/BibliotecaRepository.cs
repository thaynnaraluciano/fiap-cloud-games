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
        public async Task<IEnumerable<JogoModel>> BuscaBibliotecaUser(BibliotecaModel model)
        {
            return await _context.Jogos
            .Join(
                _context.Biblioteca.Where(bli => bli.IdUsuario == model.IdUsuario),
                jog => jog.Id,
                bli => bli.IdJogo,
                (jog, bli) => jog
            )
            .ToListAsync(); 
        }

        public async Task<ResultadoCompraModel> ComprarJogo(BibliotecaModel compra)
        {
            var jaPossui = await _context.Biblioteca
        .AnyAsync(b => b.IdUsuario == compra.IdUsuario && b.IdJogo == compra.IdJogo);

            if (jaPossui)
                return new ResultadoCompraModel { Sucesso = false, Mensagem = "Usuário já possui esse jogo." };

            try
            {
                _context.Biblioteca.Add(compra);
                await _context.SaveChangesAsync();
                return new ResultadoCompraModel { Sucesso = true, Mensagem = "Compra realizada com sucesso." };
            }
            catch (Exception ex)
            {
                return new ResultadoCompraModel { Sucesso = false, Mensagem = $"Erro ao comprar: {ex.Message}" };
            }
        }
    }
}
