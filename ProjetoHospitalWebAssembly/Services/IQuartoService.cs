namespace ProjetoHospitalWebAssembly.Services
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public interface IQuartoService
    {
        Task<ResponseModel<List<QuartoViewModel>>> GetAsync();

        Task<ResponseModel> CriarAsync(QuartoViewModel quarto);

        Task<ResponseModel> AtualizarAsync(QuartoViewModel quarto);

        Task<ResponseModel> DeletarAsync(QuartoViewModel quarto);
    }
}