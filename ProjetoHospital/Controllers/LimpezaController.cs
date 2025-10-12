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

        [HttpPost("limpezas-do-leito")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<List<LimpezaViewModel>>), 200)]
        public async Task<IActionResult> ConsultarLimpezasDoleito(
            [FromBody] LeitoViewModel leito)
        {
            var resultado = await limpezaService
                .ConsultarLimpezasDoLeito(leito)
                .ConfigureAwait(false);

            return Ok(resultado);
        }

        [HttpPost("limpezas-nao-terminadas-do-usuario")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<List<LimpezaViewModel>>), 200)]
        public async Task<IActionResult> ConsultarLimpezasNaoEncerradasDoUsuario(
            [FromBody] UsuarioViewModel usuario)
        {
            var resultado = await limpezaService
                .ConsultarLimpezasNaoEncerradasDoUsuario(usuario)
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

        [HttpPost("consultar/concorrente")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<LimpezaConcorrenteViewModel>), 200)]
        public async Task<IActionResult> ConsultarLimpezaConcorrente(
            [FromBody] LimpezaViewModel limpeza)
        {
            var resultado = await limpezaService
                .ConsultarLimpezaConcorrenteAsync(limpeza);

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

        [HttpPost("consultar/terminal")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<LimpezaTerminalViewModel>), 200)]
        public async Task<IActionResult> ConsultarLimpezaTerminal(
            [FromBody] LimpezaViewModel limpeza)
        {
            var resultado = await limpezaService
                .ConsultarLimpezaTerminalAsync(limpeza);

            return this.Ok(resultado);
        }

        [HttpPost("adicionar/emergencial")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<LimpezaViewModel>), 200)]
        public async Task<IActionResult> CriarLimpezaEmergencial(
            [FromBody] LimpezaEmergencialViewModel limpeza)
        {
            var resultado = await limpezaService
                 .CriarLimpezaEmergencialAsync(limpeza);

            return this.Ok(resultado);
        }

        [HttpPost("atender/emergencial")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<LimpezaViewModel>), 200)]
        public async Task<IActionResult> AtenderLimpezaEmergencialAsync(
            [FromBody] LimpezaEmergencialViewModel limpeza)
        {
            var resultado = await limpezaService
                 .AtenderLimpezaEmergencialAsync(limpeza);

            return this.Ok(resultado);
        }

        [HttpPost("consultar/emergencial")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<LimpezaEmergencialViewModel>), 200)]
        public async Task<IActionResult> ConsultarLimpezaEmergencial(
            [FromBody] LimpezaViewModel limpeza)
        {
            var resultado = await limpezaService
                .ConsultarLimpezaEmergencialAsync(limpeza);

            return this.Ok(resultado);
        }

        [HttpGet("consultar/limpezas-emergenciais")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<List<LimpezaEmergencialViewModel>>), 200)]
        public async Task<IActionResult> ConsultarLimpezasEmergenciaisAsync()
        {
            var resultado = await limpezaService
                .ConsultarLimpezasEmergenciais();

            return this.Ok(resultado);
        }

        [HttpPut("finalizar/emergencial")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> FinalizarEmergencial(
            [FromBody] LimpezaEmergencialViewModel limpeza)
        {
            var resultado = await limpezaService
                .FinalizarLimpezaEmergencialAsync(limpeza);

            return this.Ok(resultado);
        }
    }
}