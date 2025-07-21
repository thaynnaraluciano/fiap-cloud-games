using AutoMapper;
using Domain.Commands.v1.Jogos.BuscarJogoPorId;
using Domain.Commands.v1.Jogos.ListarJogosCommand;
using Infrastructure.Data.Interfaces.Jogos;
using MediatR;

namespace Domain.Commands.v1.Jogos.BuscarJogo
{
    public class BuscarJogoPorIdCommandHandler : IRequestHandler<BuscarJogoPorIdCommand, BuscarJogoPorIdCommandResponse>
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly IMapper _mapper;

        public BuscarJogoPorIdCommandHandler(IJogoRepository jogoRepository, IMapper mapper)
        {
            _jogoRepository = jogoRepository;
            _mapper = mapper;
        }

        public async Task<BuscarJogoPorIdCommandResponse> Handle(BuscarJogoPorIdCommand request, CancellationToken cancellationToken)
        {
            var jogo = await _jogoRepository.ObterPorIdAsync(request.Id);

            if (jogo == null)
            {
                throw new KeyNotFoundException($"Jogo com ID {request.Id} não encontrado.");
            }

            return _mapper.Map<BuscarJogoPorIdCommandResponse>(jogo);
        }
    }
}
