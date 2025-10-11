using Microsoft.AspNetCore.Components;
using ProjetoHospitalShared.ViewModels;
namespace ProjetoHospitalWebAssembly.Components
{
    public partial class CardQuarto
    {
        [Parameter]
        public LeitoStatusLimpezaViewModel Leito { get; set; }

        [Parameter]
        public LimpezaViewModel Limpeza { get; set; }

        [Parameter]
        public NecessidadeDeRevisaoViewModel NecessidadeRevisao { get; set; }

        [Parameter]
        public RevisaoViewModel Revisao { get; set; }

        [Parameter]
        public EventCallback OnClick { get; set; }

        [Parameter]
        public bool Disabled { get; set; } = false;
    }
}