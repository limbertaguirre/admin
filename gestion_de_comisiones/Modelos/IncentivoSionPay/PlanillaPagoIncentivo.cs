using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Incentivo
{
    public class PlanillaPagoIncentivo
    {
        public List<DatosPlanillaExcel> DatosClientes { get; set; }
        public int IdCiclo { get; set;}
        public string UsuarioNombre { get; set;}
    }
}
