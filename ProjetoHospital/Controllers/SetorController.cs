namespace ProjetoHospital.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SetorController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var setores = new List<SetorViewModel>
            {
                new SetorViewModel(1, "SUS", true),
                new SetorViewModel(2, "Maternidade", true),
                new SetorViewModel(3, "Pediatria", true),
                new SetorViewModel(4, "Emergência", true),
                new SetorViewModel(5, "Sala vermelha", true),
                new SetorViewModel(6, "Bloco cirúrgico", false),
            };

            return this.Ok(setores);
        }

        [HttpPost("adicionar")]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> Criar(
            [FromBody] SetorViewModel setor)
        {
            //var resultado = await this.service
            //    .CriarAsync(setor);

            return this.Ok(); // adicionar resultado
        }
    }
}