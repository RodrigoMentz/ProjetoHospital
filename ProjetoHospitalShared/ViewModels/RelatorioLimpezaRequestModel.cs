namespace ProjetoHospitalShared.ViewModels
{
    public class RelatorioLimpezaRequestModel
    {
        public RelatorioLimpezaRequestModel()
        {
        }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public int? LeitoId { get; set; }

        public string? UsuarioId { get; set; }
    }
}