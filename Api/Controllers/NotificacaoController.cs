using Domain.Commands.v1.Notificacao.Email;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/notificacao")]
    public class NotificacaoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificacaoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(
            Summary = "Dispara e-mail com token de criação de senha",
            Description = "Caso o usuário já esteja cadastrado, este endpoint envia um e-mail com o token necessário para criação da sua primeira senha."
        )]
        public async Task<IActionResult> EnviarEmail(EnviarEmailCommand command)
        {
            await _mediator.Send(command);
            return Ok("Email de confirmação enviado");
        }
    }
}
