using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace back_end.DTOs
{
    public class aArchivadosCreacionDTO
    {
        
        [Required]
        [StringLength(maximumLength: 200)]
        public string titulo { get; set; }
        public string contenido { get; set; }
        public string etiquetas { get; set; }
        public DateTime fechaLanzamiento { get; set; }
        public IFormFile poster { get; set; }
    }
}
