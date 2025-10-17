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
            this.UsuarioId = string.Empty;
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

        public LimpezaViewModel(
            int id,
            int leitoId,
            string? leitoNome,
            string nomeQuarto,
            string usuarioId,
            UsuarioViewModel? usuario,
            TipoLimpezaEnum tipoLimpeza,
            DateTime dataInicioLimpeza,
            DateTime? dataFimLimpeza)
        {
            this.Id = id;
            this.LeitoId = leitoId;
            this.LeitoNome = leitoNome;
            this.NomeQuarto = nomeQuarto;
            this.UsuarioId = usuarioId;
            this.Usuario = usuario;
            this.TipoLimpeza = tipoLimpeza;
            this.DataInicioLimpeza = dataInicioLimpeza;
            this.DataFimLimpeza = dataFimLimpeza;
        }

        public LimpezaViewModel(
            int id,
            int leitoId,
            string? leitoNome,
            string nomeQuarto,
            int idSetor,
            string nomeSetor,
            string usuarioId,
            UsuarioViewModel? usuario,
            TipoLimpezaEnum tipoLimpeza,
            DateTime dataInicioLimpeza,
            DateTime? dataFimLimpeza)
        {
            this.Id = id;
            this.LeitoId = leitoId;
            this.LeitoNome = leitoNome;
            this.NomeQuarto = nomeQuarto;
            this.IdSetor = idSetor;
            this.NomeSetor = nomeSetor;
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

        public string? NomeQuarto { get; set; }

        public int? IdSetor { get; set; }

        public string? NomeSetor { get; set; }
    }
}