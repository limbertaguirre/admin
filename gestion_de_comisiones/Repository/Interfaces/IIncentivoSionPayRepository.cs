using gestion_de_comisiones.Modelos.Incentivo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository.Interfaces
{
    public interface IIncentivoSionPayRepository
    {
        public object GuardarPlanillaIncentivoSionPay(PlanillaPagoIncentivo planillaIncentivo);
        public List<DatosPlanillaExcel> verificarIncentivosEmpresaCiNoRepetidos(PlanillaPagoIncentivo planillaIncentivo);
        public object ObtenerCiclos(string usuario);
        public object VerificarCuentaSionPay(PlanillaExcelInput param);
        public object ObtenerTipoIncentivo(string usuario);
    }
}
