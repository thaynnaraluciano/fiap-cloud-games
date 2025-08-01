using MediatR;

namespace Domain.Commands.v1.Usuarios.ListarUsuarios
{
    public class ListarUsuariosCommand : IRequest<IEnumerable<ListarUsuariosCommandResponse>>
    {
    }
}
