using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.DTOs
{
    public class ArcticulosFiltrarDTO
    {
        public int Pagina { get; set; }
        public int RecordsPorPagina { get; set; }
        public PaginacionDTO PaginacionDTO
        {
            get { return new PaginacionDTO() { Pagina = Pagina, RecordsPorPagina = RecordsPorPagina }; }
        }

        public string Titulo { get; set; }
        public int categoriasId { get; set; }
        public bool ArticulosGratis { get; set;}
        public bool ArticulosPaga { get; set;}
    }
}
