using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBLite.DTO.DTORequests
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CoverImg { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public bool? IsTvshow { get; set; }
        public decimal Rating { get; set; }
        public string Cast { get; set; }
    }
}
