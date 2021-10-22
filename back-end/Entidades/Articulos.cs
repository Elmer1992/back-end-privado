using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Entidades
{
    public class Articulos
    {
        public int Id { get; set; }
          [Required]
          [StringLength(maximumLength: 300)]

        public string titulo { get; set; }
        public string contenido { get; set; }
        public string etiquetas { get; set; }
        public bool articulosGratis { get; set; }
        public bool articulosPaga { get; set; }
        public DateTime fechaLanzamiento { get; set; }
        public string poster { get; set; }

        public List<ArticulosCategorias> ArticulosCategorias { get; set; }

    }
}
