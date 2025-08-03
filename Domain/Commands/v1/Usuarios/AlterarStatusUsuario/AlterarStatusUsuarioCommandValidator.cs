using FluentValidation;

namespace Domain.Commands.v1.Usuarios.AlterarStatusUsuario
{
    public class AlterarStatusUsuarioCommandValidator : AbstractValidator<AlterarStatusUsuarioCommand>
    {
        public AlterarStatusUsuarioCommandValidator()
        {
            RuleFor(command => command.bStatus)
                .NotNull().WithMessage("O status não pode ser nulo.");
            RuleFor(command => command.cGuid)
                .Must(id => id != Guid.Empty).WithMessage("O ID do user não pode ser vazio.");
        }
    }
}
