﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos
{
    public class Result<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
