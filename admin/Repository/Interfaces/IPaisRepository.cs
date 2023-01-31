using admin.Modelos.Pais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Repository.Interfaces
{
    public interface IPaisRepository
    {
        public object ListaPaises(string usuario);
        public List<CiudadOutPutModel> obtenerCiudadXpais(string usuario, int idPais);
    }
}
