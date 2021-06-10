using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class Ficha
    {
        public int IdFicha { get; set; }
        public string Codigo { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Ci { get; set; }
        public string CorreoElectronico { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string TelOficina { get; set; }
        public string TelMovil { get; set; }
        public string TelFijo { get; set; }
        public string Direccion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Contrasena { get; set; }
        public string Comentario { get; set; }
        public string Avatar { get; set; }
        public bool? TieneCuentaBancaria { get; set; }
        public int? IdBanco { get; set; }
        public string CuentaBancaria { get; set; }
        public bool? FacturaHabilitado { get; set; }
        public string RazonSocial { get; set; }
        public string Nit { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public int? Estado { get; set; }
    }
}
