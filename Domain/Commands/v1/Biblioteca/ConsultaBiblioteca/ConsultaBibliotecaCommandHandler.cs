using AutoMapper;
using Infrastructure.Data.Interfaces.Biblioteca;
using Infrastructure.Data.Models.Biblioteca;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Biblioteca.ConsultaBiblioteca;

public class ConsultaBibliotecaCommandHandler : IRequestHandler<ConsultaBibliotecaCommand, IEnumerable<ConsultaBibliotecaCommandResponse>>
{
    private readonly IBibliotecaRepository _bibliotecaRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ConsultaBibliotecaCommandHandler> _logger;

    public ConsultaBibliotecaCommandHandler(IBibliotecaRepository bibliotecaRepository,
                                            IMapper mapper,
                                            ILogger<ConsultaBibliotecaCommandHandler> logger)
    {
        _bibliotecaRepository = bibliotecaRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<ConsultaBibliotecaCommandResponse>> Handle(ConsultaBibliotecaCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Consultando biblioteca para o usuário ID: {IdUsuario}", request.IdUsuario);

        var bibliotecaPesquisa = _mapper.Map<BibliotecaModel>(request);
        var jogosBiblioteca = await _bibliotecaRepository.BuscaBibliotecaUser(bibliotecaPesquisa);

        _logger.LogInformation("Consulta retornou {Quantidade} jogos.", jogosBiblioteca?.Count() ?? 0);

        return _mapper.Map<IEnumerable<ConsultaBibliotecaCommandResponse>>(jogosBiblioteca);
    }
}