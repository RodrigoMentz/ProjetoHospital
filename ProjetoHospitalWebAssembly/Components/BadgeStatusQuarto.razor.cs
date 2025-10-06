namespace ProjetoHospitalWebAssembly.Components
{
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;

    public partial class BadgeStatusQuarto
    {
        [Parameter]
        public LeitoStatusLimpezaViewModel LeitoStatus { get; set; }

        [Parameter]
        public string Class { get; set; } = string.Empty;

        private string Texto => LeitoStatus switch
        {
            _ when LeitoStatus.Ocupado && !LeitoStatus.PrecisaLimpezaTerminal && !LeitoStatus.PrecisaLimpezaConcorrente => "Ocupado",
            _ when !LeitoStatus.Ocupado && !LeitoStatus.PrecisaLimpezaTerminal && !LeitoStatus.PrecisaLimpezaConcorrente => "Disponível",
            _ when LeitoStatus.Ocupado && LeitoStatus.PrecisaLimpezaConcorrente => "Limpeza Concorrente",
            _ when !LeitoStatus.Ocupado && LeitoStatus.PrecisaLimpezaTerminal => "Limpeza Terminal",
            _ => string.Empty /*TODO: aguardando revisao*/
        };

        private string ClasseTextoEFundo => LeitoStatus switch
        {
            _ when LeitoStatus.Ocupado && !LeitoStatus.PrecisaLimpezaTerminal && !LeitoStatus.PrecisaLimpezaConcorrente => "ocupados",
            _ when !LeitoStatus.Ocupado && !LeitoStatus.PrecisaLimpezaTerminal && !LeitoStatus.PrecisaLimpezaConcorrente => "disponiveis",
            _ when LeitoStatus.Ocupado && LeitoStatus.PrecisaLimpezaConcorrente => "limpeza-concorrente",
            _ when !LeitoStatus.Ocupado && LeitoStatus.PrecisaLimpezaTerminal => "limpeza-terminal",
            //StatusQuartoEnum.Aguardando_Revisao => "aguardando-revisao", 
            _ => string.Empty
        };
    }
}
