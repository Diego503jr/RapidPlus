using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid_Plus.Models
{
    internal class CajeroModel
    {
        public int IdOrden { get; set; }
        public string NombreCliente { get; set; }
        public DateTime FechaOrden { get; set; }
        public string Orden { get; set; }
        public int Cantidad { get; set; }
        public string NombreUsuario { get; set; }
        public decimal Subtotal { get; set; }
        public decimal PrecioPlatillo { get; set; }
    }
}
