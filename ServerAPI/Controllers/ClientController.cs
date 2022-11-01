using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerAPI.DTOs;
using ServerAPI.Helpers;
using ServerAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
    public class ClientController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IFileStorageService fileStorageService;
        private readonly string containerName = "clients";

        public ClientController(ApplicationDbContext context, IMapper mapper, 
            IFileStorageService fileStorageSevice)
        {
            this.context = context;
            this.mapper = mapper;
            this.fileStorageService = fileStorageSevice;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<ClientDTO>>> GetAll([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = context.Client.AsQueryable();
            await HttpContext.InsertParametersPaginationInHeader(queryable);
            var clients = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
            // var clients = await context.Client.OrderBy(x => x.Name).ToListAsync();
            var result = mapper.Map<List<ClientDTO>>(clients);

            return Ok(result);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ClientDTO>> Get(int Id)
        {
            var client = await context.Client.FirstOrDefaultAsync(x => x.Id == Id);
            if (client == null)
            {
                return NotFound();
            }
            var result = mapper.Map<ClientDTO>(client);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] ClientCreateDTO dto)
        {

            var client = mapper.Map<Client>(dto);
            if (dto.Picture != null)
            {
                client.Picture = await fileStorageService.SaveFile(containerName, dto.Picture);
            }
            context.Add(client);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult> Edit(int Id, [FromBody] ClientCreateDTO dto)
        {
            var client = mapper.Map<Client>(dto);
            client.Id = Id;
            if (dto.Picture != null)
            {
                client.Picture = await fileStorageService
                    .EditFile(containerName, dto.Picture, client.Picture);
            }
            context.Entry(client).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var client = await context.Client.FirstOrDefaultAsync(x => x.Id == Id);
            if (client == null)
            {
                return NotFound();
            }
            context.Remove(client);
            await context.SaveChangesAsync();
            await fileStorageService.DeleteFile(client.Picture, containerName);

            return Ok();
        }
    }
}
