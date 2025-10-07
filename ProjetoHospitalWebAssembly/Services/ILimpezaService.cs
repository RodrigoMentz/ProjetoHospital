namespace ProjetoHospitalWebAssembly.Services
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public interface ILimpezaService
    {
        Task<ResponseModel<List<LeitoStatusLimpezaViewModel>>> ConsultarListaStatusLimpezaAsync();

        Task<ResponseModel<List<LimpezaViewModel>>> ConsultarLimpezasDoLeitoAsync(
            LeitoViewModel leito);

        Task<ResponseModel<LimpezaViewModel>> CriarConcorrenteAsync(
            LimpezaConcorrenteViewModel limpezaConcorrente);

        Task<ResponseModel> FinalizarConcorrenteAsync(
            LimpezaConcorrenteViewModel limpeza);

        Task<ResponseModel<LimpezaViewModel>> CriarTerminalAsync(
            LimpezaTerminalViewModel limpezaTerminal);

        Task<ResponseModel> FinalizarTerminalAsync(
            LimpezaTerminalViewModel limpeza);
    }
}