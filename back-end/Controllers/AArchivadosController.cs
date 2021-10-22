using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using back_end.DTOs;
using back_end.Entidades;
using back_end.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    [Route("api/aarchivados")]
    [ApiController]
    public class AArchivadosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "aarchivados";

        public AArchivadosController(ApplicationDbContext context, IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos
            )
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] aArchivadosCreacionDTO aArchivadosCreacionDTO)
        {
            var aarchivados = mapper.Map<AArchivados>(aArchivadosCreacionDTO);

            if (aArchivadosCreacionDTO.poster !=null)
            {
              aarchivados.poster =  await almacenadorArchivos.GuardarArchivo(contenedor, aArchivadosCreacionDTO.poster);
            }

            context.Add(aarchivados);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
