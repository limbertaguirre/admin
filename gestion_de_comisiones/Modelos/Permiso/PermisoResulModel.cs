using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Permiso
{
    public class PermisoResulModel
    {
        public PermisoResulModel()
        {
        }

        public PermisoResulModel(int idPermiso, string permiso1)
        {
            this.idPermiso = idPermiso;
            this.permiso1 = permiso1;
        }

        [JsonPropertyName("id_permiso")]
        public int idPermiso { get; set; }
        [JsonPropertyName("permiso")]
        public string permiso1 { get; set; }

    }
}
