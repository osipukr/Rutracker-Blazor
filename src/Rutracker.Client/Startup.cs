using Blazored.LocalStorage;
using MatBlazor;
using Microsoft.AspNetCore.Blazor.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Rutracker.Client.Services;
using Rutracker.Client.Services.Interfaces;

namespace Rutracker.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var clientSettings = ClientSettingsService.GetSettings("clientsettings.json");

            services.AddSingleton(clientSettings.ApiUriSettings);
            services.AddSingleton(clientSettings.ViewSettings);

            services.AddAuthorizationCore();
            services.AddSingleton<HttpClientService>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ITorrentService, TorrentService>();
            services.AddSingleton<AppState>();
            services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AppState>());

            services.AddBlazoredLocalStorage();
            services.AddMatToaster((MatToastConfiguration)clientSettings.MatToasterSettings);
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            WebAssemblyHttpMessageHandler.DefaultCredentials = FetchCredentialsOption.Include;
            app.AddComponent<App>("app");
        }
    }
}