using gestion_de_comisiones.Modelos.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository.Interfaces
{
    public interface IClienteRepository
    {
        public List<ClienteOutputModel> obtenerAllClientes(string usuario);
        public List<ClienteOutputModel> buscarCliente(string usuario, string criterio);
        public FichaClienteOutPutModel obtenerClienteXID(string usuario, int idCliente);
        public object tiposdeBajasClientes(string usuario);
        public object listabancosParaClientes(string usuario);
        public object listarNivelesClientes(string usuario);
    }
}
