using AutoMapper;
using Infrastructure.Data.Interfaces.Promocoes;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Promocoes.ListarPromocoes
{
    public class ListarPromocoesCommandHandler : IRequestHandler<ListarPromocoesCommand, IEnumerable<ListarPromocoesCommandResponse>>
    {
        private readonly IPromocaoRepository _promocaoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ListarPromocoesCommandHandler> _logger;

        public ListarPromocoesCommandHandler(
            IPromocaoRepository promocaoRepository, 
            IMapper mapper, 
            ILogger<ListarPromocoesCommandHandler> logger)
        {
            _promocaoRepository = promocaoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ListarPromocoesCommandResponse>> Handle(ListarPromocoesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Listando promoções");
            var promocoes = await _promocaoRepository.ObterTodosAsync();

            return _mapper.Map<IEnumerable<ListarPromocoesCommandResponse>>(promocoes);
        }
    }
}
