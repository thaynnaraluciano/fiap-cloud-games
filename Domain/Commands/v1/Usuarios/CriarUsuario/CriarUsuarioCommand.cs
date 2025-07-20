using MediatR;

namespace Domain.Commands.v1.Usuarios.CriarUsuario
{
    public class CriarUsuarioCommand : IRequest<CriarUsuarioCommandResponse>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
