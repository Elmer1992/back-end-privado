using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Entidades
{
    public class ArticulosCategorias
    {
        public int ArticulosId { get; set; }
        public int CategoriaId { get; set; }
        public Articulos Articulo { get; set; }
        public Categoria Categoria { get; set; }
    }
}
