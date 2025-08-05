using AutoMapper;
using CrossCutting.Exceptions;
using Infrastructure.Data.Interfaces.Usuarios;
using Infrastructure.Services.Interfaces.v1;
using MediatR;

namespace Domain.Commands.v1.Usuarios.CriarSenha
{
    public class CriarSenhaCommandHandler : IRequestHandler<CriarSenhaCommand, Unit>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICriptografiaService _criptografiaService;

        public CriarSenhaCommandHandler(IUsuarioRepository usuarioRepository, ICriptografiaService criptografiaService)
        {
            _usuarioRepository = usuarioRepository;
            _criptografiaService = criptografiaService;
        }

        public async Task<Unit> Handle(CriarSenhaCommand command, CancellationToken cancellationToken)
        {
            var usuario = _usuarioRepository.ObterPorEmailAsync(command.Email);
            
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            if (usuario.ConfirmadoEm.HasValue)
                throw new ExcecaoBadRequest("Este usuário já foi confirmado.");

            usuario.Senha = _criptografiaService.HashSenha(command.Senha);
            usuario.ConfirmadoEm = DateTime.Now;

            await _usuarioRepository.AtualizarAsync(usuario);

            return Unit.Value;
        }
    }
}
