using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid_Plus.Models
{
    internal class PlatilloModel
    {
        private int _PlatilloId;
        private string _Platillo;
        private string _Descripcion;
        private int _CategoriaId;
        private string _Categoria;
        private decimal _Precio;
        private int _EstadoId;
        private string _Estado;        

        public int PlatilloId
        {
            get => _PlatilloId;
            set => _PlatilloId = value;
        }

        public string Platillo
        {
            get => _Platillo;
            set => _Platillo = value;
        }

        public string Descripcion
        {
            get => _Descripcion;
            set => _Descripcion = value;
        }

        public int CategoriaId
        {
            get => _CategoriaId;
            set => _CategoriaId = value;
        }

        public string Categoria
        {
            get => _Categoria;
            set => _Categoria = value;
        }

        public decimal Precio
        {
            get => _Precio;
            set => _Precio = value;
        }

        public int EstadoId
        {
            get => _EstadoId;
            set => _EstadoId = value;
        }

        public string Estado
        {
            get => _Estado;
            set => _Estado = value;
        }
    }
}
