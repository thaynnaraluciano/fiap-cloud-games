using AutoMapper;
using Domain.Commands.v1.Adm.AlteraStatusUser;
using Domain.Commands.v1.Login;
using Infrastructure.Data;
using Infrastructure.Data.Models.Adm.AlteraStatusUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    //TO DO: Adicionar policity
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AdmController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AdmController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        [Route(template: "AlteraStatusUser")]
        public async Task<IActionResult> AlteraStatusUser([FromBody] AlteraUserStatusCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
