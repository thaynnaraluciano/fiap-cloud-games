using MediatR;

namespace Domain.Commands.v1.Usuarios.RemoverUsuario
{
    public class RemoverUsuarioCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
