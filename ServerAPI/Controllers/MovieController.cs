using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServerAPI.DTOs;
using ServerAPI.Entities;
using ServerAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public MovieController(ILogger<MovieController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieDTO>>> GetAll([FromQuery] PaginationDTO paginationDTO) 
        {
            var queryable = context.Movie.AsQueryable();
            await HttpContext.InsertParametersPaginationInHeader(queryable);
            var movies = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
            // var movies = await context.Movie.OrderBy(x => x.Name).ToListAsync();
            var result = mapper.Map<List<MovieDTO>>(movies);

            return Ok(result);
        }

        [HttpGet("{Id:int}")]

        public async Task<ActionResult<MovieDTO>> Get(int Id)
        {
            var movie = await context.Movie            
                    .Include(x => x.MovieClients).ThenInclude(x => x.Client)
                    .FirstOrDefaultAsync(x => x.Id == Id);

            if (movie == null)
            {
                return NotFound();
            }
            var result = mapper.Map<MovieDTO>(movie);
            result.MovieClients.OrderBy(x => x.Name).ToList();

            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] MovieCreateDTO dto)
        {
            var movie = mapper.Map<Movie>(dto);
            context.Add(movie);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult> Update(int Id, [FromBody] MovieCreateDTO dto)
        {
            var movie = mapper.Map<Movie>(dto);
            movie.Id = Id;
            context.Entry(movie).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var movie = await context.Movie.FirstOrDefaultAsync(x => x.Id == Id);
            if (movie == null)
            {
                return NotFound();
            }
            context.Remove(movie);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("PostGet")]
        public async Task<ActionResult<MoviePostGetDTO>> PostGet()
        {
            var genres = await context.Client.ToListAsync();

            var clientsDTO = mapper.Map<List<ClientDTO>>(genres);

            return new MoviePostGetDTO()
            {
                Clients = clientsDTO
            };
        }

        [HttpGet("putget/{Id:int}")]
        public async Task<ActionResult<MoviePutGetDTO>> GetMoviePut(int Id)
        {
            var movieActionResult = await Get(Id);
            if (movieActionResult.Result is NotFoundResult) 
            { 
                return NotFound(); 
            }
            var movie = movieActionResult.Value;

            if (movie?.MovieClients != null)
            {
                List<int> moviesClientsIds = movie.MovieClients.Select(x => x.Id).ToList();
                var nonSelectedGenres = await context.Client.Where(x => !moviesClientsIds.Contains(x.Id))
                .ToListAsync();
            }            

            var response = new MoviePutGetDTO();
            response.Movie = movie;
            /*if (movie?.MovieClients != null)
            {
                response.MovieClients = movie.MovieClients;
            }*/

            return response;
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<MovieDTO>>> FilterMovies([FromQuery] FilterMovieDTO filter)
        {
            var moviesQueryAble = context.Movie.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
            {
                moviesQueryAble = moviesQueryAble.Where(x => x.Name.Contains(filter.Name));
            }
            if (filter.ClientId != 0)
            {
                moviesQueryAble = moviesQueryAble
                    .Where(x => x.MovieClients.Select(y => y.ClientId)
                    .Contains(filter.ClientId));
            }
            await HttpContext.InsertParametersPaginationInHeader(moviesQueryAble);
            var movies = await moviesQueryAble.OrderBy(x => x.Name)
                .Paginate(filter.PaginationDTO)
                .ToListAsync();

            return mapper.Map<List<MovieDTO>>(movies);
        }

    }
}
