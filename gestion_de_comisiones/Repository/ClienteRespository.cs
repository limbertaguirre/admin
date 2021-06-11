﻿using gestion_de_comisiones.Modelos.Cliente;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class ClienteRespository : IClienteRepository
    {
        BDMultinivelContext contextMulti = new BDMultinivelContext();
        private readonly ILogger<ClienteRespository> Logger;

        public ClienteRespository(ILogger<ClienteRespository> logger)
        {
            Logger = logger;
        }

        public List<ClienteOutputModel> obtenerAllClientes(string usuario)
        {
            try
            {
                List<ClienteOutputModel> listCliente = new List<ClienteOutputModel>();
                Logger.LogInformation($" usuario: {usuario} inicio el obtenerRolesAll");
                var clientes = contextMulti.Fichas.Select(p => new ClienteModel(p.IdFicha, p.Codigo, p.Nombres, p.Apellidos, p.Ci, p.CorreoElectronico, p.FechaRegistro, p.TelOficina, p.TelMovil, p.TelFijo, p.Direccion, p.FechaNacimiento, p.Contrasena, p.Comentario, p.Avatar, p.TieneCuentaBancaria, p.IdBanco, p.CuentaBancaria, p.FacturaHabilitado, p.RazonSocial, p.Nit, p.Estado, p.IdCiudad, p.IdUsuario, p.FechaCreacion, p.FechaActualizacion)).ToList();

                foreach(var item in clientes)
                {
                    ClienteOutputModel objCliente = new ClienteOutputModel();                    
                    objCliente.idFicha = item.IdFicha;
                    objCliente.nombreCompleto = item.Nombres + ' ' + item.Apellidos;
                    objCliente.estado = item.Estado;
                    objCliente.tieneCuentaBancaria = item.TieneCuentaBancaria;
                    objCliente.cuentaBancaria = item.CuentaBancaria;
                    objCliente.avatar = item.Avatar;
                    objCliente.ci = item.Ci;
                    objCliente.codigo = item.Codigo;
                    if (item.IdBanco > 0 && item.IdBanco != null)
                    {
                        var objBanco = contextMulti.Bancoes.Where(x => x.IdBanco == item.IdBanco).Select(p => new { p.IdBanco, p.Nombre, p.Descripcion, p.Codigo }).FirstOrDefault();
                        if(objBanco != null)
                        {
                            objCliente.nombreBanco = objBanco.Nombre;
                            objCliente.idBanco = objBanco.IdBanco;
                            objCliente.codigoBanco = objBanco.Codigo;                           
                        }
                        else
                        {
                            objCliente.nombreBanco = "-------";
                            objCliente.idBanco = 0;
                            objCliente.codigoBanco = "";
                        }
                    }
                    else
                    {
                        objCliente.nombreBanco = "-------";
                        objCliente.idBanco = 0;
                        objCliente.codigoBanco = "";
                    }
                    listCliente.Add(objCliente);
                }
                return listCliente;
            }
            catch (Exception ex)
            {
                Logger.LogInformation($" usuario: {usuario} error catch repositorio cliente inicio el obtenerRolesAll { ex.Message }");
                List<ClienteOutputModel> listCliente = new List<ClienteOutputModel>();
                return listCliente;
            }
        }
        public List<ClienteOutputModel> buscarCliente(string usuario, string criterio)
        {
            try
            {
                List<ClienteOutputModel> listCliente = new List<ClienteOutputModel>();
                Logger.LogInformation($" usuario: {usuario} inicio el buscarCliente()riterio : {criterio}");                
                var clientes = contextMulti.Fichas.Where(x => x.Nombres.Contains(criterio)).Select(p => new ClienteModel(p.IdFicha, p.Codigo, p.Nombres, p.Apellidos, p.Ci, p.CorreoElectronico, p.FechaRegistro, p.TelOficina, p.TelMovil, p.TelFijo, p.Direccion, p.FechaNacimiento, p.Contrasena, p.Comentario, p.Avatar, p.TieneCuentaBancaria, p.IdBanco, p.CuentaBancaria, p.FacturaHabilitado, p.RazonSocial, p.Nit, p.Estado, p.IdCiudad, p.IdUsuario, p.FechaCreacion, p.FechaActualizacion)).ToList();

                foreach (var item in clientes)
                {
                    ClienteOutputModel objCliente = new ClienteOutputModel();
                    objCliente.idFicha = item.IdFicha;
                    objCliente.nombreCompleto = item.Nombres + ' ' + item.Apellidos;
                    objCliente.estado = item.Estado;
                    objCliente.tieneCuentaBancaria = item.TieneCuentaBancaria;
                    objCliente.cuentaBancaria = item.CuentaBancaria;
                    objCliente.avatar = item.Avatar;
                    objCliente.ci = item.Ci;
                    objCliente.codigo = item.Codigo;
                    if (item.IdBanco > 0 && item.IdBanco != null)
                    {
                        var objBanco = contextMulti.Bancoes.Where(x => x.IdBanco == item.IdBanco).Select(p => new { p.IdBanco, p.Nombre, p.Descripcion, p.Codigo }).FirstOrDefault();
                        if (objBanco != null)
                        {
                            objCliente.nombreBanco = objBanco.Nombre;
                            objCliente.idBanco = objBanco.IdBanco;
                            objCliente.codigoBanco = objBanco.Codigo;
                        }
                        else
                        {
                            objCliente.nombreBanco = "-------";
                            objCliente.idBanco = 0;
                            objCliente.codigoBanco = "";
                        }
                    }
                    else
                    {
                        objCliente.nombreBanco = "-------";
                        objCliente.idBanco = 0;
                        objCliente.codigoBanco = "";
                    }
                    listCliente.Add(objCliente);
                }
                return listCliente;
            }
            catch (Exception ex)
            {
                Logger.LogInformation($" usuario: {usuario} error catch repositorio  buscarCliente : { ex.Message }");
                List<ClienteOutputModel> listCliente = new List<ClienteOutputModel>();
                return listCliente;
            }
        }
        public FichaClienteOutPutModel obtenerClienteXID(string usuario, int idCliente)
        {
            try
            {
                FichaClienteOutPutModel objCliente = new FichaClienteOutPutModel();
                Logger.LogInformation($" usuario: {usuario} inicio el obtenerClienteXID() idcliente : {idCliente}");
                var objCli = contextMulti.Fichas.Where(x => x.IdFicha == idCliente).Select(p => new ClienteModel(p.IdFicha, p.Codigo, p.Nombres, p.Apellidos, p.Ci, p.CorreoElectronico, p.FechaRegistro, p.TelOficina, p.TelMovil, p.TelFijo, p.Direccion, p.FechaNacimiento, p.Contrasena, p.Comentario, p.Avatar, p.TieneCuentaBancaria, p.IdBanco, p.CuentaBancaria, p.FacturaHabilitado, p.RazonSocial, p.Nit, p.Estado, p.IdCiudad, p.IdUsuario, p.FechaCreacion, p.FechaActualizacion)).FirstOrDefault();

                if (objCli != null)
                {                    
                    objCliente.idFicha = objCli.IdFicha;
                    objCliente.nombreCompleto = objCli.Nombres + ' ' + objCli.Apellidos;
                    objCliente.avatar = objCli.Avatar;
                    //string direccion = objCli.Direccion;
                    //int idciudad = objCli.ci
                    objCliente.estado = objCli.Estado;
                    objCliente.tieneCuentaBancaria = objCli.TieneCuentaBancaria;
                    objCliente.cuentaBancaria = objCli.CuentaBancaria;

                    
                    objCliente.ci = objCli.Ci;
                    objCliente.codigo = objCli.Codigo;
                    if (objCli.IdBanco > 0 && objCli.IdBanco != null)
                    {
                        var objBanco = contextMulti.Bancoes.Where(x => x.IdBanco == objCli.IdBanco).Select(p => new { p.IdBanco, p.Nombre, p.Descripcion, p.Codigo }).FirstOrDefault();
                        if (objBanco != null)
                        {
                            objCliente.nombreBanco = objBanco.Nombre;
                            objCliente.idBanco = objBanco.IdBanco;
                            objCliente.codigoBanco = objBanco.Codigo;
                        }
                        else
                        {
                            objCliente.nombreBanco = "";
                            objCliente.idBanco = 0;
                            objCliente.codigoBanco = "";
                        }
                    }
                    else
                    {
                        objCliente.nombreBanco = "";
                        objCliente.idBanco = 0;
                        objCliente.codigoBanco = "";
                    }
                   
                }
                return objCliente;
            }
            catch (Exception ex)
            {
                Logger.LogInformation($" usuario: {usuario} error catch repositorio  obtenerClienteXID : { ex.Message }");
                FichaClienteOutPutModel objCliente = new FichaClienteOutPutModel();
                return objCliente;
            }
        }


    }
}



