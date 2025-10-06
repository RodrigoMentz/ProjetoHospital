namespace ProjetoHospital.Entities
{
    public class LimpezaConcorrente : Limpeza
    {
        public LimpezaConcorrente()
        {
        }

        public LimpezaConcorrente(
            bool tirarLixo,
            bool limparVasoSanitario,
            bool limparBox,
            bool revisarMofo,
            bool limparPia,
            bool limparCama,
            bool limparMesaCabeceira,
            bool limparLixeira)
        {
            this.TirarLixo = tirarLixo;
            this.LimparVasoSanitario = limparVasoSanitario;
            this.LimparBox = limparBox;
            this.RevisarMofo = revisarMofo;
            this.LimparPia = limparPia;
            this.LimparCama = limparCama;
            this.LimparMesaCabeceira = limparMesaCabeceira;
            this.LimparLixeira = limparLixeira;
        }

        public bool TirarLixo { get; set; }
        public bool LimparVasoSanitario { get; set; }
        public bool LimparBox { get; set; }
        public bool RevisarMofo { get; set; }
        public bool LimparPia { get; set; }
        public bool LimparCama { get; set; }
        public bool LimparMesaCabeceira { get; set; }
        public bool LimparLixeira { get; set; }
    }
}