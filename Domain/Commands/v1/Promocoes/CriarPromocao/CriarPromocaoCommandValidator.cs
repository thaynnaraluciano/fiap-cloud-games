using FluentValidation;

namespace Domain.Commands.v1.Promocoes.CriarPromocao
{
    public class CriarPromocaoCommandValidator : AbstractValidator<CriarPromocaoCommand>
    {
        public CriarPromocaoCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome da promoção é obrigatório.")
                .MaximumLength(50)
                .WithMessage("O nome da promoção deve ter no máximo 50 caracteres.");

            RuleFor(x => x.Desconto)
                .GreaterThan(0)
                .WithMessage("O desconto deve ser maior que zero.");

            RuleFor(x => x.DataInicio)
                .LessThanOrEqualTo(x => x.DataFim)
                .WithMessage("A data de início deve ser anterior ou igual à data de fim.");

            RuleFor(x => x.DataFim)
                .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("A data de fim deve ser maior ou igual à data atual.");

            RuleFor(x => x.JogosIds)
                .NotEmpty()
                .WithMessage("A lista de jogos IDs não pode estar vazia.");
        }       
    }
}
