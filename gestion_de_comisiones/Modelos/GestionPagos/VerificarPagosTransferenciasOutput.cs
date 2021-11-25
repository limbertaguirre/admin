using System;
namespace gestion_de_comisiones.Modelos.GestionPagos
{
    public class VerificarPagosTransferenciasOutput
    {
        public static int PENDIENTES = 1;
        public static int CONFIRMADOS = 2;
        public static int RECHAZADOS = 3;
        public static int CONFIRMADOS_O_RECHAZADOS = 4;       

        public int totalPendientes { get; set; }
        public int totalEnviadosConfirmar { get; set; }
        public int totalConfirmados { get; set; }
        public int totalRechazados { get; set; }
        public bool recargarCicloActual { get; set; }
        public string montoTotalConfirmados { get; set; }
        public string montoTotalRechazados { get; set; }
        public string montoTotalPendientes { get; set; }
        public object descargarExcel { get; set; }
        public string empresa { get; set; }
        public string ciclo { get; set; }
        public int type { get; set; }
    }
}
