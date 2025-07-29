using MediatR;

namespace Domain.Commands.v1.Adm.BuscarUsuarioPorId
{
    public class BuscarUsuarioPorIdCommand : IRequest<BuscarUsuarioPorIdCommandResponse>
    {
        public Guid Id { get; set; }

        public BuscarUsuarioPorIdCommand(Guid id)
        {
            Id = id;
        }
    }
}
