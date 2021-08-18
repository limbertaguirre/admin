using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.AplicacionDetalleProducto
{
    public class ComisionDetalleModel
    {
        public ComisionDetalleModel()
        {
        }

        public ComisionDetalleModel(int idComisionDetalle, decimal? montoBruto, decimal? porcentajeRetencion, decimal? montoRetencion, decimal? montoAplicacion, decimal? montoNeto, int? idComision, int? idFicha, int? idUsuario, DateTime? fechaCreacion, DateTime? fechaActualizacion)
        {
            this.idComisionDetalle = idComisionDetalle;
            this.montoBruto = montoBruto;
            this.porcentajeRetencion = porcentajeRetencion;
            this.montoRetencion = montoRetencion;
            this.montoAplicacion = montoAplicacion;
            this.montoNeto = montoNeto;
            this.idComision = idComision;
            this.idFicha = idFicha;
            this.idUsuario = idUsuario;
            this.fechaCreacion = fechaCreacion;
            this.fechaActualizacion = fechaActualizacion;
        }

        public int idComisionDetalle { get; set; }
        public decimal? montoBruto { get; set; }
        public decimal? porcentajeRetencion { get; set; }
        public decimal? montoRetencion { get; set; }
        public decimal? montoAplicacion { get; set; }
        public decimal? montoNeto { get; set; }
        public int? idComision { get; set; }
        public int? idFicha { get; set; }
        public int? idUsuario { get; set; }
        public DateTime? fechaCreacion { get; set; }
        public DateTime? fechaActualizacion { get; set; }
    }
} 
