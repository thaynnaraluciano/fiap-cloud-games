using AutoMapper;
using CrossCutting.Exceptions;
using Infrastructure.Data.Interfaces.Usuarios;
using Infrastructure.Data.Models.Notificacao.Email;
using Infrastructure.Services.Interfaces.v1;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Notificacao.Email
{
    public class EnviarEmailCommandHandler : IRequestHandler<EnviarEmailCommand, Unit>
    {
        private readonly ILogger<EnviarEmailCommand> _logger;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IEmailTemplateService _emailTemplateService;

        public EnviarEmailCommandHandler(
            ILogger<EnviarEmailCommand> logger,
            IEmailService emailService,
            IMapper mapper,
            IUsuarioRepository userRepository,
            ITokenService tokenService, 
            IEmailTemplateService emailTemplateService)
        {
            _logger = logger;
            _emailService = emailService;
            _mapper = mapper;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _emailTemplateService = emailTemplateService;
        }

        public async Task<Unit> Handle(EnviarEmailCommand command, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Confirming user email");

            var user = _userRepository.ObterPorEmailAsync(command.EmailDestinatario!);

            if (user == null)
            {
                _logger.LogError("User not found at database");
                throw new ExcecaoUsuarioNaoEncontrado("Usuário não encontrado.");
            }

            if (user.ConfirmadoEm.HasValue)
            {
                _logger.LogError("User email is already confirmed");
                throw new ExcecaoBadRequest("Este email já foi validado.");
            }

            var token = _tokenService.GerarToken(command.EmailDestinatario!, "CriarSenha");
            command.Corpo = _emailTemplateService.GerarEmailDeConfirmacao(user.Nome!, token);
            command.TipoCorpo = "html";

            _logger.LogInformation($"Sending email to {command.EmailDestinatario}");

            var request = _mapper.Map<EmailModel>(command);
            await _emailService.EnviarEmail(request);

            _logger.LogInformation($"Email sent to {command.EmailDestinatario}");
            return Unit.Value;
        }
    }
}
