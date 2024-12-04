using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid_Plus.Models
{
    internal class DetalleOrdenModel
    {
        private int _IdOrden;
        private int _IdPlatillo;
        private int _IdPlatilloOrden;
        private int _IdEstado;
        private int _IdEstadoOrden;
        private string _EstadoOrden;
        private int _Cantidad;

        public int IdOrden { 
            get => _IdOrden; 
            set => _IdOrden = value; 
        }
        public int IdPlatillo {
            get => _IdPlatillo; 
            set => _IdPlatillo = value; 
        }
        public int IdPlatilloOrden { 
            get => _IdPlatilloOrden; 
            set => _IdPlatilloOrden = value; 
        }
        public int IdEstado { 
            get => _IdEstado; 
            set => _IdEstado = value; 
        }
        public int IdEstadoOrden { 
            get => _IdEstadoOrden; 
            set => _IdEstadoOrden = value; 
        }
        public string EstadoOrden { 
            get => _EstadoOrden; 
            set => _EstadoOrden = value; 
        }
        public int Cantidad { 
            get => _Cantidad; 
            set => _Cantidad = value; 
        }
    }
}
