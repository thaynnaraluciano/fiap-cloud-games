using FluentValidation;

namespace Domain.Commands.v1.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(usuario => usuario.Email)
                .NotNull().NotEmpty().WithMessage("O email deve ser informado")
                .EmailAddress().WithMessage("O email deve possuir um formato válido, example@example.com");

            RuleFor(usuario => usuario.Senha)
                .NotNull().NotEmpty().WithMessage("A senha deve ser informada");
        }
    }
}
