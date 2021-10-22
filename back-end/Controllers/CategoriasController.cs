using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using back_end.DTOs;
using back_end.Entidades;
using back_end.Utilidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace back_end.Controllers
{
    [Route("api/categorias")]
    [ApiController]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriasController : ControllerBase
    {
        
      
        private readonly ILogger<CategoriasController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CategoriasController( 


            
            ILogger<CategoriasController> logger,
            ApplicationDbContext context,
            IMapper mapper)
        {
          
            
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriaDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {

            var queryable= context.Categorias.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var categorias = await queryable.OrderBy(X => X.Nombre).Paginar(paginacionDTO).ToListAsync();
            return mapper.Map<List<CategoriaDTO>>(categorias);

        }

        [HttpGet("todos")]
        public async Task<ActionResult<List<CategoriaDTO>>> Todos()
        {
            var categorias = await context.Categorias.ToListAsync();
            return mapper.Map<List<CategoriaDTO>>(categorias);
        }


        [HttpGet("{Id:int}")] 
        
        public async Task<ActionResult<CategoriaDTO>> Get(int Id)
        {
            var categoria = await context.Categorias.FirstOrDefaultAsync(X => X.Id == Id);

            if (categoria == null)
            {
                return NotFound();
            }
            return mapper.Map <CategoriaDTO>(categoria);
        }

        private int CategoriaDTO(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoriaCreacionDTO categoriaCreacionDTO) // FromForm
        {
            var categoria = mapper.Map<Categoria>(categoriaCreacionDTO);
            context.Add(categoria);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut ("{id:int}")]
        public async Task<ActionResult> Put(int Id, [FromBody] CategoriaCreacionDTO categoriaCreacionDTO)
        {
            var categoria = await context.Categorias.FirstOrDefaultAsync(x => x.Id == Id);

            if(categoria == null)
            {
                return NotFound();
            }
            categoria = mapper.Map(categoriaCreacionDTO, categoria);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Categorias.AnyAsync(X => X.Id == id);

            if (!existe)
            {
                return NotFound();
            }
            context.Remove(new Categoria() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
                }


    }
}
