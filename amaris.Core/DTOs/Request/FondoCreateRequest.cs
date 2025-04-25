using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amaris.Core.DTOs.Request
{
    public class FondoUpdateRequest
    {
        public decimal MontoMinimo { get; set; }
        public string? Categoria { get; set; }
    }
}
