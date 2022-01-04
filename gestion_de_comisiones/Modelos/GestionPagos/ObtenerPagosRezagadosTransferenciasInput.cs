using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.GestionPagos
{
    public class ObtenerPagosRezagadosTransferenciasInput
    {
        public string user { get; set; }
        public string ci { get; set; }
        public int comisionId { get; set; }
        public int cicloId { get; set; }
        public int empresaId { get; set; }
        //public DateTime date { get; set; }
    }
}
