using Microsoft.AspNetCore.Components;
using ProjetoHospitalShared;

namespace ProjetoHospitalWebAssembly.Components
{
    public partial class CardStatusQuartoPainel
    {
        [Parameter]
        public StatusQuartoEnum Status { get; set; }

        [Parameter]
        public string ClasseCssExterno { get; set; } = string.Empty;

        [Parameter]
        public int Quantidade { get; set; } = 0;

        private string Texto => Status switch
        {
            StatusQuartoEnum.Ocupado => "Ocupados",
            StatusQuartoEnum.Disponivel => "Disponíveis",
            StatusQuartoEnum.Aguardando_revisao => "Aguardando Revisão",
            StatusQuartoEnum.Limpeza_apos_revisao => "Limpeza Após Revisão",
            StatusQuartoEnum.Limpeza_concorrente => "Limpeza Concorrente",
            StatusQuartoEnum.Limpeza_terminal => "Limpeza Terminal",
            StatusQuartoEnum.Limpeza_emergencial => "Limpeza Emergencial",
            _ => string.Empty
        };

        private string ClasseCss =>  Status switch
        {
            StatusQuartoEnum.Ocupado => "ocupados",
            StatusQuartoEnum.Disponivel => "disponiveis",
            StatusQuartoEnum.Aguardando_revisao => "aguardando-revisao",
            StatusQuartoEnum.Limpeza_apos_revisao => "limpeza-apos-revisao",
            StatusQuartoEnum.Limpeza_concorrente => "limpeza-concorrente",
            StatusQuartoEnum.Limpeza_terminal => "limpeza-terminal",
            StatusQuartoEnum.Limpeza_emergencial => "limpeza-emergencial",
            _ => string.Empty
        };
    }
}