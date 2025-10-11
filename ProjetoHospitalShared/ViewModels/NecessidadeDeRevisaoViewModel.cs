namespace ProjetoHospitalShared.ViewModels
{
    public class NecessidadeDeRevisaoViewModel
    {
        public NecessidadeDeRevisaoViewModel()
        {
        }

        public NecessidadeDeRevisaoViewModel(
            int limpezaId,
            int leitoId,
            string leitoNome,
            string quartoNome,
            string setorNome,
            TipoLimpezaEnum tipoLimpeza)
        {
            LimpezaId = limpezaId;
            LeitoId = leitoId;
            LeitoNome = leitoNome;
            QuartoNome = quartoNome;
            SetorNome = setorNome;
            TipoLimpeza = tipoLimpeza;
        }

        public int LimpezaId { get; set; }

        public int LeitoId { get; set; }

        public string LeitoNome { get; set; }

        public string QuartoNome { get; set; }

        public string SetorNome { get; set; }

        public TipoLimpezaEnum TipoLimpeza { get; set; }
    }
}