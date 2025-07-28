using MediatR;

namespace Domain.Commands.v1.Adm.AlteraStatusUser
{
    public class AlteraUserStatusCommand: IRequest<AlteraUserStatusCommandResponse>
    {
        public Guid cGuid { get; set; }
        public bool bStatus { get; set; }
    }
}
