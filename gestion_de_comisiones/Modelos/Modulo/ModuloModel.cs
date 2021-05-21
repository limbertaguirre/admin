using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Modulo
{
    public class ModuloModel
    {
        public ModuloModel(int idModulo, string nombre, string icono, int? orden, bool? habilitado, int? idModuloPadre, int? idUsuario, DateTime? fechaCreacion, DateTime? fechaActualizacion)
        {
            IdModulo = idModulo;
            Nombre = nombre;
            Icono = icono;
            Orden = orden;
            Habilitado = habilitado;
            IdModuloPadre = idModuloPadre;
            IdUsuario = idUsuario;
            FechaCreacion = fechaCreacion;
            FechaActualizacion = fechaActualizacion;
        }

        public int IdModulo { get; set; }
        public string Nombre { get; set; }
        public string Icono { get; set; }
        public int? Orden { get; set; }
        public bool? Habilitado { get; set; }
        public int? IdModuloPadre { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
