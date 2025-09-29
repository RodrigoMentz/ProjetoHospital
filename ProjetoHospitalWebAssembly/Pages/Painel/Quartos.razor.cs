using ProjetoHospitalShared.ViewModels;

namespace ProjetoHospitalWebAssembly.Pages.Painel
{
    public partial class Quartos
    {
        private bool isLoading = false;

        private List<QuartoViewModel> quartos = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            this.quartos = new List<QuartoViewModel>
            {
                new QuartoViewModel("101", 1, "SUS", 1, true),
                new QuartoViewModel("102", 2, "Maternidade", 1, true),
                new QuartoViewModel("103", 3, "Pediatria", 1, true),
                new QuartoViewModel("104", 4, "Emergência", 1, true),
                new QuartoViewModel("105", 5, "Sala vermelha", 1, true),
                new QuartoViewModel("106", 6, "Bloco cirúrgico", 1, false),
            };

            this.isLoading = false;
            this.StateHasChanged();
        }
    }
}