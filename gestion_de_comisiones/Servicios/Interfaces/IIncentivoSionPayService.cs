using gestion_de_comisiones.Modelos.Incentivo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios.Interfaces
{
    public interface IIncentivoSionPayService
    {
        public object CargarDatosPlanillaExcel(PlanillaExcelInput param);
    }
}
