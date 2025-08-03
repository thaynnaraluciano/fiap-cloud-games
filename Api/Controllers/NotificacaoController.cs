using Domain.Commands.v1.Notificacao.Email;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> EnviarEmail(EnviarEmailCommand command)
        {
            await _mediator.Send(command);
            return Ok("Email de confirmação enviado");
        }
    }
}
