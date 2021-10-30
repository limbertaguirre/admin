using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class VwVerificarCuentasUsuario
    {
        public string Ci { get; set; }
        public string Nombres { get; set; }
        public bool TieneCuentaBancaria { get; set; }
        public string SionPay { get; set; }
        public int? EstadoSionPay { get; set; }
    }
}
