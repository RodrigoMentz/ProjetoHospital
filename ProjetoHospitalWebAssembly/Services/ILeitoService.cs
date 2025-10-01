namespace ProjetoHospitalWebAssembly.Services
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public interface ILeitoService
    {
        Task<ResponseModel<List<LeitoViewModel>>> GetAsync();

        Task<ResponseModel> CriarAsync(LeitoViewModel leito);

        Task<ResponseModel> AtualizarAsync(LeitoViewModel leito);

        Task<ResponseModel> DeletarAsync(LeitoViewModel leito);


    }
}