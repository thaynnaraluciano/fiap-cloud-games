using AutoMapper;
using Infrastructure.Data.Interfaces.Biblioteca;
using Infrastructure.Data.Interfaces.Pagamento;
using Infrastructure.Data.Interfaces.Promocoes;
using Infrastructure.Data.Models.Biblioteca;
using MediatR;

namespace Domain.Commands.v1.Biblioteca.ComprarJogo;

public class ComprarJogoCommandHandler : IRequestHandler<ComprarJogoCommand, ComprarJogoCommandResponse>
{
    private readonly IBibliotecaRepository _bibliotecaRepository;
    private readonly IMapper _mapper;
    private readonly IPromocaoRepository _promocaoRepository;
    private readonly IPagamentoService _pagamentoService;

    public ComprarJogoCommandHandler(IBibliotecaRepository bibliotecaRepository, 
                                     IMapper mapper,
                                     IPromocaoRepository promocaoRepository,
                                     IPagamentoService pagamentoService)
    {
        _bibliotecaRepository = bibliotecaRepository;
        _mapper = mapper;
        _promocaoRepository = promocaoRepository;
        _pagamentoService = pagamentoService;
    }

    public async Task<ComprarJogoCommandResponse> Handle(ComprarJogoCommand request, CancellationToken cancellationToken)
    {
        var bibliotecaCompra = _mapper.Map<BibliotecaModel>(request);

        // 1. Buscar promoção ativa
        var promocaoAtiva = await _promocaoRepository.ObterPromocaoAtivaPorJogoAsync(request.IdJogo);

        // 2. Calcular preços
        bibliotecaCompra.PrecoOriginal = request.Preco;

        if (promocaoAtiva != null)
        {
            var descontoValor = request.Preco * promocaoAtiva.Desconto;
            bibliotecaCompra.PrecoFinal = request.Preco - descontoValor;
        }
        else
        {
            bibliotecaCompra.PrecoFinal = request.Preco;
        }

        // 3. Simular pagamento
        var pagamentoAprovado = await _pagamentoService
            .ProcessarPagamento(bibliotecaCompra.PrecoFinal, "Cartão de Crédito");

        if (!pagamentoAprovado)
            throw new Exception("Pagamento não aprovado pelo gateway.");

        // 4. Registrar compra
        var resultadoCompra = await _bibliotecaRepository.ComprarJogo(bibliotecaCompra);

        return _mapper.Map<ComprarJogoCommandResponse>(resultadoCompra);
    }
}
