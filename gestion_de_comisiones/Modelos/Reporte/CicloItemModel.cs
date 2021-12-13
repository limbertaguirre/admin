using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestion_de_comisiones.Modelos.Reporte
{
    public class CicloItemModel
    {
        public CicloItemModel(int idCiclo, string nombre)
        {
            this.idCiclo = idCiclo;
            this.nombre = nombre;
        }

        [Column("id_ciclo")]
        public int idCiclo { get; private set; }

        [Column("nombre")]
        public string nombre { get; private set; }
    }
}
