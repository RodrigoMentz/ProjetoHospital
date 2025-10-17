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

        public NecessidadeDeRevisaoViewModel(
            int limpezaId,
            int leitoId,
            string leitoNome,
            string quartoNome,
            string setorNome,
            TipoLimpezaEnum tipoLimpeza,
            int revisaoId,
            string solicitanteId)
        {
            LimpezaId = limpezaId;
            LeitoId = leitoId;
            LeitoNome = leitoNome;
            QuartoNome = quartoNome;
            SetorNome = setorNome;
            TipoLimpeza = tipoLimpeza;
            RevisaoId = revisaoId;
            SolicitanteId = solicitanteId;
        }

        public int LimpezaId { get; set; }

        public int LeitoId { get; set; }

        public string LeitoNome { get; set; }

        public string QuartoNome { get; set; }

        public string SetorNome { get; set; }

        public int RevisaoId { get; set; }

        public string SolicitanteId { get; set; }

        public TipoLimpezaEnum TipoLimpeza { get; set; }
    }
}