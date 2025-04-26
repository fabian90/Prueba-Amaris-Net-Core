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
        public string IdCliente { get; set; } // Este campo es necesario para la actualización
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Ciudad { get; set; }
        public decimal Saldo { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
