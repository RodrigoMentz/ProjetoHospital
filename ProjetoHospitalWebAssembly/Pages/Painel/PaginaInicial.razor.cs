namespace ProjetoHospitalWebAssembly.Pages.Painel
{
    using Blazored.Modal;
    using Blazored.Modal.Services;
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Components.Modais;
    using ProjetoHospitalWebAssembly.Services;

    public partial class PaginaInicial : ComponentBase
    {
        [Inject]
        private ILeitoService LeitoService { get; set; }

        [Inject]
        private ILimpezaService LimpezaService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private IModalService ModalService { get; set; }

        [Inject]
        private ISetorService SetorService { get; set; }

        [Inject]
        private IQuartoService QuartoService { get; set; }

        private bool isLoading = false;
        private List<LeitoStatusLimpezaViewModel> statusLeitos = new();
        private List<LeitoStatusLimpezaViewModel> statusLeitosFiltrados = new();
        private List<QuartoViewModel> quartos = new();
        private List<SetorViewModel> setores = new();
        private SetorViewModel setorSelecionado = new();
        private StatusQuartoEnum statusSelecionado = StatusQuartoEnum.Todos;
        private int quantidadeLeitosLimpezaEmergencial = 0;
        private int quantidadeLeitosLimpezaTerminal = 0;
        private int quantidadeLeitosLimpezaConcorrente = 0;
        private int quantidadeLeitosLimpezaPosRevisao = 0;
        private int quantidadeLeitosOcupados = 0;
        private int quantidadeLeitosDisponiveis = 0;
        private int quantidadeLeitosAguardandoRevisao = 0;
        private int idQuartoSelecionado;
        private int idSetorSelecionado = 0;
        private int IdQuartoSelecionado
        {
            get => idQuartoSelecionado;
            set
            {
                if (idQuartoSelecionado != value)
                {
                    idQuartoSelecionado = value;
                    OnFiltroChange(this.statusSelecionado, this.setorSelecionado);
                }
            }
        }

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

                    OnFiltroChange(this.statusSelecionado, setor);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            await this.ConsultarStatus()
                .ConfigureAwait(true);

            await this.ConsultarSetores()
                .ConfigureAwait(true);

            await this.ConsultarQuartos()
                .ConfigureAwait(true);

            var setorTodos = new SetorViewModel(0, "Todos");
            this.setores.Insert(0, setorTodos);
            this.setorSelecionado = this.setores.First();

            this.quantidadeLeitosLimpezaEmergencial = this.statusLeitos.Where(l => l.PrecisaDeLimpezaEmergencial).Count();
            this.quantidadeLeitosLimpezaTerminal = this.statusLeitos.Where(l => l.PrecisaLimpezaTerminal && !l.Ocupado).Count();
            this.quantidadeLeitosLimpezaConcorrente = this.statusLeitos.Where(l => l.PrecisaLimpezaConcorrente && l.Ocupado).Count();
            this.quantidadeLeitosLimpezaPosRevisao = this.statusLeitos.Where(l => l.PrecisaDeLimpezaDeRevisao).Count();
            this.quantidadeLeitosOcupados = this.statusLeitos.Where(l => l.Ocupado).Count();
            this.quantidadeLeitosDisponiveis = this.statusLeitos
                .Where(l => !l.Ocupado
                    && !l.PrecisaLimpezaTerminal
                    && !l.PrecisaLimpezaConcorrente
                    && !l.PrecisaDeRevisao
                    && !l.PrecisaDeLimpezaDeRevisao
                    && !l.PrecisaDeLimpezaEmergencial)
                .Count();
            this.quantidadeLeitosAguardandoRevisao = this.statusLeitos.Where(l => l.PrecisaDeRevisao
                && !l.PrecisaLimpezaTerminal
                && !l.PrecisaLimpezaConcorrente
                && !l.PrecisaDeLimpezaEmergencial).Count();


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

        private async Task ConsultarQuartos()
        {
            try
            {
                var responseSetores = await this.QuartoService
                    .GetAsync()
                    .ConfigureAwait(true);

                if (responseSetores != null && responseSetores.Success)
                {
                    this.quartos = responseSetores.Data;
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
                    this.statusLeitosFiltrados = this.statusLeitos;
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

        private async Task VisualizarDetalhesAsync(int IdLeito)
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
                    nameof(ModalHistoricoLeito.IdLeito),
                    IdLeito);

                var retornoModal = await this.ModalService
                    .Show<ModalHistoricoLeito>(
                        "Histórico de limpezas do leito",
                        parametros,
                        options)
                    .Result
                    .ConfigureAwait(true);

                if (!retornoModal.Cancelled)
                {
                    
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

        private async Task AdicionarLimpezaEmergencialAsync()
        {
            try
            {
                var options = new ModalOptions
                {
                    Position = ModalPosition.Middle,
                    Size = ModalSize.Large,
                };

                var retornoModal = await this.ModalService
                    .Show<ModalCadastroLimpezaEmergencial>(
                        "Adicionar limpeza emergencial",
                        options)
                    .Result
                    .ConfigureAwait(true);

                if (!retornoModal.Cancelled)
                {
                    await this.ConsultarStatus()
                        .ConfigureAwait(true);
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

        private void OnFiltroChange(StatusQuartoEnum status, SetorViewModel setor)
        {
            this.statusSelecionado = status;
            this.setorSelecionado = setor;

            var statusLeitosFiltradosSetor = this.statusLeitos;

            if (setor.Id != 0)
            {
                statusLeitosFiltradosSetor = this.statusLeitos
                    .Where(l => l.SetorId == setor.Id)
                    .ToList();
            }

            if (idQuartoSelecionado != 0)
            {
                statusLeitosFiltradosSetor = this.statusLeitos
                    .Where(l => l.QuartoId == idQuartoSelecionado)
                    .ToList();
            }

            this.statusLeitosFiltrados = status switch
            {
                StatusQuartoEnum.Todos => statusLeitosFiltradosSetor,

                StatusQuartoEnum.Limpeza_emergencial => statusLeitosFiltradosSetor
                    .Where(l => l.PrecisaDeLimpezaEmergencial)
                    .ToList(),

                StatusQuartoEnum.Ocupado => statusLeitosFiltradosSetor
                   .Where(l => l.Ocupado
                       && !l.PrecisaLimpezaTerminal
                       && !l.PrecisaLimpezaConcorrente
                       && !l.PrecisaDeRevisao
                       && !l.PrecisaDeLimpezaDeRevisao
                       && l.PrecisaDeLimpezaEmergencial)
                   .ToList(),

                StatusQuartoEnum.Disponivel => statusLeitosFiltradosSetor
                     .Where(l => !l.Ocupado
                         && !l.PrecisaLimpezaTerminal
                         && !l.PrecisaLimpezaConcorrente
                         && !l.PrecisaDeRevisao
                         && !l.PrecisaDeLimpezaDeRevisao
                         && !l.PrecisaDeLimpezaEmergencial)
                     .ToList(),

                StatusQuartoEnum.Limpeza_concorrente => statusLeitosFiltradosSetor
                   .Where(l => l.PrecisaLimpezaConcorrente
                       && l.Ocupado)
                   .ToList(),

                StatusQuartoEnum.Limpeza_terminal => statusLeitosFiltradosSetor
                    .Where(l => l.PrecisaLimpezaTerminal
                        && !l.Ocupado)
                    .ToList(),

                StatusQuartoEnum.Aguardando_revisao => statusLeitosFiltradosSetor
                    .Where(l => l.PrecisaDeRevisao
                        && !l.PrecisaLimpezaTerminal
                        && !l.PrecisaLimpezaConcorrente
                        && !l.PrecisaDeLimpezaEmergencial)
                    .ToList(),

                StatusQuartoEnum.Limpeza_apos_revisao => statusLeitosFiltradosSetor
                    .Where(l => !l.PrecisaDeRevisao
                        && l.PrecisaDeLimpezaDeRevisao)
                    .ToList(),

                _ => new List<LeitoStatusLimpezaViewModel>()
            };
        }
    }
}