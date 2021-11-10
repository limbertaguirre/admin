using System;
using gestion_de_comisiones.Modelos.GestionPagos;

namespace gestion_de_comisiones.Controllers.Events
{
    public class GestionPagosEvent
    {
        public static int SUCCESS = 0;
        public static int ERROR = 1;
        public static int ROLLBACK_ERROR = 2;
        public static int ERROR_CONFIRMAR_TRANSFERIDOS_NO_SELECCIONADOS = 3;
        public static int ERROR_CONFIRMAR_TRANSFERIDOS_SELECCIONADOS = 4;

        public int eventType { get; set; }
        public string errorMessage { get; set; }
        public DownloadFileTransferenciaOutput file { get; set; }
    }
}
