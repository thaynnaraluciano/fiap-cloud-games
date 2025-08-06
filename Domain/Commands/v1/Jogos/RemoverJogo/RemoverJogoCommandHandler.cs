using Infrastructure.Data.Interfaces.Jogos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Jogos.RemoverJogo
{
    public class RemoverJogoCommandHandler : IRequestHandler<RemoverJogoCommand, Unit>
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly ILogger<RemoverJogoCommandHandler> _logger;

        public RemoverJogoCommandHandler(IJogoRepository jogoRepository, ILogger<RemoverJogoCommandHandler> logger)
        {
            _jogoRepository = jogoRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(RemoverJogoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Removendo o jogo {request.Id}");
            var jogo = await _jogoRepository.ObterPorIdAsync(request.Id);
            
            if (jogo == null)
            {
                _logger.LogError($"Jogo com ID {request.Id} não encontrado.");
                throw new Exception($"Jogo com ID {request.Id} não encontrado.");
            }

            await _jogoRepository.RemoverAsync(jogo);

            _logger.LogInformation($"Jogo {request.Id} removido");
            return Unit.Value;
        }
    }
}
