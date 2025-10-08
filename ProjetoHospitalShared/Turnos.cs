namespace ProjetoHospitalShared
{
    public static class Turnos
    {
        public const string Manha = "Manhã";
        public const string Tarde = "Tarde";
        public const string Noite = "Noite";

        public static List<string> Listar()
        {
            return new List<string> { Manha, Tarde, Noite };
        }
    }
}