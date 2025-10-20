namespace ProjetoHospitalWebAssembly.Pages.Painel
{
    using Blazored.Modal;
    using Blazored.Modal.Services;
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Components.Modais;
    using ProjetoHospitalWebAssembly.Services;

    public partial class Leitos : ComponentBase
    {
        [Inject]
        private IModalService ModalService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private ILeitoService LeitoService { get; set; }

        [Inject]
        private ISetorService SetorService { get; set; }

        private bool isLoading = false;

        private List<LeitoViewModel> leitos = new();
        private List<LeitoViewModel> leitosFiltrados = new();
        private List<SetorViewModel> setores = new();
        private SetorViewModel setorSelecionado = new();
        private int idSetorSelecionado = 0;

        private int IdSetorSelecionado
        {
            get => idSetorSelecionado;
            set
            {
                if (idSetorSelecionado != value)
                {
                    idSetorSelecionado = value;

                    var setor = this.setores
                        .Where(s => s.Id == idSetorSelecionado)
                        .FirstOrDefault() ?? this.setores.First();

                    OnFiltroChange(setor);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            await this.Consultarleitos()
                .ConfigureAwait(true);

            await ConsultarSetores()
                .ConfigureAwait(true);

            var setorTodos = new SetorViewModel(0, "Todos");
            this.setores.Insert(0, setorTodos);
            this.setorSelecionado = this.setores.First();

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task Consultarleitos()
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                var response = await this.LeitoService
                    .GetAsync()
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.leitos = response.Data;
                    this.leitosFiltrados = leitos;
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
                    .Show<ModalCadastroLeito>(
                        "Cadastrar leito",
                        options)
                    .Result
                    .ConfigureAwait(true);

                if (!retornoModal.Cancelled)
                {
                    this.isLoading = true;
                    this.StateHasChanged();

                    var novoLeito = (LeitoViewModel)retornoModal.Data;

                    if (novoLeito != null)
                    {
                        var response = await this.LeitoService
                            .CriarAsync(novoLeito)
                            .ConfigureAwait(true);

                        if (response != null && response.Success)
                        {
                            this.ToastService.ShowSuccess(
                                "Sucesso: Cadastro de leito realizado");
                        }
                        else if (response != null && response.Notifications.Any())
                        {
                            foreach (var notification in response.Notifications)
                            {
                                this.ToastService.ShowError(
                                    $"Erro: {notification.Message}");
                            }
                        }

                        await this.Consultarleitos()
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

        private async Task EditarAsync(LeitoViewModel leitoParaEdicao)
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
                    nameof(ModalCadastroLeito.IdLeito),
                    leitoParaEdicao.Id);
                parametros.Add(
                    nameof(ModalCadastroLeito.Nome),
                    leitoParaEdicao.Nome);
                parametros.Add(
                    nameof(ModalCadastroLeito.IdQuarto),
                    leitoParaEdicao.Quarto.Id);
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
                    this.isLoading = true;
                    this.StateHasChanged();

                    var leitoEditado = (LeitoViewModel)retornoModal.Data;

                    if (leitoEditado != null)
                    {
                        var response = await this.LeitoService
                            .AtualizarAsync(leitoEditado)
                            .ConfigureAwait(true);

                        if (response != null && response.Success)
                        {
                            this.ToastService.ShowSuccess(
                                "Sucesso: Atualização de leito realizada");
                        }
                        else if (response != null && response.Notifications.Any())
                        {
                            foreach (var notification in response.Notifications)
                            {
                                this.ToastService.ShowError(
                                    $"Erro: {notification.Message}");
                            }
                        }

                        await this.Consultarleitos()
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

        private async Task ExcluirAsync(LeitoViewModel leitoParaExclusao)
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
                    "Você deseja excluir esse leito? Essa ação é irreversível.");

                var retornoModal = await this.ModalService
                    .Show<ModalDeConfirmacao>(
                        "Excluir permanentemente o leito",
                        parametros,
                        options)
                    .Result
                    .ConfigureAwait(true);

                if (!retornoModal.Cancelled)
                {
                    this.isLoading = true;
                    this.StateHasChanged();

                    var leitoExcluido = (bool)retornoModal.Data;

                    if (leitoExcluido)
                    {
                        var response = await this.LeitoService
                            .DeletarAsync(leitoParaExclusao)
                            .ConfigureAwait(true);

                        if (response != null && response.Success)
                        {
                            this.ToastService.ShowSuccess(
                                "Sucesso: Exclusão de leito realizada");
                        }

                        await this.Consultarleitos()
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

        private async Task ConsultarSetores()
        {
            try
            {
                var responseSetores = await this.SetorService
                    .GetAsync()
                    .ConfigureAwait(true);

                if (responseSetores != null && responseSetores.Success)
                {
                    this.setores = responseSetores.Data;
                }
                else
                {
                    this.ToastService.ShowError(
                        "Erro: Erro inesperado contate o suporte");
                }
            }
            catch (Exception e)
            {
                this.ToastService.ShowError(
                    "Erro: Erro inesperado contate o suporte");
            }
        }

        private void OnFiltroChange(SetorViewModel setor)
        {
            this.setorSelecionado = setor;

            this.leitosFiltrados = this.leitos;

            if (setor.Id != 0)
            {
                this.leitosFiltrados = this.leitosFiltrados
                    .Where(l => l.Quarto.IdSetor == setor.Id)
                    .ToList();
            }
        }
    }
}