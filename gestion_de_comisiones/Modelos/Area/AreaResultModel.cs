using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Area
{
    public class AreaResultModel
    {
        public AreaResultModel()
        {
        }

        public AreaResultModel(int idArea, string nombre)
        {
            this.idArea = idArea;
            this.nombre = nombre;
        }

        public int idArea { get; set; }
        public string nombre { get; set; }
    }
}
