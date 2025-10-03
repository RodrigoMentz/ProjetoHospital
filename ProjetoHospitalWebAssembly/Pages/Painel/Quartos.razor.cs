namespace ProjetoHospitalWebAssembly.Pages.Painel
{
    using Blazored.Modal;
    using Blazored.Modal.Services;
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Components.Modais;
    using ProjetoHospitalWebAssembly.Services;

    public partial class Quartos : ComponentBase
    {
        [Inject]
        private IQuartoService QuartoService { get; set; }

        [Inject]
        private IModalService ModalService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        private bool isLoading = false;

        private List<QuartoViewModel> quartos = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            await this.ConsultarQuartos()
                .ConfigureAwait(true);

            //this.quartos = new List<QuartoViewModel>
            //{
            //    new QuartoViewModel(1, "101", 1, "SUS", 1, true),
            //    new QuartoViewModel(2, "102", 2, "Maternidade", 1, true),
            //    new QuartoViewModel(3, "103", 3, "Pediatria", 1, true),
            //    new QuartoViewModel(4, "104", 4, "Emergência", 1, true),
            //    new QuartoViewModel(5,"105", 5, "Sala vermelha", 1, true),
            //    new QuartoViewModel(6, "106", 6, "Bloco cirúrgico", 1, false),
            //};

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

        private async Task AdicionarAsync()
        {
            try
            {
                var options = new ModalOptions
                {
                    Position = ModalPosition.Middle,
                    Size = ModalSize.Large,
                };

                var retornoModal = await this.ModalService
                    .Show<ModalCadastroQuarto>(
                        "Cadastrar quarto",
                        options)
                    .Result
                    .ConfigureAwait(true);

                if (!retornoModal.Cancelled)
                {
                    this.isLoading = true;
                    this.StateHasChanged();

                    var novoQuarto = (QuartoViewModel)retornoModal.Data;

                    if (novoQuarto != null)
                    {
                        var response = await this.QuartoService
                            .CriarAsync(novoQuarto)
                            .ConfigureAwait(true);

                        if (response != null && response.Success)
                        {
                            this.ToastService.ShowSuccess(
                                "Sucesso: Cadastro de quarto realizado");
                        }

                        await this.ConsultarQuartos()
                            .ConfigureAwait(true);
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

        private async Task EditarAsync(QuartoViewModel quartoParaEdicao)
        {
            try
            {
                var options = new ModalOptions
                {
                    Position = ModalPosition.Middle,
                    Size = ModalSize.Large,
                };

                var parametros = new ModalParameters();

                parametros.Add(
                    nameof(ModalCadastroQuarto.IdQuarto),
                    quartoParaEdicao.Id);
                parametros.Add(
                    nameof(ModalCadastroQuarto.Nome),
                    quartoParaEdicao.Nome);
                parametros.Add(
                    nameof(ModalCadastroQuarto.IdSetor),
                    quartoParaEdicao.IdSetor);
                parametros.Add(
                    nameof(ModalCadastroQuarto.Capacidade),
                    quartoParaEdicao.Capacidade);
                parametros.Add(
                    nameof(ModalCadastroQuarto.Ativo),
                    quartoParaEdicao.Ativo);

                var retornoModal = await this.ModalService
                    .Show<ModalCadastroQuarto>(
                        "Editar quarto",
                        parametros,
                        options)
                    .Result
                    .ConfigureAwait(true);

                if (!retornoModal.Cancelled)
                {
                    this.isLoading = true;
                    this.StateHasChanged();

                    var quartoEditado = (QuartoViewModel)retornoModal.Data;

                    if (quartoEditado != null)
                    {
                        var response = await this.QuartoService
                            .AtualizarAsync(quartoEditado)
                            .ConfigureAwait(true);

                        if (response != null && response.Success)
                        {
                            this.ToastService.ShowSuccess(
                            "Sucesso: Atualização de quarto realizada");
                        }

                        await this.ConsultarQuartos()
                            .ConfigureAwait(true);
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

        private async Task ExcluirAsync(QuartoViewModel quartoParaExclusao)
        {
            try
            {
                var options = new ModalOptions
                {
                    Position = ModalPosition.Middle,
                    Size = ModalSize.Large,
                };

                var parametros = new ModalParameters();

                parametros.Add(
                    nameof(ModalDeConfirmacao.Texto),
                    "Você deseja excluir esse quarto? Essa ação é irreversível.");

                var retornoModal = await this.ModalService
                    .Show<ModalDeConfirmacao>(
                        "Excluir permanentemente o quarto",
                        parametros,
                        options)
                    .Result
                    .ConfigureAwait(true);

                if (!retornoModal.Cancelled)
                {
                    this.isLoading = true;
                    this.StateHasChanged();

                    var quartoExcluido = (bool)retornoModal.Data;

                    if (quartoExcluido)
                    {
                        var response = await this.QuartoService
                            .DeletarAsync(quartoParaExclusao)
                            .ConfigureAwait(true);

                        if (response != null && response.Success)
                        {
                            this.ToastService.ShowSuccess(
                                "Sucesso: Exclusão de quarto realizada");
                        }

                        await this.ConsultarQuartos()
                            .ConfigureAwait(true);
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