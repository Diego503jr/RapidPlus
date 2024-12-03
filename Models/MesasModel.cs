using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid_Plus.Models
{
    class MesasModel
    {
        private int _MesaId;
        private int _Mesa;
        private int _EstadoId;
        private string _Estado;

        public int MesaId
        {
            get => _MesaId;
            set => _MesaId = value;
        }

        public int Mesa
        {
            get => _Mesa;
            set => _Mesa = value;
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
