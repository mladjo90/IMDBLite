using IMDBLite.DTO.DTORequests;
using IMDBLite.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBLite.BLL.Interfaces
{
    public interface IRatingBLL
    {
        Task<bool> SaveDataForRating(List<RatingDTO> request);

    }
}
