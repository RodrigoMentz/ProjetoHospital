using Microsoft.AspNetCore.Components;
using ProjetoHospitalShared;

namespace ProjetoHospitalWebAssembly.Components
{
    public partial class NumeroQuartos
    {
        [Parameter]
        public TipoLimpezaEnum TipoLimpeza { get; set; }

        [Parameter]
        public string Texto { get; set; } = string.Empty;

        [Parameter]
        public int Quantidade { get; set; } = 0;
    }
}