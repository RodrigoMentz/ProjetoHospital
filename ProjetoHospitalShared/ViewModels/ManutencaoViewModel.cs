namespace ProjetoHospitalShared.ViewModels
{
    public class ManutencaoViewModel
    {
        public int Id { get; set; }

        public string NomeSolicitante { get; set; }

        public string ContatoSolicitante { get; set; }

        public string Turno { get; set; }

        public int SetorId { get; set; }

        public DateTime DataDeSolicitacao { get; set; }

        public string Descricao { get; set; }
    }
}