using gestion_de_comisiones.Modelos.Pais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository.Interfaces
{
    public interface IPaisRepository
    {
        public object ListaPaises(string usuario);
        public List<CiudadOutPutModel> obtenerCiudadXpais(string usuario, int idPais);
    }
}
