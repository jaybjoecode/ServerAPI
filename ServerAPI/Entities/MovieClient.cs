using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Entities
{
    public class MovieClient
    {
        public int MovieId { get; set; }
        public int ClientId { get; set; }

        public Movie Movie { get; set; }
        public Client Client { get; set; }
    }
}
