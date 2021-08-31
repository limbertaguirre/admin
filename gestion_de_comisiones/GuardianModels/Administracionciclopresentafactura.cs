using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.GuardianModels
{
    public partial class Administracionciclopresentafactura
    {
        public string Susuarioadd { get; set; }
        public DateTime Dtfechaadd { get; set; }
        public string Susuariomod { get; set; }
        public DateTime Dtfechamod { get; set; }
        public int LciclopresentafacturaId { get; set; }
        public int? LcicloId { get; set; }
        public long? LcontactoId { get; set; }
        public long? LsemanaId { get; set; }
    }
}
