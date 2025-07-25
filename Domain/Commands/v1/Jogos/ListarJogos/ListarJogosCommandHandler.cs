﻿using AutoMapper;
using Infrastructure.Data.Interfaces.Jogos;
using MediatR;

namespace Domain.Commands.v1.Jogos.ListarJogos
{
    public class ListarJogosCommandHandler : IRequestHandler<ListarJogosCommand, IEnumerable<ListarJogoCommandResponse>>
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly IMapper _mapper;

        public ListarJogosCommandHandler(IJogoRepository jogoRepository, IMapper mapper)
        {
            _jogoRepository = jogoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ListarJogoCommandResponse>> Handle(ListarJogosCommand request, CancellationToken cancellationToken)
        {
            var jogos = await _jogoRepository.ObterTodosAsync();
            return _mapper.Map<IEnumerable<ListarJogoCommandResponse>>(jogos);
        }
    }
}
