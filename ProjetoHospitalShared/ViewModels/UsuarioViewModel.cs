namespace ProjetoHospitalShared.ViewModels
{
    public class UsuarioViewModel
    {
        public UsuarioViewModel()
        {
        }

        public UsuarioViewModel(
            string id,
            string nome,
            PerfilViewModel perfil,
            string numeroTelefone,
            bool ativo)
        {
            this.Id = id;
            this.Nome = nome;
            this.Perfil = perfil;
            this.NumeroTelefone = numeroTelefone;
            this.Ativo = ativo;
        }

        public UsuarioViewModel(
            string nome,
            PerfilViewModel perfil,
            string numeroTelefone,
            bool ativo)
        {
            this.Nome = nome;
            this.Perfil = perfil;
            this.NumeroTelefone = numeroTelefone;
            this.Ativo = ativo;
        }

        public UsuarioViewModel(
            string? id,
            string nome)
        {
            this.Id = id;
            this.Nome = nome;
        }

        public string? Id { get; set; }

        public string Nome { get; set; }

        public PerfilViewModel Perfil { get; set; }

        public string NumeroTelefone { get; set; }

        public bool Ativo { get; set; }
    }
}