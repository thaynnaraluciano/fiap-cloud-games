using FluentValidation;

namespace Domain.Commands.v1.Promocoes.BuscarPromocaoPorId
{
    public class BuscarPromocaoPorIdCommandValidator : AbstractValidator<BuscarPromocaoPorIdCommand>
    {
        public BuscarPromocaoPorIdCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID da promoção não pode ser vazio.")
                .Must(id => id != Guid.Empty).WithMessage("O ID da promoção não pode ser um GUID vazio.");
        }
    }
}
