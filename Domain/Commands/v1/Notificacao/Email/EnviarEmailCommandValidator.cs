using FluentValidation;

namespace Domain.Commands.v1.Notificacao.Email
{
    public class EnviarEmailCommandValidator : AbstractValidator<EnviarEmailCommand>
    {
        private const string plainTextBodyType = "plain";
        private const string htmlBodyType = "html";

        public EnviarEmailCommandValidator()
        {
            RuleFor(x => x.NomeDestinatario)
                .NotEmpty().WithMessage("NomeDestinatario deve ser informado")
                .MinimumLength(3).WithMessage("NomeDestinatario deve possuir pelo menos 3 caracteres");

            RuleFor(x => x.Assunto)
                .NotEmpty().WithMessage("Assunto deve ser informado")
                .MinimumLength(5).WithMessage("Assunto deve possuir pelo menos 5 caracteres");

            RuleFor(x => x.EmailDestinatario)
                .NotEmpty().WithMessage("EmailDestinatario deve ser informado")
                .EmailAddress().WithMessage("O formato de email informado é inválido");
        }
    }
}
