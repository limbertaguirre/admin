using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos
{
    public class PagoComisiones : GpComision
    {
        public override int getType()
        {
            return TYPE_PAGO_COMISIONES;
        }
    }
}
