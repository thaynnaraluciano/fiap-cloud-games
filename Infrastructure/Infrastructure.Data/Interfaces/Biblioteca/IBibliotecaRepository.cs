using Infrastructure.Data.Models.Biblioteca;
using Infrastructure.Data.Models.Jogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Interfaces.Biblioteca
{
    public interface IBibliotecaRepository
    {
        Task<IEnumerable<JogoModel>> BuscaBibliotecaUser(BibliotecaModel model);
        Task<ResultadoCompraModel> ComprarJogo(BibliotecaModel compra);
    }
}
