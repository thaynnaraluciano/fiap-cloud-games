using FluentValidation;

namespace Domain.Commands.v1.Usuarios.AtualizarUsuario
{
    public class AtualizarUsuarioCommandValidator : AbstractValidator<AtualizarUsuarioCommand>
    {
        public AtualizarUsuarioCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID do usuário é obrigatório.");

            RuleFor(command => command.Nome)
                .NotEmpty().WithMessage("O nome do usuário é obrigatório.")
                .Length(3, 100).WithMessage("O nome o usuário deve ter entre 5 e 100 caracteres");

            RuleFor(command => command.Email)
                .EmailAddress().WithMessage("Insira um endereço de e-mail válido");

            RuleFor(command => (int)command.PerfilUsuario)
                .NotEmpty().WithMessage("Necessário informar o perfil do usuário")
                .LessThanOrEqualTo(2).WithMessage("Tipo de usuário incorreto");
        }
    }
}
