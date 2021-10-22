using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.DTOs
{
    public class ArticulosPutGetDTO
    {
        public ArticuloDTO Articulo { get; set; }
        public List<CategoriaDTO> CategoriasSeleccionadas { get; set; }
        public List<CategoriaDTO> CategoriasNoSeleccionadas { get; set; }
    }
}
