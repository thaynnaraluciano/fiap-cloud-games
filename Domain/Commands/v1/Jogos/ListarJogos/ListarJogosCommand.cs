using MediatR;

namespace Domain.Commands.v1.Jogos.ListarJogos
{
    public class ListarJogosCommand : IRequest<IEnumerable<ListarJogoCommandResponse>>
    {
    }
}
