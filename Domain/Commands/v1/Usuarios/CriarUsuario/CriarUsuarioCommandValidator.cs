using FluentValidation;

namespace Domain.Commands.v1.Usuarios.CriarUsuario
{
    public class CriarUsuarioCommandValidator : AbstractValidator<CriarUsuarioCommand>
    {
        public CriarUsuarioCommandValidator()
        {
            RuleFor(command => command.Nome)
                .NotEmpty().WithMessage("O nome do usuário é obrigatório.")
                .Length(3, 100).WithMessage("O nome o usuário deve ter entre 5 e 100 caracteres");

            RuleFor(command => command.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("Insira um e-mail válido");
        }
    }
}
