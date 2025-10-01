namespace ProjetoHospitalWebAssembly.Services
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public interface ISetorService
    {
        Task<ResponseModel<List<SetorViewModel>>> GetAsync();

        Task<ResponseModel> CriarAsync(SetorViewModel setor);

        Task<ResponseModel> AtualizarAsync(SetorViewModel setor);

        Task<ResponseModel> DeletarAsync(SetorViewModel setor);
    }
}