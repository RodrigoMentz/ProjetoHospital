namespace ProjetoHospitalShared.ViewModels
{
    public class RevisaoViewModel
    {
        public RevisaoViewModel()
        {
        }

        public RevisaoViewModel(
            int id)
        {
            this.Id = id;
            this.SolicitanteId = string.Empty;
            this.Observacoes = string.Empty;
        }

        public RevisaoViewModel(
            string observacoes,
            bool necessitaLimpeza,
            DateTime dataSolicitacao,
            string solicitanteId,
            int limpezaId,
            int leitoId)
        {
            this.Observacoes = observacoes;
            this.NecessitaLimpeza = necessitaLimpeza;
            this.DataSolicitacao = dataSolicitacao;
            this.SolicitanteId = solicitanteId;
            this.LimpezaId = limpezaId;
            this.LeitoId = leitoId;
        }

        public RevisaoViewModel(
            int id,
            string observacoes,
            DateTime dataSolicitacao,
            DateTime? dataInicioLimpeza,
            DateTime? dataFimLimpeza,
            string solicitanteId,
            string? executanteId,
            int limpezaId,
            int leitoId,
            string leitoNome,
            string quartoNome,
            int setorId,
            string setorNome)
        {
            this.Id = id;
            this.Observacoes = observacoes;
            this.DataSolicitacao = dataSolicitacao;
            this.DataInicioLimpeza = dataInicioLimpeza;
            this.DataFimLimpeza = dataFimLimpeza;
            this.SolicitanteId = solicitanteId;
            this.ExecutanteId = executanteId;
            this.LimpezaId = limpezaId;
            this.LeitoId = leitoId;
            this.LeitoNome = leitoNome;
            this.QuartoNome = quartoNome;
            this.SetorId = setorId;
            this.SetorNome = setorNome;
        }

        public int Id { get; set; }

        public string Observacoes { get; set; }

        public bool NecessitaLimpeza { get; set; }

        public DateTime DataSolicitacao { get; set; }

        public DateTime? DataInicioLimpeza { get; set; }

        public DateTime? DataFimLimpeza { get; set; }

        public string SolicitanteId { get; set; }

        public string? ExecutanteId { get; set; }

        public int LimpezaId { get; set; }

        public int LeitoId { get; set; }

        public string? LeitoNome { get; set; }

        public string? QuartoNome { get; set; }

        public string? SetorNome { get; set; }

        public int? SetorId { get; set; }
    }
}