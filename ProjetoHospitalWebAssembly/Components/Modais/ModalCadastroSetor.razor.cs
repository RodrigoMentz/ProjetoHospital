namespace ProjetoHospitalWebAssembly.Components.Modais
{
    using Blazored.Modal;
    using Blazored.Modal.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;

    public partial class ModalCadastroSetor : ComponentBase
    {
        [CascadingParameter]
        public BlazoredModalInstance ModalInstance { get; set; }

        [Parameter]
        public int IdSetor { get; set; }

        [Parameter]
        public string NomeSetor { get; set; } = string.Empty;

        [Parameter]
        public bool Ativo { get; set; }

        private bool isLoading = false;
        private bool exibirMensagemCampoObrigatorio = false;

        private bool ativoInterno { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            if (IdSetor != 0)
            {
                this.ativoInterno = this.Ativo;
            }
            else
            {
                this.ativoInterno = true;
            }

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task SalvarAsync()
        {
            if (string.IsNullOrWhiteSpace(this.NomeSetor))
            {
                this.exibirMensagemCampoObrigatorio = true;

                return;
            }

            if (IdSetor != 0)
            {
                var setorParaEdicao = new SetorViewModel(
                    this.IdSetor,
                    this.NomeSetor,
                    this.ativoInterno);

                await this.ModalInstance
                    .CloseAsync(ModalResult.Ok(setorParaEdicao))
                    .ConfigureAwait(true);
            }
            else
            {
                var setorParaAdicao = new SetorViewModel(
                    this.NomeSetor,
                    this.ativoInterno);

                await this.ModalInstance
                    .CloseAsync(ModalResult.Ok(setorParaAdicao))
                    .ConfigureAwait(true);
            }
        }
    }
}