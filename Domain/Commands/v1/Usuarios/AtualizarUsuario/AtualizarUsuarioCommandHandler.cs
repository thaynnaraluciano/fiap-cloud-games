using AutoMapper;
using Infrastructure.Data.Interfaces.Usuarios;
using Infrastructure.Data.Models.Usuarios;
using MediatR;
using System.Linq;

namespace Domain.Commands.v1.Usuarios.AtualizarUsuario
{
    public class AtualizarUsuarioCommandHandler : IRequestHandler<AtualizarUsuarioCommand, AtualizarUsuarioCommandResponse>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public AtualizarUsuarioCommandHandler(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<AtualizarUsuarioCommandResponse> Handle(AtualizarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuarios = await _usuarioRepository.ObterTodosAsync();

            if (!usuarios.Any(x => x.Id == request.Id))
            {
                throw new Exception($"Usuário com ID {request.Id} não encontrado.");
            }

            if (usuarios.Any(x => x.Email == request.Email && x.Id != request.Id))
            {
                throw new Exception("Já existe um usuário criado para o e-mail fornecido");
            }

            var usuario = _mapper.Map<UsuarioModel>(request);

            usuario.Atualizar(request.Nome, request.Email, (int)request.PerfilUsuario);

            await _usuarioRepository.AtualizarAsync(usuario);

            return _mapper.Map<AtualizarUsuarioCommandResponse>(usuario);
        }
    }
}
