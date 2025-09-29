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
            string numeroQuarto,
            int idSetor,
            string nomeSetor,
            TipoLimpezaEnum tipoLimpeza)
        {
            this.Id = id;
            this.Nome = nome;
            this.NomeQuarto = numeroQuarto;
            this.IdSetor = idSetor;
            this.NomeSetor = nomeSetor;
            this.TipoLimpeza = tipoLimpeza;
        }

        public LeitoViewModel(
            int id,
            string nome,
            string numeroQuarto,
            string nomeSetor,
            bool ativo)
        {
            this.Id = id;
            this.Nome = nome;
            this.NomeQuarto = numeroQuarto;
            this.NomeSetor = nomeSetor;
            this.Ativo = ativo;
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public string NomeQuarto { get; set; }

        public int IdSetor { get; set; }

        public string NomeSetor { get; set; }

        public TipoLimpezaEnum TipoLimpeza { get; set; }

        public bool Ativo { get; set; }
    }
}