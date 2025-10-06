namespace ProjetoHospitalWebAssembly
{
    using Blazored.LocalStorage;
    using Blazored.Modal;
    using Blazored.Toast;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using ProjetoHospitalWebAssembly.Services;
    using ProjetoHospitalWebAssembly.Services.Http;
    using System.Net;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddBlazoredModal();
            builder.Services.AddBlazoredToast();
            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddSingleton(sp =>
            {
                var client = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
                client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
                //client.DefaultRequestHeaders.Add("Origin", builder.HostEnvironment.BaseAddress);

                return client;
            });

            builder.Services.AddTransient<IHttpService, HttpService>();
            builder.Services.AddTransient<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            builder.Services.AddTransient<ISetorService, SetorService>();
            builder.Services.AddTransient<IQuartoService, QuartoService>();
            builder.Services.AddTransient<ILeitoService, LeitoService>();
            builder.Services.AddTransient<ILimpezaService, LimpezaService>();

            await builder.Build().RunAsync();
        }
    }
}