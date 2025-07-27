using FluentValidation;

namespace Domain.Commands.v1.Jogos.AtualizarJogo
{
    public class AtualizarJogoCommandValidator : AbstractValidator<AtualizarJogoCommand>
    {
        public AtualizarJogoCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID do jogo é obrigatório.");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome do jogo é obrigatório.")
                .Length(3, 100).WithMessage("O nome do jogo deve ter entre 3 e 100 caracteres.");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("A descrição do jogo é obrigatória.")
                .Length(10, 500).WithMessage("A descrição do jogo deve ter entre 10 e 500 caracteres.");

            RuleFor(x => x.Preco)
                .GreaterThan(0).WithMessage("O preço do jogo deve ser maior que zero.");

            RuleFor(x => x.DataLancamento)
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("A data de lançamento não pode ser no passado.");
        }
    }
}
