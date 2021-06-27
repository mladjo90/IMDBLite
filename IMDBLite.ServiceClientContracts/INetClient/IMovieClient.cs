using IMDBLite.DTO.DTORequests;
using IMDBLite.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBLite.ServiceClientContracts.INetClient
{
    public interface IMovieClient
    {
        Task<PaginatedList<MovieDTO>> GetAll(bool isTvShow, string searchData, int? pageNumber, int? pageSize); //moze se ubaciti dodatni parametar kako bi se znalo
                                                                                                                //da li se koristi sortiranje ili ne. U tom slucaju ne
                                                                                                                //treba GetAllForRating.
        Task<PaginatedList<MovieDTO>> GetAllForRating(bool isTvShow, int? pageNumber, int? pageSize);
    }
}
