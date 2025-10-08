namespace ProjetoHospitalWebAssembly.Services
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public interface IManutencaoService
    {
        Task<ResponseModel<List<ManutencaoViewModel>>> GetAsync();

        Task<ResponseModel> CriarAsync(
            ManutencaoViewModel manutencao);

        Task<ResponseModel> AtualizarAsync(
            ManutencaoViewModel manutencao);
    }
}