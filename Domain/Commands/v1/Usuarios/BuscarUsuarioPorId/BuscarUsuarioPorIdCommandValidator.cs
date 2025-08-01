using FluentValidation;

namespace Domain.Commands.v1.Usuarios.BuscarUsuarioPorId
{
    public class BuscarUsuarioPorIdCommandValidator : AbstractValidator<BuscarUsuarioPorIdCommand>
    {
        public BuscarUsuarioPorIdCommandValidator()
        {

            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("O ID do usuário não pode estar vazio.")
                .NotNull().WithMessage("O ID do usuário não pode ser nulo.")
                .Must(id => id != Guid.Empty).WithMessage("O ID do usuário não pode ser um GUID vazio.");
        }
    }
}
