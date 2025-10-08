namespace ProjetoHospitalWebAssembly.Pages.App
{
    using Blazored.LocalStorage;
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;
    using System.Threading.Tasks;

    public partial class Home : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IUsuarioService UsuarioService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private ILocalStorageService LocalStorageService { get; set; }

        private bool isLoading = false;

        private string numeroTelefone = string.Empty;
        private string senha = string.Empty;
        private bool exibirSenha = false;

        private bool exibirMensagemCamposVazios = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                await this.TratarCacheLogado()
                    .ConfigureAwait(true);
            }
            catch (Exception e)
            {
                this.ToastService.ShowError(
                    "Erro: Erro inesperado contate o suporte");
            }

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task LoginAsync()
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                this.exibirMensagemCamposVazios = false;

                if (string.IsNullOrWhiteSpace(this.numeroTelefone) ||
                    string.IsNullOrWhiteSpace(this.senha))
                {
                    this.exibirMensagemCamposVazios = true;

                    return;
                }

                var login = new LoginViewModel(
                    this.numeroTelefone,
                    this.senha);

                var response = await this.UsuarioService
                    .LoginAsync(
                        login)
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.ToastService.ShowSuccess(
                        "Sucesso: Usuário logado com sucesso");

                    await Task.Delay(2000);

                    if (!response.Data.NumTelefoneConfirmado)
                    {
                        this.NavigationManager
                            .NavigateTo("/cadastrar-senha");
                    }
                    else
                    {
                        this.NavigationManager
                            .NavigateTo("/perfis");
                    }
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

        private async Task TratarCacheLogado()
        {
            var IdUsuarioLogado = await this.LocalStorageService
                .GetItemAsync<string>("IdUsuario")
                .ConfigureAwait(true);

            var authToken = await this.LocalStorageService
                .GetItemAsync<string>("authToken")
                .ConfigureAwait(true);

            var perfilUsuario = await this.LocalStorageService
                .GetItemAsync<PerfilViewModel>("perfil")
                .ConfigureAwait(true);

            if (IdUsuarioLogado != null
                && authToken != null
                && perfilUsuario != null)
            {
                if (perfilUsuario.Nome == "Limpeza")
                {
                    this.NavigationManager
                        .NavigateTo("/quartos-para-limpar");
                }
                else if (perfilUsuario.Nome == "Recepcão/Enfermagem")
                {
                    this.NavigationManager
                        .NavigateTo("/painel");
                }
                /* TODO: implementar outros perfis*/
            }
        }
    }
}