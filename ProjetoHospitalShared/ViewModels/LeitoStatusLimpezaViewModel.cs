namespace ProjetoHospitalShared.ViewModels
{
    public class LeitoStatusLimpezaViewModel
    {
        public LeitoStatusLimpezaViewModel()
        {
        }

        public LeitoStatusLimpezaViewModel(
            int leitoId,
            string leitoNome,
            string quartoNome,
            int setorId,
            bool ocupado,
            bool precisaLimpezaConcorrente,
            bool precisaLimpezaTerminal,
            bool limpezaEmAndamento)
        {
            LeitoId = leitoId;
            LeitoNome = leitoNome;
            QuartoNome = quartoNome;
            SetorId = setorId;
            Ocupado = ocupado;
            PrecisaLimpezaConcorrente = precisaLimpezaConcorrente;
            PrecisaLimpezaTerminal = precisaLimpezaTerminal;
            LimpezaEmAndamento = limpezaEmAndamento;
        }

        public int LeitoId { get; set; }

        public string LeitoNome { get; set; }

        public string QuartoNome { get; set; }

        public int SetorId { get; set; }

        public string SetorNome { get; set; }

        public bool Ocupado { get; set; }

        public bool PrecisaLimpezaConcorrente { get; set; }

        public bool PrecisaLimpezaTerminal { get; set; }

        public bool LimpezaEmAndamento { get; set; }

        public DateTime DataHoraUltimaLimpeza { get; set; }

    }
}