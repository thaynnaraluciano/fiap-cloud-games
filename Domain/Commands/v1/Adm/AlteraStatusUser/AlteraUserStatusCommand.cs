using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.v1.Adm.AlteraStatusUser
{
    public class AlteraUserStatusCommand: IRequest<AlteraUserStatusCommandResponse>
    {
        public Guid cGuid { get; set; }
        public bool bStatus { get; set; }
    }
}
