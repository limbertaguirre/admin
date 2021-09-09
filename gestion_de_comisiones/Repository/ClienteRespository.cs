
using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Cliente;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Servicios;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class ClienteRespository : IClienteRepository
    {
        ConfiguracionService Respuesta = new ConfiguracionService();
        BDMultinivelContext contextMulti = new BDMultinivelContext();
        private readonly ILogger<ClienteRespository> Logger;
        private readonly IHostingEnvironment EEnv;
        public ClienteRespository(ILogger<ClienteRespository> logger, IHostingEnvironment env)
        {
            Logger = logger;
            EEnv = env;
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
                var cliente = contextMulti.VwObtenerFichas.Where(x => x.Ci.Contains(criterio)).Select(c => new ClienteOutputModel(c.IdFicha, c.Codigo, c.NombreCompleto, c.Ci, c.TieneCuentaBancaria, c.IdBanco, c.NombreBanco, c.CodigoBanco, c.CuentaBancaria, c.Estado, c.Avatar, c.Nivel)).ToList();
                return cliente;
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
                                                  Ficha => Ficha.Codigo,
                                                   (GpClienteVendedorI, Ficha) => new 
                                                   {
                                                       idVendedor = Ficha.IdFicha,
                                                       idcliente= GpClienteVendedorI.IdCliente,
                                                       nombreVendedor = Ficha.Nombres+ " " + Ficha.Apellidos,
                                                       codigoVendedor= Ficha.Codigo,
                                                   }).Where(x =>  x.idcliente == objCli.Codigo).FirstOrDefault();
                    if(patrocinad != null)
                    {
                        objCliente.codigoPatrocinador = patrocinad.codigoVendedor;
                        objCliente.nombrePatrocinador = patrocinad.nombreVendedor;
                    }else
                    {
                        objCliente.codigoPatrocinador = 0;
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

        public Result<string> ValidarRegistros(ClienteUpdateInputModel ficha)
        {
           

            try
            {
                var oldPatrocinador = contextMulti.GpClienteVendedorIs.Join(contextMulti.Fichas,
                                            GpClienteVendedorI => GpClienteVendedorI.IdVendedor,Ficha => Ficha.IdFicha,
                                            (GpClienteVendedorI, Ficha) => new
                                            {
                                                idVendedor = Ficha.IdFicha,
                                                idcliente = GpClienteVendedorI.IdCliente,
                                                nombreVendedor = Ficha.Nombres + Ficha.Apellidos,
                                                codigoVendedor = Ficha.Codigo,
                                            }).Where(x => x.idcliente == ficha.idFicha).FirstOrDefault();

                if(oldPatrocinador != null)
                {
                    if(oldPatrocinador.codigoVendedor != ficha.codigoPatrocinador)
                    {
                        var vendedorExite = contextMulti.Fichas.Where(x => x.Codigo == ficha.codigoPatrocinador).Select(p => new ClienteModel(p.IdFicha, p.Codigo, p.Nombres, p.Apellidos, p.Ci, p.CorreoElectronico, p.FechaRegistro, p.TelOficina, p.TelMovil, p.TelFijo, p.Direccion, p.FechaNacimiento, p.Contrasena, p.Comentario, p.Avatar, p.TieneCuentaBancaria, p.IdBanco, p.CuentaBancaria, p.FacturaHabilitado, p.RazonSocial, p.Nit, p.Estado, p.IdCiudad, p.IdUsuario, p.FechaCreacion, p.FechaActualizacion)).FirstOrDefault();

                        if(vendedorExite == null)
                        {                          
                            return Respuesta.ReturnResultdo(1, "No existe el codigo del patrocinador que quiere registrar", "");
                        }
                    }

                }
                if (ficha.tieneCuenta == true && ficha.idBanco== 0)
                {
                    return Respuesta.ReturnResultdo(1, "Debe seleccionar un Banco para habilitar la cuenta", "");
                }
                if(ficha.tieneBaja == true && ficha.idTipoBaja == 0)
                {
                    return Respuesta.ReturnResultdo(1, "Debe seleccionar un tipo de baja, para poder dar de baja", "");
                }
                if(ficha.tieneFactura == true )
                {
                    if (ficha.razonSocial == "")
                    {
                        return Respuesta.ReturnResultdo(1, "Razon social requerido ", "");
                    }
                    if (ficha.nit == "")
                    {
                        return Respuesta.ReturnResultdo(1, "Nit requerido ", "");
                    }

                }
                if(ficha.idCiudad == 0)
                {
                    return Respuesta.ReturnResultdo(1, "La ciudad es requerida ", "");
                }


                return Respuesta.ReturnResultdo(0, "Valido para pagar", "");


            }
            catch (Exception ex)
            {
                    return Respuesta.ReturnResultdo(1, "error al intente mas tarde", "");
            }
              
        }
   
        public bool ActualizarFichaCliente(ClienteUpdateInputModel ficha)
        {
            Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  inicio el ActualizarFichaCliente() en repos");
            using (BDMultinivelContext context = new BDMultinivelContext())
            {
                using (var dbcontextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var objCli = context.Fichas.Where(x => x.IdFicha == ficha.idFicha).FirstOrDefault();

                        if (objCli != null)
                        {
                            Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} - VERIFICAMOS BAJA!!");
                            if (ficha.tieneBaja == true)// se bloqueara o se verifica si sigue bloqueado
                            {
                                Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  se dara baja a usuario a usuario idficha:{ficha.idFicha} - nombre:{ficha.nombre}");
                                if (ficha.idFichaTipoBaja != 0)//se verifica si ya estuvo bloqueado o es nuevo
                                {
                                    var oldBaja = context.FichaTipoBajaIs.Where(x => x.IdFichaTipoBajaI == ficha.idFichaTipoBaja).FirstOrDefault();
                                    if(oldBaja != null)
                                    {
                                        Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  ya tenia una baja el cliente :{ficha.idFicha + " nombre "+ ficha.nombre }, baja:{oldBaja.IdTipoBaja +", motivo : "+oldBaja.Motivo}");
                                        Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  baja q esta agregando a ificha{ficha.idFicha}, tipobaja:{ficha.idTipoBaja}, motivo:{ficha.motivoBaja}");
                                        oldBaja.IdTipoBaja = ficha.idTipoBaja;
                                        oldBaja.Motivo = ficha.motivoBaja;
                                        oldBaja.Estado = true;
                                        oldBaja.FechaBaja = Convert.ToDateTime(ficha.fechaBaja);
                                        context.SaveChanges();
                                        objCli.Estado = 0; //es igual a false 
                                    }
                                }
                                else // se registra un nuevo bloqueo
                                {
                                    Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} - se le agregara una nueva baja ala idficha: {ficha.idFicha+" - "+ficha.nombre}, tipo baja: {ficha.idTipoBaja}, motivo: {ficha.motivoBaja}");
                                    FichaTipoBajaI newBaja = new FichaTipoBajaI();
                                    newBaja.IdTipoBaja = ficha.idTipoBaja;
                                    newBaja.FechaBaja= Convert.ToDateTime(ficha.fechaBaja);
                                    newBaja.Motivo = ficha.motivoBaja;
                                    newBaja.Estado = true;
                                    newBaja.FechaActualizacion = DateTime.Now;
                                    newBaja.IdFicha = ficha.idFicha;
                                    newBaja.IdUsuario = ficha.usuarioIDLogueado;
                                    context.FichaTipoBajaIs.Add(newBaja);
                                    context.SaveChanges();
                                    objCli.Estado = 0;
                                }
                            }
                            else
                            {//posiblemente se le alta  o nunca estuvo bloqueado
                                Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  se verifica para dar de alta a la ficha: {ficha.idFicha+"- "+ ficha.nombre+" "+ficha.apellido}");
                                if (ficha.idFichaTipoBaja != 0)
                                {
                                    var oldBaj = context.FichaTipoBajaIs.Where(x => x.IdFichaTipoBajaI == ficha.idFichaTipoBaja).FirstOrDefault();
                                    if (oldBaj != null)
                                    {
                                        Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} - la ficha tenia baja se le dara de alta");
                                        oldBaj.Estado = false;
                                        oldBaj.FechaActualizacion = DateTime.Now;
                                        oldBaj.IdUsuario = ficha.usuarioIDLogueado;
                                        context.SaveChanges();
                                        objCli.Estado = 1;
                                    }
                                }
                                else
                                {
                                    Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  no tenia baja, se lo mantiene como activo ");
                                    objCli.Estado = 1;//se activa al cliente
                                }
                            }
                            
                            Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} - VERFICAMOS LA CUENTA!!");
                            if (ficha.tieneCuenta)
                            {
                                Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  DATOS: antes de actualizar de idficha: {ficha.idFicha}, idBanco:{objCli.IdBanco}, nro cuenta:{objCli.CuentaBancaria}");
                                Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -CUENTA?  tiene la cuenta checkeado, banco que selecciono idbanco:{ficha.idBanco}, nro cuenta: {ficha.cuentaBancaria}");                                
                                objCli.IdBanco = ficha.idBanco;
                                objCli.CuentaBancaria = ficha.cuentaBancaria;
                                objCli.TieneCuentaBancaria = ficha.tieneCuenta;
                            }
                            else
                            {  //al no tener cuenta se lo reinicia en 0
                                Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  se desahbilitara la  cuenta, antes de actualizar de idficha: {ficha.idFicha}, idBanco:{objCli.IdBanco}, nro cuenta:{objCli.CuentaBancaria}");
                                Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  se quitara el banco y el numero de cuenta del la idficha:{ficha.idFicha + ", " + ficha.nombre + " " + ficha.apellido}");
                                objCli.IdBanco = 0;
                                objCli.CuentaBancaria = "";
                                objCli.TieneCuentaBancaria = ficha.tieneCuenta;
                            }
                            Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  VERIFICAMOS FACTURA!!....");
                            if (ficha.tieneFactura)
                            {
                                Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  se habilitara la factura,  idficha: {ficha.idFicha + "  " + ficha.nombre + " " + ficha.apellido}");
                                Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  DATOS: antes de actualizar la factura, razon social:{objCli.RazonSocial}, nit: {objCli.Nit}, estado de factura:{objCli.FacturaHabilitado}");
                                Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  se habilitar factura nuevos parametros razon social :{ficha.razonSocial}, nit: {ficha.nit}");
                                objCli.FacturaHabilitado = ficha.tieneFactura;
                                objCli.RazonSocial = ficha.razonSocial;
                                objCli.Nit = ficha.nit;
                            }
                            else
                            {
                                Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  se deshabilitado la factura  la idficha: {ficha.idFicha + "  " + ficha.nombre + " " + ficha.apellido}");
                                Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  DATOS:  antes de dar de baja la facturacion del cliente:{ficha.nombre}, razonsocial:{objCli.RazonSocial}, nit:{objCli.Nit}");
                                objCli.FacturaHabilitado = ficha.tieneFactura;
                                objCli.RazonSocial = "";
                                objCli.Nit = "";
                            }

                            Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  VERIFICAMOS PAtROCINADOR!!....");
                            var patrocinador = context.GpClienteVendedorIs.Join(context.Fichas,
                                                   GpClienteVendedorI => GpClienteVendedorI.IdVendedor,
                                                  Ficha => Ficha.IdFicha,
                                                   (GpClienteVendedorI, Ficha) => new
                                                   {
                                                       idVendedor = Ficha.IdFicha,
                                                       idcliente = GpClienteVendedorI.IdCliente,
                                                       nombreVendedor = Ficha.Nombres + Ficha.Apellidos,
                                                       codigoVendedor = Ficha.Codigo,
                                                   }).Where(x => x.idcliente == ficha.idFicha).FirstOrDefault();
                            if (patrocinador != null && ficha.codigoPatrocinador != 0)
                            {
                                if (patrocinador.codigoVendedor != ficha.codigoPatrocinador)
                                {
                                    Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  se agregara un nuevo patrocinador a {ficha.nombre} ");
                                    var newVendedorPatrocinador = context.Fichas.Where(x => x.Codigo == ficha.codigoPatrocinador).FirstOrDefault();
                                    if (newVendedorPatrocinador != null)
                                    {
                                        var detalleVendedor = context.GpClienteVendedorIs.Where(x => x.IdCliente == ficha.idFicha && x.IdVendedor == patrocinador.idVendedor && x.Activo == true).FirstOrDefault();
                                        if (detalleVendedor != null)
                                        {
                                            Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} - Actualizando un nuevo patrocinador codigo: {ficha.codigoPatrocinador}");
                                            var vende = context.GpClienteVendedorIs.Where(x => x.Id == detalleVendedor.Id).FirstOrDefault();
                                            vende.IdVendedor = newVendedorPatrocinador.IdFicha;
                                            vende.FechaActualizacion = DateTime.Now;
                                            context.SaveChanges();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (patrocinador == null && ficha.codigoPatrocinador != 0)
                                {
                                    var newPatrocinador = contextMulti.Fichas.Where(x => x.Codigo == ficha.codigoPatrocinador).Select(p => new ClienteModel(p.IdFicha, p.Codigo, p.Nombres, p.Apellidos, p.Ci, p.CorreoElectronico, p.FechaRegistro, p.TelOficina, p.TelMovil, p.TelFijo, p.Direccion, p.FechaNacimiento, p.Contrasena, p.Comentario, p.Avatar, p.TieneCuentaBancaria, p.IdBanco, p.CuentaBancaria, p.FacturaHabilitado, p.RazonSocial, p.Nit, p.Estado, p.IdCiudad, p.IdUsuario, p.FechaCreacion, p.FechaActualizacion)).FirstOrDefault();
                                    if (newPatrocinador != null)
                                    {
                                        Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} - agregando un nuevo patrocinador id:{newPatrocinador.IdFicha}, codigo: {ficha.codigoPatrocinador}, {newPatrocinador.Nombres +" " +newPatrocinador.Apellidos } ");
                                        GpClienteVendedorI ven = new GpClienteVendedorI();
                                        ven.IdCliente = ficha.idFicha;
                                        ven.IdVendedor = newPatrocinador.IdFicha;
                                        ven.FechaActivacion = DateTime.Now;
                                        ven.Activo = true;
                                        ven.IdUsuario = ficha.usuarioIDLogueado;
                                        ven.FechaCreacion = DateTime.Now;
                                        ven.FechaActualizacion = DateTime.Now;
                                        context.GpClienteVendedorIs.Add(ven);
                                        context.SaveChanges();

                                    }

                                }

                            }

                            Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  VERIFICAMOS EL RANGO  NIVEL DE LA FICHA!!....");
                            if (ficha.idNivelDetalle > 0)
                            {
                                var objNivel = context.FichaNivelIs.Where(x => x.IdFichaNivelI == ficha.idNivelDetalle).FirstOrDefault();
                                if(objNivel != null)
                                {
                                    if(objNivel.IdNivel != ficha.idNivel)
                                    {                                        
                                        if(ficha.idNivel == 0)
                                        {
                                            Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  se dio de baja el  rango de nivel que tenia, idNivelDetalle :{objNivel.IdFichaNivelI}");
                                            objNivel.Habilitado = false;
                                        }else{
                                            Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  se actualizo el rango nivel del cliente a idNivel:{ficha.idNivel} ");
                                            objNivel.IdNivel = ficha.idNivel;
                                            objNivel.Habilitado = true;
                                        }                                       
                                        objNivel.FechaActualizacion = DateTime.Now;
                                        context.SaveChanges();
                                    }
                                }
                            }
                            else
                            {
                                if(ficha.idNivel != 0)
                                {
                                    Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  se le dio un nuevo rango al cliente,  idnivel :{ficha.idNivel}");
                                    FichaNivelI newNivel = new FichaNivelI();
                                    newNivel.IdFicha = ficha.idFicha;
                                    newNivel.IdNivel = ficha.idNivel;
                                    newNivel.Habilitado = true;
                                    newNivel.IdUsuario = ficha.usuarioIDLogueado;
                                    context.FichaNivelIs.Add(newNivel);
                                    context.SaveChanges();
                                }
                            }
                            //------------------------------------------
                            if (ficha.nuevoAvatar)
                            {
                                string contentRootPath = EEnv.ContentRootPath + "\\Avatars";
                                if (!System.IO.Directory.Exists(contentRootPath))
                                {
                                    System.IO.Directory.CreateDirectory(contentRootPath);
                                }
                                string basess = ficha.avatar.Substring(23);
                                string nombreImage = ficha.nombre + DateTime.Now.Year.ToString() + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Minute + DateTime.Now.Second + ".jpg";
                                System.IO.File.WriteAllBytes(Path.Combine(contentRootPath, nombreImage), Convert.FromBase64String(ficha.avatar.Substring(23)));
                                objCli.Avatar =  nombreImage;
                            }

                            //---------------------------------------------------------
                            objCli.Nombres = ficha.nombre;
                            objCli.Apellidos = ficha.apellido;
                            objCli.Ci = ficha.ci;
                            objCli.TelOficina = ficha.telOficina.ToString();
                            objCli.TelMovil = ficha.telMovil.ToString();
                            objCli.TelFijo = ficha.telFijo.ToString();
                            objCli.Direccion = ficha.direccion;

                            objCli.IdCiudad = ficha.idCiudad;
                            objCli.CorreoElectronico = ficha.correoElectronico;
                            objCli.FechaNacimiento = Convert.ToDateTime(ficha.fechaNacimiento);
                            objCli.Comentario = ficha.comentario;
                                                    
                            context.SaveChanges();
                            dbcontextTransaction.Commit();
                            Logger.LogInformation($" usuario: {ficha.usuarioNameLogueado} -  SE ACTUALIZO EXITOSAMENTE los parametros de la ficha del cliente :{ficha.nombre+" "+ ficha.apellido}");
                            return true;
                        }
                        else
                        {
                            Logger.LogWarning($" usuario: {ficha.usuarioNameLogueado} - RETURN!! no se encontro la ficha  idficha {ficha.idFicha}");
                            dbcontextTransaction.Rollback();
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogWarning($" usuario: {ficha.usuarioNameLogueado} -catch al actualizar el cliente  {ex.Message}");
                        dbcontextTransaction.Rollback();
                        return false;
                    }
                }
            }
        }
  
    }
}



