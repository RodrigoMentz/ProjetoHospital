namespace ProjetoHospitalWebAssembly.Components.Modais
{
    using Blazored.Modal;
    using Blazored.Modal.Services;
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;
    using System.Text.RegularExpressions;

    public partial class ModalCadastroUsuario : ComponentBase
    {
        [CascadingParameter]
        public BlazoredModalInstance ModalInstance { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public string Nome { get; set; } = string.Empty;

        [Parameter]
        public PerfilViewModel Perfil { get; set; }

        [Parameter]
        public string NumeroTelefone { get; set; }

        [Parameter]
        public bool Ativo { get; set; }

        [Inject]
        public IUsuarioService UsuarioService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        private bool isLoading = false;
        private bool exibirMensagemNomeCampoObrigatorio = false;
        private bool exibirMensagemNumeroCampoObrigatorio = false;
        private bool exibirMensagemNumeroFormatoErrado = false;
        private bool exibirMensagemPerfilCampoObrigatorio = false;
        private bool exibirMensagemSenhaRedefinida = false;

        private bool ativoInterno = true;

        private List<PerfilViewModel> perfis = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            if ((!string.IsNullOrEmpty(this.Id)))
            {
                this.ativoInterno = this.Ativo;
            }
            else
            {
                this.ativoInterno = true;
            }

            // TODO: chamada para consultar perfis
            var response = await this.UsuarioService
                .GetPerfisAsync()
                .ConfigureAwait(true);

            if (response != null && response.Success)
            {
                this.perfis = response.Data;
            }

            if (this.Perfil == null)
            {
                var perfilIndefinido = new PerfilViewModel(Guid.Empty.ToString(), "Selecione");
                this.perfis.Add(perfilIndefinido);
                this.Perfil = this.perfis.Where(p => p.Nome == "Selecione").FirstOrDefault();
            }

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task ReiniciarSenha()
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                var resetarSenhaViewModel = new ResetarSenhaViewModel(
                    "HpCanela25!",
                    "HpCanela25!");

                resetarSenhaViewModel.Id = this.Id;

                var response = await this.UsuarioService
                    .ResetarSenhaAsync(resetarSenhaViewModel)
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.exibirMensagemSenhaRedefinida = true;

                    this.ToastService.ShowSuccess(
                        "Sucesso: Senha reiniciada com sucesso");
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
            this.Perfil = this.perfis
                .Where(p => p.Id == this.Perfil.Id)
                .FirstOrDefault() ?? new PerfilViewModel();

            if (this.ValidarCampos())
            {
                return;
            }

            if ((!string.IsNullOrEmpty(this.Id)))
            {
                var usuarioparaEdicao = new UsuarioViewModel(
                    this.Id,
                    this.Nome,
                    this.Perfil,
                    this.NumeroTelefone,
                    this.ativoInterno);

                await this.ModalInstance
                    .CloseAsync(ModalResult.Ok(usuarioparaEdicao))
                    .ConfigureAwait(true);
            }
            else
            {
                var usuarioParaAdicao = new UsuarioViewModel(
                    this.Nome,
                    this.Perfil,
                    this.NumeroTelefone,
                    this.ativoInterno);

                await this.ModalInstance
                    .CloseAsync(ModalResult.Ok(usuarioParaAdicao))
                    .ConfigureAwait(true);
            }
        }

        private bool ValidarCampos()
        {
            this.exibirMensagemNumeroCampoObrigatorio = false;
            this.exibirMensagemNumeroFormatoErrado = false;
            this.exibirMensagemNomeCampoObrigatorio = false;
            this.exibirMensagemPerfilCampoObrigatorio = false;

            if (string.IsNullOrWhiteSpace(NumeroTelefone))
            {
                this.exibirMensagemNumeroCampoObrigatorio = true;
            }
            else if (!Regex.IsMatch(NumeroTelefone, @"^\d{11}$"))
            {
                this.exibirMensagemNumeroFormatoErrado = true;
            }

            if (string.IsNullOrWhiteSpace(Nome))
            {
                this.exibirMensagemNomeCampoObrigatorio = true;
            }

            if (Perfil == null
                || Perfil.Id == "0")
            {
                this.exibirMensagemPerfilCampoObrigatorio = true;
            }

            return this.exibirMensagemNumeroCampoObrigatorio
                || this.exibirMensagemNumeroFormatoErrado
                || this.exibirMensagemNomeCampoObrigatorio
                || this.exibirMensagemPerfilCampoObrigatorio;
        }
    }
}