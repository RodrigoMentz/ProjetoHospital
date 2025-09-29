using ProjetoHospitalShared.ViewModels;

namespace ProjetoHospitalWebAssembly.Pages.Painel
{
    public partial class Setores
    {
        private bool isLoading = false;

        private List<SetorViewModel> setores = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

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
    }
}