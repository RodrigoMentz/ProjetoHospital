using Microsoft.AspNetCore.Components;
using ProjetoHospitalShared;
namespace ProjetoHospitalWebAssembly.Components
{
    public partial class CardQuarto
    {
        [Parameter]
        public TipoLimpezaEnum TipoLimpeza { get; set; }

        [Parameter]
        public string Quarto { get; set; } = string.Empty;

        [Parameter]
        public string Andar { get; set; } = string.Empty;
    }
}