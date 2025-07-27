using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Models.Biblioteca
{
    public class BibliotecaModel
    {
        public int Id { get; set; }
        public Guid IdJogo { get; set; }
        public Guid IdUsuario { get; set; }
        public DateTime DtAdquirido { get; set; }
    }
}
