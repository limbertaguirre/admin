using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.GuardianModels
{
    public partial class Administraciondescuentociclodetalle
    {
        public string Susuarioadd { get; set; }
        public DateTime Dtfechaadd { get; set; }
        public string Susuariomod { get; set; }
        public DateTime Dtfechamod { get; set; }
        public long LdescuentociclodetalleId { get; set; }
        public long? LdescuentocicloId { get; set; }
        public long? LdescuentociclotipoId { get; set; }
        public long? LcomplejoId { get; set; }
        public string Smanzano { get; set; }
        public string Slote { get; set; }
        public string Suv { get; set; }
        public decimal? Dmonto { get; set; }
        public string Sobservacion { get; set; }

        public virtual Administraciondescuentociclo Ldescuentociclo { get; set; }
    }
}
