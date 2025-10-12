namespace ProjetoHospitalWebAssembly.Components
{
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class CardQuarto
    {
        [Parameter]
        public LeitoStatusLimpezaViewModel Leito { get; set; }

        [Parameter]
        public LimpezaViewModel Limpeza { get; set; }

        [Parameter]
        public LimpezaEmergencialViewModel LimpezaEmergencial { get; set; }

        [Parameter]
        public NecessidadeDeRevisaoViewModel NecessidadeRevisao { get; set; }

        [Parameter]
        public RevisaoViewModel Revisao { get; set; }

        [Parameter]
        public EventCallback OnClick { get; set; }

        [Parameter]
        public bool Disabled { get; set; } = false;

        [Inject]
        private IUsuarioService UsuarioService { get; set; }

        private bool isMesmoUsuario = false;

        protected override async Task OnInitializedAsync()
        {
            var usuarioLocaLStorage = await UsuarioService
                .ConsultarUsuarioLocalStorage()
                .ConfigureAwait(true);

            if (LimpezaEmergencial != null) {
                this.isMesmoUsuario = LimpezaEmergencial.UsuarioId == usuarioLocaLStorage.Id;
            }
        }
    }
}