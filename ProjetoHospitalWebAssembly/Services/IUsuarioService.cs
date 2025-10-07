namespace ProjetoHospitalWebAssembly.Services
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public interface IUsuarioService
    {
        Task<ResponseModel<List<PerfilViewModel>>> GetPerfisAsync();

        Task<ResponseModel> CadastrarAsync(
            UsuarioViewModel usuario);
    }
}