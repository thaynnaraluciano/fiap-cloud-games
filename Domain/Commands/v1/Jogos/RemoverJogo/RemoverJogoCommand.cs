using MediatR;

namespace Domain.Commands.v1.Jogos.RemoverJogo
{
    public class RemoverJogoCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public RemoverJogoCommand(Guid id)
        {
            Id = id;
        }
    }
}
