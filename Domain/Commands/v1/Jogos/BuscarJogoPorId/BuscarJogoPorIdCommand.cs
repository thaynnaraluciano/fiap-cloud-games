﻿using Domain.Commands.v1.Jogos.BuscarJogoPorId;
using MediatR;

namespace Domain.Commands.v1.Jogos.BuscarJogo
{
    public class BuscarJogoPorIdCommand : IRequest<BuscarJogoPorIdCommandResponse>
    {
        public Guid Id { get; set; }

        public BuscarJogoPorIdCommand(Guid id)
        {
            Id = id;
        }
    }
}
