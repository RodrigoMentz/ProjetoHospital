using Microsoft.AspNetCore.Components;
using ProjetoHospitalShared.ViewModels;
namespace ProjetoHospitalWebAssembly.Components
{
    public partial class CardQuarto
    {
        [Parameter]
        public LeitoViewModel Leito { get; set; }
    }
}