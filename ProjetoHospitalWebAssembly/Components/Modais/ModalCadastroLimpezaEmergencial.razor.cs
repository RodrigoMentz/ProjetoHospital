using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using ProjetoHospitalShared.ViewModels;
using ProjetoHospitalWebAssembly.Services;

namespace ProjetoHospitalWebAssembly.Components.Modais
{
    public partial class ModalCadastroLimpezaEmergencial : ComponentBase
    {
        [CascadingParameter]
        public BlazoredModalInstance ModalInstance { get; set; }

        [Inject]
        private ILeitoService LeitoService { get; set; }

        [Inject]
        private ILimpezaService LimpezaService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private IUsuarioService UsuarioService { get; set; }

        private bool isLoading = false;
        private bool exibirMensagemLeitoCampoObrigatorio = false;

        private LimpezaEmergencialViewModel limpeza = new();
        private List<LeitoViewModel> leitos = new();
        private UsuarioViewModel usuarioLocalStorage = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            this.usuarioLocalStorage = await this.UsuarioService
                .ConsultarUsuarioLocalStorage()
                .ConfigureAwait(true);

            this.limpeza.SolicitanteId = this.usuarioLocalStorage.Id;

            await this.ConsultarLeitos()
                .ConfigureAwait(true);

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task ConsultarLeitos()
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                var response = await this.LeitoService
                    .GetAsync()
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.leitos = response.Data;
                }
                else if (response != null && response.Notifications.Any())
                {
                    foreach (var notification in response.Notifications)
                    {
                        this.ToastService.ShowError(
                            $"Erro: {notification.Message}");
                    }
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

        private async Task SalvarAsync()
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                var response = await this.LimpezaService
                    .CriarEmergencialAsync(limpeza)
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.ToastService.ShowSuccess(
                        "Sucesso: Cadastro de limpeza emergencial realizado");

                    await this.ModalInstance
                        .CloseAsync(ModalResult.Ok())
                        .ConfigureAwait(true);
                }
                else if (response != null && response.Notifications.Any())
                {
                    foreach (var notification in response.Notifications)
                    {
                        this.ToastService.ShowError(
                            $"Erro: {notification.Message}");
                    }
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