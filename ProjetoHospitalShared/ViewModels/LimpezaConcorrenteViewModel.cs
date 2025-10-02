namespace ProjetoHospitalShared.ViewModels
{
    public class LimpezaConcorrenteViewModel
    {
        public LimpezaConcorrenteViewModel()
        {
        }

        public LimpezaConcorrenteViewModel(
            int leitoId,
            int funcionarioId,
            DateTime dataHoraFim,
            bool tirarLixo,
            bool vasoSanitario,
            bool limparBox,
            bool revisarMofo,
            bool limparPia,
            bool limparCama,
            bool limparMesaCabeceira,
            bool limparLixeira)
        {
            this.LeitoId = leitoId;
            this.FuncionarioId = funcionarioId;
            this.DataHoraFim = dataHoraFim;
            this.TirarLixo = tirarLixo;
            this.VasoSanitario = vasoSanitario;
            this.LimparBox = limparBox;
            this.RevisarMofo = revisarMofo;
            this.LimparPia = limparPia;
            this.LimparCama = limparCama;
            this.LimparMesaCabeceira = limparMesaCabeceira;
            this.LimparLixeira = limparLixeira;
        }

        public int LeitoId { get; set; }

        public int FuncionarioId { get; set; }

        public DateTime DataHoraFim { get; set; }

        public bool TirarLixo { get; set; }

        public bool VasoSanitario { get; set; }

        public bool LimparBox { get; set; }

        public bool RevisarMofo { get; set; }

        public bool LimparPia { get; set; }

        public bool LimparCama { get; set; }

        public bool LimparMesaCabeceira { get; set; }

        public bool LimparLixeira { get; set; }
    }
}