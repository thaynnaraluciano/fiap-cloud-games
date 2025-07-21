using FluentValidation;

namespace Domain.Commands.v1.Jogos.BuscarJogo
{
    public class BuscarJogoPorIdCommandValidator : AbstractValidator<BuscarJogoPorIdCommand>
    {
        public BuscarJogoPorIdCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("O ID do jogo não pode estar vazio.")
                .NotNull().WithMessage("O ID do jogo não pode ser nulo.")
                .Must(id => id != Guid.Empty).WithMessage("O ID do jogo não pode ser um GUID vazio.");
        }
    }
}
