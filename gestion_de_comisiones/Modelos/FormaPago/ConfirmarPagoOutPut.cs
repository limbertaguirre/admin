using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.FormaPago
{
    public class ConfirmarPagoOutPut
    {
        public bool Habilitado { get; set; }
        public List<AutorizacionAreaModel> ListaPorAreas { get; set; }
        public List<FormaPagoDisponiblesModel> ListSeleccionados { get; set; }
    } 
}
