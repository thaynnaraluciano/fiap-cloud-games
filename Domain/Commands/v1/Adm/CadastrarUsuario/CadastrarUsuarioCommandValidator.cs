using FluentValidation;

namespace Domain.Commands.v1.Adm.CadastrarUsuario
{
    public class CadastrarUsuarioCommandValidator : AbstractValidator<CadastrarUsuarioCommand>
    {
        public CadastrarUsuarioCommandValidator()
        {
            RuleFor(command => command.Nome)
                .NotEmpty().WithMessage("O nome do usuário é obrigatório.")
                .Length(3, 100).WithMessage("O nome o usuário deve ter entre 5 e 100 caracteres");

            RuleFor(command => command.Email)
                .EmailAddress().WithMessage("Insira um endereço de e-mail válido.");

            RuleFor(command => (int)command.PerfilUsuario)
                .LessThanOrEqualTo(1).WithMessage("Tipo de usuário incorreto");
        }
    }
}
