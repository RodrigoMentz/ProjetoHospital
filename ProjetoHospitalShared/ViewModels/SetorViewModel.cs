namespace ProjetoHospitalShared.ViewModels
{
    public class SetorViewModel
    {
        public SetorViewModel()
        {
        }

        public SetorViewModel(
            int id,
            string nome)
        {
            this.Id = id;
            this.Nome = nome;
        }

        public SetorViewModel(
            int id,
            string nome,
            bool ativo)
        {
            this.Id = id;
            this.Nome = nome;
            this.Ativo = ativo;
        }

        public SetorViewModel(
            string nome,
            bool ativo)
        {
            this.Nome = nome;
            this.Ativo = ativo;
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public bool Ativo { get; set; }
    }
}