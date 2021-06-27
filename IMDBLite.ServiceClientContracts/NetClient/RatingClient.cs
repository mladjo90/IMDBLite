using IMDBLite.ServiceClientContracts.Helper;
using IMDBLite.ServiceClientContracts.INetClient;
using IMDBLite.DTO.DTORequests;
using IMDBLite.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using IMDBLite.DTO.DTOResponse;

namespace IMDBLite.ServiceClientContracts.NetClient
{
    public class RatingClient : IRatingClient
    {
        private readonly IBaseHttpClient _baseHttpClient;

        public RatingClient(IBaseHttpClient baseHttpClient)
        {
            _baseHttpClient = baseHttpClient;
        }
        public async Task<BaseResponse> SaveData(List<RatingDTO> dataToSave)
        {
            HttpResponseMessage response = await _baseHttpClient.HttpPost($"api/rating/saveDataForRating", dataToSave);

           // if (response != null && response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<BaseResponse>();
           // return null;
        }
    }
}
