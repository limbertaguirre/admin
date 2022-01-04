using gestion_de_comisiones.Modelos.Incentivo;
using gestion_de_comisiones.Modelos.IncentivoSionPay;
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
        public object ObtenerTipoIncentivo(string usuario);
        public object ObtenerTiposPagos(string usuario);
        public object ObtenerTipoIncentivosPagosSegunCiclo(int nroCicloMensual, string usuario);
        public object RegistrarTipoIncentivoPago(string descripcion);
        public object ObtenerPagosIncentivosSegunCicloIdTipoIncentivo(int nroCicloMensual, int tipoIncentivo, string usuario);
        public List<PagoIncentivo> PagarIncentivos(List<PagoIncentivo> incentivosPagar, string usuario);
    }
}
