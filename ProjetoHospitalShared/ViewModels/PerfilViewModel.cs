namespace ProjetoHospitalShared.ViewModels
{
    public class PerfilViewModel
    {
        public PerfilViewModel()
        {
        }
        public PerfilViewModel(
            int id,
            string nome)
        {
            this.Id = id;
            this.Nome = nome;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
    }
}