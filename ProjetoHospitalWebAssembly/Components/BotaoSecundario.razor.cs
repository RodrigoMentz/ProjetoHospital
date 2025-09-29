namespace ProjetoHospitalWebAssembly.Components
{
    using Microsoft.AspNetCore.Components;

    public partial class BotaoSecundario
    {
        [Parameter]
        public string Class { get; set; } = string.Empty;

        [Parameter]
        public string ClassTexto { get; set; } = string.Empty;

        [Parameter]
        public string IconeGoogle { get; set; } = string.Empty;

        [Parameter]
        public string IconeBootstrap { get; set; } = string.Empty;

        [Parameter]
        public string Texto { get; set; } = string.Empty;

        [Parameter]
        public string TextoSecundario { get; set; } = string.Empty;

        [Parameter]
        public EventCallback OnClick { get; set; }

        [Parameter]
        public bool Disabled { get; set; } = false;
    }
}