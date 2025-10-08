namespace ProjetoHospital.Entities
{
    public class Manutencao
    {
        public Manutencao()
        {
        }

        public Manutencao(
            int id,
            string nomeSolicitante,
            string contatoSolicitante,
            string turno,
            int setorId,
            DateTime dataDeSolicitacao,
            string descricao,
            string status,
            DateTime? dataDeConclusao,
            string? nomeExecutante,
            string? contatoExecutante,
            string? turnoExecutante,
            Setor setor)
        {
            Id = id;
            NomeSolicitante = nomeSolicitante;
            ContatoSolicitante = contatoSolicitante;
            Turno = turno;
            SetorId = setorId;
            DataDeSolicitacao = dataDeSolicitacao;
            Descricao = descricao;
            Status = status;
            DataDeConclusao = dataDeConclusao;
            NomeExecutante = nomeExecutante;
            ContatoExecutante = contatoExecutante;
            TurnoExecutante = turnoExecutante;
            Setor = setor;
        }

        public int Id { get; set; }

        public string NomeSolicitante { get; set; }

        public string ContatoSolicitante { get; set; }

        public string Turno { get; set; }

        public int SetorId { get; set; }

        public DateTime DataDeSolicitacao { get; set; }

        public string Descricao { get; set; }

        public string Status { get; set; }

        public DateTime? DataDeConclusao { get; set; }

        public string? NomeExecutante { get; set; }

        public string? ContatoExecutante { get; set; }

        public string? TurnoExecutante { get; set; }

        public virtual Setor Setor { get; set; }
    }
}