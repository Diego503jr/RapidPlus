using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid_Plus.Models
{
    internal class ClienteModel
    {
        private int _IdCliente;
        private string _NombreCliente;
        private string _ApellidoCliente;

        public int IdCliente
        {
            get => _IdCliente;
            set => _IdCliente = value;
        }

        public string NombreCliente
        {
            get => _NombreCliente;
            set => _NombreCliente = value;
        }
        public string ApellidoCliente
        {
            get => _ApellidoCliente;
            set => _ApellidoCliente = value;
        }
    }
}
