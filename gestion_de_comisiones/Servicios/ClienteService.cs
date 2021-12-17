using gestion_de_comisiones.Modelos.Cliente;
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

        public ClienteService(ILogger<ClienteService> logger , IClienteRepository repository, IPaisRepository repositoryPais)
        {
            Logger = logger;
            Repository = repository;
            RepositoryPais = repositoryPais;
        }
        public IClienteRepository Repository { get; set; }
        public IPaisRepository RepositoryPais { get; set; }


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
        public object ListarPaises(string usuario)
        {
            try
            {
                Logger.LogInformation($" es el usuario : {usuario} inicio el servicio ListarPaises en service cliente() ");
                var cliente = RepositoryPais.ListaPaises(usuario);
                return Respuesta.ReturnResultdo(0, "ok", cliente);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch ListarPaises() error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener los paises", "problemas en el servidor, intente mas tarde");
            }
        }
        public object listaCiudadesXPais(string usuario, int idPais)
        {
            try
            {
                Logger.LogInformation($" es el usuario : {usuario} inicio el servicio listaCiudadesXPais() ");
                var ciudades = RepositoryPais.obtenerCiudadXpais(usuario, idPais);
                return Respuesta.ReturnResultdo(0, "ok", ciudades);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch listaCiudadesXPais() error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener los paises", "problemas en el servidor, intente mas tarde");
            }
        }
        public object obtenerListadeBajas(string usuario)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio obtenerListadeBajas() ");
                var bajas = Repository.tiposdeBajasClientes(usuario);
                return Respuesta.ReturnResultdo(0, "ok", bajas);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch obtenerListadeBajas() al obtener lista de bajas,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la lista de bajas para el cliente", "problemas en el servidor, intente mas tarde");
            }
        }
        public object obtenerBancoParaclientes(string usuario)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio obtenerBancoParaclientes() ");
                var bajas = Repository.listabancosParaClientes(usuario);
                return Respuesta.ReturnResultdo(0, "ok", bajas);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch obtenerBancoParaclientes() al obtener lista de bancos,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la lista de bancos para el cliente", "problemas en el servidor, intente mas tarde");
            }
        }
        public object obtenerNivelesCliente(string usuario)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio obtenerNivelesCliente() ");
                var niveles = Repository.listarNivelesClientes(usuario);
                return Respuesta.ReturnResultdo(0, "ok", niveles);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch obtenerNivelesCliente() al obtener lista de niveles,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la lista de niveles para el cliente", "problemas en el servidor, intente mas tarde");
            }
        }

        public object ActualizarFichaCliente(ClienteUpdateInputModel fichaClient)
        {
            try
            {
                Logger.LogInformation($"usuario : {fichaClient.usuarioNameLogueado} inicio el servicio ActualizarFichaCliente() ");
                if (fichaClient.tieneCuenta){                  
                    if (!fichaClient.cuentaBancaria.All(char.IsDigit))
                    return Respuesta.ReturnResultdo(1, "¡El número de cuanta bancaria  debe ser valor Numérico!", "");
                }
                var verificarData = Repository.ValidarRegistros(fichaClient);
                if (verificarData.Code == 1)
                {
                    Logger.LogWarning($"usuario : {fichaClient.usuarioNameLogueado} - fail parametro : {verificarData.Message}");
                    return verificarData;
                } else{
                    Logger.LogInformation($"usuario : {fichaClient.usuarioNameLogueado} - valido para actualiar data ");
                    var updateFicha = Repository.ActualizarFichaCliente(fichaClient);
                    if (updateFicha)
                    {
                        return Respuesta.ReturnResultdo(0, "Se Actualizo correctamente", "");
                    }
                    else
                    {
                        return Respuesta.ReturnResultdo(1, "Intente mas tarde- update", "");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"usuario : {fichaClient.usuarioNameLogueado} error catch ActualizarFichaCliente(),error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener actualizar la ficha del cliente", "problemas en el servidor, intente mas tarde");
            }
        }
        public object ObtenerTipoPagosXFreelancer(ClienteInputObtenerModel param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio ObtenerTipoPagosXFreelancer() ");
                var TipoPagosDisponibles = Repository.ObtenerTipoPagosXFreelancer(param);
                return Respuesta.ReturnResultdo(0, "listado de forma de pago", TipoPagosDisponibles);                              
            }
            catch (Exception ex)
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch ObtenerTipoPagosXFreelancer(),error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener el tipo de pago el freelancer", "problemas en el servidor, intente mas tarde");
            }
        }

    }
}
