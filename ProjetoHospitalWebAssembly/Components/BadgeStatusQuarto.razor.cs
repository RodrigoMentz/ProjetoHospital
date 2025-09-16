using Microsoft.AspNetCore.Components;
using ProjetoHospitalShared;

namespace ProjetoHospitalWebAssembly.Components
{
    public partial class BadgeStatusQuarto
    {
        [Parameter]
        public StatusQuartoEnum Status { get; set; }

        [Parameter]
        public string Class { get; set; } = string.Empty;

        private string Texto => Status switch
        {
            StatusQuartoEnum.Ocupado => "Ocupado",
            StatusQuartoEnum.Disponivel => "Disponível",
            StatusQuartoEnum.Limpeza_corrente => "Limpeza Corrente",
            StatusQuartoEnum.Limpeza_terminal => "Limpeza Terminal",
            _ => string.Empty
        };

        private string ClasseTextoEFundo => Status switch
        {
            StatusQuartoEnum.Ocupado => "ocupados",
            StatusQuartoEnum.Disponivel => "disponiveis",
            StatusQuartoEnum.Limpeza_corrente => "limpeza-corrente",
            StatusQuartoEnum.Limpeza_terminal => "limpeza-terminal",
            _ => string.Empty
        };
    }
}
