using System;
using System.Collections.Generic;

#nullable disable

namespace IMDBLite.API.DataModels.Models
{
    public partial class Rating
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal RatingStarts { get; set; }
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
