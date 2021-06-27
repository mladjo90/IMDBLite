using Blazored.LocalStorage;
using IMDBLite.ServiceClientContracts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IMDBLite.Client.Handlers
{
    public class IMDBLiteApiHttpClient : IBaseHttpClient
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService storageService;


        public IMDBLiteApiHttpClient(IHttpClientFactory clientFactory, ILocalStorageService storageService)
        {
            this.httpClient = clientFactory.CreateClient("IMDBLiteApiHttpClient");
            this.storageService = storageService;
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
            return await httpClient.PostAsJsonAsync(url, request);
        }

        public async Task<HttpResponseMessage> HttpPut(string url, object request)
        {
            await SetBearerToken();
            return await httpClient.PutAsJsonAsync(url, request);
        }
    }
}
