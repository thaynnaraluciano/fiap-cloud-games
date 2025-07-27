using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.v1.Biblioteca.ConsultaBiblioteca
{
    public class ConsultaBibliotecaCommandValidator : AbstractValidator<ConsultaBibliotecaCommand>
    {
        public ConsultaBibliotecaCommandValidator() 
        {
            RuleFor(command => command.IdUsuario)
            .NotNull().WithMessage("Usuário não pode ser nulo");
        }
    }
}
