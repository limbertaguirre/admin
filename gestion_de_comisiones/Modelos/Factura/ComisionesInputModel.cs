using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Factura
{
    public class ComisionesInputModel
    {
        public string usuarioLogin { get; set; }
        public int idCiclo { get; set; }
        public int comisionId { get; set; }
    }
}
