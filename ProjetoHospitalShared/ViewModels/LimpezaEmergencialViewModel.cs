namespace ProjetoHospitalShared.ViewModels
{
    public class LimpezaEmergencialViewModel
    {
        public LimpezaEmergencialViewModel()
        {
        }

        public LimpezaEmergencialViewModel(
            int id,
            DateTime? dataInicioLimpeza,
            DateTime? dataFimLimpeza,
            string descricao,
            int leitoId,
            string solicitanteId,
            string? solicitanteNome,
            string? leitoNome,
            string? quartoNome,
            int? setorId,
            string? setorNome,
            string? usuarioId,
            bool? revisado)
        {
            this.Id = id;
            this.DataInicioLimpeza = dataInicioLimpeza;
            this.DataFimLimpeza = dataFimLimpeza;
            this.Descricao = descricao;
            this.LeitoId = leitoId;
            this.SolicitanteId = solicitanteId;
            this.SolicitanteNome = solicitanteNome;
            this.LeitoNome = leitoNome;
            this.QuartoNome = quartoNome;
            this.SetorId = setorId;
            this.SetorNome = setorNome;
            this.UsuarioId = usuarioId;
            this.Revisado = revisado;
        }

        public int Id { get; set; }

        public DateTime? DataInicioLimpeza { get; set; }

        public DateTime? DataFimLimpeza { get; set; }

        public string Descricao { get; set; }

        public int LeitoId { get; set; }

        public string SolicitanteId { get; set; }

        public string? SolicitanteNome { get; set; }

        public string? LeitoNome { get; set; }

        public string? QuartoNome { get; set; }

        public int? SetorId { get; set; }

        public string? SetorNome { get; set; }

        public string? UsuarioId { get; set; }

        public bool? Revisado { get; set; }
    }
}