using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid_Plus.Models.Mesero
{
    class OrdenesModel
    {
        private int _IdCliente;
        private int _IdUsuario;
        private DateTime _FechaOrden; 
        private int _IdMesa;
        private int _Mesa;
        private int _IdEstadoOrden;
        private string _EstadoOrden;
        private int _IdOrden;
        private string _Orden;
        private int _IdPlatilloOrden;
        private string _NombrePlatillo;
        private string _DescripcionPlatillo;
        private int _IdDetalleOrden;
        private int _Cantidad;
        private decimal _Total;

        public int IdCliente { 
            get => _IdCliente; 
            set => _IdCliente = value; 
        }
        public int IdUsuario { 
            get => _IdUsuario; 
            set => _IdUsuario = value; 
        }
        public DateTime FechaOrden { 
            get => _FechaOrden; 
            set => _FechaOrden = value; 
        }
        public int IdMesa { 
            get => _IdMesa; 
            set => _IdMesa = value; 
        }
        public int Mesa { 
            get => _Mesa; 
            set => _Mesa = value; 
        }
        public int IdEstadoOrden { 
            get => _IdEstadoOrden; 
            set => _IdEstadoOrden = value; 
        }
        public string EstadoOrden { 
            get => _EstadoOrden; 
            set => _EstadoOrden = value;
        }
        public int IdOrden { 
            get => _IdOrden; 
            set => _IdOrden = value; 
        }
        public string Orden { 
            get => _Orden; 
            set => _Orden = value; 
        }
        public int IdPlatilloOrden { 
            get => _IdPlatilloOrden;
            set => _IdPlatilloOrden = value; 
        }
        public string NombrePlatillo { 
            get => _NombrePlatillo; 
            set => _NombrePlatillo = value; 
        }
        public string DescripcionPlatillo {
            get => _DescripcionPlatillo; 
            set => _DescripcionPlatillo = value; 
        }
        public int IdDetalleOrden { 
            get => _IdDetalleOrden; 
            set => _IdDetalleOrden = value; 
        }
        public int Cantidad {
            get => _Cantidad; 
            set => _Cantidad = value; 
        }
        public decimal Total { 
            get => _Total; 
            set => _Total = value; 
        }
    }
}
