namespace ProjetoHospital.Entities
{
    public class Leito
    {
        public Leito(
            string nome,
            int idQuarto,
            bool ativo)
        {
            this.Nome = nome;
            this.IdQuarto = idQuarto;
            this.Ativo = ativo;
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public int IdQuarto { get; set; }

        public bool Ativo { get; set; }

        public bool Ocupado { get; set; }

        public DateTime? UltimaModificacao { get; set; }

        public bool SoftDelete { get; set; }

        public virtual Quarto Quarto { get; set; }
    }
}