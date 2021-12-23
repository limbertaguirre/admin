using gestion_de_comisiones.Modelos.Incentivo;
using gestion_de_comisiones.Modelos.IncentivoSionPay;
using gestion_de_comisiones.MultinivelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios.Interfaces
{
    public interface IIncentivoSionPayService
    {
        public object CargarDatosPlanillaExcel(PlanillaPagoIncentivo param);
        public object ObtenerCiclos(string usuario);
        public object VerificarCuentaSionPay(string cuenta);
        public object ObtenerTipoIncentivo(string usuario);
        public object ObtenerTipoPagos(string usuario);
        public object ObtenerTipoIncentivosPagosSegunCiclo(int  nroCicloMensual, string usuario);
        public object RegistrarTipoIncentivoPago(TipoIncentivoPagoModel tipoIncentivoPago,string usuario);
        public object ObtenerPagosIncentivosSegunCicloIdTipoIncentivo(int nroCicloMensual, int tipoIncentivo, string usuario);
        public object pagarIncentivos(List<PagoIncentivo> incentivosPagar, string usuario);
    }
}
