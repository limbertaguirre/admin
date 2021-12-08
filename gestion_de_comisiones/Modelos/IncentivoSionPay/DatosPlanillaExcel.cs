using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Incentivo
{
    public class DatosPlanillaExcel
    {      
        public int Nro { get; set; }
        public string Usuario { get; set; }
        public string Empresa { get; set; }
        public int IdEmpresa { get; set; }
        public string NombreCliente { get; set; }
        public string CiCliente { get; set; }
        public string CuentaSionPay { get; set; }
        public decimal Monto { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public string Detalle { get; set; }
    }
}
