using gestion_de_comisiones.Modelos.Cliente;
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
                    objCliente.nombre= objCli.Nombres;
                    objCliente.apellido = objCli.Apellidos;
                    objCliente.avatar = objCli.Avatar;
                    objCliente.Contrasena = objCli.Contrasena;
                    objCliente.estado = objCli.Estado;
                    objCliente.tieneCuentaBancaria = objCli.TieneCuentaBancaria;
                    objCliente.cuentaBancaria = objCli.CuentaBancaria;
                    
                    objCliente.ci = objCli.Ci;
                    objCliente.codigo = objCli.Codigo;
                    objCliente.CorreoElectronico = objCli.CorreoElectronico;
                    objCliente.FechaNacimiento = objCli.FechaNacimiento;
                    objCliente.FechaRegistro = objCli.FechaRegistro;

                    objCliente.TelOficina = objCli.TelOficina;
                    objCliente.TelFijo = objCli.TelFijo;
                    objCliente.TelMovil = objCli.TelMovil;
                    objCliente.Direccion = objCli.Direccion;

                    objCliente.Comentario = objCli.Comentario;
                    objCliente.RazonSocial = objCli.RazonSocial;
                    objCliente.Nit = objCli.Nit;
                    objCliente.FacturaHabilitado = objCli.FacturaHabilitado;


                    

                    //---------------------------------------------
                    var objCiudad = contextMulti.Ciudads.Where(x => x.IdCiudad == objCli.IdCiudad).Select(p => new { p.IdCiudad, p.Nombre, p.IdPais }).FirstOrDefault();
                    if(objCiudad != null)
                    {
                        objCliente.idCiudad = objCiudad.IdCiudad;
                        objCliente.idPais = (int)objCiudad.IdPais;
                    }
                    //-------------------------------------------------------------
                    var objNivel = contextMulti.FichaNivelIs.Join(contextMulti.Nivels,
                                                  FichaNivelI => FichaNivelI.IdNivel,
                                                  Nivel => Nivel.IdNivel,
                                                  (FichaNivelI, Nivel) => new
                                                  {
                                                      idNivelDetalle=FichaNivelI.IdFichaNivelI,
                                                      idNivel = Nivel.IdNivel,
                                                      nombre = Nivel.Nombre,
                                                      idFicha= FichaNivelI.IdFicha,
                                                      fechaCreacion= FichaNivelI.FechaCreacion,
                                                      habilitado=FichaNivelI.Habilitado,
                                                  }).Where(x => x.idFicha == objCli.IdFicha && x.habilitado == true ).OrderByDescending( x=> x.fechaCreacion).FirstOrDefault();
                    if (objNivel != null)
                    {
                        objCliente.idNivelDetalle = objNivel.idNivelDetalle;
                        objCliente.nivel = objNivel.nombre;
                        objCliente.idNivel = objNivel.idNivel;
                    }
                    else
                    {
                        objCliente.idNivelDetalle = 0;
                        objCliente.nivel = "";
                        objCliente.idNivel = 0;
                    }
                    //--------------------------------------------------
                    var patrocinad = contextMulti.GpClienteVendedorIs.Join(contextMulti.Fichas,
                                                   GpClienteVendedorI => GpClienteVendedorI.IdVendedor,
                                                  Ficha => Ficha.IdFicha,
                                                   (GpClienteVendedorI, Ficha) => new 
                                                   {
                                                       idVendedor = Ficha.IdFicha,
                                                       idcliente= GpClienteVendedorI.IdCliente,
                                                       nombreVendedor = Ficha.Nombres + Ficha.Apellidos,
                                                       codigoVendedor= Ficha.Codigo,
                                                   }).Where(x =>  x.idcliente == objCli.IdFicha).FirstOrDefault();
                    if(patrocinad != null)
                    {
                        objCliente.codigoPatrocinador = patrocinad.codigoVendedor;
                        objCliente.nombrePatrocinador = patrocinad.nombreVendedor;
                    }else
                    {
                        objCliente.codigoPatrocinador = "";
                        objCliente.nombrePatrocinador = "";
                    }
                    //----------------------------------------------------------------------------------
                    var objBaja = contextMulti.FichaTipoBajaIs.Where(x => x.IdFicha == objCli.IdFicha && x.Estado == true).Select(p => new { p.IdFichaTipoBajaI, p.IdFicha, p.IdTipoBaja, p.Motivo, p.FechaBaja }).FirstOrDefault(); 
                    if(objBaja != null)
                    {
                        objCliente.idFichaTipoBajaDetalle = objBaja.IdFichaTipoBajaI;
                        objCliente.idTipoBaja = (int)objBaja.IdTipoBaja;
                        objCliente.FechaBaja = (DateTime)objBaja.FechaBaja;
                        objCliente.motivoBaja = objBaja.Motivo;
                    } else
                    {
                        objCliente.idFichaTipoBajaDetalle = 0;
                        objCliente.idTipoBaja = 0;
                        objCliente.FechaBaja = DateTime.Now;
                        objCliente.motivoBaja = "";
                    }
                    //-------------------------------------------------------------------------------------

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
        public object tiposdeBajasClientes(string usuario)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario} inicio el tiposdeBajasClientes");
                var tipoBajas = contextMulti.TipoBajas.Select(p => new {p.IdTipoBaja, p.Nombre, p.Descripcion }).ToList();
                return tipoBajas;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch tiposdeBajasClientes() mensaje : {ex}");
                List<TipoBaja> lis = new List<TipoBaja>();
                return lis;
            }
        }
        public object listabancosParaClientes(string usuario)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario} inicio el listabancosParaClientes");
                var tipoBajas = contextMulti.Bancoes.Select(p => new { p.IdBanco, p.Nombre, p.Codigo }).ToList();
                return tipoBajas;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch listabancosParaClientes() mensaje : {ex}");
                List<Banco> lis = new List<Banco>();
                return lis;
            }
        }
        public object listarNivelesClientes(string usuario)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario} inicio el listarNivelesClientes() repository");
                var niveles = contextMulti.Nivels.Select(p => new { p.IdNivel, p.Nombre }).ToList();
                return niveles;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch listarNivelesClientes() mensaje : {ex}");
                List<Nivel> lis = new List<Nivel>();
                return lis;
            }
        }


    }
}



