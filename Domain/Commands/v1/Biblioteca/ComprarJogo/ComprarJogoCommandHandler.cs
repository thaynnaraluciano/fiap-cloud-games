using AutoMapper;
using Infrastructure.Data.Interfaces.Biblioteca;
using Infrastructure.Data.Models.Biblioteca;
using MediatR;

namespace Domain.Commands.v1.Biblioteca.ComprarJogo
{
    public class ComprarJogoCommandHandler : IRequestHandler<ComprarJogoCommand, ComprarJogoCommandResponse>
    {
        private readonly IBibliotecaRepository _bibliotecaRepository;
        private readonly IMapper _mapper;

        public ComprarJogoCommandHandler(IBibliotecaRepository bibliotecaRepository, IMapper mapper)
        {
            _bibliotecaRepository = bibliotecaRepository;
            _mapper = mapper;
        }

        public async Task<ComprarJogoCommandResponse> Handle(ComprarJogoCommand request, CancellationToken cancellationToken)
        {
            var bibliotecaCompra = _mapper.Map<BibliotecaModel>(request);

            //Todo: Incluir uma api fake de pagamento para validar a compra

            var resultadoCompra = await _bibliotecaRepository.ComprarJogo(bibliotecaCompra);
            return _mapper.Map<ComprarJogoCommandResponse>(resultadoCompra);
        }
    }
}
