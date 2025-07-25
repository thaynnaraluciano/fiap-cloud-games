using MediatR;

namespace Domain.Commands.v1.Promocoes.BuscarPromocaoPorId
{
    public class BuscarPromocaoPorIdCommand : IRequest<BuscarPromocaoPorIdCommandResponse>
    {
        public Guid Id { get; set; }

        public BuscarPromocaoPorIdCommand(Guid id)
        {
            Id = id;
        }
    }
}
