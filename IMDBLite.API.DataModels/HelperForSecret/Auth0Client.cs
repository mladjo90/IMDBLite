using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBLite.API.DataModels
{
    public class Auth0Client
    {
        public string Base_Address { get; set; }
        public string Grant_type { get; set; }
        public string Client_id { get; set; }
        public string Client_secret { get; set; }
        public string Audience { get; set; }

    }
}
