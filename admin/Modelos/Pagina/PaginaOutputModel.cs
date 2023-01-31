using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace admin.Modelos.Pagina
{
    public class PaginaOutputModel
    {
        
        public int idPage { get; set; }
        public string title { get; set; }
        public string descripion { get; set; }
        public string namePage { get; set; }
        public string path { get; set; }
        public string icon { get; set; }
        public int Orden { get; set; }


    }
}
