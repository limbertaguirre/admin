using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Modelos.Pais
{
    public class CiudadOutPutModel
    {
        public CiudadOutPutModel(int idCiudad, string nombre)
        {
            this.idCiudad = idCiudad;
            this.nombre = nombre;
        }

        public int idCiudad { get; set; }
        public string nombre { get; set; }
    }
}
