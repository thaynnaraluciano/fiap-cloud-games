﻿using FluentValidation;

namespace Domain.Commands.v1.Jogos.RemoverJogo
{
    public class RemoverJogoCommandValidator : AbstractValidator<RemoverJogoCommand>
    {
        public RemoverJogoCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("O ID do jogo não pode estar vazio.")
                .NotNull().WithMessage("O ID do jogo não pode ser nulo.");
        }
    }
}
