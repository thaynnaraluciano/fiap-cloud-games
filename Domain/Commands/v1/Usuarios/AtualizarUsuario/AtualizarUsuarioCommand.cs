using MediatR;

namespace Domain.Commands.v1.Usuarios.AtualizarUsuario
{
    public class AtualizarUsuarioCommand : IRequest<AtualizarUsuarioCommandResponse>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int PerfilUsuario { get; set; }
    }
}
