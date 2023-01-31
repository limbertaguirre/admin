using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Modelos.Usuario
{
    public class ControlUsuarioModel
    {
        public ControlUsuarioModel(int idControlUsuario, string usuario, int? cantidadIntentos, DateTime? fechaBloquedo, DateTime? fechaDesbloqueo, string netSessionId, int estado)
        {
            IdControlUsuario = idControlUsuario;
            Usuario = usuario;
            CantidadIntentos = cantidadIntentos;
            FechaBloquedo = fechaBloquedo;
            FechaDesbloqueo = fechaDesbloqueo;
            NetSessionId = netSessionId;
            Estado = estado;
        }
        public int IdControlUsuario { get; set; }
        public string Usuario { get; set; }
        public int? CantidadIntentos { get; set; }
        public DateTime? FechaBloquedo { get; set; }
        public DateTime? FechaDesbloqueo { get; set; }
        public string NetSessionId { get; set; }
        public int Estado { get; set; }
    }
}
