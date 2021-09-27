using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.FormaPago
{
    public class TipoPagoInputmodel
    {
        public TipoPagoInputmodel()
        {
        }

        public TipoPagoInputmodel(int idTipoPago, string nombre, string icono)
        {
            this.idTipoPago = idTipoPago;
            this.nombre = nombre;
            this.icono = icono;
        }

        public TipoPagoInputmodel(int idTipoPago, string nombre, bool estado, string descripcion, string icono)
        {
            this.idTipoPago = idTipoPago;
            this.nombre = nombre;
            this.estado = estado;
            this.descripcion = descripcion;
            this.icono = icono;
        }

        public int idTipoPago { get; set; }
        public string nombre { get; set; }
        public bool estado { get; set; }
        public string descripcion { get; set; }
        public string icono { get; set; }
    }
}
