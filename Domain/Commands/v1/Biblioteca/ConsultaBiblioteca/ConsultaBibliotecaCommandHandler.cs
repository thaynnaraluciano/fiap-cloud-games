using AutoMapper;
using Infrastructure.Data.Interfaces.Biblioteca;
using Infrastructure.Data.Models.Biblioteca;
using MediatR;

namespace Domain.Commands.v1.Biblioteca.ConsultaBiblioteca;

public class ConsultaBibliotecaCommandHandler : IRequestHandler<ConsultaBibliotecaCommand, IEnumerable<ConsultaBibliotecaCommandResponse>>
{
    private readonly IBibliotecaRepository _bibliotecaRepository;
    private readonly IMapper _mapper;

    public ConsultaBibliotecaCommandHandler(IBibliotecaRepository bibliotecaRepository, IMapper mapper)
    {
        _bibliotecaRepository = bibliotecaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ConsultaBibliotecaCommandResponse>> Handle(ConsultaBibliotecaCommand request, CancellationToken cancellationToken)
    {
        var bibliotecaPesquisa = _mapper.Map<BibliotecaModel>(request);
        var jogosBiblioteca = await _bibliotecaRepository.BuscaBibliotecaUser(bibliotecaPesquisa);
        return _mapper.Map<IEnumerable<ConsultaBibliotecaCommandResponse>>(jogosBiblioteca);
    }
}