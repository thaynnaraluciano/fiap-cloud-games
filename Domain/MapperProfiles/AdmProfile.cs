using AutoMapper;
using Domain.Commands.v1.Adm.AlteraStatusUser;
using Infrastructure.Data.Models;
using Infrastructure.Data.Models.Adm.AlteraStatusUser;

namespace Domain.MapperProfiles
{
    public class AdmProfile:Profile
    {
        public AdmProfile()
        {
            CreateMap<AlteraUserStatusCommand, AlteraStatusUserModel>();
            CreateMap<AlteraStatusUserModel, AlteraUserStatusCommand>();
            CreateMap<Boolean, AlteraUserStatusCommandResponse>()
                .ForMember(dest => dest.EstaAtivo, opt => opt.MapFrom(src => src));
        }

    }
}
