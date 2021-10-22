using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.DTOs
{
    public class AArchivadosDTO
    {
        public int Id { get; set; }
        
        public string titulo { get; set; }
        public string contenido { get; set; }
        public string etiquetas { get; set; }
        public DateTime fechaLanzamiento { get; set; }
        public string poster { get; set; }
    }
}
