namespace ProjetoHospitalShared.ViewModels
{
    public class LimpezaTerminalViewModel
    {
        public LimpezaTerminalViewModel()
        {
        }

        public LimpezaTerminalViewModel(
            int leitoId,
            int funcionarioId,
            DateTime dataHoraFim,
            bool tirarLixo,
            bool vasoSanitario,
            bool limparBox,
            bool revisarMofo,
            bool limparPia,
            bool limparCama,
            bool limparEscadaCama,
            bool limparMesaCabeceira,
            bool limparArmario,
            bool recolherRoupaSuja,
            bool revisarPapelToalhaEHigienico,
            bool limparDispensers,
            bool limparLixeira,
            bool limparTeto,
            bool limparParedes,
            bool limparChao)
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
            this.LimparEscadaCama = limparEscadaCama;
            this.LimparMesaCabeceira = limparMesaCabeceira;
            this.LimparArmario = limparArmario;
            this.RecolherRoupaSuja = recolherRoupaSuja;
            this.RevisarPapelToalhaEHigienico = revisarPapelToalhaEHigienico;
            this.LimparDispensers = limparDispensers;
            this.LimparLixeira = limparLixeira;
            this.LimparTeto = limparTeto;
            this.LimparParedes = limparParedes;
            this.LimparChao = limparChao;
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

        public bool LimparEscadaCama { get; set; }

        public bool LimparMesaCabeceira { get; set; }

        public bool LimparArmario { get; set; }

        public bool RecolherRoupaSuja { get; set; }

        public bool RevisarPapelToalhaEHigienico { get; set; }

        public bool LimparDispensers { get; set; }

        public bool LimparLixeira { get; set; }

        public bool LimparTeto { get; set; }

        public bool LimparParedes { get; set; }

        public bool LimparChao { get; set; }
    }
}