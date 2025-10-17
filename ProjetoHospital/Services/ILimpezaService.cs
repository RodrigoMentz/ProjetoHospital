namespace ProjetoHospital.Services
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public interface ILimpezaService
    {
        Task<ResponseModel<List<LeitoStatusLimpezaViewModel>>> ConsultarListaStatusLimpezaAsync();

        Task<ResponseModel<List<LimpezaViewModel>>> ConsultarLimpezasDoLeito(
            LeitoViewModel leito);

        Task<ResponseModel<List<LimpezaViewModel>>> ConsultarLimpezasRelatorioAsync(
            RelatorioLimpezaRequestModel requestModel);

        Task<ResponseModel<List<LimpezaViewModel>>> ConsultarLimpezasNaoEncerradasDoUsuario(
            UsuarioViewModel usuario);

        Task<ResponseModel<LimpezaViewModel>> CriarLimpezaConcorrenteAsync(
            LimpezaConcorrenteViewModel limpeza);

        Task<ResponseModel> FinalizarLimpezaConcorrenteAsync(
            LimpezaConcorrenteViewModel limpeza);

        Task<ResponseModel<LimpezaConcorrenteViewModel>> ConsultarLimpezaConcorrenteAsync(
           LimpezaViewModel limpeza);

        Task<ResponseModel<LimpezaViewModel>> CriarLimpezaTerminalAsync(
            LimpezaTerminalViewModel limpeza);

        Task<ResponseModel> FinalizarLimpezaTerminalAsync(
            LimpezaTerminalViewModel limpeza);

        Task<ResponseModel<LimpezaTerminalViewModel>> ConsultarLimpezaTerminalAsync(
           LimpezaViewModel limpeza);

        Task<ResponseModel<LimpezaViewModel>> CriarLimpezaEmergencialAsync(
            LimpezaEmergencialViewModel limpeza);

        Task<ResponseModel<LimpezaViewModel>> AtenderLimpezaEmergencialAsync(
            LimpezaEmergencialViewModel limpeza);

        Task<ResponseModel<LimpezaEmergencialViewModel>> ConsultarLimpezaEmergencialAsync(
           LimpezaViewModel limpeza);

        Task<ResponseModel<List<LimpezaEmergencialViewModel>>> ConsultarLimpezasEmergenciais();

        Task<ResponseModel> FinalizarLimpezaEmergencialAsync(
            LimpezaEmergencialViewModel limpeza);
    }
}