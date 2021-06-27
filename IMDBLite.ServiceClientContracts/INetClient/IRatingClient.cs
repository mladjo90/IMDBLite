using IMDBLite.DTO.DTORequests;
using IMDBLite.DTO.DTOResponse;
using IMDBLite.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBLite.ServiceClientContracts.INetClient
{
    public interface IRatingClient
    {
        Task<BaseResponse> SaveData(List<RatingDTO> dataForSave);
    }
}
