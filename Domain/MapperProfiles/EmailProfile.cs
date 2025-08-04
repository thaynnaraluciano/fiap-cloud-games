using AutoMapper;
using Domain.Commands.v1.Notificacao.Email;
using Infrastructure.Data.Models.Notificacao.Email;

namespace Domain.MapperProfiles
{
    public class EmailProfile : Profile
    {
        public EmailProfile()
        {
            CreateMap<EmailModel, EnviarEmailCommand>();
            CreateMap<EnviarEmailCommand, EmailModel>();
        }
    }
}
