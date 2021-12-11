using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestion_de_comisiones.Modelos.Reporte
{
    public class ReporteFreelancerModel
    {
        public ReporteFreelancerModel(int idComisionDetalle, string ciclo, string tipoPago, float montoNeto, string nroCuenta, string cuentaBancaria)
        {
            this.idComisionDetalle = idComisionDetalle;
            this.ciclo = ciclo;
            this.montoNeto = montoNeto;
            this.nroCuenta = nroCuenta;
            this.cuentaBancaria = cuentaBancaria;
            this.tipoPago = tipoPago;
        }

        [Column("id_comision_detalle")]
        public int idComisionDetalle { get; private set; }

        [Column("ciclo")]
        public string ciclo { get; private set; }
               
        [Column("monto_neto", TypeName = "decimal(18, 2)")]
        public float montoNeto { get; private set; }

        [Column("nro_cuenta")]
        public string nroCuenta { get; private set; }

        [Column("cuenta_banco")]
        public string cuentaBancaria { get; private set; }

        [Column("tipo_pago")]
        public string tipoPago { get; private set; }

    }
}
