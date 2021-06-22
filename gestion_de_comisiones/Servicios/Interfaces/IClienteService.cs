using gestion_de_comisiones.Modelos.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios.Interfaces
{
    public interface IClienteService
    {
        public object ObtenerClientes(string usuario);
        public object buscarClientesNombre(string usuario, string criterio);
        public object obtenerClientePorID(string usuario, int idCliente);
        public object ListarPaises(string usuario);
        public object listaCiudadesXPais(string usuario, int idPais);
        public object obtenerListadeBajas(string usuario);
        public object obtenerBancoParaclientes(string usuario);
        public object obtenerNivelesCliente(string usuario);
        public object ActualizarFichaCliente(ClienteUpdateInputModel fichaClient);

    }
}
