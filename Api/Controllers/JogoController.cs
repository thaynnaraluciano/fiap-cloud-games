using CrossCutting.Configuration.Authorization;
using Domain.Commands.v1.Jogos.AtualizarJogo;
using Domain.Commands.v1.Jogos.BuscarJogo;
using Domain.Commands.v1.Jogos.ListarJogos;
using Domain.Commands.v1.Jogos.CriarJogo;
using Domain.Commands.v1.Jogos.RemoverJogo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/jogo")]
    public class JogoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JogoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Policy = PoliticasDeAcesso.Usuario)]
        public async Task<IActionResult> BuscarTodos()
        {
            var query = new ListarJogosCommand();
            var jogos = await _mediator.Send(query);

            return Ok(jogos);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PoliticasDeAcesso.Usuario)]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            var query = new BuscarJogoPorIdCommand(id);
            var jogo = await _mediator.Send(query);
            return Ok(jogo);
        }

        [HttpPost]
        [Authorize(Policy = PoliticasDeAcesso.Admin)]
        public async Task<IActionResult> Criar([FromBody] CriarJogoCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = PoliticasDeAcesso.Admin)]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarJogoCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID do corpo e da URL não coincidem.");

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = PoliticasDeAcesso.Admin)]
        public async Task<ActionResult> Remover(Guid id)
        {
            var command = new RemoverJogoCommand(id);
            await _mediator.Send(command);
            return NoContent(); 
        }
    }
}
