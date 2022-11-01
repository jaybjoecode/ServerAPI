using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.DTOs
{
    public class ClientCreateDTO
    {
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        [Required]
        [StringLength(120)]
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        
        public IFormFile Picture { get; set; }
    }
}
