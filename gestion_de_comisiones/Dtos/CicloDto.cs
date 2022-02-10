using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Dtos
{
    public class CicloDto
    {
        public CicloDto(int IdCiclo, string Nombre)
        {
            this.IdCiclo = IdCiclo;
            this.Nombre = Nombre;
        }

        public CicloDto(int IdCiclo, string Nombre, int comisionId)
        {
            this.IdCiclo = IdCiclo;
            this.Nombre = Nombre;
            this.comisionId = comisionId;
        }

        public int comisionId { get; set; }
        public int IdCiclo { get; set; }
        public string Nombre { get; set; }
    }
}
