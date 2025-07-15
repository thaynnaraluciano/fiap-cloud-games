using FluentValidation;

namespace Domain.Commands.v1.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(usuario => usuario.Email)
                .NotEmpty().WithMessage("O email deve ser informado");

            RuleFor(usuario => usuario.Senha)
                .NotEmpty().WithMessage("A senha deve ser informada");
        }
    }
}
