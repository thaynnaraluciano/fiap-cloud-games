using AutoMapper;
using Domain.Commands.v1.Jogos.BuscarJogoPorId;
using Infrastructure.Data.Interfaces.Jogos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Jogos.BuscarJogo
{
    public class BuscarJogoPorIdCommandHandler : IRequestHandler<BuscarJogoPorIdCommand, BuscarJogoPorIdCommandResponse>
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BuscarJogoPorIdCommandHandler> _logger;

        public BuscarJogoPorIdCommandHandler(IJogoRepository jogoRepository, IMapper mapper, ILogger<BuscarJogoPorIdCommandHandler> logger)
        {
            _jogoRepository = jogoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BuscarJogoPorIdCommandResponse> Handle(BuscarJogoPorIdCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Consultando jogo {request.Id}");

            var jogo = await _jogoRepository.ObterPorIdAsync(request.Id);

            if (jogo == null)
            {
                _logger.LogError($"Jogo com ID {request.Id} não encontrado.");
                throw new KeyNotFoundException($"Jogo com ID {request.Id} não encontrado.");
            }

            _logger.LogInformation($"Jogo {request.Id} encontrado");
            return _mapper.Map<BuscarJogoPorIdCommandResponse>(jogo);
        }
    }
}
