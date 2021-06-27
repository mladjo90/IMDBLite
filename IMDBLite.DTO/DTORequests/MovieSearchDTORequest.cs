using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBLite.DTO.DTORequests
{
    public class MovieSearchDTORequest
    {
        public string SearchedData { get; set; }
        public bool IsTvShow { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
