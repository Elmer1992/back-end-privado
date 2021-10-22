using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.DTOs
{
    public class LandingPageDTO
    {
        public List<ArticuloDTO> ArticulosGratis { get; set; }
        public List<ArticuloDTO> ArticulosPaga { get; set; }
    }
}
