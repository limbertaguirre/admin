using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.FormaPago
{
    public class AutorizadorRespuestaModel
    {
        
        public int idciclo { get; set; }
        public int idComision { get; set; }
        public bool autorizador { get; set; }
        public bool comisionAutorizada { get; set; }
        public  List<Autorizador> autorizadores { get; set; }

    }
}
