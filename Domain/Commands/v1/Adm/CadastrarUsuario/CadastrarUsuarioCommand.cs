using Domain.Enums;
using MediatR;

namespace Domain.Commands.v1.Adm.CadastrarUsuario
{
    public class CadastrarUsuarioCommand : IRequest<CadastrarUsuarioCommandResponse>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public int PerfilUsuario { get; set; }
    }
}
