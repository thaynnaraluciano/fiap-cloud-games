using Infrastructure.Data.Interfaces.Jogos;
using Infrastructure.Data.Models.Jogos;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Data.Repositories.Jogos
{
    public class JogoRepository : IJogoRepository
    {
        private readonly AppDbContext _context;

        public JogoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(JogoModel jogo)
        {
            await _context.Jogos.AddAsync(jogo);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(JogoModel jogo)
        {
            _context.Jogos.Update(jogo);
            await _context.SaveChangesAsync();
        }

        public async Task<JogoModel> ObterPorIdAsync(Guid id)
        {
            return await _context.Jogos.FindAsync(id);
        }

        public async Task<IEnumerable<JogoModel>> ObterTodosAsync()
        {
            return await _context.Jogos.ToListAsync();
        }

        public async Task RemoverAsync(JogoModel jogo)
        {
            _context.Jogos.Remove(jogo);
            await _context.SaveChangesAsync();
        }
    }
}
