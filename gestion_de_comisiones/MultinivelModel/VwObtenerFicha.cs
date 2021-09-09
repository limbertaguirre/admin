using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class VwObtenerFicha
    {
        public int IdFicha { get; set; }
        public int Codigo { get; set; }
        public string NombreCompleto { get; set; }
        public string Ci { get; set; }
        public bool TieneCuentaBancaria { get; set; }
        public int IdBanco { get; set; }
        public string NombreBanco { get; set; }
        public string CodigoBanco { get; set; }
        public string CuentaBancaria { get; set; }
        public int Estado { get; set; }
        public string Avatar { get; set; }
        public string Nivel { get; set; }
    }
}
