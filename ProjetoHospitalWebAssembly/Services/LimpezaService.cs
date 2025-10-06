namespace ProjetoHospitalWebAssembly.Services
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services.Http;

    public class LimpezaService(
        IHttpService httpService)
        : ILimpezaService
    {
        public async Task<ResponseModel<List<LeitoStatusLimpezaViewModel>>> ConsultarListaStatusLimpezaAsync()
        {
            var url = "Limpeza/status-limpeza";

            var response = await httpService
                .GetJsonAsync<ResponseModel<List<LeitoStatusLimpezaViewModel>>>(url)
                .ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseModel<LimpezaViewModel>> CriarConcorrenteAsync(
            LimpezaConcorrenteViewModel limpezaConcorrente)
        {
            var url = $"Limpeza/adicionar/concorrente";

            var response = await httpService
                .PostJsonAsync<LimpezaConcorrenteViewModel, ResponseModel<LimpezaViewModel>>(
                    url,
                    limpezaConcorrente)
                .ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseModel> FinalizarConcorrenteAsync(
            LimpezaConcorrenteViewModel limpeza)
        {
            var url = $"Limpeza/finalizar/concorrente";

            var response = await httpService
                 .PutJsonAsync<LimpezaConcorrenteViewModel, ResponseModel>(
                     url,
                     limpeza)
                 .ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseModel<LimpezaViewModel>> CriarTerminalAsync(
            LimpezaTerminalViewModel limpezaTerminal)
        {
            var url = $"Limpeza/adicionar/terminal";

            var response = await httpService
                .PostJsonAsync<LimpezaTerminalViewModel, ResponseModel<LimpezaViewModel>>(
                    url,
                    limpezaTerminal)
                .ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseModel> FinalizarTerminalAsync(
            LimpezaTerminalViewModel limpeza)
        {
            var url = $"Limpeza/finalizar/terminal";

            var response = await httpService
                 .PutJsonAsync<LimpezaTerminalViewModel, ResponseModel>(
                     url,
                     limpeza)
                 .ConfigureAwait(false);

            return response;
        }
    }
}