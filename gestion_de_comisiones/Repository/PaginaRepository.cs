using gestion_de_comisiones.Modelos.Pagina;
using gestion_de_comisiones.MultinivelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class PaginaRepository
    {
        BDMultinivelContext contextMulti = new BDMultinivelContext();
        public List<PaginaModel> ObtenerPaginas(int idModulo)
        {
            try
            {
                var objUsuario = contextMulti.Paginas.Where(x =>  x.IdModulo == idModulo && x.Habilitado == true ).Select(p =>  new PaginaModel(p.IdPagina, p.Nombre, p.UrlPagina,p.Icono, p.Orden, p.Habilitado, p.IdModulo, p.IdUsuario, p.FechaCreacion, p.FechaActualizacion)).ToList();
                return objUsuario;
            }
            catch (Exception ex)
            {
                List<PaginaModel> modelos = new List<PaginaModel>();
                return modelos;
            }
        }


    }
}
