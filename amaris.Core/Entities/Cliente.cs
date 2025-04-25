using Commons.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amaris.Core.Entities
{
    public class Cliente: BaseEntity
    {
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public decimal Saldo { get; set; }
        public string? MedioNotificacion { get; set; }
    }
}
