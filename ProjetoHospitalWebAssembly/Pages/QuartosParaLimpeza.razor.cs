using Microsoft.AspNetCore.Components;
using ProjetoHospitalShared;
using ProjetoHospitalShared.ViewModels;

namespace ProjetoHospitalWebAssembly.Pages
{
    public partial class QuartosParaLimpeza
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;

        private bool isLoading = false;

        private List<SetorViewModel> setores = new()
        {
            new SetorViewModel(1, "SUS"),
            new SetorViewModel(2, "Maternidade"),
            new SetorViewModel(3, "Pediatria"),
            new SetorViewModel(4, "Emergência"),
            new SetorViewModel(5, "Sala vermelha"),
            new SetorViewModel(6, "Bloco cirúrgico"),
        };
        private List<LeitoViewModel> leitos = new();
        private List<LeitoViewModel> leitosFiltrados = new();

        private SetorViewModel setorSelecionado = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            var leito1 = new LeitoViewModel
            {
               Id = 1,
               NomeQuarto = "A1",
               IdSetor = 3,
               NomeSetor = "Pediatria",
               Nome = "01",
               TipoLimpeza = TipoLimpezaEnum.Terminal,
            };

            var leito2 = new LeitoViewModel
            {
                Id = 1,
                NomeQuarto = "102",
                IdSetor = 4,
                NomeSetor = "Emergência",
                Nome = "03",
                TipoLimpeza = TipoLimpezaEnum.Concorrente,
            };

            leitos.Add(leito1);
            leitos.Add(leito2);

            this.leitosFiltrados = this.leitos;

            var setorTodos = new SetorViewModel(0, "Todos");
            this.setores.Insert(0, setorTodos);
            this.setorSelecionado = this.setores.First();

            this.isLoading = false;
            this.StateHasChanged();
        }

        private void OnSetorChange(SetorViewModel setor)
        {
            this.setorSelecionado = setor;

            if (setor.Id == 0)
            {
                this.leitosFiltrados = this.leitos;

                return;
            }

            this.leitosFiltrados = this.leitos
                .Where(l => l.IdSetor == setor.Id)
                .ToList();
        }

        private void LimparLeito(LeitoViewModel leito)
        {
            if (leito.TipoLimpeza == TipoLimpezaEnum.Concorrente)
            {
                this.NavigationManager
                    .NavigateTo($"/limpeza-concorrente/{leito.Id}");
            }
            else
            {
                this.NavigationManager
                    .NavigateTo($"/limpeza-terminal/{leito.Id}");
            }
        }
    }
}