using MediatR;

namespace Domain.Commands.v1.Adm.ListarUsuarios
{
    public class ListarUsuariosCommand : IRequest<IEnumerable<ListarUsuariosCommandResponse>>
    {
    }
}
