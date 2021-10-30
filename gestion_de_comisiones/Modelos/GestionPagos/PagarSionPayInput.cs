using System;
namespace gestion_de_comisiones.Modelos.GestionPagos
{
    public class PagarSionPayInput
    {
        public string UsuarioLogin { get; set; }
        public int idUsuario { get; set; }
        public int idCiclo { get; set; }
    }
}
