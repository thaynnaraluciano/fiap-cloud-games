using Domain.Commands.v1.Adm.AlteraStatusUser;
using Domain.Commands.v1.Biblioteca.ComprarJogo;
using Domain.Commands.v1.Biblioteca.ConsultaBiblioteca;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/biblioteca")]
    public class BibliotecaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BibliotecaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{idUser}")]
        public async Task<IActionResult> ConsultaBiblioteca(Guid idUser)
        {
            var command = new ConsultaBibliotecaCommand(idUser);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [Route(template: "ComprarJogo")]
        public async Task<IActionResult> ComprarJogo([FromBody]ComprarJogoCommand compra)
        {
            var result = await _mediator.Send(compra);
            return Ok(result);
        }
    }
}
