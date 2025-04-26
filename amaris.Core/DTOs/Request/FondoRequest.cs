using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amaris.Core.DTOs.Request
{
    public class FondoRequest
    {
        public string IdFondo { get; set; } // Este campo es necesario para la actualización
        public string Nombre { get; set; }
        public decimal MontoMinimo { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
