namespace ProjetoHospitalShared.ViewModels
{
    public class LeitoParaLimpezaViewModel
    {
        public LeitoParaLimpezaViewModel()
        {
        }

        public LeitoParaLimpezaViewModel(
            int id,
            string nome,
            string numeroQuarto,
            int idSetor,
            string nomeSetor,
            TipoLimpezaEnum tipoLimpeza)
        {
            this.Id = id;
            this.Nome = nome;
            this.NumeroQuarto = numeroQuarto;
            this.IdSetor = idSetor;
            this.NomeSetor = nomeSetor;
            this.TipoLimpeza = tipoLimpeza;
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public string NumeroQuarto { get; set; }

        public int IdSetor { get; set; }

        public string NomeSetor { get; set; }

        public TipoLimpezaEnum TipoLimpeza { get; set; }
    }
}