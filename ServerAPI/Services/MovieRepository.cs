using ServerAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Services
{
    public class MovieRepository : IMovieRepository
    {
        private List<Movie> movies;

        public MovieRepository()
        {
            movies = new List<Movie>()
            {
                new Movie() { Id=1, Name="Movie 1"},
                new Movie() { Id=2, Name="Movie 2"}
            };
        }

        public async Task<List<Movie>> GetAll()
        {
            await Task.Delay(1000);
            return movies;
        }

        public Movie Get(int Id)
        {
            return movies.FirstOrDefault(x => x.Id == Id);
        }

        public void Create(Movie movie)
        {
            // int NewId = movies.Count();
            int NewId = movies.Max(x => x.Id) + 1;
            movie.Id = NewId;
            movies.Add(movie);
        }

        public void Update(int Id, Movie movie)
        {
            var m = movies.FirstOrDefault(x => x.Id == Id);
            if (m != null)
            {
                m.Name = movie.Name;
            }
        }

        public void Delete(int Id)
        {
            var m = movies.FirstOrDefault(x => x.Id == Id);
            movies.Remove(m);
            
        }
    }
}
