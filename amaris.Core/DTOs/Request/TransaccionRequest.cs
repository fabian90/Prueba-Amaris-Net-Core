using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace amaris.Core.DTOs.Request
{
    public class TransaccionRequest
    {
        [JsonIgnore]
        public string? IdTransaccion { get; set; } // Este campo es necesario para la actualización
        public string IdCliente { get; set; }
        public string IdFondo { get; set; }
        [JsonIgnore]
        public string? Tipo { get; set; }
        [JsonIgnore]
        public decimal Monto { get; set; }
        [JsonIgnore]
        public DateTime Fecha { get; set; }
        public string MedioNotificacion { get; set; }
        [JsonIgnore]
        public string? Descripcion { get; set; }
    }
}
