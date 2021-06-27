using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IMDBLite.Client.StateProvider
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _storageService;
        public readonly IConfiguration Configuration;

        public ApiAuthenticationStateProvider(ILocalStorageService storageService, IConfiguration configuration)
        {
            _storageService = storageService;
            Configuration = configuration;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var savedToken = await _storageService.GetItemAsStringAsync("authToken");
                //var refreshToken = await storageService.GetItemAsStringAsync("refreshToken");
                if (string.IsNullOrWhiteSpace(savedToken))
                {
                    await GetTokenForClient();
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            catch
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        private async Task GetTokenForClient()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Configuration["APIBaseURI"]);
                    var resultAuth = await client.GetAsync("api/authApi/getToken");
                    string resultContent = await resultAuth.Content.ReadAsStringAsync();
                    await _storageService.SetItemAsStringAsync("authToken", resultContent);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("e    " + JsonConvert.SerializeObject(e));
            }
        }
    }
}
