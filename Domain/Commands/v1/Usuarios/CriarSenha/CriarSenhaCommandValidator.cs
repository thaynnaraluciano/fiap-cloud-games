using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.v1.Usuarios.CriarSenha
{
    public class CriarSenhaCommandValidator : AbstractValidator<CriarSenhaCommand>
    {
        public CriarSenhaCommandValidator()
        {
            RuleFor(command => command.Email)
                .EmailAddress().WithMessage("Insira um e-mail válido.");

            RuleFor(command => command.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$")
                .WithMessage("A senha deve ter no mínimo 8 caracteres e incluir letras, números e caracteres especiais.");
        }
    }
}
