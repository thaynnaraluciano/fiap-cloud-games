using Domain.Enums;
using MediatR;

namespace Domain.Commands.v1.Usuarios.CriarUsuario
{
    public class CriarUsuarioCommand : IRequest<CriarUsuarioCommandResponse>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public PerfilUsuario PerfilUsuario { get; set; }
    }
}
