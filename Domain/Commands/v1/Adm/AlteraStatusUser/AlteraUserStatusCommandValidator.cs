using Domain.Commands.v1.Jogos.BuscarJogo;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.v1.Adm.AlteraStatusUser
{
    public class AlteraUserStatusCommandValidator : AbstractValidator<AlteraUserStatusCommand>
    {
        public AlteraUserStatusCommandValidator()
        {
            RuleFor(command => command.bStatus)
                .NotNull().WithMessage("O status não pode ser nulo.");
            RuleFor(command=> command.cGuid)
                .Must(id => id != Guid.Empty).WithMessage("O ID do user não pode ser vazio.");
        }
    }
}
