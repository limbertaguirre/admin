using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.GuardianModels
{
    public partial class Administraciondescuentociclo
    {
        public Administraciondescuentociclo()
        {
            Administraciondescuentociclodetalles = new HashSet<Administraciondescuentociclodetalle>();
        }

        public string Susuarioadd { get; set; }
        public DateTime Dtfechaadd { get; set; }
        public string Susuariomod { get; set; }
        public DateTime Dtfechamod { get; set; }
        public long LdescuentocicloId { get; set; }
        public long? LcicloId { get; set; }
        public long? LcontactoId { get; set; }
        public decimal? Dtotal { get; set; }
        public string Sdetalles { get; set; }
        public long? LsemanaId { get; set; }

        public virtual ICollection<Administraciondescuentociclodetalle> Administraciondescuentociclodetalles { get; set; }
    }
}
