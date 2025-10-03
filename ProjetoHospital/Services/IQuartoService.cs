using ProjetoHospitalShared;
using ProjetoHospitalShared.ViewModels;

namespace ProjetoHospital.Services
{
    public interface IQuartoService
    {
        Task<ResponseModel<List<QuartoViewModel>>> GetAsync();
        Task<ResponseModel> CriarAsync
            (QuartoViewModel quarto);

        Task<ResponseModel> AtualizarAsync
            (QuartoViewModel quarto);

        Task<ResponseModel> DeletarAsync
            (QuartoViewModel quarto);
    }
}
