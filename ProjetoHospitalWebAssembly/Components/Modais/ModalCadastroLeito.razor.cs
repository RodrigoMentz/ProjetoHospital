namespace ProjetoHospitalWebAssembly.Components.Modais
{
    using Blazored.Modal;
    using Blazored.Modal.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;

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

        private bool isLoading = false;
        private bool exibirMensagemNomeCampoObrigatorio = false;
        private bool exibirMensagemQuartoCampoObrigatorio = false;

        private List<QuartoViewModel> quartos = new();

        private bool ativoInterno { get; set; } = true;

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

            // TODO: chamada para consultar quartos
            this.quartos = new List<QuartoViewModel>
            {
                new QuartoViewModel(1, "101", 1, "SUS", 1, true),
                new QuartoViewModel(2, "102", 2, "Maternidade", 1, true),
                new QuartoViewModel(3, "103", 3, "Pediatria", 1, true),
                new QuartoViewModel(4, "104", 4, "Emergência", 1, true),
                new QuartoViewModel(5,"105", 5, "Sala vermelha", 1, true),
                new QuartoViewModel(6, "106", 6, "Bloco cirúrgico", 1, false),
            };

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