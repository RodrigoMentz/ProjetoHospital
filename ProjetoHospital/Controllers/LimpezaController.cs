namespace ProjetoHospital.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ProjetoHospital.Services;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LimpezaController(
        ILimpezaService limpezaService)
        : ControllerBase
    {
        [HttpGet("status-limpeza")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<List<LeitoStatusLimpezaViewModel>>), 200)]
        public async Task<IActionResult> ConsultarListaStatusLimpeza()
        {
            var resultado = await limpezaService
                .ConsultarListaStatusLimpezaAsync()
                .ConfigureAwait(false);

            return Ok(resultado);
        }

        [HttpPost("adicionar/concorrente")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<LimpezaViewModel>), 200)]
        public async Task<IActionResult> CriarLimpezaConcorrente(
            [FromBody] LimpezaConcorrenteViewModel limpeza)
        {
            var resultado = await limpezaService
                .CriarLimpezaConcorrenteAsync(limpeza);

            return this.Ok(resultado);
        }

        [HttpPut("finalizar/concorrente")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> FinalizarConcorrente(
            [FromBody] LimpezaConcorrenteViewModel limpeza)
        {
            var resultado = await limpezaService
                .FinalizarLimpezaConcorrenteAsync(limpeza);

            return this.Ok(resultado);
        }

        [HttpPost("adicionar/terminal")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<LimpezaViewModel>), 200)]
        public async Task<IActionResult> CriarLimpezaTerminal(
            [FromBody] LimpezaTerminalViewModel limpeza)
        {
           var resultado = await limpezaService
                .CriarLimpezaTerminalAsync(limpeza);

            return this.Ok(resultado);
        }

        [HttpPut("finalizar/terminal")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> FinalizarTerminal(
            [FromBody] LimpezaTerminalViewModel limpeza)
        {
            var resultado = await limpezaService
                .FinalizarLimpezaTerminalAsync(limpeza);

            return this.Ok(resultado);
        }
    }
}