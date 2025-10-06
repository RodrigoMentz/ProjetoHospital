namespace ProjetoHospitalWebAssembly.Pages
{
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class LimpezaConcorrente : ComponentBase
    {
        [Parameter]
        public string IdLeito { get; set; } = string.Empty;

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private ILimpezaService LimpezaService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        private bool isLoading = false;

        private LimpezaConcorrenteViewModel limpeza = new();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                this.limpeza.DataInicioLimpeza = DateTime.Now;
                this.limpeza.LeitoId = int.Parse(this.IdLeito);
                this.limpeza.UsuarioId = "1"; // TODO: Pegar o ID do usuário logado
            
                var response = await LimpezaService
                    .CriarConcorrenteAsync(this.limpeza)
                    .ConfigureAwait(true);

                if (response.Success)
                {
                    this.limpeza.Id = response.Data.Id;
                }
                else
                {
                    this.ToastService.ShowError(
                        "Erro: Erro inesperado contate o suporte");

                    await Task.Delay(5000)
                        .ConfigureAwait(true);

                    this.NavigationManager
                        .NavigateTo($"/quartos-para-limpar");
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

        private void MarcarTodosComoRealizado()
        {
            this.limpeza.TirarLixo = true;
            this.limpeza.LimparVasoSanitario = true;
            this.limpeza.LimparBox = true;
            this.limpeza.RevisarMofo = true;
            this.limpeza.LimparPia = true;
            this.limpeza.LimparCama = true;
            this.limpeza.LimparMesaCabeceira = true;
            this.limpeza.LimparLixeira = true;
        }

        private async Task FinalizarLimpezaAsync()
        {
            try
            {
                this.limpeza.DataFimLimpeza = DateTime.Now;

                var response = await LimpezaService
                    .FinalizarConcorrenteAsync(this.limpeza)
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.ToastService.ShowSuccess(
                        "Sucesso: Limpeza finalizada com sucesso");

                    await Task.Delay(3000)
                        .ConfigureAwait(true);

                    this.NavigationManager
                    .NavigateTo($"/quartos-para-limpar");
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

            this.isLoading = false;
            this.StateHasChanged();
        }
    }
}