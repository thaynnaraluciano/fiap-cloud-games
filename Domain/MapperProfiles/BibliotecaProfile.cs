using AutoMapper;
using Domain.Commands.v1.Biblioteca.ComprarJogo;
using Domain.Commands.v1.Biblioteca.ConsultaBiblioteca;
using Infrastructure.Data.Models.Biblioteca;
using Infrastructure.Data.Models.Jogos;

namespace Domain.MapperProfiles;

public class BibliotecaProfile : Profile
{
    public BibliotecaProfile()
    {
        // Consulta
        CreateMap<ConsultaBibliotecaCommand, BibliotecaModel>();
        CreateMap<BibliotecaModel, ConsultaBibliotecaCommandResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Jogo.Id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Jogo.Nome))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Jogo.Descricao))
                .ForMember(dest => dest.DataLancamento, opt => opt.MapFrom(src => src.Jogo.DataLancamento))
                .ForMember(dest => dest.DtAdquirido, opt => opt.MapFrom(src => src.DtAdquirido))
                .ForMember(dest => dest.PrecoOriginal, opt => opt.MapFrom(src => src.PrecoOriginal))
                .ForMember(dest => dest.PrecoFinal, opt => opt.MapFrom(src => src.PrecoFinal));

        // Compra
        CreateMap<ComprarJogoCommand, BibliotecaModel>();
        CreateMap<ResultadoCompraModel, ComprarJogoCommandResponse>();

    }
}
