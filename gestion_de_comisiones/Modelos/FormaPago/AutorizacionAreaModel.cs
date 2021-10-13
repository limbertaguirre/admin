using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.FormaPago
{
    public class AutorizacionAreaModel
    {
        public int IdArea { get; set; }
        public string Area { get; set; }
        public bool Habilitado { get; set; }
        public int CantidadHabilitados { get; set; }
        public int CantidadConfigMinima { get; set; }
        public List<Autorizador> ListaAutorizadores { get; set; }

    }
}
