using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.GestionPagos
{
    public class RespuestaDetalleComision
    {
        public string NombreEmprea { get; set; }
        public decimal MontoComision { get; set; }
        public int IdDetalleComision { get; set; }
        public bool EstaPagado { get; set; }
    }
}
