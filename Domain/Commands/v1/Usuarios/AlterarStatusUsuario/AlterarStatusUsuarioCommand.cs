using MediatR;

namespace Domain.Commands.v1.Usuarios.AlterarStatusUsuario
{
    public class AlterarStatusUsuarioCommand : IRequest<AlterarStatusUsuarioCommandResponse>
    {
        public Guid cGuid { get; set; }
        public bool bStatus { get; set; }
    }
}
