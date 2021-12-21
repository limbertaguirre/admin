using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Cliente
{
    public class ClienteModel
    {
        public ClienteModel(int idFicha, int codigo, string nombres, string apellidos, string ci, string correoElectronico, DateTime? fechaRegistro, string telOficina, string telMovil, string telFijo, string direccion, DateTime? fechaNacimiento, string contrasena, string comentario, string avatar, bool tieneCuentaBancaria, int idBanco, string cuentaBancaria, bool facturaHabilitado, string razonSocial, string nit, int estado, int idCiudad, int idUsuario, DateTime? fechaCreacion, DateTime? fechaActualizacion)
        {
            IdFicha = idFicha;
            Codigo = codigo;
            Nombres = nombres;
            Apellidos = apellidos;
            Ci = ci;
            CorreoElectronico = correoElectronico;
            FechaRegistro = fechaRegistro;
            TelOficina = telOficina;
            TelMovil = telMovil;
            TelFijo = telFijo;
            Direccion = direccion;
            FechaNacimiento = fechaNacimiento;
            Contrasena = contrasena;
            Comentario = comentario;
            Avatar = avatar;
            TieneCuentaBancaria = tieneCuentaBancaria;
            IdBanco = idBanco;
            CuentaBancaria = cuentaBancaria;
            FacturaHabilitado = facturaHabilitado;
            RazonSocial = razonSocial;
            Nit = nit;
            Estado = estado;
            IdCiudad = idCiudad;
            IdUsuario = idUsuario;
            FechaCreacion = fechaCreacion;
            FechaActualizacion = fechaActualizacion;
        }

        public ClienteModel(int idFicha, int codigo, string nombres, string apellidos, string ci, string correoElectronico, DateTime? fechaRegistro, string telOficina, string telMovil, string telFijo, string direccion, DateTime? fechaNacimiento, string contrasena, string comentario, string avatar, bool? tieneCuentaBancaria, int? idBanco, string cuentaBancaria, bool? facturaHabilitado, string razonSocial, string nit, int? estado,int idCiudad, int? idUsuario, DateTime? fechaCreacion, DateTime? fechaActualizacion,int idTipoPago )
        {
            IdFicha = idFicha;
            Codigo = codigo;
            Nombres = nombres;
            Apellidos = apellidos;
            Ci = ci;
            CorreoElectronico = correoElectronico;
            FechaRegistro = fechaRegistro;
            TelOficina = telOficina;
            TelMovil = telMovil;
            TelFijo = telFijo;
            Direccion = direccion;
            FechaNacimiento = fechaNacimiento;
            Contrasena = contrasena;
            Comentario = comentario;
            Avatar = avatar;
            TieneCuentaBancaria = tieneCuentaBancaria;
            IdBanco = idBanco;
            CuentaBancaria = cuentaBancaria;
            FacturaHabilitado = facturaHabilitado;
            RazonSocial = razonSocial;
            Nit = nit;
            Estado = estado;
            IdCiudad = idCiudad;
            IdUsuario = idUsuario;
            FechaCreacion = fechaCreacion;
            FechaActualizacion = fechaActualizacion;
            IdTipoPago = idTipoPago;
        }

        [JsonPropertyName("idFicha")]
        public int IdFicha { get; set; }
        [JsonPropertyName("codigo")]
        public int Codigo { get; set; }
        [JsonPropertyName("Nombres")]
        public string Nombres { get; set; }
        [JsonPropertyName("apellidos")]
        public string Apellidos { get; set; }
        [JsonPropertyName("ci")]
        public string Ci { get; set; }
        [JsonPropertyName("correoElectronico")]
        public string CorreoElectronico { get; set; }
        [JsonPropertyName("fechaRegistro")]
        public DateTime? FechaRegistro { get; set; }
        [JsonPropertyName("telOficina")]
        public string TelOficina { get; set; }
        [JsonPropertyName("telMovil")]
        public string TelMovil { get; set; }
        [JsonPropertyName("telFijo")]
        public string TelFijo { get; set; }
        [JsonPropertyName("direccion")]
        public string Direccion { get; set; }
        [JsonPropertyName("fechaNacimiento")]
        public DateTime? FechaNacimiento { get; set; }
        [JsonPropertyName("contrasena")]
        public string Contrasena { get; set; }
        [JsonPropertyName("comentario")]
        public string Comentario { get; set; }
        [JsonPropertyName("avatar")]
        public string Avatar { get; set; }
        [JsonPropertyName("tieneCuentaBancaria")]
        public bool? TieneCuentaBancaria { get; set; }
        [JsonPropertyName("idBanco")]
        public int? IdBanco { get; set; }
        [JsonPropertyName("cuentaBancaria")]
        public string CuentaBancaria { get; set; }
        [JsonPropertyName("facturaHabilitado")]
        public bool? FacturaHabilitado { get; set; }
        [JsonPropertyName("razonSocial")]
        public string RazonSocial { get; set; }
        [JsonPropertyName("nit")]
        public string Nit { get; set; }
        [JsonPropertyName("estado")]
        public int? Estado { get; set; }
        [JsonPropertyName("idCiudad")]
        public int IdCiudad { get; set; }
        [JsonPropertyName("idUsuario")]        
        public int? IdUsuario { get; set; }
        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }
        [JsonPropertyName("fechaActualizacion")]
        public DateTime? FechaActualizacion { get; set; }
        [JsonPropertyName("idTipoPago")]
        public int IdTipoPago { get; set; }

    }
}
