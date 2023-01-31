using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Modelos.Pagina
{
    public class PaginaModel
    {
        public PaginaModel(int idPagina, string nombre, string urlPagina, string icono, int? orden, bool? habilitado, int? idModulo, int? idUsuario, DateTime? fechaCreacion, DateTime? fechaActualizacion)
        {
            IdPagina = idPagina;
            Nombre = nombre;
            UrlPagina = urlPagina;
            Icono = icono;
            Orden = orden;
            Habilitado = habilitado;
            IdModulo = idModulo;
            IdUsuario = idUsuario;
            FechaCreacion = fechaCreacion;
            FechaActualizacion = fechaActualizacion;
        }

        public int IdPagina { get; set; }
        public string Nombre { get; set; }
        public string UrlPagina { get; set; }
        public string Icono { get; set; }
        public int? Orden { get; set; }
        public bool? Habilitado { get; set; }
        public int? IdModulo { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

    }
}
