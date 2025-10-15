namespace ProjetoHospital.Hub
{
    using Microsoft.AspNetCore.SignalR;

    public class AtualizacaoHub : Hub
    {
        public async Task ConectarAtualizacao(string groupName)
        {
            await this.Groups.AddToGroupAsync(
                this.Context.ConnectionId,
                groupName);
        }
    }  
}