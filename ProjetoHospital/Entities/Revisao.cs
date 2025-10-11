namespace ProjetoHospital.Entities
{
    public class Revisao
    {
        public Revisao()
        {
        }

        public Revisao(
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

        public bool SoftDelete { get; set; }

        public virtual Leito Leito { get; set; }

        public virtual Limpeza Limpeza { get; set; }

        public virtual Usuario Solicitante { get; set; }

        public virtual Usuario Executante { get; set; }
    }
}