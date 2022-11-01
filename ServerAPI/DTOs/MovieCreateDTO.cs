using Microsoft.AspNetCore.Mvc;
using ServerAPI.Helpers;
using ServerAPI.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.DTOs
{
    public class MovieCreateDTO
    {
        [Required]
        [StringLength(10)]
        [FirstLetterUppercaseAttribute]
        public string Name { get; set; }
        [Range(1, 4)]
        public int Weeks { get; set; }
        [CreditCard]
        public string CreditCard { get; set; }
        [Url]
        public string Url { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<>))]
        public List<int> ClientsIds { get; set; }
    }
}
