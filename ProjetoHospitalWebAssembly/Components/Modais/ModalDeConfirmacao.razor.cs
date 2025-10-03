namespace ProjetoHospitalWebAssembly.Components.Modais
{
    using Blazored.Modal;
    using Blazored.Modal.Services;
    using Microsoft.AspNetCore.Components;

    public partial class ModalDeConfirmacao : ComponentBase
    {
        [CascadingParameter]
        public BlazoredModalInstance ModalInstance { get; set; }

        [Parameter]
        public string Texto { get; set; } = string.Empty;

        private async Task Confirmar()
        {
            await this.ModalInstance
                .CloseAsync(ModalResult.Ok(true))
                .ConfigureAwait(true);
        }

        private async Task Cancelar()
        {
            await this.ModalInstance
                .CancelAsync()
                .ConfigureAwait(true);
        }
    }
}