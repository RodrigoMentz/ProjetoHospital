namespace ProjetoHospital.Entities
{
    public class Setor
    {
        public Setor()
        {
        }

        public Setor(
            int id,
            string nome,
            bool ativo)
        {
            this.Id = id;
            this.Nome = nome;
            this.Ativo = ativo;
        }

        public Setor(
            string nome,
            bool ativo)
        {
            this.Nome = nome;
            this.Ativo = ativo;
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public bool SoftDelete { get; set; }

        public virtual ICollection<Manutencao> Manutencoes { get; set; }
    }
}