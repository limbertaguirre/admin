using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios
{
    public class ClienteService : IClienteService
    {
        ConfiguracionService Respuesta = new ConfiguracionService();
        private readonly ILogger<ClienteService> Logger;

        public ClienteService(ILogger<ClienteService> logger , IClienteRepository repository)
        {
            Logger = logger;
            Repository = repository;
        }
        public IClienteRepository Repository { get; set; }


        public object ObtenerClientes(string usuario)
        {
            try
            {
                Logger.LogInformation($" es el usuario : {usuario} inicio el servicio ObtenerClientes() ");
                var listaCliente = Repository.obtenerAllClientes(usuario);
                return Respuesta.ReturnResultdo(0, "ok", listaCliente);
                

            }
            catch (Exception ex)
            {              
                return Respuesta.ReturnResultdo(1, "OK", "problemas en el servidor, intente mas tarde");
            }
        }
        public object buscarClientesNombre(string usuario, string criterio)
        {
            try
            {
                Logger.LogInformation($" es el usuario : {usuario} inicio el servicio buscarClientes() criterio de busqueda: {criterio} ");
                var listaCliente = Repository.buscarCliente(usuario, criterio);
                return Respuesta.ReturnResultdo(0, "ok", listaCliente);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch buscarClientes() criterio de busqueda: {criterio} error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "OK", "problemas en el servidor, intente mas tarde");
            }
        }
        public object obtenerClientePorID(string usuario, int idCliente)
        {
            try
            {
                Logger.LogInformation($" es el usuario : {usuario} inicio el servicio obtenerClientePorID() idCliente : {idCliente} ");
                var cliente = Repository.obtenerClienteXID(usuario, idCliente);
                return Respuesta.ReturnResultdo(0, "ok", cliente);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch obtenerClientePorID() idcliente : {idCliente} error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "OK", "problemas en el servidor, intente mas tarde");
            }
        }

    }
}
