using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.BDSionPayModels
{
    public partial class EstadoUsuario
    {
        public const int SIN_REGISTRO = 1;
        public const int ACTIVO = 2;
        public const int DESBLOQUEO = 3;
        public const int CAMBIO_CONTRASENA = 4;
    }
}
