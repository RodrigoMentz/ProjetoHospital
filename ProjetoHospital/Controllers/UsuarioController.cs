namespace ProjetoHospital.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ProjetoHospital.Entities;
    using ProjetoHospital.Models;
    using ProjetoHospital.Services;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController(
        IUsuarioService UsuarioService,
        SignInManager<Usuario> signInManager)
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

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(ResponseModel<AcessoViewModel>), 200)]
        public async Task<IActionResult> Login(
            [FromBody] LoginViewModel login)
        {
            var resultado = await UsuarioService
                .LoginAsync(
                    login)
                .ConfigureAwait(true);

            if (resultado.Data == null)
            {
                return this.Ok(resultado);
            }

            return await this.GerarAcesso(resultado.Data);
        }

        [AllowAnonymous]
        private async Task<IActionResult> GerarAcesso(
            AcessoModel acesso)
        {
            this.HttpContext.User = await signInManager
                .CreateUserPrincipalAsync(acesso.Usuario);

            var acessoResponse = new AcessoViewModel(
                acesso.Token,
                acesso.NumTelefone,
                new PerfilViewModel(
                    acesso.Role.Id,
                    acesso.Role.Name),
                acesso.Usuario.Id,
                acesso.Usuario.Nome,
                acesso.NumTelefoneConfirmado);

            return this.Ok(new ResponseModel<AcessoViewModel>(acessoResponse));
        }
    }
}