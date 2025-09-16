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
            StatusQuartoEnum.Limpeza_corrente => "Limpeza Corrente",
            StatusQuartoEnum.Limpeza_terminal => "Limpeza Terminal",
            _ => string.Empty
        };

        private string ClasseCss =>  Status switch
        {
            StatusQuartoEnum.Ocupado => "ocupados",
            StatusQuartoEnum.Disponivel => "disponiveis",
            StatusQuartoEnum.Limpeza_corrente => "limpeza-corrente",
            StatusQuartoEnum.Limpeza_terminal => "limpeza-terminal",
            _ => string.Empty
        };
    }
}