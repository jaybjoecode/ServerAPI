using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertParametersPaginationInHeader<T>(
            this HttpContext httpContext, 
            IQueryable<T> queryable
            )
        {
            if (httpContext == null)
            {
                throw new ArgumentException(nameof(httpContext));
            }
            double count = await queryable.CountAsync();
            httpContext.Response.Headers.Add("totalAmmountOfRecords", count.ToString());
        }
    }
}
