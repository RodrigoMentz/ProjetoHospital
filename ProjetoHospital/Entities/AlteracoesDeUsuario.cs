namespace ProjetoHospital.Entities
{
    public class AlteracoesDeUsuario
    {
        public AlteracoesDeUsuario()
        {
        }

        public AlteracoesDeUsuario(
            string idUsuarioExecutante,
            string idUsuarioAfetado,
            string descricao,
            DateTime? dataAlteracao)
        {
            this.idUsuarioExecutante = idUsuarioExecutante;
            this.idUsuarioAfetado = idUsuarioAfetado;
            this.Descricao = descricao;
            this.DataAlteracao = dataAlteracao;
        }

        public int Id { get; set; }

        public string idUsuarioExecutante { get; set; }

        public string idUsuarioAfetado { get; set; }

        public string Descricao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public virtual Usuario Executante { get; set; }

        public virtual Usuario Afetado { get; set; }
    }
}