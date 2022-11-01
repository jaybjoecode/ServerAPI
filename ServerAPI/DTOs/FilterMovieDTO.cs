using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.DTOs
{
    public class FilterMovieDTO
    {
        public int Page { get; set; }
        public int RecordsPerPage { get; set; }

        public PaginationDTO PaginationDTO
        {
            get { return new PaginationDTO() { Page = Page, RecordsPerPage = RecordsPerPage }; }
        }
        public string Name { get; set; }
        public int ClientId { get; set; }
    }
}
