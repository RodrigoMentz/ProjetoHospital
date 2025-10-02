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
            StatusQuartoEnum.Limpeza_concorrente => "Limpeza Concorrente",
            StatusQuartoEnum.Limpeza_terminal => "Limpeza Terminal",
            StatusQuartoEnum.Aguardando_Revisao => "Aguardando Revisão",
            _ => string.Empty
        };

        private string ClasseTextoEFundo => Status switch
        {
            StatusQuartoEnum.Ocupado => "ocupados",
            StatusQuartoEnum.Disponivel => "disponiveis",
            StatusQuartoEnum.Limpeza_concorrente => "limpeza-concorrente",
            StatusQuartoEnum.Limpeza_terminal => "limpeza-terminal",
            StatusQuartoEnum.Aguardando_Revisao => "aguardando-revisao",
            _ => string.Empty
        };
    }
}
