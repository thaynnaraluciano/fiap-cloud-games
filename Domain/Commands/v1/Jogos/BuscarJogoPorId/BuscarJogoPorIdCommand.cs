using Domain.Commands.v1.Jogos.JogoResponses;
using MediatR;

namespace Domain.Commands.v1.Jogos.BuscarJogo
{
    public class BuscarJogoPorIdCommand : IRequest<ListarJogoCommandResponse>
    {
        public Guid Id { get; set; }

        public BuscarJogoPorIdCommand(Guid id)
        {
            Id = id;
        }
    }
}
