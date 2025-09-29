namespace ProjetoHospitalShared.ViewModels
{
    public class QuartoViewModel
    {
        public QuartoViewModel()
        {
        }

        public QuartoViewModel(
            string nome,
            int idSetor,
            string nomeSetor,
            int capacidade,
            bool ativo)
        {
            this.Nome = nome;
            this.IdSetor = idSetor;
            this.NomeSetor = nomeSetor;
            this.Capacidade = capacidade;
            this.Ativo = ativo;
        }

        public string Nome { get; set; }

        public int IdSetor { get; set; }

        public string NomeSetor { get; set; }

        public int Capacidade { get; set; }

        public bool Ativo { get; set; }
    }
}