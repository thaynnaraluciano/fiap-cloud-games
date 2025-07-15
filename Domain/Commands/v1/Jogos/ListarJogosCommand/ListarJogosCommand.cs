using Domain.Commands.v1.Jogos.JogoResponses;
using Infrastructure.Data.Models.Jogos;
using MediatR;

namespace Domain.Commands.v1.Jogos.BuscarTodosJogosCommand
{
    public class ListarJogosCommand : IRequest<IEnumerable<ListarJogoCommandResponse>>
    {
    }
}
