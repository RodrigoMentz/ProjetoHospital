namespace ProjetoHospitalShared.ViewModels
{
    public class LeitoViewModel
    {
        public LeitoViewModel()
        {
        }

        public LeitoViewModel(
            int id,
            string nome,
            int idQuarto,
            bool ativo)
        {
            this.Id = id;
            this.Nome = nome;
            this.IdQuarto = idQuarto;
            this.Ativo = ativo;
        }

        public LeitoViewModel(
            string nome,
            int idQuarto,
            bool ativo)
        {
            this.Nome = nome;
            this.IdQuarto = idQuarto;
            this.Ativo = ativo;
        }

        public LeitoViewModel(
            int id,
            string nome,
            int idQuarto,
            string nomeQuarto,
            string nomeSetor,
            bool ativo)
        {
            this.Id = id;
            this.Nome = nome;
            this.IdQuarto = idQuarto;
            this.NomeQuarto = nomeQuarto;
            this.NomeSetor = nomeSetor;
            this.Ativo = ativo;
        }

        public LeitoViewModel(
            int id,
            string nome,
            int idQuarto,
            string nomeQuarto,
            string nomeSetor,
            StatusQuartoEnum status,
            DateTime dataHoraUltimaLimpeza)
        {
            this.Id = id;
            this.Nome = nome;
            this.IdQuarto = idQuarto;
            this.NomeQuarto = nomeQuarto;
            this.NomeSetor = nomeSetor;
            this.Status = status;
            this.DataHoraUltimaLimpeza = dataHoraUltimaLimpeza;
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public int IdQuarto { get; set; }

        public string NomeQuarto { get; set; }

        public int IdSetor { get; set; }

        public string NomeSetor { get; set; }

        public TipoLimpezaEnum TipoLimpeza { get; set; }

        public StatusQuartoEnum Status { get; set; }

        public DateTime DataHoraUltimaLimpeza { get; set; }

        public bool Ativo { get; set; }
    }
}