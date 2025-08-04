using FluentValidation;

namespace Domain.Commands.v1.Notificacao.Email
{
    public class EnviarEmailCommandValidator : AbstractValidator<EnviarEmailCommand>
    {
        public EnviarEmailCommandValidator()
        {
            RuleFor(x => x.EmailDestinatario)
                .NotEmpty().WithMessage("EmailDestinatario deve ser informado")
                .EmailAddress().WithMessage("O formato de email informado é inválido");
        }
    }
}
