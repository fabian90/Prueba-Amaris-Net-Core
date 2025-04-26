using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amaris.Core.DTOs.Request
{
    public class TransaccionRequest
    {
        public string IdTransaccion { get; set; } // Este campo es necesario para la actualización
        public string IdCliente { get; set; }
        public string IdFondo { get; set; }
        public string Tipo { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string MedioNotificacion { get; set; }
        public string Descripcion { get; set; }
    }
}
