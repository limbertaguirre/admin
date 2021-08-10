using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos
{
    public abstract class GpComision
    {
        public static readonly int TYPE_PAGO_COMISIONES = 1;
        public static readonly int TYPE_PAGO_REZAGADOS = 2;

        abstract public int getType();
    }
}
