using FluentValidation;

namespace Domain.Commands.v1.Biblioteca.ComprarJogo
{
    public class ComprarJogoCommandValidator : AbstractValidator<ComprarJogoCommand>
    {
        public ComprarJogoCommandValidator()
        {
            RuleFor(command => command.IdUsuario)
                .NotNull().WithMessage("Usuário não pode ser nulo")
                .Must(id => id != Guid.Empty).WithMessage("O ID do usuário não pode ser um GUID vazio."); 
            RuleFor(command => command.IdJogo)
                .NotNull().WithMessage("Jogo não pode ser nulo")
                .Must(id => id != Guid.Empty).WithMessage("O ID do jogo não pode ser um GUID vazio.");
        }
    }
}