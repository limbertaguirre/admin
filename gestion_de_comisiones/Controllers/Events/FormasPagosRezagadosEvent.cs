using System;
namespace gestion_de_comisiones.Controllers.Events
{
    public class FormasPagosRezagadosEvent
    {
        public static int SUCCESS = 0;
        public static int ERROR = 1;
        public static int ROLLBACK_ERROR = 2;
        public static int ERROR_CERRAR_FORMAS_PAGOS = 3;
        public static int EXISTE_DOS_REGISTROS_COMISIONES_REZAGADOS = 4;

        public int eventType { get; set; }
        public string message { get; set; }
    }
}
