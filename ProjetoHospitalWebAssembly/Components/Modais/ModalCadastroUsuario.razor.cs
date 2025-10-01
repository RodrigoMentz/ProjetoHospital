namespace ProjetoHospitalWebAssembly.Components.Modais
{
    using Blazored.Modal;
    using Blazored.Modal.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using System.Text.RegularExpressions;

    public partial class ModalCadastroUsuario : ComponentBase
    {
        [CascadingParameter]
        public BlazoredModalInstance ModalInstance { get; set; }

        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public string Nome { get; set; } = string.Empty;

        [Parameter]
        public PerfilViewModel Perfil { get; set; }

        [Parameter]
        public string NumeroTelefone { get; set; }

        [Parameter]
        public bool Ativo { get; set; }

        private bool isLoading = false;
        private bool exibirMensagemNomeCampoObrigatorio = false;
        private bool exibirMensagemNumeroCampoObrigatorio = false;
        private bool exibirMensagemNumeroFormatoErrado = false;
        private bool exibirMensagemPerfilCampoObrigatorio = false;

        private bool ativoInterno = true;

        private List<PerfilViewModel> perfis = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            if (Id != 0)
            {
                this.ativoInterno = this.Ativo;
            }
            else
            {
                this.ativoInterno = true;
            }

            // TODO: chamada para consultar perfis
            this.perfis = new List<PerfilViewModel>
            {
                new PerfilViewModel(1, "Limpeza"),
                new PerfilViewModel(2, "Recepcão/Enfermagem"),
                new PerfilViewModel(3, "Manutenção"),
            };

            if (this.Perfil == null)
            {
                var perfilIndefinido = new PerfilViewModel(0, "Selecione");
                this.perfis.Insert(0, perfilIndefinido);
                this.Perfil = this.perfis.First();
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

            if (this.Id != 0)
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
                || Perfil.Id == 0)
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