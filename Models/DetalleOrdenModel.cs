using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid_Plus.Models
{
    internal class DetalleOrdenModel
    {
        public int IdOrden { get; set; }
        public string EstadoOrden { get; set; }
        public int Cantidad { get; set; }
        public int IdEstado { get; set; }
        public int IdPlatillo { get; set; }
        public int IdPlatilloOrden { get; set; }
        public int IdEstadoOrden { get; set; }
    }
}
