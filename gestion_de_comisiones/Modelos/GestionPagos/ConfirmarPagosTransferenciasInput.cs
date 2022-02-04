using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.GestionPagos
{
    public class ConfirmarPagosTransferenciasInput
    {
        public string user { get; set; }
        public int cicloId { get; set; }
        public int empresaId { get; set; }
        public int comisionId { get; set; }
        public int idComisionDetalle { get; set; }
        public List<int> confirmados { get; set; }
        public List<int> rechazados { get; set; }
    }

}
