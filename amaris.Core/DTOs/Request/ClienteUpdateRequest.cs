using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amaris.Core.DTOs.Request
{
    public class ClienteUpdateRequest
    {
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string MedioNotificacion { get; set; }
    }
}
