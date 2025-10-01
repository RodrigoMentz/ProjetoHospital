namespace ProjetoHospitalWebAssembly
{
    using Blazored.LocalStorage;
    using Blazored.Modal;
    using Blazored.Toast;
    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using ProjetoHospitalWebAssembly.Services;
    using ProjetoHospitalWebAssembly.Services.Http;

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

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddTransient<IHttpService, HttpService>();
            builder.Services.AddTransient<ISetorService, SetorService>();
            builder.Services.AddTransient<IQuartoService, QuartoService>();
            builder.Services.AddTransient<ILeitoService, LeitoService>();

            await builder.Build().RunAsync();
        }
    }
}