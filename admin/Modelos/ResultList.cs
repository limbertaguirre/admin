using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Modelos
{
    public class ResultList<T>
    {
        
        public int Code { get; set; }        
        public string Message { get; set; }
        public List<T> Data { get; set; }
    }
}
