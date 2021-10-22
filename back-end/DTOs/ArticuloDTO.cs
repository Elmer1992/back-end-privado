using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.DTOs
{
    public class ArticuloDTO
    {
        public int Id { get; set; }


        public string titulo { get; set; }
        public string contenido { get; set; }
        public string etiquetas { get; set; }
        public bool articulosGratis { get; set; }
        public bool articulosPaga { get; set; }
        public DateTime fechaLanzamiento { get; set; }
        public string poster { get; set; }

        public List<CategoriaDTO> Categorias { get; set; }

    }
}
