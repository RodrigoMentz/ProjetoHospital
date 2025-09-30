namespace ProjetoHospitalWebAssembly.Pages.Painel
{
    using Blazored.Modal;
    using Blazored.Modal.Services;
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Components.Modais;

    public partial class Leitos : ComponentBase
    {
        [Inject]
        private IModalService ModalService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        private bool isLoading = false;

        private List<LeitoViewModel> leitos = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            this.leitos = new List<LeitoViewModel>
            {
                new LeitoViewModel(1, "101A", 1, "101", "SUS", true),
                new LeitoViewModel(2, "101B", 2, "101", "Maternidade", true),
                new LeitoViewModel(3, "102A", 3, "102", "Pediatria", true),
                new LeitoViewModel(4, "102B", 4, "102", "Emergência", true),
                new LeitoViewModel(5, "103A", 5, "103", "Sala vermelha",true),
                new LeitoViewModel(6, "103B", 6, "103", "Bloco cirúrgico", false),
            };

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task AdicionarAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            try
            {
                var options = new ModalOptions
                {
                    Position = ModalPosition.Middle,
                    Size = ModalSize.Large,
                };

                var retornoModal = await this.ModalService
                    .Show<ModalCadastroLeito>(
                        "Cadastrar leito",
                        options)
                    .Result
                    .ConfigureAwait(true);

                if (!retornoModal.Cancelled)
                {
                    var novoLeito = (LeitoViewModel)retornoModal.Data;

                    if (novoLeito != null)
                    {
                        this.ToastService.ShowSuccess(
                            "Sucesso: Cadastro de leito realizado");
                        // TODO: chamada para adicionar leito
                        // TODO: chamada para atualizar a lista de leitos
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

        private async Task EditarAsync(LeitoViewModel leitoParaEdicao)
        {
            this.isLoading = true;
            this.StateHasChanged();

            try
            {
                var options = new ModalOptions
                {
                    Position = ModalPosition.Middle,
                    Size = ModalSize.Large,
                };

                var parametros = new ModalParameters();

                parametros.Add(
                    nameof(ModalCadastroLeito.IdLeito),
                    leitoParaEdicao.Id);
                parametros.Add(
                    nameof(ModalCadastroLeito.Nome),
                    leitoParaEdicao.Nome);
                parametros.Add(
                    nameof(ModalCadastroLeito.IdQuarto),
                    leitoParaEdicao.IdQuarto);
                parametros.Add(
                    nameof(ModalCadastroLeito.Ativo),
                    leitoParaEdicao.Ativo);

                var retornoModal = await this.ModalService
                    .Show<ModalCadastroLeito>(
                        "Editar leito",
                        parametros,
                        options)
                    .Result
                    .ConfigureAwait(true);

                if (!retornoModal.Cancelled)
                {
                    var leitoEditado = (LeitoViewModel)retornoModal.Data;

                    if (leitoEditado != null)
                    {
                        this.ToastService.ShowSuccess(
                            "Sucesso: Atualização de leito realizada");
                        // TODO: chamada para editar leito
                        // TODO: chamada para atualizar a lista de leitos
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
    }
}