using Blazored.LocalStorage;
using IMDBLite.Client.StateProvider;
using IMDBLite.ServiceClientContracts.Helper;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace IMDBLite.Client.Handlers
{
    public class IMDBLiteApiHttpClient : IBaseHttpClient
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService storageService;
        private readonly IConfiguration configuration;

        public IMDBLiteApiHttpClient(IHttpClientFactory clientFactory, ILocalStorageService storageService, IConfiguration configuration)
        {
            this.httpClient = clientFactory.CreateClient("IMDBLiteApiHttpClient");
            this.storageService = storageService;
            this.configuration = configuration;
        }
        private async Task SetBearerToken()
        {
            var authToken = await storageService.GetItemAsStringAsync("authToken");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
        }
        public async Task<HttpResponseMessage> HttpDelete(string url)
        {
            await SetBearerToken();
            return await httpClient.DeleteAsync(url);
        }

        public async Task<HttpResponseMessage> HttpGet(string url)
        {
            await SetBearerToken();
            return await httpClient.GetAsync(url);
        }

        public async Task<HttpResponseMessage> HttpPost(string url, object request)
        {
            await SetBearerToken();
            var response = await httpClient.PostAsJsonAsync(url, request);
            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await storageService.ClearAsync();
                ApiAuthenticationStateProvider apiAuthenticationState = new ApiAuthenticationStateProvider(storageService, configuration);
                await apiAuthenticationState.GetAuthenticationStateAsync();
                await SetBearerToken();
                response = await httpClient.PostAsJsonAsync(url, request);
            }
            return response;
        }

        public async Task<HttpResponseMessage> HttpPut(string url, object request)
        {
            await SetBearerToken();
            return await httpClient.PutAsJsonAsync(url, request);
        }
    }
}
