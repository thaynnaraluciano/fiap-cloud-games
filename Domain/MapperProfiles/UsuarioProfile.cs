using AutoMapper;
using Domain.Commands.v1.Adm.ListarUsuarios;
using Domain.Commands.v1.Usuarios.CriarUsuario;
using Infrastructure.Data.Models.Usuarios;
using Domain.Commands.v1.Adm.AtualizarUsuario;
using Domain.Commands.v1.Adm.BuscarUsuarioPorId;
using Domain.Commands.v1.Adm.CadastrarUsuario;

namespace Domain.MapperProfiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioModel, CadastrarUsuarioCommandResponse>();
            CreateMap<UsuarioModel, CriarUsuarioCommandResponse>();
            CreateMap<UsuarioModel, ListarUsuariosCommandResponse>();
            CreateMap<UsuarioModel, AtualizarUsuarioCommandResponse>();
            CreateMap<UsuarioModel, BuscarUsuarioPorIdCommandResponse>();
            CreateMap<CriarUsuarioCommand, UsuarioModel>();
            CreateMap<CadastrarUsuarioCommand, UsuarioModel>();
        }
    }
}
