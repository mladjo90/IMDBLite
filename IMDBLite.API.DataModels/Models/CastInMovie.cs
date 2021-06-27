using System;
using System.Collections.Generic;

#nullable disable

namespace IMDBLite.API.DataModels.Models
{
    public partial class CastInMovie
    {
        public int MovieId { get; set; }
        public int CastId { get; set; }

        public virtual Cast Cast { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
