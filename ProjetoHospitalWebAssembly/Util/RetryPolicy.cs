namespace ProjetoHospitalWebAssembly.Util
{
    using Microsoft.AspNetCore.SignalR.Client;

    public class RetryPolicy : IRetryPolicy
    {
        public TimeSpan? NextRetryDelay(RetryContext retryContext)
        {
            return TimeSpan.FromMinutes(1);
        }
    }
}