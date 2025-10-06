namespace ProjetoHospitalShared.ViewModels
{
    public class LimpezaViewModel
    {
        public LimpezaViewModel()
        {
        }

        public LimpezaViewModel(
            int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }

        public int LeitoId { get; set; }

        public string UsuarioId { get; set; }

        public DateTime DataInicioLimpeza { get; set; }

        public DateTime? DataFimLimpeza { get; set; }
    }
}