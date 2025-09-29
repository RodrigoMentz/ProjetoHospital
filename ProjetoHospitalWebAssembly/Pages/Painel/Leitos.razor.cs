using ProjetoHospitalShared.ViewModels;

namespace ProjetoHospitalWebAssembly.Pages.Painel
{
    public partial class Leitos
    {
        private bool isLoading = false;

        private List<LeitoViewModel> leitos = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            this.leitos = new List<LeitoViewModel>
            {
                new LeitoViewModel(1, "101A", "101", "SUS", true),
                new LeitoViewModel(2, "101B", "101", "Maternidade", true),
                new LeitoViewModel(3, "102A", "102", "Pediatria", true),
                new LeitoViewModel(4, "102B", "102", "Emergência", true),
                new LeitoViewModel(5, "103A", "103", "Sala vermelha",true),
                new LeitoViewModel(6, "103B", "103", "Bloco cirúrgico", false),
            };

            this.isLoading = false;
            this.StateHasChanged();
        }
    }
}