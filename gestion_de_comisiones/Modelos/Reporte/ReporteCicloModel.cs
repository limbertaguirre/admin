using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace gestion_de_comisiones.Modelos.Reporte
{
    public class ReporteCicloModel
    {
        public ReporteCicloModel()
        {
        }

        public ReporteCicloModel(int idComisionDetalle, string nombres, string apellidos, string ci, float montoNeto, string nroCuenta, string cuentaBancaria, string tipoPago, int idTipoPago)
        {
            this.idComisionDetalle = idComisionDetalle;
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.ci = ci;
            this.montoNeto = montoNeto;
            this.nroCuenta = nroCuenta;
            this.cuentaBancaria = cuentaBancaria;
            this.tipoPago = tipoPago;
            this.idTipoPago = idTipoPago;
        }

        
        [Column("id_comision_detalle")]
        public int idComisionDetalle { get; private set; }
        
        [Column("nombres")]
        public string nombres { get; private set; }
        
        [Column("apellidos")]
        public string apellidos { get; private set; }
        
        [Column("ci")]
        public string ci { get; private set; }
        
        [Column("monto_neto", TypeName = "decimal(18, 2)")]
        public float montoNeto { get; private set; }
        
        [Column("nro_cuenta")]
        public string nroCuenta { get; private set; }
        
        [Column("cuenta_bancaria")]
        public string cuentaBancaria { get; private set; }
        
        [Column("tipo_pago")]
        public string tipoPago { get; private set; }
        
        [Column("id_tipo_pago")]
        public int idTipoPago { get; private set; }
    }
}
