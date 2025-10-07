namespace ProjetoHospitalShared.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
        }

        public LoginViewModel(
            string numTelefone,
            string senha)
        {
            this.NumTelefone = numTelefone;
            this.Senha = senha;
        }

        public string NumTelefone { get; set; }

        public string Senha { get; set; }
    }
}