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
            QuartoViewModel quarto,
            bool ativo)
        {
            this.Id = id;
            this.Nome = nome;
            this.Quarto = quarto;
            this.Ativo = ativo;
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public int IdQuarto { get; set; }

        public QuartoViewModel? Quarto { get; set; }

        public bool Ativo { get; set; }
    }
}