namespace ProjetoHospitalWebAssembly.Pages.Painel
{
    using Blazored.Modal;
    using Blazored.Modal.Services;
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Components.Modais;
    using ProjetoHospitalWebAssembly.Services;

    public partial class Setores : ComponentBase
    {
        [Inject]
        private IModalService ModalService { get; set; }

        [Inject]
        private ISetorService SetorService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        private bool isLoading = false;

        private List<SetorViewModel> setores = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            //var setoress = await this.SetorService
            //    .GetAsync()
            //    .ConfigureAwait(true);

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
                    .Show<ModalCadastroSetor>(
                        "Cadastrar setor",
                        options)
                    .Result
                    .ConfigureAwait(true);

                if (!retornoModal.Cancelled)
                {
                    var novoSetor = (SetorViewModel)retornoModal.Data;

                    if (novoSetor != null)
                    {
                        this.ToastService.ShowSuccess(
                            "Sucesso: Cadastro de setor realizado");

                        //var response = await this.SetorService
                        //    .CriarAsync(novoSetor)
                        //    .ConfigureAwait(true);

                        // TODO: chamada para atualizar a lista de setores
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

        private async Task EditarAsync(SetorViewModel setorParaEdicao)
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
                    nameof(ModalCadastroSetor.IdSetor),
                    setorParaEdicao.Id);
                parametros.Add(
                    nameof(ModalCadastroSetor.NomeSetor),
                    setorParaEdicao.Nome);
                parametros.Add(
                    nameof(ModalCadastroSetor.Ativo),
                    setorParaEdicao.Ativo);

                var retornoModal = await this.ModalService
                    .Show<ModalCadastroSetor>(
                        "Editar setor",
                        parametros,
                        options)
                    .Result
                    .ConfigureAwait(true);

                if (!retornoModal.Cancelled)
                {
                    var setorEditado = (SetorViewModel)retornoModal.Data;

                    if (setorEditado != null)
                    {
                        this.ToastService.ShowSuccess(
                            "Sucesso: Atualização de setor realizada");
                        var response = await this.SetorService
                            .AtualizarAsync(setorEditado)
                            .ConfigureAwait(true);

                        // TODO: chamada para atualizar a lista de setores
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