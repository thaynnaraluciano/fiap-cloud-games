using AutoMapper;
using Infrastructure.Data.Interfaces.Jogos;
using Infrastructure.Data.Interfaces.Promocoes;
using Infrastructure.Data.Models.Promocao;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Promocoes.CriarPromocao
{
    public class CriarPromocaoCommandHandler : IRequestHandler<CriarPromocaoCommand, CriarPromocaoCommandResponse>
    {
        private readonly IPromocaoRepository _promocaoRepository;
        private readonly IMapper _mapper;
        private readonly IJogoRepository _jogoRepository;
        private readonly ILogger<CriarPromocaoCommandHandler> _logger;

        public CriarPromocaoCommandHandler(
            IPromocaoRepository promocaoRepository, 
            IMapper mapper, 
            IJogoRepository jogoRepository, 
            ILogger<CriarPromocaoCommandHandler> logger)
        {
            _promocaoRepository = promocaoRepository;
            _mapper = mapper;
            _jogoRepository = jogoRepository;
            _logger = logger;
        }

        public async Task<CriarPromocaoCommandResponse> Handle(CriarPromocaoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Criando nova promoção");

            var jogos = await _jogoRepository.BuscarPorIdsAsync(request.JogosIds.ToList());

            var jogosComPromocao = await _promocaoRepository.ExistemJogosComPromocaoAsync(request.JogosIds);

            if (jogosComPromocao)
            {
                _logger.LogError("Um ou mais jogos já estão em outra promoção.");
                throw new Exception("Um ou mais jogos já estão em outra promoção.");
            }

            var promocao = _mapper.Map<PromocaoModel>(request);

            _logger.LogInformation("Adicionando jogos à promoção");
            foreach (var jogo in jogos)
            {
                promocao.AdicionarJogo(jogo);
            }

            await _promocaoRepository.AdicionarAsync(promocao);

            _logger.LogInformation("Promoção criada com sucesso");
            return _mapper.Map<CriarPromocaoCommandResponse>(promocao);
        }
    }
}
