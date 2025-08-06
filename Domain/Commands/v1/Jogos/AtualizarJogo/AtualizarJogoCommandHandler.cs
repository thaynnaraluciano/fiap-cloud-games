using AutoMapper;
using Infrastructure.Data.Interfaces.Jogos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Jogos.AtualizarJogo
{
    public class AtualizarJogoCommandHandler : IRequestHandler<AtualizarJogoCommand, AtualizarJogoCommandResponse>
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AtualizarJogoCommandHandler> _logger;

        public AtualizarJogoCommandHandler(IJogoRepository jogoRepository, IMapper mapper, ILogger<AtualizarJogoCommandHandler> logger)
        {
            _jogoRepository = jogoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AtualizarJogoCommandResponse> Handle(AtualizarJogoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Atualizando jogo {request.Id}");
            
            var jogoExistente = await _jogoRepository.ObterPorIdAsync(request.Id);
            
            if (jogoExistente == null)
            {
                _logger.LogError($"Jogo com ID {request.Id} não encontrado.");
                throw new Exception($"Jogo com ID {request.Id} não encontrado.");
            }

            jogoExistente.Atualizar(request.Nome, request.Descricao, request.Preco, request.DataLancamento);

            await _jogoRepository.AtualizarAsync(jogoExistente);

            _logger.LogInformation($"Jogo com ID {request.Id} atualizado.");

            return _mapper.Map<AtualizarJogoCommandResponse>(jogoExistente);
        }
    }
}
