namespace ProjetoHospital.Services
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public interface IManutencaoService
    {
        Task<ResponseModel<List<ManutencaoViewModel>>> GetAsync();

        Task<ResponseModel<ManutencaoViewModel>> GetDetalhesDaManutencaoAsync(
            ManutencaoViewModel manutencao);

        Task<ResponseModel> CriarAsync(
            ManutencaoViewModel manutencao);

        Task<ResponseModel> AtualizarAsync(
            ManutencaoViewModel manutencao);
    }
}