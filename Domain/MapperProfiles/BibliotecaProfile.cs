using AutoMapper;
using Domain.Commands.v1.Biblioteca.ComprarJogo;
using Domain.Commands.v1.Biblioteca.ConsultaBiblioteca;
using Infrastructure.Data.Models.Biblioteca;
using Infrastructure.Data.Models.Jogos;

namespace Domain.MapperProfiles;

public class BibliotecaProfile:Profile
{
    public BibliotecaProfile()
    {
        CreateMap<ConsultaBibliotecaCommand, BibliotecaModel>();
        CreateMap<JogoModel, ConsultaBibliotecaCommandResponse>();
        CreateMap<ComprarJogoCommand, BibliotecaModel>();
        CreateMap<ResultadoCompraModel, ComprarJogoCommandResponse>();            
    }
}
