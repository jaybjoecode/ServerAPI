using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.DTOs
{
    public class MoviePostGetDTO
    {
        public List<ClientDTO> Clients { get; set; }
    }
}
