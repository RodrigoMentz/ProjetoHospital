namespace ProjetoHospitalWebAssembly.Components.Modais
{
    using Blazored.Modal;
    using Blazored.Modal.Services;
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class ModalCadastroLeito : ComponentBase
    {
        [CascadingParameter]
        public BlazoredModalInstance ModalInstance { get; set; }

        [Parameter]
        public int IdLeito { get; set; }

        [Parameter]
        public string Nome { get; set; }

        [Parameter]
        public int IdQuarto { get; set; }

        [Parameter]
        public bool Ativo { get; set; }

        [Inject]
        private IQuartoService QuartoService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        private bool isLoading = false;
        private bool exibirMensagemNomeCampoObrigatorio = false;
        private bool exibirMensagemQuartoCampoObrigatorio = false;

        private List<QuartoViewModel> quartos = new();

        private bool ativoInterno = true;

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            if (IdLeito != 0)
            {
                this.ativoInterno = this.Ativo;
            }
            else
            {
                this.ativoInterno = true;
            }

            await this.ConsultarQuartos()
                .ConfigureAwait(true);

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task ConsultarQuartos()
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                var response = await this.QuartoService
                    .GetAsync()
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.quartos = response.Data;
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
            if (this.ValidarCampos())
            {
                return;
            }

            if (this.IdLeito != 0)
            {
                var leitoParaEdicao = new LeitoViewModel(
                    this.IdLeito,
                    this.Nome,
                    this.IdQuarto,
                    this.ativoInterno);

                await this.ModalInstance
                    .CloseAsync(ModalResult.Ok(leitoParaEdicao))
                    .ConfigureAwait(true);
            }
            else
            {
                var leitoParaAdicao = new LeitoViewModel(
                    this.IdLeito,
                    this.Nome,
                    this.IdQuarto,
                    this.ativoInterno);

                await this.ModalInstance
                    .CloseAsync(ModalResult.Ok(leitoParaAdicao))
                    .ConfigureAwait(true);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(this.Nome))
            {
                this.exibirMensagemNomeCampoObrigatorio = true;

                if (this.IdQuarto == 0)
                {
                    this.exibirMensagemQuartoCampoObrigatorio = true;
                }

                return true;
            }

            if (this.IdQuarto == 0)
            {
                this.exibirMensagemQuartoCampoObrigatorio = true;

                return true;
            }

            return false;
        }
    }
}