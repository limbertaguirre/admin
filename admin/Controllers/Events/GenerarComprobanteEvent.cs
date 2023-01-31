using System;
namespace admin.Controllers.Events
{
    public class GenerarComprobanteEvent
    {
        public const int ERROR_FECHA_PAGO_NULL = 20;
        public const int ERROR_GENERAR_COMPROBANTE_BANCO = 21;
        public const int CATCH_GENERAR_COMPROBANTE = 22;
        public const int SUCCESS_GENERACION_COMPROBANTE_BANCO = 23;
        public const int ERROR_LISTA_VACIA = 24;
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
    }
}
