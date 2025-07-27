using Infrastructure.Data.Interfaces.Biblioteca;
using Infrastructure.Data.Models.Biblioteca;
using Infrastructure.Data.Models.Jogos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
