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
            _ when LeitoStatus.Ocupado && !LeitoStatus.PrecisaLimpezaTerminal && !LeitoStatus.PrecisaLimpezaConcorrente && !LeitoStatus.PrecisaDeRevisao && !LeitoStatus.PrecisaDeLimpezaDeRevisao => "Ocupado",
            _ when !LeitoStatus.Ocupado && !LeitoStatus.PrecisaLimpezaTerminal && !LeitoStatus.PrecisaLimpezaConcorrente && !LeitoStatus.PrecisaDeRevisao && !LeitoStatus.PrecisaDeLimpezaDeRevisao => "Disponível",
            _ when LeitoStatus.Ocupado && LeitoStatus.PrecisaLimpezaConcorrente => "Limpeza Concorrente",
            _ when !LeitoStatus.Ocupado && LeitoStatus.PrecisaLimpezaTerminal => "Limpeza Terminal",
            _ when LeitoStatus.PrecisaDeRevisao => "Necessita revisão",
            _ when !LeitoStatus.PrecisaDeRevisao && LeitoStatus.PrecisaDeLimpezaDeRevisao => "Necessita limpeza de revisão",
            _ => string.Empty /*TODO: aguardando revisao*/
        };

        private string ClasseTextoEFundo => LeitoStatus switch
        {
            _ when LeitoStatus.Ocupado && !LeitoStatus.PrecisaLimpezaTerminal && !LeitoStatus.PrecisaLimpezaConcorrente && !LeitoStatus.PrecisaDeRevisao && !LeitoStatus.PrecisaDeLimpezaDeRevisao => "ocupados",
            _ when !LeitoStatus.Ocupado && !LeitoStatus.PrecisaLimpezaTerminal && !LeitoStatus.PrecisaLimpezaConcorrente && !LeitoStatus.PrecisaDeRevisao && !LeitoStatus.PrecisaDeLimpezaDeRevisao => "disponiveis",
            _ when LeitoStatus.Ocupado && LeitoStatus.PrecisaLimpezaConcorrente => "limpeza-concorrente",
            _ when !LeitoStatus.Ocupado && LeitoStatus.PrecisaLimpezaTerminal => "limpeza-terminal",
            _ when LeitoStatus.PrecisaDeRevisao => "aguardando-revisao",
            _ when !LeitoStatus.PrecisaDeRevisao && LeitoStatus.PrecisaDeLimpezaDeRevisao => "necessita-limpeza-revisao",
            _ => string.Empty
        };
    }
}
