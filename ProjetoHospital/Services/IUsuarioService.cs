namespace ProjetoHospital.Services
{
    using ProjetoHospital.Entities;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public interface IUsuarioService
    {
        Task<ResponseModel<List<UsuarioViewModel>>> GetAsync();

        Task<ResponseModel> CadastrarAsync(
            UsuarioViewModel cadastro);

        Task<ResponseModel> AtualizarAsync(
            UsuarioViewModel cadastro);

        Task<ResponseModel<List<PerfilViewModel>>> GetPerfis();

        Task<ResponseModel> ResetarSenhaAsync(
            ResetarSenhaViewModel resetarSenhaViewModel);
    }
}