using AutoMapper;
using Infrastructure.Data.Interfaces.Promocoes;
using MediatR;

namespace Domain.Commands.v1.Promocoes.BuscarPromocaoPorId
{
    public class BuscarPromocaoPorIdCommandHandler : IRequestHandler<BuscarPromocaoPorIdCommand, BuscarPromocaoPorIdCommandResponse>
    {
        private readonly IPromocaoRepository _promocaoRepository;
        private readonly IMapper _mapper;

        public BuscarPromocaoPorIdCommandHandler(IPromocaoRepository promocaoRepository, IMapper mapper)
        {
            _promocaoRepository = promocaoRepository;
            _mapper = mapper;
        }

        public async Task<BuscarPromocaoPorIdCommandResponse> Handle(BuscarPromocaoPorIdCommand request, CancellationToken cancellationToken)
        {
            var promocao = await _promocaoRepository.BuscarPorIdAsync(request.Id);

            if (promocao == null)
            {
                throw new KeyNotFoundException($"Promoção com ID {request.Id} não encontrada.");
            }

            return _mapper.Map<BuscarPromocaoPorIdCommandResponse>(promocao);
        }
    }
}
