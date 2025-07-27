using AutoMapper;
using Domain.Commands.v1.Adm.AlteraStatusUser;
using Infrastructure.Data.Interfaces.Adm;
using Infrastructure.Data.Interfaces.Biblioteca;
using Infrastructure.Data.Models.Adm.AlteraStatusUser;
using Infrastructure.Data.Models.Biblioteca;
using Infrastructure.Data.Models.Jogos;
using Infrastructure.Data.Repositories.Adm;
using Infrastructure.Data.Repositories.Biblioteca;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.v1.Biblioteca.ConsultaBiblioteca
{
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
}
