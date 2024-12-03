using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid_Plus.Models
{
    class UsuarioModel
    {
        private int _UsuarioId;
        private string _Usuario;
        private string _Clave;
        private string _Nombres;
        private string _Apellidos;
        private int _RolId;
        private string _Rol;
        private int _DUI;
        private int _SexoId;
        private string _Sexo;
        private DateTime _FechaNacimiento;
        private int _EstadoId;
        private string _Estado;
        private int _ContactoId;
        private string _Telefono1;
        private string _Telefono2;

        public int UsuarioId
        {
            get => _UsuarioId;
            set => _UsuarioId = value;
        }

        public string Usuario
        {
            get => _Usuario;
            set => _Usuario = value;
        }

        public string Clave
        {
            get => _Clave; 
            set => _Clave = value;
        }

        public string Nombres
        {
            get => _Nombres;
            set => _Nombres = value;
        }

        public string Apellidos
        {
            get => _Apellidos;
            set => _Apellidos = value;
        }

        public int RolId
        {
            get => _RolId;
            set => _RolId = value;
        }

        public string Rol
        {
            get => _Rol;
            set => _Rol = value;
        }

        public int DUI
        {
            get => _DUI;
            set => _DUI = value;
        }

        public int SexoId
        {
            get => _SexoId;
            set => _SexoId = value;
        }

        public string Sexo
        {
            get => _Sexo;
            set => _Sexo = value;
        }

        public DateTime FechaNacimiento
        {
            get => _FechaNacimiento;
            set => _FechaNacimiento = value;
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

        public int ContactoId
        {
            get => _ContactoId;
            set => _ContactoId = value;
        }

        public string Telefono1
        {
            get => _Telefono1;
            set => _Telefono1 = value;
        }

        public string Telefono2
        {
            get => _Telefono2;
            set => _Telefono2 = value;
        }

    }
}
