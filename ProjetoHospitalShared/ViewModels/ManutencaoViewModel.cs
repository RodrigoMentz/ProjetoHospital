namespace ProjetoHospitalShared.ViewModels
{
    public class ManutencaoViewModel
    {
        public ManutencaoViewModel()
        {
        }

        public ManutencaoViewModel(
            int id,
            string nomeSolicitante,
            string contatoSolicitante,
            string turno,
            int setorId,
            string setorNome,
            DateTime dataDeSolicitacao,
            string descricao)
        {
            Id = id;
            NomeSolicitante = nomeSolicitante;
            ContatoSolicitante = contatoSolicitante;
            Turno = turno;
            SetorId = setorId;
            SetorNome = setorNome;
            DataDeSolicitacao = dataDeSolicitacao;
            Descricao = descricao;
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
    }
}