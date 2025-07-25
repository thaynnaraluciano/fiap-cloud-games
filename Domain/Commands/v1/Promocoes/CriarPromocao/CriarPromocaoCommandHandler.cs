using AutoMapper;
using Infrastructure.Data.Interfaces.Jogos;
using Infrastructure.Data.Interfaces.Promocoes;
using Infrastructure.Data.Models.Promocao;
using MediatR;

namespace Domain.Commands.v1.Promocoes.CriarPromocao
{
    public class CriarPromocaoCommandHandler : IRequestHandler<CriarPromocaoCommand, CriarPromocaoCommandResponse>
    {
        private readonly IPromocaoRepository _promocaoRepository;
        private readonly IMapper _mapper;
        private readonly IJogoRepository _jogoRepository;

        public CriarPromocaoCommandHandler(IPromocaoRepository promocaoRepository, IMapper mapper, IJogoRepository jogoRepository)
        {
            _promocaoRepository = promocaoRepository;
            _mapper = mapper;
            _jogoRepository = jogoRepository;
        }

        public async Task<CriarPromocaoCommandResponse> Handle(CriarPromocaoCommand request, CancellationToken cancellationToken)
        {
            var jogos = await _jogoRepository.BuscarPorIdsAsync(request.JogosIds.ToList());

            var jogosComPromocao = await _promocaoRepository.ExistemJogosComPromocaoAsync(request.JogosIds);

            if (jogosComPromocao)
                throw new Exception("Um ou mais jogos já estão em outra promoção.");

            var promocao = _mapper.Map<PromocaoModel>(request);

            foreach (var jogo in jogos)
            {
                promocao.AdicionarJogo(jogo);
            }

            await _promocaoRepository.AdicionarAsync(promocao);
            return _mapper.Map<CriarPromocaoCommandResponse>(promocao);
        }
    }
}
