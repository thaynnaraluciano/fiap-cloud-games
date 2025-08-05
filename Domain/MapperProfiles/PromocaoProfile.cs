using AutoMapper;
using Domain.Commands.v1.Promocoes.AtualizarPromocao;
using Domain.Commands.v1.Promocoes.BuscarPromocaoPorId;
using Domain.Commands.v1.Promocoes.CriarPromocao;
using Domain.Commands.v1.Promocoes.ListarPromocoes;
using Infrastructure.Data.Models.Promocao;

namespace Domain.MapperProfiles
{
    public class PromocaoProfile : Profile
    {
        public PromocaoProfile()
        {
            CreateMap<PromocaoModel, CriarPromocaoCommandResponse>()
                .ForMember(dest => dest.JogosIds, opt => opt.MapFrom(src =>
                    src.PromocaoJogos.Select(pj => pj.JogoId)));

            CreateMap<CriarPromocaoCommand, PromocaoModel>()
            .ForCtorParam("nome", opt => opt.MapFrom(src => src.Nome))
            .ForCtorParam("desconto", opt => opt.MapFrom(src => src.Desconto))
            .ForCtorParam("dataInicio", opt => opt.MapFrom(src => src.DataInicio))
            .ForCtorParam("dataFim", opt => opt.MapFrom(src => src.DataFim));

            CreateMap<PromocaoModel, AtualizarPromocaoCommandResponse>()
                .ForMember(dest => dest.JogosIds, opt => opt.MapFrom(src =>
                    src.PromocaoJogos.Select(pj => pj.JogoId)));

            CreateMap<PromocaoModel, ListarPromocoesCommandResponse>()
            .ForMember(dest => dest.JogosIds, opt => opt.MapFrom(src =>
                src.PromocaoJogos.Select(pj => pj.JogoId)));

            CreateMap<PromocaoModel, BuscarPromocaoPorIdCommandResponse>()
                .ForMember(dest => dest.JogosIds, opt => opt.MapFrom(src =>
                    src.PromocaoJogos.Select(pj => pj.JogoId)));
        }
    }
}
