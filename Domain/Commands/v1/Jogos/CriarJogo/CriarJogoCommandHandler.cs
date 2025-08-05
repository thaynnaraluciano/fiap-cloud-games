using AutoMapper;
using Infrastructure.Data.Interfaces.Jogos;
using Infrastructure.Data.Models.Jogos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Jogos.CriarJogo
{
    public class CriarJogoCommandHandler : IRequestHandler<CriarJogoCommand, CriarJogoCommandResponse>
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CriarJogoCommandHandler> _logger;

        public CriarJogoCommandHandler(IJogoRepository jogoRepository, IMapper mapper, ILogger<CriarJogoCommandHandler> logger)
        {
            _jogoRepository = jogoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CriarJogoCommandResponse> Handle(CriarJogoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Criando novo jogo");
            var jogo = _mapper.Map<JogoModel>(request);

            await _jogoRepository.AdicionarAsync(jogo);

            _logger.LogInformation($"Jogo criado com sucesso");
            return _mapper.Map<CriarJogoCommandResponse>(jogo);
        }
    }
}
