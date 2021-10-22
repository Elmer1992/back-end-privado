using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using back_end.Utilidades;

namespace back_end.Utilidades
{
    public class ArticulosCreacionDTO
    {
         [Required]
         [StringLength(maximumLength: 300)]

        public string titulo { get; set; }
        public string contenido { get; set; }
        public string etiquetas { get; set; }
        public bool articulosGratis { get; set; }
        public bool articulosPaga { get; set; }
        public DateTime fechaLanzamiento { get; set; }
        public IFormFile poster { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> CategoriasIds { get; set; }
    }
}
