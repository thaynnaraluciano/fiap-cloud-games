using FluentValidation;

namespace Domain.Commands.v1.Adm.RemoverUsuario
{
    public class RemoverUsuarioCommandValidator : AbstractValidator<RemoverUsuarioCommand>
    {
        public RemoverUsuarioCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("O ID do usuário não pode estar vazio.")
                .NotNull().WithMessage("O ID do usuário não pode ser nulo.");
        }
    }
}
