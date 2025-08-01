using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.v1.Usuarios.CriarSenha
{
    public class CriarSenhaCommand: IRequest<Unit>
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
