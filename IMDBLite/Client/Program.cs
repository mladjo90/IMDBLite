using Blazored.LocalStorage;
using Blazored.Toast;
using IMDBLite.Client.Handlers;
using IMDBLite.Client.StateProvider;
using IMDBLite.ServiceClientContracts.INetClient;
using IMDBLite.ServiceClientContracts.NetClient;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace IMDBLite.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            
            builder.Services.AddBlazoredToast();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient("IMDBLiteApiHttpClient", client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            });

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

            builder.Services.AddScoped<IMDBLiteApiHttpClient>();

            builder.Services.AddScoped<IMovieClient, MovieClient>(sp =>
            {
                var IMDBLiteHttpClient = sp.GetService<IMDBLiteApiHttpClient>();
                return new MovieClient(IMDBLiteHttpClient);
            });

            builder.Services.AddScoped<IRatingClient, RatingClient>(sp =>
            {
                var IMDBLiteHttpClient = sp.GetService<IMDBLiteApiHttpClient>();
                return new RatingClient(IMDBLiteHttpClient);
            });

            await builder.Build().RunAsync();
        }
    }
}
