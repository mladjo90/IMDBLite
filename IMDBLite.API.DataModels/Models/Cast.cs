using System;
using System.Collections.Generic;

#nullable disable

namespace IMDBLite.API.DataModels.Models
{
    public partial class Cast
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ulong Gender { get; set; }
    }
}
