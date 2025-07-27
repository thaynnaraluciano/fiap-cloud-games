using AutoMapper;
using Infrastructure.Data.Interfaces.Promocoes;
using MediatR;

namespace Domain.Commands.v1.Promocoes.ListarPromocoes
{
    public class ListarPromocoesCommandHandler : IRequestHandler<ListarPromocoesCommand, IEnumerable<ListarPromocoesCommandResponse>>
    {
        private readonly IPromocaoRepository _promocaoRepository;
        private readonly IMapper _mapper;

        public ListarPromocoesCommandHandler(IPromocaoRepository promocaoRepository, IMapper mapper)
        {
            _promocaoRepository = promocaoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ListarPromocoesCommandResponse>> Handle(ListarPromocoesCommand request, CancellationToken cancellationToken)
        {
            var promocoes = await _promocaoRepository.ObterTodosAsync();

            return _mapper.Map<IEnumerable<ListarPromocoesCommandResponse>>(promocoes);
        }
    }
}
