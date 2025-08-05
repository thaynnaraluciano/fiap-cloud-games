using AutoMapper;
using Infrastructure.Data.Interfaces.Jogos;
using Infrastructure.Data.Interfaces.Promocoes;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Promocoes.AtualizarPromocao
{
    public class AtualizarPromocaoCommandHandler : IRequestHandler<AtualizarPromocaoCommand, AtualizarPromocaoCommandResponse>
    {
        private readonly IPromocaoRepository _promocaoRepository;
        private readonly IJogoRepository _jogoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AtualizarPromocaoCommandHandler> _logger;

        public AtualizarPromocaoCommandHandler(
            IPromocaoRepository promocaoRepository, 
            IJogoRepository jogoRepository, 
            IMapper mapper, 
            ILogger<AtualizarPromocaoCommandHandler> logger)
        {
            _promocaoRepository = promocaoRepository;
            _jogoRepository = jogoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AtualizarPromocaoCommandResponse> Handle(AtualizarPromocaoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Atualizando promoção {request.Id}");

            var promocaoExistente = await _promocaoRepository.BuscarPorIdAsync(request.Id);

            if (promocaoExistente == null)
            {
                _logger.LogError($"Promoção não encontrada {request.Id}");
                throw new Exception("Promoção não encontrada.");
            }

            var jogos = await _jogoRepository.BuscarPorIdsAsync(request.JogosIds.ToList());

            var jogosComPromocao = await _promocaoRepository.ExistemJogosComPromocaoAsync(request.JogosIds, request.Id);

            if (jogosComPromocao)
            {
                _logger.LogError($"Um ou mais jogos já estão em outra promoção.");
                throw new Exception("Um ou mais jogos já estão em outra promoção.");
            }

            promocaoExistente.Atualizar(request.Nome, request.Desconto, request.DataInicio, request.DataFim);

            promocaoExistente.RemoverTodosJogos();
            foreach (var jogo in jogos)
            {
                promocaoExistente.AdicionarJogo(jogo);
            }

            await _promocaoRepository.AtualizarAsync(promocaoExistente);

            _logger.LogInformation($"Promoção {request.Id} atualizada");

            return _mapper.Map<AtualizarPromocaoCommandResponse>(promocaoExistente);   
        }
    }
}
