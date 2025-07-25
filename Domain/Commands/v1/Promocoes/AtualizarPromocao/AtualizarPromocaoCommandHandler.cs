using AutoMapper;
using Infrastructure.Data.Interfaces.Jogos;
using Infrastructure.Data.Interfaces.Promocoes;
using MediatR;

namespace Domain.Commands.v1.Promocoes.AtualizarPromocao
{
    public class AtualizarPromocaoCommandHandler : IRequestHandler<AtualizarPromocaoCommand, AtualizarPromocaoCommandResponse>
    {
        private readonly IPromocaoRepository _promocaoRepository;
        private readonly IJogoRepository _jogoRepository;
        private readonly IMapper _mapper;

        public AtualizarPromocaoCommandHandler(IPromocaoRepository promocaoRepository, IJogoRepository jogoRepository, IMapper mapper)
        {
            _promocaoRepository = promocaoRepository;
            _jogoRepository = jogoRepository;
            _mapper = mapper;
        }

        public async Task<AtualizarPromocaoCommandResponse> Handle(AtualizarPromocaoCommand request, CancellationToken cancellationToken)
        {
            var promocaoExistente = await _promocaoRepository.BuscarPorIdAsync(request.Id);

            if (promocaoExistente == null)
            {
                throw new Exception("Promoção não encontrada.");
            }

            var jogos = await _jogoRepository.BuscarPorIdsAsync(request.JogosIds.ToList());

            var jogosComPromocao = await _promocaoRepository.ExistemJogosComPromocaoAsync(request.JogosIds, request.Id);

            if (jogosComPromocao)
            {
                throw new Exception("Um ou mais jogos já estão em outra promoção.");
            }

            promocaoExistente.Atualizar(request.Nome, request.Desconto, request.DataInicio, request.DataFim);

            promocaoExistente.RemoverTodosJogos();
            foreach (var jogo in jogos)
            {
                promocaoExistente.AdicionarJogo(jogo);
            }

            await _promocaoRepository.AtualizarAsync(promocaoExistente);

            return _mapper.Map<AtualizarPromocaoCommandResponse>(promocaoExistente);   
        }
    }
}
