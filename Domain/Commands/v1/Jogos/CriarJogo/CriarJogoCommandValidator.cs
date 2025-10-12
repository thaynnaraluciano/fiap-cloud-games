using FluentValidation;

namespace Domain.Commands.v1.Jogos.CriarJogo
{
    public class CriarJogoCommandValidator : AbstractValidator<CriarJogoCommand>
    {
        public CriarJogoCommandValidator()
        {
            RuleFor(command => command.Nome)
                .NotEmpty().WithMessage("O nome do jogo é obrigatório.")
                .Length(3, 100).WithMessage("O nome do jogo deve ter entre 3 e 100 caracteres.");

            RuleFor(command => command.Descricao)
                .NotEmpty().WithMessage("A descrição do jogo é obrigatória.")
                .Length(10, 500).WithMessage("A descrição do jogo deve ter entre 10 e 500 caracteres.");

            RuleFor(command => command.Preco)
                .GreaterThan(0).WithMessage("O preço do jogo deve ser maior que zero.");

            RuleFor(command => command.DataLancamento)
                .GreaterThan(new DateTime(1970, 1, 1))
                .WithMessage("A data de lançamento deve ser posterior a 01/01/1970.");

            RuleFor(command => command.DataLancamento)
               .LessThanOrEqualTo(command => DateTime.Now.AddYears(2))
               .WithMessage("A data de lançamento não pode ser superior a 2 anos.");
        }
    }
}
