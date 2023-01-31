using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace admin.Modelos.Rol
{
    public class RolResulModel
    {
        private bool? habilitado;

        public RolResulModel()
        {
        }

        public RolResulModel(int idRol, string nombre, string descripcion, bool? habilitado)
        {
            IdRol = idRol;
            Nombre = nombre;
            Descripcion = descripcion;
            this.habilitado = habilitado;
        }

        [JsonPropertyName("idRol")]
        public int IdRol { get; set; }
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }
        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }
        [JsonPropertyName("estado")]
        public bool Habilitado { get; set; }
        [JsonPropertyName("listModulos")]
        public List<ModuloResulwithPermisoModel> Modulos { get; set; }

    }
}
