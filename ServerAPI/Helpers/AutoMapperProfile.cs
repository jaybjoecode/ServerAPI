using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ServerAPI.DTOs;
using ServerAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MovieDTO, Movie>().ReverseMap()
                .ForMember(x => x.MovieClients, options => options.MapFrom(MapMovieClients));

            // CreateMap<MovieCreateDTO, Movie>().ReverseMap();
            CreateMap<MovieCreateDTO, Movie>()
                .ForMember(x => x.MovieClients, options => options.MapFrom(MapMovieClients));

            CreateMap<ClientDTO, Client>().ReverseMap();

            CreateMap<ClientCreateDTO, Client>()
                .ForMember(x => x.Picture, options => options.Ignore());

            CreateMap<MovieCreateDTO, Movie>()
                .ForMember(x=> x.MovieClients, options => options.MapFrom(MapClients));

            CreateMap<IdentityUser, UserDTO>();
        }

        private List<MovieClient> MapClients(MovieCreateDTO dto, Movie movie)
        {
            var result = new List<MovieClient>();
            if (dto.ClientsIds == null)
            {
                return result;
            }

            foreach (var id in dto.ClientsIds)
            {
                result.Add(new MovieClient() { ClientId = id });
            }

            return result;
        }

        private List<ClientDTO> MapMovieClients(Movie movie, MovieDTO moviedto)
        {
            var result = new List<ClientDTO>();
            if (movie.MovieClients != null)
            {
                foreach (var client in movie.MovieClients)
                {
                    result.Add(new ClientDTO()
                    {
                        Id = client.ClientId,
                        Name = client.Client.Name,
                        Lastname = client.Client.Lastname,
                        DateOfBirth = client.Client.DateOfBirth,
                        Picture = client.Client.Picture
                    });
                }
            }
            return result;
        }

        private List<MovieClient> MapMovieClients(MovieCreateDTO movieCreateDTO, Movie movie)
        {
            var movieClients = new List<MovieClient>();
            if (movieCreateDTO.ClientsIds == null)
            {
                return movieClients;
            }
            foreach (var id in movieCreateDTO.ClientsIds)
            {
                movieClients.Add(new MovieClient()
                {
                    ClientId = id
                });
            }
            return movieClients;
        }
    }
}
