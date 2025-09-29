using Microsoft.AspNetCore.Components;

namespace ProjetoHospitalWebAssembly.Components
{
    public partial class BadgeStatusAtivo
    {
        [Parameter]
        public bool Ativo { get; set; }

        [Parameter]
        public string Class { get; set; } = string.Empty;

        private string Texto => Ativo switch
        {
            true => "Ativo",
            false => "Inativo",
        };

        private string ClasseTextoEFundo => Ativo switch
        {
            true => "ativo",
            false => "inativo",
        };
    }
}