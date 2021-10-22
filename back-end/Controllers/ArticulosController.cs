using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using back_end.DTOs;
using back_end.Entidades;
////using back_end.Migrations;
using back_end.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controllers
{
    [ApiController]
    [Route("api/articulos")]
    public class ArticulosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "articulos";
        public ArticulosController(ApplicationDbContext context,
         IMapper mapper,
         IAlmacenadorArchivos almacenadorArchivos)
        {
           
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

      
        [HttpGet]
        public async Task<ActionResult<LandingPageDTO>> Get()
        {
            var top = 6;
            var hoy = DateTime.Today;

            var articulosPaga = await context.Articulos
                .Where(x => x.fechaLanzamiento > hoy)
                .OrderBy(x => x.fechaLanzamiento)
                .Take(top)
                .ToListAsync();

            var articulosGratis = await context.Articulos
                .Where(x => x.articulosGratis)
                .OrderBy(x => x.fechaLanzamiento)
                .Take(top)
                .ToListAsync();

            var resultado = new LandingPageDTO();
            resultado.ArticulosPaga = mapper.Map<List<ArticuloDTO>>(articulosPaga);
            resultado.ArticulosGratis = mapper.Map<List<ArticuloDTO>>(articulosGratis);

            return resultado;
        }




        [HttpGet("{id:int}")]
        public async Task<ActionResult<ArticuloDTO>> Get(int id)
        {
            var articulo = await context.Articulos
                .Include(x => x.ArticulosCategorias).ThenInclude(x => x.Categoria)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (articulo == null) { return NotFound(); }

            var dto = mapper.Map<ArticuloDTO>(articulo);
            return dto;

        }

        [HttpGet("filtrar")]
        public async Task<ActionResult<List<ArticuloDTO>>> Filtrar([FromQuery] ArcticulosFiltrarDTO arcticulosFiltrarDTO)
        {
            var articulosQueryable = context.Articulos.AsQueryable();

            if (!string.IsNullOrEmpty(arcticulosFiltrarDTO.Titulo))
            {
                articulosQueryable = articulosQueryable.Where(x => x.titulo.Contains(arcticulosFiltrarDTO.Titulo));
            }

            if (arcticulosFiltrarDTO.ArticulosGratis)
            {
                articulosQueryable = articulosQueryable.Where(x => x.articulosGratis);
            }

            if (arcticulosFiltrarDTO.ArticulosPaga )
            {
                var hoy = DateTime.Today;
                articulosQueryable = articulosQueryable.Where(x => x.fechaLanzamiento > hoy);
            }

            if (arcticulosFiltrarDTO.categoriasId != 0)
            {
                articulosQueryable = articulosQueryable
                    .Where(x => x.ArticulosCategorias.Select(y => y.CategoriaId)
                    .Contains(arcticulosFiltrarDTO.categoriasId));
            }

            await HttpContext.InsertarParametrosPaginacionEnCabecera(articulosQueryable);
            var articulos = await articulosQueryable.Paginar(arcticulosFiltrarDTO.PaginacionDTO).ToListAsync();
            return mapper.Map<List<ArticuloDTO>>(articulos);


        }



        [HttpPost]
        public async Task<ActionResult<int>> Post([FromForm] ArticulosCreacionDTO articulosCreacionDTO)
        {
            var articulo = mapper.Map<Articulos>(articulosCreacionDTO);

            if (articulosCreacionDTO.poster != null)
            {
                articulo.poster = await almacenadorArchivos.GuardarArchivo(contenedor, articulosCreacionDTO.poster);
            }

            context.Add(articulo);
            await context.SaveChangesAsync();
            return articulo.Id;
        }
        
        [HttpGet("PostGet")]
        public async Task<ActionResult<ArticulosPostGetDTO>> PostGet()
        {
            var categorias = await context.Categorias.ToListAsync();

            var categoriaDTO = mapper.Map<List<CategoriaDTO>>(categorias);


            return new ArticulosPostGetDTO() { Categorias = categoriaDTO };
        }

        [HttpGet("PutGet/{id:int}")]
        public async Task<ActionResult<ArticulosPutGetDTO>> PutGet(int id)
        {
            var articuloActionResult = await Get(id);
            if(articuloActionResult.Result is NotFoundResult) { return NotFound(); }

            var articulo = articuloActionResult.Value;

            var categoriasSeleccionadasIds = articulo.Categorias.Select(x => x.Id).ToList();
            var categoriasNoSeleccionadas = await context.Categorias
                .Where(x => !categoriasSeleccionadasIds.Contains(x.Id))
                .ToListAsync();

            var categoriasNoSeleccionadasDTO = mapper.Map<List<CategoriaDTO>>(categoriasNoSeleccionadas);

            var respuesta = new ArticulosPutGetDTO();
            respuesta.Articulo = articulo;
            respuesta.CategoriasSeleccionadas = articulo.Categorias;
            respuesta.CategoriasNoSeleccionadas = categoriasNoSeleccionadasDTO;
            return respuesta;

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ArticulosCreacionDTO articulosCreacionDTO)
        {
            var articulo = await context.Articulos
                .Include(x => x.ArticulosCategorias)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo = mapper.Map(articulosCreacionDTO, articulo);
            if (articulosCreacionDTO.poster != null)
            {
                articulo.poster = await almacenadorArchivos.EditarArchivo(contenedor, articulosCreacionDTO.poster, articulo.poster);
            }

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var articulo = await context.Articulos.FirstOrDefaultAsync(x => x.Id == id);

            if (articulo == null)
            {
                return NotFound();
            }

            context.Remove(articulo);
            await context.SaveChangesAsync();

            await almacenadorArchivos.BorrarArchivo(articulo.poster, contenedor);

            return NoContent();
        }
    }


}
