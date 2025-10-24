namespace ProjetoHospitalWebAssembly.Pages.App
{
    using Blazored.LocalStorage;
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;
    using System.Text.RegularExpressions;

    public partial class CriarNovaSenha : ComponentBase
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

        private bool exibirSenhas = false;
        private bool exibirMensagemCamposVazios = false;
        private bool exibirMensagemCamposDiferentes = false;
        private bool senhaTemNoMinimo6Digitos = false;
        private bool senhaTemNoMinimo1NaoAlfanumerico = false;
        private bool senhaTemNoMinimo1Numero = false;
        private bool senhaTemNoMinimo1Maiuscula = false;
        private UsuarioViewModel usuarioLocalStorage = new();
        private ResetarSenhaViewModel resetarSenha = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            this.usuarioLocalStorage = await this.UsuarioService
                .ConsultarUsuarioLocalStorage()
                .ConfigureAwait(true);

            this.resetarSenha.Id = usuarioLocalStorage.Id;

            this.isLoading = false;
            this.StateHasChanged();
        }

        public string Senha
        {
            get => this.resetarSenha.Senha;
            set
            {
                this.resetarSenha.Senha = value;
                ValidarSenhas();
            }
        }

        private async Task CadastrarSenha()
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                this.exibirMensagemCamposVazios = false;
                this.exibirMensagemCamposDiferentes = false;

                if (string.IsNullOrEmpty(this.resetarSenha.Senha)
                    || string.IsNullOrEmpty(this.resetarSenha.ConfirmarSenha))
                {
                    this.exibirMensagemCamposVazios = true;
                    this.isLoading = false;
                    this.StateHasChanged();

                    return;
                }

                if (this.resetarSenha.Senha != this.resetarSenha.ConfirmarSenha)
                {
                    this.exibirMensagemCamposDiferentes = true;
                    this.isLoading = false;
                    this.StateHasChanged();

                    return;
                }

                if (!ValidarSenhas())
                {
                    this.exibirMensagemCamposVazios = true;
                    this.isLoading = false;
                    this.StateHasChanged();

                    return;
                }

                var idUsuarioLogado = await this.LocalStorageService
                    .GetItemAsync<string>("IdUsuario")
                    .ConfigureAwait(true);

                if (string.IsNullOrWhiteSpace(idUsuarioLogado))
                {
                    this.NavigationManager
                        .NavigateTo("/inicio");
                }

                var response = await this.UsuarioService
                    .ResetarSenhaAsync(resetarSenha)
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.ToastService.ShowSuccess(
                        "Sucesso: Senha definida com sucesso");

                    this.isLoading = false;
                    this.StateHasChanged();

                    if (usuarioLocalStorage.Perfil.Nome == "Limpeza")
                    {
                        this.NavigationManager
                            .NavigateTo("/quartos-para-limpar");
                    }
                    else if (usuarioLocalStorage.Perfil.Nome == "Recepcão/Enfermagem")
                    {
                        this.NavigationManager
                            .NavigateTo("/painel");
                    }
                    else if (usuarioLocalStorage.Perfil.Nome == "Manutenção")
                    {
                        this.NavigationManager
                            .NavigateTo("/manutencoes");
                    }
                    else if (usuarioLocalStorage.Perfil.Nome == "Inspeção da limpeza")
                    {
                        this.NavigationManager
                            .NavigateTo("/revisoes");
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

                Console.WriteLine(e);
            }

            this.isLoading = false;
            this.StateHasChanged();
        }

        private bool ValidarSenhas()
        {
            this.senhaTemNoMinimo6Digitos = false;
            this.senhaTemNoMinimo1NaoAlfanumerico = false;
            this.senhaTemNoMinimo1Numero = false;
            this.senhaTemNoMinimo1Maiuscula = false;
            this.exibirMensagemCamposVazios = false;
            this.exibirMensagemCamposDiferentes = false;

            this.senhaTemNoMinimo6Digitos = this.resetarSenha.Senha.Length >= 6;
            this.senhaTemNoMinimo1NaoAlfanumerico = Regex.IsMatch(this.resetarSenha.Senha, @"[^A-Za-z0-9]");
            this.senhaTemNoMinimo1Numero = Regex.IsMatch(this.resetarSenha.Senha, @"\d");
            this.senhaTemNoMinimo1Maiuscula = Regex.IsMatch(this.resetarSenha.Senha, @"[A-Z]");

            this.StateHasChanged();

            return senhaTemNoMinimo6Digitos
                && senhaTemNoMinimo1NaoAlfanumerico
                && senhaTemNoMinimo1Numero
                && senhaTemNoMinimo1Maiuscula;
        }
    }
}