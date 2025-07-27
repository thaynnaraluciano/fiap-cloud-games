using FluentValidation;

namespace Domain.Commands.v1.Biblioteca.ComprarJogo
{
    public class ComprarJogoCommandValidator : AbstractValidator<ComprarJogoCommand>
    {
        public ComprarJogoCommandValidator()
        {
            RuleFor(command => command.IdUsuario)
                .NotNull().WithMessage("Usuário não pode ser nulo");
            RuleFor(command => command.IdJogo)
                .NotNull().WithMessage("Jogo não pode ser nulo");
        }
    }
}