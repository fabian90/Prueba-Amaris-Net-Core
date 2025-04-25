using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amaris.Core.DTOs.Response
{
    public class TransaccionResponse
    {
        public string? Id { get; set; }
        public string? ClienteId { get; set; }
        public string? FondoId { get; set; }
        public string? Tipo { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
    }
}
