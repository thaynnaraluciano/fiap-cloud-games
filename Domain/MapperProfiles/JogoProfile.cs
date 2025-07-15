using AutoMapper;
using Domain.Commands.v1.Jogos.JogoResponses;
using Infrastructure.Data.Models.Jogos;

namespace Domain.MapperProfiles
{
    public class JogoProfile : Profile
    {
        public JogoProfile()
        {
            CreateMap<JogoModel, ListarJogoCommandResponse>();
        }
    }
}
