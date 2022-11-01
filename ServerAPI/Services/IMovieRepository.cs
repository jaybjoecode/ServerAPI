using ServerAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Services
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAll();

        Movie Get(int Id);

        void Create(Movie movie);

        void Update(int Id, Movie movie);

        void Delete(int Id);
    }
}
