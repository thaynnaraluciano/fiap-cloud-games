using MediatR;

namespace Domain.Commands.v1.Promocoes.RemoverPromocao
{
    public class RemoverPromocaoCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public RemoverPromocaoCommand(Guid id)
        {
            Id = id;
        }
    }
}
