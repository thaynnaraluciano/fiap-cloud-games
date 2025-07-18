using Domain.Commands.v1.Login;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/login")]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        private AppDbContext context;

        public LoginController(IMediator mediator, AppDbContext context)
        {
            _mediator = mediator;
            this.context = context;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

    }
}
