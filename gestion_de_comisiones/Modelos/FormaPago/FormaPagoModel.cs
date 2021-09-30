using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.FormaPago
{
    public class FormaPagoModel
    {
        public FormaPagoModel()
        {
        }

        public FormaPagoModel(int idTipoPago, string nombre, string descripcion, int idUsuario, DateTime? fechaCreacion, DateTime? fechaActualizacion, bool estado, string icono)
        {
            IdTipoPago = idTipoPago;
            Nombre = nombre;
            Descripcion = descripcion;
            IdUsuario = idUsuario;
            FechaCreacion = fechaCreacion;
            FechaActualizacion = fechaActualizacion;
            Estado = estado;
            Icono = icono;
        }

        public int IdTipoPago { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public bool Estado { get; set; }
        public string Icono { get; set; }
    }
}
