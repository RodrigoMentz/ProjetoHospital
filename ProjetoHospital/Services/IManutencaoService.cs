namespace ProjetoHospital.Services
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public interface IManutencaoService
    {
        Task<ResponseModel> CriarAsync(
            ManutencaoViewModel manutencao);

        Task<ResponseModel> AtualizarAsync(
            ManutencaoViewModel manutencao);
    }
}