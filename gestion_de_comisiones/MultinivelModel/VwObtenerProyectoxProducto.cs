using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class VwObtenerProyectoxProducto
    {
        public int IdProyecto { get; set; }
        public string NombreProyecto { get; set; }
        public int IdEmpresa { get; set; }
        public string Producto { get; set; }
        public string NombreEmpresa { get; set; }
    }
}
