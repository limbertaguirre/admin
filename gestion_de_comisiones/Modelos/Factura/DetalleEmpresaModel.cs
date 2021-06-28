using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Factura
{
    public class DetalleEmpresaModel
    {
        public int  idFicla { get; set; }
        public string nombreFicha { get; set; }
        public string rango { get; set; }
        public string ciclo { get; set; }
        public string avatar { get; set; }
        public int idCiclo { get; set; }
        public List<VwObtenerComisionesDetalleEmpresaModel> listDetalle { get; set; }
    }
}
