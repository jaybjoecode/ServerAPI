using ServerAPI.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Weeks { get; set; }
        public string CreditCard { get; set; }
        public string Url { get; set; }
        public List<ClientDTO> MovieClients { get; set; }
    }
}
