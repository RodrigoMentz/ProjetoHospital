namespace ProjetoHospital.Entities
{
    public class Quarto
    {
        public Quarto(
            string nome,
            int idSetor,
            int capacidade,
            bool ativo)
        {
            this.Nome = nome;
            this.IdSetor = idSetor;
            this.Capacidade = capacidade;
            this.Ativo = ativo;
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public int IdSetor { get; set; }

        public int Capacidade { get; set; }

        public bool Ativo { get; set; }

        public bool SoftDelete { get; set; }

        public virtual Setor Setor { get; set; }
    }
}