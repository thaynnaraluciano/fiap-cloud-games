using AutoMapper;
using CrossCutting.Configuration.Authorization;
using Domain.Commands.v1.Adm.AlteraStatusUser;
using Domain.Commands.v1.Login;
using Infrastructure.Data;
using Infrastructure.Data.Models.Adm.AlteraStatusUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Policy = PoliticasDeAcesso.Admin)]
    public class AdmController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AdmController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        [Route(template: "AlteraStatusUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AlteraStatusUser([FromBody] AlteraUserStatusCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
