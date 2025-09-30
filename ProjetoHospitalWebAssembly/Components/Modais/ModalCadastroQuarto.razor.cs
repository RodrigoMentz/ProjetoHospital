namespace ProjetoHospitalWebAssembly.Components.Modais
{
    using Blazored.Modal;
    using Blazored.Modal.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;

    public partial class ModalCadastroQuarto : ComponentBase
    {
        [CascadingParameter]
        public BlazoredModalInstance ModalInstance { get; set; }

        [Parameter]
        public int IdQuarto { get; set; }

        [Parameter]
        public string Nome { get; set; } = string.Empty;

        [Parameter]
        public int IdSetor { get; set; }

        [Parameter]
        public int Capacidade { get; set; } = 1;

        [Parameter]
        public bool Ativo { get; set; }

        private bool isLoading = false;
        private bool exibirMensagemNomeCampoObrigatorio = false;
        private bool exibirMensagemSetorCampoObrigatorio = false;

        private bool ativoInterno { get; set; } = true;

        private List<SetorViewModel> setores = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            if (IdQuarto != 0)
            {
                this.ativoInterno = this.Ativo;
            }
            else
            {
                this.ativoInterno = true;
            }

            // TODO: chamada para consultar setores
            this.setores = new List<SetorViewModel>
            {
                new SetorViewModel(1, "SUS", true),
                new SetorViewModel(2, "Maternidade", true),
                new SetorViewModel(3, "Pediatria", true),
                new SetorViewModel(4, "Emergência", true),
                new SetorViewModel(5, "Sala vermelha", true),
                new SetorViewModel(6, "Bloco cirúrgico", false),
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

            if (this.IdQuarto != 0)
            {
                var quartoParaEdicao = new QuartoViewModel(
                    this.IdQuarto,
                    this.Nome,
                    this.IdSetor,
                    this.Capacidade,
                    this.ativoInterno);

                await this.ModalInstance
                    .CloseAsync(ModalResult.Ok(quartoParaEdicao))
                    .ConfigureAwait(true);
            }
            else
            {
                var quartoParaAdicao = new QuartoViewModel(
                    this.Nome,
                    this.IdSetor,
                    this.Capacidade,
                    this.ativoInterno);

                await this.ModalInstance
                    .CloseAsync(ModalResult.Ok(quartoParaAdicao))
                    .ConfigureAwait(true);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(this.Nome))
            {
                this.exibirMensagemNomeCampoObrigatorio = true;

                return true;
            }

            if (this.IdSetor == 0)
            {
                this.exibirMensagemSetorCampoObrigatorio = true;

                return true;
            }

            return false;
        }
    }
}