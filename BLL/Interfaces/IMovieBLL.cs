using IMDBLite.DTO.DTORequests;
using IMDBLite.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBLite.BLL.Interfaces
{
    public interface IMovieBLL
    {
        Task<PaginatedList<MovieDTO>> GetAll(MovieSearchDTORequest request);
        Task<PaginatedList<MovieDTO>> GetAllForRating(MovieSearchDTORequest request);

    }
}
