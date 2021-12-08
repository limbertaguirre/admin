using System;
using gestion_de_comisiones.Modelos.GestionPagos;

namespace gestion_de_comisiones.Controllers.Events
{
    public class GestionPagosRezagadosEvent
    {        
        public static int SUCCESS = 0;
        public static int ERROR = 1;
        public static int ROLLBACK_ERROR = 2;
        public static int ERROR_CONFIRMAR_TRANSFERIDOS_NO_SELECCIONADOS = 3;
        public static int ERROR_CONFIRMAR_TRANSFERIDOS_SELECCIONADOS = 4;
        public static int EXISTEN_PENDIENTES = 5;
        public static int EXISTEN_RECHAZADOS = 6;
        public static int NO_EXISTEN_PENDIENTES_NI_RECHAZADOS = 7;
        public static int CATCH_SP_REGISTRAR_REZAGADOS_POR_PAGOS_TRANSFERENCIAS_RECHAZADOS = 8;
        public static int ERROR_SP_REGISTRAR_REZAGADOS_POR_PAGOS_TRANSFERENCIAS_RECHAZADOS = 9;
        public static int SUCCESS_SP_REGISTRAR_REZAGADOS_POR_PAGOS_TRANSFERENCIAS_RECHAZADOS = 10;

        public int eventType { get; set; }
        public string message { get; set; }
        public DownloadFileTransferenciaOutput file { get; set; }
        public VerificarPagosTransferenciasOutput dataVerify { get; set; }
    }
}
