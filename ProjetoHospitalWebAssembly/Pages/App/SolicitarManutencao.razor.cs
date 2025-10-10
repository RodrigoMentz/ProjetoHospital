namespace ProjetoHospitalWebAssembly.Pages.App
{
    using Blazored.LocalStorage;
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class SolicitarManutencao : ComponentBase
    {
        [Parameter]
        public string Id { get; set; } = string.Empty;

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

        private ManutencaoViewModel manutencao = new();
        private List<string> turnos;
        private List<SetorViewModel> setores = new();

        private bool isLoading = false;
        private bool exibirMensagemSetorCampoObrigatorio = false;
        private bool exibirMensagemDescricaoObrigatoria = false;

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            var response = await this.SetorService
                .GetAsync()
                .ConfigureAwait(true);

            this.turnos = Turnos.Listar();

            if (response != null && response.Success)
            {
                this.setores = response.Data;
            }

            if (Id != null)
            {
                await this.ConsultarManutencaoParaEdicao()
                    .ConfigureAwait(true);
            }
            else
            {
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

                this.manutencao.Turno = turnos.FirstOrDefault();
                this.manutencao.IdSolicitante = idUsuarioLocalStorage;
                this.manutencao.NomeSolicitante = nomeUsuarioLocalStorage;
                this.manutencao.ContatoSolicitante = numTelefoneUsuarioLocalStorage;
                this.manutencao.DataDeSolicitacao = DateTime.Now;
                
            }

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task ConsultarManutencaoParaEdicao()
        {
            try
            {
                var idManutencao = int.Parse(this.Id);

                var manutencaoParaEdicao = new ManutencaoViewModel(idManutencao);
                var response = await this.ManutencaoService
                    .GetDetalhesDaManutencaoAsync(
                        manutencaoParaEdicao)
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.manutencao = response.Data;
                }
            }
            catch (Exception e)
            {
                this.ToastService.ShowError(
                    "Erro: Erro inesperado contate o suporte");
            }
        }

        private async Task CriarManutencao()
        {
            try
            {
                this.exibirMensagemSetorCampoObrigatorio = false;
                this.exibirMensagemDescricaoObrigatoria = false;

                if (manutencao.SetorId == 0)
                {
                    this.exibirMensagemSetorCampoObrigatorio = true;

                    return;
                }

                if (string.IsNullOrEmpty(manutencao.Descricao))
                {
                    this.exibirMensagemDescricaoObrigatoria = true;

                    return;
                }

                var response = await this.ManutencaoService
                    .CriarAsync(this.manutencao);

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
            catch (Exception e)
            {
                this.ToastService.ShowError(
                    "Erro: Erro inesperado contate o suporte");
            }
        }

        private async Task EditarManutencao()
        {
            try
            {
                this.exibirMensagemSetorCampoObrigatorio = false;
                this.exibirMensagemDescricaoObrigatoria = false;

                if (manutencao.SetorId == 0)
                {
                    this.exibirMensagemSetorCampoObrigatorio = true;

                    return;
                }

                if (string.IsNullOrEmpty(manutencao.Descricao))
                {
                    this.exibirMensagemDescricaoObrigatoria = true;

                    return;
                }

                var response = await this.ManutencaoService
                    .AtualizarAsync(this.manutencao);

                if (response != null && response.Success)
                {
                    this.ToastService.ShowSuccess(
                        "Sucesso: Edição da manutenção realizada");

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
            catch (Exception e)
            {
                this.ToastService.ShowError(
                    "Erro: Erro inesperado contate o suporte");
            }
        }
    }
}