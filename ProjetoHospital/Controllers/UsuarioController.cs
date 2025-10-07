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
    public class UsuarioController(
        IUsuarioService UsuarioService)
        : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<List<UsuarioViewModel>>), 200)]
        public async Task<IActionResult> GetUsuarios()
        {
            var resultado = await UsuarioService
                .GetAsync()
                .ConfigureAwait(false);

            return Ok(resultado);
        }

        [HttpGet("perfis")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<List<PerfilViewModel>>), 200)]
        public async Task<IActionResult> GetPerfis()
        {
            var resultado = await UsuarioService
                .GetPerfis();

            return this.Ok(resultado);
        }

        [HttpPost("cadastrar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> Cadastrar(
            [FromBody] UsuarioViewModel cadastro)
        {
            var resultado = await UsuarioService
                .CadastrarAsync(cadastro);

            return this.Ok(resultado);
        }

        [HttpPost("atualizar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> Atualizar(
            [FromBody] UsuarioViewModel cadastro)
        {
            var resultado = await UsuarioService
                .AtualizarAsync(cadastro);

            return this.Ok(resultado);
        }

        [HttpPost("resetar-senha")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> ResetarSenhaAsyncV2(
        [FromBody] ResetarSenhaViewModel resetarSenhaViewModel)
        {
            var resultado = await UsuarioService
                .ResetarSenhaAsync(resetarSenhaViewModel)
                .ConfigureAwait(false);

            return Ok(resultado);
        }
    }
}