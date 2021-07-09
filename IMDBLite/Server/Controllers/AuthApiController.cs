using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using IMDBLite.API.DataModels;

namespace IMDBLite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthApiController : ControllerBase
    {
        private ILogger<AuthApiController> _logger;
        private readonly IOptions<Auth0Client> _config;

        public AuthApiController(ILogger<AuthApiController> logger, IOptions<Auth0Client> config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetToken()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_config.Value.Base_Address);
                    var content = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("grant_type", _config.Value.Grant_type),
                    new KeyValuePair<string, string>("client_id", _config.Value.Client_id),
                    new KeyValuePair<string, string>("client_secret", _config.Value.Client_secret),
                    new KeyValuePair<string, string>("audience", _config.Value.Audience)
                });
                    var resultAuth = await client.PostAsync("token", content);
                    string resultContent = await resultAuth.Content.ReadAsStringAsync();

                    JObject tokenResponse = JsonConvert.DeserializeObject<JObject>(resultContent);
                    return Ok(tokenResponse.GetValue("access_token").ToString());
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Problem with getting access token for client application!");
                return BadRequest();
            }
        }      
    }
}
