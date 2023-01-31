using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Modelos.Usuario
{
    public class UsuarioModel
    {
        public UsuarioModel()
        {
        }

        public UsuarioModel(int idUsuario, string usuario1, string nombres, string apellidos, string telefono, string corporativo, DateTime? fechaNacimiento, int? idRol, int? idSucursal, int? idArea, int? usuarioId, DateTime? fechaCreacion, DateTime? fechaActualizacion)
        {
            IdUsuario = idUsuario;
            Usuario1 = usuario1;
            Nombres = nombres;
            Apellidos = apellidos;
            Telefono = telefono;
            Corporativo = corporativo;
            FechaNacimiento = fechaNacimiento;
            IdRol = idRol;
            IdSucursal = idSucursal;
            IdArea = idArea;
            UsuarioId = usuarioId;
            FechaCreacion = fechaCreacion;
            FechaActualizacion = fechaActualizacion;
        }

        public int IdUsuario { get; set; }
        public string Usuario1 { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Corporativo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? IdRol { get; set; }
        public int? IdSucursal { get; set; }
        public int? IdArea { get; set; }
        public int? UsuarioId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

    }
}
