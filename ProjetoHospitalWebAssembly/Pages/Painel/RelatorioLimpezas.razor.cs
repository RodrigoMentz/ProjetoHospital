namespace ProjetoHospitalWebAssembly.Pages.Painel
{
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class RelatorioLimpezas
    {
        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private ILeitoService LeitoService { get; set; }

        [Inject]
        private ILimpezaService LimpezaService { get; set; }

        [Inject]
        private IUsuarioService UsuarioService { get; set; }

        private bool isLoading = false;
        private bool mostrarMensagemLeitoUsuarioNecessario = false;

        private List<LeitoViewModel> leitos = new();
        private List<UsuarioViewModel> usuarios = new();
        private List<LimpezaViewModel> limpezas = new();
        private RelatorioLimpezaRequestModel requestModelRelatorio = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            await this.Consultarleitos()
                .ConfigureAwait(true);

            await this.ConsultarUsuariosAsync()
                .ConfigureAwait(true);

            this.requestModelRelatorio.DataInicio = DateTime.Now.AddDays(-7);
            this.requestModelRelatorio.DataFim = DateTime.Now;

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task Consultarleitos()
        {
            try
            {
                var response = await this.LeitoService
                    .GetAsync()
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.leitos = response.Data;
                }
            }
            catch (Exception e)
            {
                this.ToastService.ShowError(
                    "Erro: Erro inesperado contate o suporte");
            }
        }

        private async Task ConsultarUsuariosAsync()
        {
            try
            {
                var response = await this.UsuarioService
                    .GetUsuariosAsync()
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.usuarios = response.Data;
                }
            }
            catch (Exception e)
            {
                this.ToastService.ShowError(
                    "Erro: Erro inesperado contate o suporte");
            }
        }

        private async Task ConsultarLimpezas()
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                this.mostrarMensagemLeitoUsuarioNecessario = false;

                if ((requestModelRelatorio.LeitoId == 0
                    || requestModelRelatorio.LeitoId == null)
                    && string.IsNullOrEmpty(requestModelRelatorio.UsuarioId))
                {
                    this.mostrarMensagemLeitoUsuarioNecessario = true;

                    this.isLoading = false;
                    this.StateHasChanged();

                    return;
                }

                var response = await this.LimpezaService
                    .ConsultarRelatorioLimpezasAsync(requestModelRelatorio)
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.limpezas = response.Data;
                }
            }
            catch (Exception e)
            {
                this.ToastService.ShowError(
                    "Erro: Erro inesperado contate o suporte");
            }

            this.isLoading = false;
            this.StateHasChanged();
        }
    }
}