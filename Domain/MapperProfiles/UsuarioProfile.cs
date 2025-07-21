using AutoMapper;
using Domain.Commands.v1.Usuarios.ListarUsuarios;
using Domain.Commands.v1.Usuarios.CriarUsuario;
using Infrastructure.Data.Models.Usuarios;
using Domain.Commands.v1.Usuarios.AtualizarUsuario;
using Domain.Commands.v1.Usuarios.BuscarUsuarioPorId;

namespace Domain.MapperProfiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioModel, CriarUsuarioCommandResponse>();
            CreateMap<UsuarioModel, ListarUsuariosCommandResponse>();
            CreateMap<UsuarioModel, AtualizarUsuarioCommandResponse>();
            CreateMap<UsuarioModel, BuscarUsuarioPorIdCommandResponse>();
            CreateMap<CriarUsuarioCommand, UsuarioModel>();
        }
    }
}
