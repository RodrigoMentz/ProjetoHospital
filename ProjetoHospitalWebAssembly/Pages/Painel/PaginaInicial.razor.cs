namespace ProjetoHospitalWebAssembly.Pages.Painel
{
    using Blazored.Modal;
    using Blazored.Modal.Services;
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Components.Modais;
    using ProjetoHospitalWebAssembly.Services;

    public partial class PaginaInicial
    {
        [Inject]
        private ILeitoService LeitoService { get; set; }

        [Inject]
        private ILimpezaService LimpezaService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private IModalService ModalService { get; set; }

        private bool isLoading = false;

        private List<LeitoStatusLimpezaViewModel> statusLeitos = new();
        private int quantidadeLeitosOcupados = 0;
        private int quantidadeLeitosDisponiveis = 0;
        private int quantidadeLeitosLimpezaConcorrente = 0;
        private int quantidadeLeitosLimpezaTerminal = 0;

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            await this.ConsultarStatus()
                .ConfigureAwait(true);

            this.quantidadeLeitosOcupados = this.statusLeitos.Where(l => l.Ocupado).Count();
            this.quantidadeLeitosDisponiveis = this.statusLeitos.Where(l => !l.Ocupado && !l.PrecisaLimpezaTerminal && !l.PrecisaLimpezaConcorrente).Count();
            this.quantidadeLeitosLimpezaConcorrente = this.statusLeitos.Where(l => l.PrecisaLimpezaConcorrente).Count();
            this.quantidadeLeitosLimpezaTerminal = this.statusLeitos.Where(l => l.PrecisaLimpezaTerminal).Count();

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task ConsultarStatus()
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                var response = await this.LimpezaService
                    .ConsultarListaStatusLimpezaAsync()
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.statusLeitos = response.Data;
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

        private async Task IniciarInternacao(
            LeitoStatusLimpezaViewModel leito)
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
                    "Você deseja iniciar uma nova internação neste leito?");

                var retornoModal = await this.ModalService
                    .Show<ModalDeConfirmacao>(
                        "Adicionar internação no leito",
                        parametros,
                        options)
                    .Result
                    .ConfigureAwait(true);

                if (!retornoModal.Cancelled)
                {
                    this.isLoading = true;
                    this.StateHasChanged();

                    var internacaoConfirmada = (bool)retornoModal.Data;

                    if (internacaoConfirmada)
                    {
                        leito.Ocupado = true;

                        var leitoParaAtualizar = new LeitoViewModel(
                            leito.LeitoId,
                            leito.Ocupado);

                        var response = await this.LeitoService
                            .AtualizarOcupadoAsync(leitoParaAtualizar)
                            .ConfigureAwait(true);

                        if (response != null && response.Success)
                        {
                            this.ToastService.ShowSuccess(
                                "Sucesso: Internação no leito realizada");
                        }

                        await this.ConsultarStatus()
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

        private async Task EncerrarInternacao(
            LeitoStatusLimpezaViewModel leito)
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
                    "Você deseja encerrar a internação neste leito?");

                var retornoModal = await this.ModalService
                    .Show<ModalDeConfirmacao>(
                        "Encerrar internação no leito",
                        parametros,
                        options)
                    .Result
                    .ConfigureAwait(true);

                if (!retornoModal.Cancelled)
                {
                    this.isLoading = true;
                    this.StateHasChanged();

                    var internacaoEncerrada = (bool)retornoModal.Data;

                    if (internacaoEncerrada)
                    {
                        leito.Ocupado = false;

                        var leitoParaAtualizar = new LeitoViewModel(
                            leito.LeitoId,
                            leito.Ocupado);

                        var response = await this.LeitoService
                            .AtualizarOcupadoAsync(leitoParaAtualizar)
                            .ConfigureAwait(true);

                        if (response != null && response.Success)
                        {
                            this.ToastService.ShowSuccess(
                                "Sucesso: Internação no leito encerrada");
                        }

                        await this.ConsultarStatus()
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