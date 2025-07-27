using AutoMapper;
using Domain.Commands.v1.Biblioteca.ConsultaBiblioteca;
using Infrastructure.Data.Models.Biblioteca;
using Infrastructure.Data.Models.Jogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MapperProfiles
{
    public class BibliotecaProfile:Profile
    {
        public BibliotecaProfile()
        {
            CreateMap<ConsultaBibliotecaCommand, BibliotecaModel>();
            CreateMap<JogoModel, ConsultaBibliotecaCommandResponse>();
        }

    }
}
