using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Entidades
{
    public class AArchivados
    {
        public int Id { get; set; }
      //  [Required]
       // [StringLength(maximumLength: 200)]
        public string titulo { get; set; }
        public string contenido { get; set; }
        public string etiquetas { get; set; }
        public DateTime fechaLanzamiento { get; set; }
        public string poster { get; set; }
    }
}
