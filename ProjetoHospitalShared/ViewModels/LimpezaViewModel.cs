namespace ProjetoHospitalShared.ViewModels
{
    public class LimpezaViewModel
    {
        public LimpezaViewModel()
        {
        }

        public LimpezaViewModel(
            int id)
        {
            this.Id = id;
        }

        public LimpezaViewModel(
            int id,
            int leitoId,
            string? leitoNome,
            string usuarioId,
            UsuarioViewModel? usuario,
            TipoLimpezaEnum tipoLimpeza,
            DateTime dataInicioLimpeza,
            DateTime? dataFimLimpeza)
        {
            this.Id = id;
            this.LeitoId = leitoId;
            this.LeitoNome = leitoNome;
            this.UsuarioId = usuarioId;
            this.Usuario = usuario;
            this.TipoLimpeza = tipoLimpeza;
            this.DataInicioLimpeza = dataInicioLimpeza;
            this.DataFimLimpeza = dataFimLimpeza;
        }

        public int Id { get; set; }

        public int LeitoId { get; set; }

        public string? LeitoNome { get; set; }

        public string UsuarioId { get; set; }

        public UsuarioViewModel? Usuario { get; set; }

        public TipoLimpezaEnum? TipoLimpeza { get; set; }

        public DateTime DataInicioLimpeza { get; set; }

        public DateTime? DataFimLimpeza { get; set; }
    }
}