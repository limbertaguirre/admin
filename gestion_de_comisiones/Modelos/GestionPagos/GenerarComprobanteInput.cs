using System;
namespace gestion_de_comisiones.Modelos.GestionPagos
{
    public class GenerarComprobanteInput
    {
        public int usuarioId { get; set; }
        public string username { get; set; }
        public int comisionId { get; set; }
        public int cicloId { get; set; }
        public int empresaId { get; set; }
    }
}
