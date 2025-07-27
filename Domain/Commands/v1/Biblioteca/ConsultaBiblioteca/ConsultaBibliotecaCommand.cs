using Domain.Commands.v1.Jogos.ListarJogos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.v1.Biblioteca.ConsultaBiblioteca
{
    public class ConsultaBibliotecaCommand: IRequest<IEnumerable<ConsultaBibliotecaCommandResponse>>
    {
        public Guid IdUsuario { get; set; }
        public Guid IdJogo { get; set; }

        public ConsultaBibliotecaCommand(Guid idUser)
        {
            IdUsuario = idUser;
        }   
    }
}
