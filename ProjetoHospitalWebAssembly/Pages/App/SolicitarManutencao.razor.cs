namespace ProjetoHospitalWebAssembly.Pages.App
{
    using Blazored.LocalStorage;
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class SolicitarManutencao
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IManutencaoService ManutencaoService { get; set; }

        [Inject]
        private ISetorService SetorService { get; set; }

        [Inject]
        private ILocalStorageService localStorageService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        private ManutencaoViewModel manutencao { get; set; } = new();
        private List<string> turnos;
        private List<SetorViewModel> setores = new();

        private bool isLoading = false;
        private bool exibirMensagemSetorCampoObrigatorio = false;

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            var response = await this.SetorService
                .GetAsync()
                .ConfigureAwait(true);

            if (response != null && response.Success)
            {
                this.setores = response.Data;
            }

            var nomeUsuarioLocalStorage = await this.localStorageService
                .GetItemAsync<string>("nomeUsuario")
                .ConfigureAwait(true);

            var idUsuarioLocalStorage = await this.localStorageService
                .GetItemAsync<string>("IdUsuario")
                .ConfigureAwait(true);

            var numTelefoneUsuarioLocalStorage = await this.localStorageService
                .GetItemAsync<string>("numTelefone")
                .ConfigureAwait(true);

            if (string.IsNullOrWhiteSpace(nomeUsuarioLocalStorage)
                || string.IsNullOrWhiteSpace(idUsuarioLocalStorage)
                || string.IsNullOrWhiteSpace(numTelefoneUsuarioLocalStorage))
            {
                this.NavigationManager
                    .NavigateTo("/inicio");

                return;
            }

            this.turnos = Turnos.Listar();
            this.manutencao.Turno = turnos.FirstOrDefault();
            this.manutencao.IdSolicitante = idUsuarioLocalStorage;
            this.manutencao.NomeSolicitante = nomeUsuarioLocalStorage;
            this.manutencao.ContatoSolicitante = numTelefoneUsuarioLocalStorage;
            this.manutencao.DataDeSolicitacao = DateTime.Now;

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task CriarManutencao()
        {
            var response = await this.ManutencaoService
                .CriarAsync(manutencao);

            if (response != null && response.Success)
            {
                this.ToastService.ShowSuccess(
                    "Sucesso: Cadastro de manutenção realizado");

                this.NavigationManager
                    .NavigateTo("/manutencoes");
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
    }
}