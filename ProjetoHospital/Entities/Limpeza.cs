namespace ProjetoHospital.Entities
{
    using ProjetoHospitalShared;

    public abstract class Limpeza
    {
        protected Limpeza()
        {
        }

        protected Limpeza(
            int id,
            DateTime dataInicioLimpeza,
            DateTime? dataFimLimpeza,
            int leitoId,
            string usuarioId,
            TipoLimpezaEnum tipoLimpeza)
        {
            this.Id = id;
            this.DataInicioLimpeza = dataInicioLimpeza;
            this.DataFimLimpeza = dataFimLimpeza;
            this.LeitoId = leitoId;
            this.UsuarioId = usuarioId;
            this.TipoLimpeza = tipoLimpeza;
        }

        public int Id { get; set; }

        public DateTime DataInicioLimpeza { get; set; }

        public DateTime? DataFimLimpeza { get; set; }

        public int LeitoId { get; set; }

        public string UsuarioId { get; set; }

        public TipoLimpezaEnum TipoLimpeza { get; set; }

        public virtual Leito Leito { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}