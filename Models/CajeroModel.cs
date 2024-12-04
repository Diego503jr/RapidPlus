using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid_Plus.Models
{
    internal class CajeroModel
    {
        private int _IdOrden;
        private string _NombreCliente;
        private DateTime _FechaOrden;
        private string _Orden;
        private int _Cantidad;
        private string _NombreUsuario;
        private decimal _Subtotal;
        private decimal _PrecioPlatillo;

        public int IdOrden { 
            get => _IdOrden; 
            set => _IdOrden = value; 
        }
        public string NombreCliente { 
            get => _NombreCliente; 
            set => _NombreCliente = value; 
        }
        public DateTime FechaOrden { 
            get => _FechaOrden; 
            set => _FechaOrden = value; 
        }
        public string Orden { 
            get => _Orden; 
            set => _Orden = value; 
        }
        public int Cantidad { 
            get => _Cantidad; 
            set => _Cantidad = value; 
        }
        public string NombreUsuario { 
            get => _NombreUsuario; 
            set => _NombreUsuario = value; 
        }
        public decimal Subtotal { 
            get => _Subtotal;
            set => _Subtotal = value;
        }
        public decimal PrecioPlatillo { 
            get => _PrecioPlatillo; 
            set => _PrecioPlatillo = value; 
        }
    }
}
