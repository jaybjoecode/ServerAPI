using ServerAPI.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Entities
{
    // public class Movie : IValidatableObject
    public class Movie {
        public int Id { get; set; }
        [Required(ErrorMessage = "The field name is required")]
        [StringLength(10, ErrorMessage = "Too long")]
        [FirstLetterUppercaseAttribute]
        public string Name { get; set; }
        [Range(1,4)]
        public int Weeks { get; set; }
        [CreditCard]
        public string CreditCard { get; set; }
        [Url]
        public string Url { get; set; }

        public List<MovieClient> MovieClients { get; set; }
    }
}
