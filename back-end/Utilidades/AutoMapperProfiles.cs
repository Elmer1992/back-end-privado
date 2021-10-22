using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using back_end.DTOs;
using back_end.Entidades;

namespace back_end.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<CategoriaCreacionDTO, Categoria>();
            CreateMap<AArchivados, AArchivadosDTO>().ReverseMap();
            CreateMap<aArchivadosCreacionDTO, AArchivados>()
                .ForMember(x => x.poster, options => options.Ignore());


            CreateMap<ArticulosCreacionDTO, Articulos>()
                .ForMember(x => x.poster, opciones => opciones.Ignore())
                .ForMember(x => x.ArticulosCategorias, opciones => opciones.MapFrom(MapearArticulosCategorias));


            CreateMap<Articulos, ArticuloDTO>()
                .ForMember(x => x.Categorias, option => option.MapFrom(MapearArticulosCategorias));
        }

        private List<CategoriaDTO> MapearArticulosCategorias(Articulos articulos, ArticuloDTO articuloDTO)
        {
            var resultado = new List<CategoriaDTO>();

            if (articulos.ArticulosCategorias != null)
            {
                foreach (var categorias in articulos.ArticulosCategorias)
                {
                    resultado.Add(new CategoriaDTO() { Id = categorias.CategoriaId, Nombre = categorias.Categoria.Nombre });
                }
            }
            return resultado;
        }

        private List<ArticulosCategorias> MapearArticulosCategorias(ArticulosCreacionDTO articulosCreacionDTO,
           Articulos articulos )
        {
            var resultado = new List<ArticulosCategorias>();

            if (articulosCreacionDTO.CategoriasIds == null) { return resultado; }

            foreach (var id in articulosCreacionDTO.CategoriasIds)
            {
                resultado.Add(new ArticulosCategorias() { CategoriaId = id });
            }

            return resultado;
        }
    }
}
