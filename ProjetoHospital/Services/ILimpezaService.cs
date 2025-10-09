namespace ProjetoHospital.Services
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public interface ILimpezaService
    {
        Task<ResponseModel<List<LeitoStatusLimpezaViewModel>>> ConsultarListaStatusLimpezaAsync();

        Task<ResponseModel<List<LimpezaViewModel>>> ConsultarLimpezasDoLeito(
            LeitoViewModel leito);

        Task<ResponseModel<List<LimpezaViewModel>>> ConsultarLimpezasNaoEncerradasDoUsuario(
            UsuarioViewModel usuario);

        Task<ResponseModel<LimpezaViewModel>> CriarLimpezaConcorrenteAsync(
            LimpezaConcorrenteViewModel limpeza);

        Task<ResponseModel> FinalizarLimpezaConcorrenteAsync(
            LimpezaConcorrenteViewModel limpeza);

        Task<ResponseModel<LimpezaViewModel>> CriarLimpezaTerminalAsync(
            LimpezaTerminalViewModel limpeza);

        Task<ResponseModel> FinalizarLimpezaTerminalAsync(
            LimpezaTerminalViewModel limpeza);
    }
}