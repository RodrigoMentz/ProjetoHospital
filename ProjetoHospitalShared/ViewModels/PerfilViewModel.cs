namespace ProjetoHospitalShared.ViewModels
{
    public class PerfilViewModel
    {
        public PerfilViewModel()
        {
        }
        public PerfilViewModel(
            string id,
            string nome)
        {
            this.Id = id;
            this.Nome = nome;
        }

        public string Id { get; set; }
        public string Nome { get; set; }
    }
}