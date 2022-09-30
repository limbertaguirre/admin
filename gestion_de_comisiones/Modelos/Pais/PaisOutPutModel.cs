using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Pais
{
    public class PaisOutPutModel
    {
        public PaisOutPutModel(int idPais, string nombre)
        {
            this.idPais = idPais;
            this.nombre = nombre;
        }

        public int idPais { get; set; }
        public string nombre { get; set; }

    }
}
