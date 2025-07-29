using Domain.Enums;
using MediatR;

namespace Domain.Commands.v1.Adm.AtualizarUsuario
{
    public class AtualizarUsuarioCommand : IRequest<AtualizarUsuarioCommandResponse>
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public PerfilUsuarioEnum PerfilUsuario { get; set; }
    }
}
