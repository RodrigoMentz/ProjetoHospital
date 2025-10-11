namespace ProjetoHospitalWebAssembly.Services
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public interface IRevisaoService
    {
        Task<ResponseModel<RevisaoViewModel>> GetDetalhesDaRevisaoAsync(
            RevisaoViewModel revisao);

        Task<ResponseModel<List<RevisaoViewModel>>> GetRevisoesQueNecessitamLimpezaAsync();

        Task<ResponseModel<List<NecessidadeDeRevisaoViewModel>>> GetRevisoesPendentesAsync();

        Task<ResponseModel<RevisaoViewModel>> CriarAsync(
            RevisaoViewModel revisao);

        Task<ResponseModel> AtualizarAsync(
            RevisaoViewModel revisao);

        Task<ResponseModel> AtenderAsync(
            RevisaoViewModel revisao);

        Task<ResponseModel> FinalizarAsync(
            RevisaoViewModel revisao);

        Task<ResponseModel> DeletarAsync(
            RevisaoViewModel revisao);
    }
}