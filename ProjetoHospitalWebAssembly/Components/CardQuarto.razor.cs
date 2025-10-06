using Microsoft.AspNetCore.Components;
using ProjetoHospitalShared.ViewModels;
namespace ProjetoHospitalWebAssembly.Components
{
    public partial class CardQuarto
    {
        [Parameter]
        public LeitoStatusLimpezaViewModel Leito { get; set; }

        [Parameter]
        public EventCallback OnClick { get; set; }
    }
}