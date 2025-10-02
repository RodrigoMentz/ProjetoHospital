namespace ProjetoHospitalWebAssembly.Pages.Painel
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public partial class PaginaInicial
    {
        private bool isLoading = false;

        private List<LeitoViewModel> leitos = new();
        private int quantidadeLeitosOcupados = 0;
        private int quantidadeLeitosDisponiveis = 0;
        private int quantidadeLeitosLimpezaConcorrente = 0;
        private int quantidadeLeitosLimpezaTerminal = 0;

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            this.leitos = new List<LeitoViewModel>
            {
                new LeitoViewModel(1, "101A", 1, "101", "SUS", StatusQuartoEnum.Disponivel, DateTime.Now.AddHours(-2)),
                new LeitoViewModel(2, "101B", 2, "101", "Maternidade", StatusQuartoEnum.Ocupado, DateTime.Now.AddHours(-10)),
                new LeitoViewModel(3, "102A", 3, "102", "Pediatria", StatusQuartoEnum.Aguardando_Revisao, DateTime.Now.AddMinutes(-10)),
                new LeitoViewModel(4, "102B", 4, "102", "Emergência", StatusQuartoEnum.Limpeza_concorrente, DateTime.Now.AddHours(-3)),
                new LeitoViewModel(5, "103A", 5, "103", "Sala vermelha",StatusQuartoEnum.Limpeza_terminal, DateTime.Now.AddHours(-20)),
            };

            this.quantidadeLeitosOcupados = this.leitos.Where(l => l.Status == StatusQuartoEnum.Ocupado).Count();
            this.quantidadeLeitosDisponiveis = this.leitos.Where(l => l.Status == StatusQuartoEnum.Disponivel).Count();
            this.quantidadeLeitosLimpezaConcorrente = this.leitos.Where(l => l.Status == StatusQuartoEnum.Limpeza_concorrente).Count();
            this.quantidadeLeitosLimpezaTerminal = this.leitos.Where(l => l.Status == StatusQuartoEnum.Limpeza_terminal).Count();


            this.isLoading = false;
            this.StateHasChanged();
        }
    }
}