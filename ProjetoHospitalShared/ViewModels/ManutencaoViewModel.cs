namespace ProjetoHospitalShared.ViewModels
{
    public class ManutencaoViewModel
    {
        public ManutencaoViewModel()
        {
        }

        public ManutencaoViewModel(
            int id)
        {
            this.Id = id;
            this.Turno = string.Empty;
            this.Descricao = string.Empty;
            this.IdSolicitante = string.Empty;
            this.NomeSolicitante = string.Empty;
            this.ContatoSolicitante = string.Empty;
        }

        public ManutencaoViewModel(
            int id,
            string idSolicitante,
            string nomeSolicitante,
            string contatoSolicitante,
            string turno,
            int setorId,
            string setorNome,
            DateTime dataDeSolicitacao,
            string descricao)
        {
            this.Id = id;
            this.IdSolicitante = idSolicitante;
            this.NomeSolicitante = nomeSolicitante;
            this.ContatoSolicitante = contatoSolicitante;
            this.Turno = turno;
            this.SetorId = setorId;
            this.SetorNome = setorNome;
            this.DataDeSolicitacao = dataDeSolicitacao;
            this.Descricao = descricao;
        }

        public int Id { get; set; }

        public string IdSolicitante { get; set; }

        public string NomeSolicitante { get; set; }

        public string ContatoSolicitante { get; set; }

        public string Turno { get; set; }

        public int SetorId { get; set; }

        public string? SetorNome { get; set; }

        public DateTime DataDeSolicitacao { get; set; }

        public string Descricao { get; set; }

        public string? IdExecutante { get; set; }

        public string? NomeExecutante { get; set; }

        public string? ContatoExecutante { get; set; }

        public string? TurnoExecutante { get; set; }

        public DateTime? DataDeConclusao { get; set; }
    }
}