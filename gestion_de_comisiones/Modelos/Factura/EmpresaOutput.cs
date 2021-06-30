using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Factura
{
    public class EmpresaOutput
    {
        public EmpresaOutput(int idEmpresa, string nombre)
        {
            this.idEmpresa = idEmpresa;
            this.nombre = nombre;
        }

        public int idEmpresa { get; set; }
        public string nombre { get; set; }
    }
}
