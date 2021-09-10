using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.GuardianModels
{
    public partial class Administracionciclo
    {
        public Administracionciclo()
        {
            Administraciondescuentocicloes = new HashSet<Administraciondescuentociclo>();
        }

        public string Susuarioadd { get; set; }
        public DateTime Dtfechaadd { get; set; }
        public string Susuariomod { get; set; }
        public DateTime Dtfechamod { get; set; }
        public long LcicloId { get; set; }
        public string Snombre { get; set; }
        public DateTime? Dtfechainicio { get; set; }
        public DateTime? Dtfechafin { get; set; }
        public int? Lestado { get; set; }
        public DateTime? Dtfechacierre { get; set; }
        public DateTime? Dtfechaprecierre1 { get; set; }
        public DateTime? Dtfechaprecierre2 { get; set; }
        public string Cverenweb { get; set; }
        public string Estadogestor { get; set; }

        public virtual ICollection<Administraciondescuentociclo> Administraciondescuentocicloes { get; set; }
    }
}
