using System.Net.Http;
using System.Threading.Tasks;

namespace IMDBLite.ServiceClientContracts.Helper
{
    public interface IBaseHttpClient
    {
        Task<HttpResponseMessage> HttpGet(string url);
        Task<HttpResponseMessage> HttpPost(string url, object request);
        Task<HttpResponseMessage> HttpPut(string url, object request);
        Task<HttpResponseMessage> HttpDelete(string url);
    }
}
