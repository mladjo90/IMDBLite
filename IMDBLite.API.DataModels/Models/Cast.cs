using System;
using System.Collections.Generic;

#nullable disable

namespace IMDBLite.API.DataModels.Models
{
    public partial class Cast
    {
        public Cast()
        {
            CastInMovies = new HashSet<CastInMovie>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ulong Gender { get; set; }

        public virtual ICollection<CastInMovie> CastInMovies { get; set; }
    }
}
