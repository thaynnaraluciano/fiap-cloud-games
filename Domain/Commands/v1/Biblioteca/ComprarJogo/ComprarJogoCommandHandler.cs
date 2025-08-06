using AutoMapper;
using Infrastructure.Data.Interfaces.Biblioteca;
using Infrastructure.Data.Interfaces.Pagamento;
using Infrastructure.Data.Interfaces.Promocoes;
using Infrastructure.Data.Models.Biblioteca;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Biblioteca.ComprarJogo;

public class ComprarJogoCommandHandler : IRequestHandler<ComprarJogoCommand, ComprarJogoCommandResponse>
{
    private readonly IBibliotecaRepository _bibliotecaRepository;
    private readonly IMapper _mapper;
    private readonly IPromocaoRepository _promocaoRepository;
    private readonly IPagamentoService _pagamentoService;
    private readonly ILogger<ComprarJogoCommandHandler> _logger;

    public ComprarJogoCommandHandler(IBibliotecaRepository bibliotecaRepository,
                                     IMapper mapper,
                                     IPromocaoRepository promocaoRepository,
                                     IPagamentoService pagamentoService,
                                     ILogger<ComprarJogoCommandHandler> logger)
    {
        _bibliotecaRepository = bibliotecaRepository;
        _mapper = mapper;
        _promocaoRepository = promocaoRepository;
        _pagamentoService = pagamentoService;
        _logger = logger;
    }

    public async Task<ComprarJogoCommandResponse> Handle(ComprarJogoCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando compra do jogo ID: {IdJogo} pelo usuário ID: {IdUsuario}", request.IdJogo, request.IdUsuario);

        var bibliotecaCompra = _mapper.Map<BibliotecaModel>(request);

        // 1. Buscar promoção ativa
        var promocaoAtiva = await _promocaoRepository.ObterPromocaoAtivaPorJogoAsync(request.IdJogo);
        if (promocaoAtiva != null)
        {
            _logger.LogInformation("Promoção ativa encontrada: {Desconto:P}", promocaoAtiva.Desconto);
        }
        else
        {
            _logger.LogInformation("Nenhuma promoção ativa encontrada para o jogo.");
        }

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
        _logger.LogInformation("Preço final calculado: {PrecoFinal}", bibliotecaCompra.PrecoFinal);

        // 3. Simular pagamento
        _logger.LogInformation("Processando pagamento no valor de {Valor} via {Metodo}", bibliotecaCompra.PrecoFinal, "Cartão de Crédito");
        var pagamentoAprovado = await _pagamentoService
            .ProcessarPagamento(bibliotecaCompra.PrecoFinal, "Cartão de Crédito");

        if (!pagamentoAprovado)
        {
            _logger.LogError("Pagamento não aprovado para o jogo ID: {IdJogo}", request.IdJogo);
            throw new Exception("Pagamento não aprovado pelo gateway.");
        }

        _logger.LogInformation("Pagamento aprovado com sucesso.");

        // 4. Registrar compra
        var resultadoCompra = await _bibliotecaRepository.ComprarJogo(bibliotecaCompra);

        _logger.LogInformation("Compra registrada com sucesso para o jogo ID: {IdJogo}", request.IdJogo);

        return _mapper.Map<ComprarJogoCommandResponse>(resultadoCompra);
    }
}