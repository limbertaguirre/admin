using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.GestionPagos
{
    public class BuscarComisionInput
    {
        public string usuarioLogin { get; set; }
        public int idCiclo { get; set; }
        public string nombreCriterio { get; set; }
        public int comisionId { get; set; }
    }
}
