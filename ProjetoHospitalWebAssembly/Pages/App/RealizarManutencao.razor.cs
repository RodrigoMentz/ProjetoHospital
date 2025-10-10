namespace ProjetoHospitalWebAssembly.Pages.App
{
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class RealizarManutencao : ComponentBase
    {
        [Parameter]
        public string Id { get; set; } = string.Empty;

        [Inject]
        private IManutencaoService ManutencaoService { get; set; }

        [Inject]
        private IUsuarioService UsuarioService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private bool isLoading = false;

        private List<string> turnos;
        private ManutencaoViewModel manutencao = new();
        private UsuarioViewModel usuarioLocalStorage = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            this.turnos = Turnos.Listar();

            if (Id != null)
            {
                await this.ConsultarManutencaoParaRealizar()
                    .ConfigureAwait(true);
            }
            else
            {
                this.NavigationManager
                    .NavigateTo("/inicio");

                return;
            }

            this.usuarioLocalStorage = await this.UsuarioService
                .ConsultarUsuarioLocalStorage()
                .ConfigureAwait(true);

            this.manutencao.Turno = this.turnos.FirstOrDefault();
            this.manutencao.IdExecutante = usuarioLocalStorage.Id;
            this.manutencao.NomeExecutante = usuarioLocalStorage.Nome;
            this.manutencao.ContatoExecutante = usuarioLocalStorage.NumeroTelefone;

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task ConsultarManutencaoParaRealizar()
        {
            try
            {
                var idManutencao = int.Parse(this.Id);

                var manutencaoParaRealizacao = new ManutencaoViewModel(idManutencao);
                var response = await this.ManutencaoService
                    .GetDetalhesDaManutencaoAsync(
                        manutencaoParaRealizacao)
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.manutencao = response.Data;
                }
            }
            catch (Exception e)
            {
                this.ToastService.ShowError(
                    "Erro: Erro inesperado contate o suporte");
            }
        }

        private async Task FinalizarManutencao()
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                this.manutencao.DataDeConclusao = DateTime.Now;

                var response = await this.ManutencaoService
                    .FinalizarAsync(manutencao)
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.ToastService.ShowSuccess(
                        "Sucesso: Manutenção realizada");

                    this.NavigationManager
                            .NavigateTo("/manutencoes");
                }
                else if (response != null && response.Notifications.Any())
                {
                    foreach (var notification in response.Notifications)
                    {
                        this.ToastService.ShowError(
                            $"Erro: {notification.Message}");
                    }
                }
            }
            catch (Exception e)
            {
                this.ToastService.ShowError(
                    "Erro: Erro inesperado contate o suporte");
            }

            this.isLoading = true;
            this.StateHasChanged();
        }
    }
}