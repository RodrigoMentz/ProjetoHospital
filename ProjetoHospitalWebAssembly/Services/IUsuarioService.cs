namespace ProjetoHospitalWebAssembly.Services
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public interface IUsuarioService
    {
        Task<ResponseModel<List<UsuarioViewModel>>> GetUsuariosAsync();

        Task<ResponseModel<List<PerfilViewModel>>> GetPerfisAsync();

        Task<ResponseModel> CadastrarAsync(
            UsuarioViewModel usuario);

        Task<ResponseModel> AtualizarAsync(
            UsuarioViewModel usuario);

        Task<ResponseModel> ResetarSenhaAsync(
            ResetarSenhaViewModel resetarSenha);
    }
}