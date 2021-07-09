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

namespace IMDBLite.ServiceClientContracts.NetClient
{
    public class MovieClient : IMovieClient
    {
        private readonly IBaseHttpClient _baseHttpClient;

        public MovieClient(IBaseHttpClient baseHttpClient)
        {
            _baseHttpClient = baseHttpClient;
        }
        public async Task<PaginatedList<MovieDTO>> GetAll(bool isTvShow, string searchData, int? pageNumber, int? pageSize)
        {
            HttpResponseMessage response = await _baseHttpClient.HttpPost($"api/movie/getAll", new MovieSearchDTORequest
            {
              IsTvShow = isTvShow,
              SearchedData = searchData,
              PageNumber = pageNumber,
              PageSize = pageSize
            });
            //if (response != null && response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<PaginatedList<MovieDTO>>();
            //return null;
        }

        public async Task<PaginatedList<MovieDTO>> GetAllForRating(bool isTvShow, int? pageNumber, int? pageSize)
        {
            HttpResponseMessage response = await _baseHttpClient.HttpPost($"api/movie/getAllForRating", new MovieSearchDTORequest
            {
                IsTvShow = isTvShow,
                PageNumber = pageNumber,
                PageSize = pageSize
            });

            //if (response != null && response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<PaginatedList<MovieDTO>>();
           // return null;
        }
    }
}
