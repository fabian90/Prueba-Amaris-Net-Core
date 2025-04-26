using Commons.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amaris.Core.Entities
{
    public class Fondo : BaseEntity
    {
        public string IdFondo { get; set; }
        public string Nombre { get; set; }
        public decimal MontoMinimo { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
