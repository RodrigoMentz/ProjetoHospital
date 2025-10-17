namespace ProjetoHospitalWebAssembly.Services
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public interface ILimpezaService
    {
        Task<ResponseModel<List<LeitoStatusLimpezaViewModel>>> ConsultarListaStatusLimpezaAsync();

        Task<ResponseModel<List<LimpezaViewModel>>> ConsultarLimpezasDoLeitoAsync(
            LeitoViewModel leito);

        Task<ResponseModel<List<LimpezaViewModel>>> ConsultarRelatorioLimpezasAsync(
            RelatorioLimpezaRequestModel requestModel);

        Task<ResponseModel<List<LimpezaViewModel>>> ConsultarLimpezasNaoEncerradasDoUsuario(
            UsuarioViewModel usuario);

        Task<ResponseModel<LimpezaViewModel>> CriarConcorrenteAsync(
            LimpezaConcorrenteViewModel limpezaConcorrente);

        Task<ResponseModel> FinalizarConcorrenteAsync(
            LimpezaConcorrenteViewModel limpeza);

        Task<ResponseModel<LimpezaConcorrenteViewModel>> ConsultarConcorrenteAsync(
            LimpezaViewModel limpeza);

        Task<ResponseModel<LimpezaViewModel>> CriarTerminalAsync(
            LimpezaTerminalViewModel limpezaTerminal);

        Task<ResponseModel> FinalizarTerminalAsync(
            LimpezaTerminalViewModel limpeza);

        Task<ResponseModel<LimpezaTerminalViewModel>> ConsultarTerminalAsync(
            LimpezaViewModel limpeza);

        Task<ResponseModel<LimpezaViewModel>> CriarEmergencialAsync(
            LimpezaEmergencialViewModel limpezaEmergencial);

        Task<ResponseModel<LimpezaViewModel>> AtenderEmergencialAsync(
            LimpezaEmergencialViewModel limpezaEmergencial);

        Task<ResponseModel<LimpezaEmergencialViewModel>> ConsultarEmergencialAsync(
            LimpezaViewModel limpeza);

        Task<ResponseModel<List<LimpezaEmergencialViewModel>>> ConsultarLimpezasEmergenciaisAsync();

        Task<ResponseModel> FinalizarEmergencialAsync(
            LimpezaEmergencialViewModel limpeza);
    }
}