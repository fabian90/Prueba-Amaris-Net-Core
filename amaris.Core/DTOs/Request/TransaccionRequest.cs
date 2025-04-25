using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amaris.Core.DTOs.Request
{
    public class TransaccionRequest
    {
        public string? ClienteId { get; set; }
        public string? FondoId { get; set; }
        public string? Tipo { get; set; } // "suscripcion" o "cancelacion"
        public decimal Monto { get; set; }
    }
}
