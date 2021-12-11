using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace gestion_de_comisiones.Modelos.Reporte
{
    public class ReporteDetalleCicloModel
    {
        public ReporteDetalleCicloModel()
        {
        }

        public ReporteDetalleCicloModel(int idComisionDetalleEmpresa,  float montoNeto, string nombreEmpresa, string tipoComision, int idTipoComision)
        {
            this.idComisionDetalleEmpresa = idComisionDetalleEmpresa;
            this.montoNeto = montoNeto;
            this.nombreEmpresa = nombreEmpresa;
            this.tipoComision = tipoComision;
            this.idTipoComision = idTipoComision;
        }

        
        [Column("id_comision_detalle_empresa")]
        public int idComisionDetalleEmpresa { get; set; }
        
        [Column("monto_neto", TypeName = "decimal(18, 2)")]
        public float montoNeto { get; set; }
        
        [Column("nombre_empresa")]
        public string nombreEmpresa { get; set; }
        
        [Column("tipo_comision")]
        public string tipoComision { get; set; }
        
        [Column("id_tipo_comision")]
        public int idTipoComision { get; set; }
    }
}
