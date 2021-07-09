using System;
using System.Collections.Generic;

#nullable disable

namespace IMDBLite.API.DataModels.Models
{
    public partial class Movie
    {
        public Movie()
        {
            CastInMovies = new HashSet<CastInMovie>();
            Ratings = new HashSet<Rating>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string CoverImg { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public bool? IsTvshow { get; set; }

        public virtual ICollection<CastInMovie> CastInMovies { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
