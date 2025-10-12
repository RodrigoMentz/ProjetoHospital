namespace ProjetoHospital.Entities
{
    public class LimpezaEmergencial : Limpeza
    {
        public LimpezaEmergencial()
        {
        }

        public string Descricao { get; set; }

        public string IdSolicitante { get; set; }

        public virtual Usuario Solicitante { get; set; }
    }
}