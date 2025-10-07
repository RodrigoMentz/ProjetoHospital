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
                .Cadastrar(cadastro);

            return this.Ok(resultado);
        }
    }
}