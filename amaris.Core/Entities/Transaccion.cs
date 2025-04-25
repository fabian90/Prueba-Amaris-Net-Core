using Commons.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amaris.Core.Entities
{
    public class Transaccion : BaseEntity
    {
        public string? ClienteId { get; set; }
        public string? FondoId { get; set; }
        public string? Tipo { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
    }
}
