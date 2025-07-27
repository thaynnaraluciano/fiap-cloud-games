using Infrastructure.Data.Interfaces.Promocoes;
using Infrastructure.Data.Models.Promocao;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories.Promocoes
{
    public class PromocaoRepository : IPromocaoRepository
    {
        private readonly AppDbContext _context;

        public PromocaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(PromocaoModel promocao)
        {
            await _context.Promocoes.AddAsync(promocao);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(PromocaoModel promocao)
        {
            _context.Promocoes.Update(promocao);
            await _context.SaveChangesAsync();
        }

        public async Task<PromocaoModel?> BuscarPorIdAsync(Guid id)
        {
            return await _context.Promocoes
                .Include(p => p.PromocaoJogos)
                .ThenInclude(pj => pj.Jogo)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> ExistemJogosComPromocaoAsync(IEnumerable<Guid> jogosIds, Guid? promocaoIdAtual = null)
        {
            return await _context.PromocaoJogos
                .AnyAsync(pj =>
                jogosIds.Contains(pj.JogoId) &&
                (promocaoIdAtual == null || pj.PromocaoId != promocaoIdAtual));
        }

        public async Task<bool> JogoEstaEmOutraPromocaoAsync(Guid jogoId)
        {
            return await _context.PromocaoJogos
                .AnyAsync(pj => pj.JogoId == jogoId);
        }

        public async Task<IEnumerable<PromocaoModel>> ObterTodosAsync()
        {
            return await _context.Promocoes
                .Include(p => p.PromocaoJogos)
                .ThenInclude(pj => pj.Jogo)
                .ToListAsync();
        }

        public async Task RemoverAsync(PromocaoModel promocao)
        {
            _context.Promocoes.Remove(promocao);
            await _context.SaveChangesAsync();
        }
    }
}
