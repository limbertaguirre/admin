using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Modulo;
using gestion_de_comisiones.Modelos.Pagina;
using gestion_de_comisiones.Modelos.Permiso;
using gestion_de_comisiones.Modelos.Rol;
using gestion_de_comisiones.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios
{
    public class RolService
    {
        ConfiguracionService Respuesta = new ConfiguracionService();
        RolRepository rolRepository = new RolRepository();
        public object RegistraRol(RolRegisterInputModel data)
        {

            RolRepository rolRepo = new RolRepository();
               var objRolId = rolRepo.RegistrarRol(data.nombre, data.descripcion, data.idUsuario);
                if(objRolId >= 1) {
                foreach (var item in data.modulos) {
                    int idprueba = 0;
                    foreach(var itempagina in item.paginas)
                    {
                        int idpagina = itempagina.idPagina;
                        string nombrepagina = itempagina.nombrePagina;
                        if(idpagina > 0)
                        {                        
                            RolesPaginasRepository RepoRolPagina = new RolesPaginasRepository();
                            var objRolPaginaId = RepoRolPagina.AgregarRolPagina(true, objRolId, itempagina.idPagina, data.idUsuario);
                            //aqui hacer el forreach para add las cuentas
                            if (objRolPaginaId > 0)
                            {
                                foreach (var itemPermisos in itempagina.permisos)
                                {
                                    int idpermiso = itemPermisos.idPermiso;
                                    RolesPaginasPermisosRepository RepoPermisos = new RolesPaginasPermisosRepository();
                                    var objPermisos = RepoPermisos.AgregarRolPagina(true, objRolPaginaId, idpermiso, data.idUsuario);

                                }
                            }
                        }
                    }
                
                }

                var Result = new GenericDataJson<string> { Code = 0, Message = "SE REGISTRO CORRECTAMENTE" };
                return Result;

                }
                else
                {
                    var Result = new GenericDataJson<string> { Code = 1, Message = "El usuario se encuentra ya registrado" };
                    return Result;
                }                     
        }
        public object ObtenerMooduloPaginas()
        {
            ConfiguracionService Confi = new ConfiguracionService();
            ModuloRepository MoRepo = new ModuloRepository();
            PaginaRepository PaRepo = new PaginaRepository();
            PermisoRepository PerRepo = new PermisoRepository();
            List<ModuloResulModel> ListaModulos = new List<ModuloResulModel>();

           var modulos = MoRepo.ObtenerModulos();
            
            foreach(var modu in modulos)
            {
              //  List<PaginaModel> Lispaginas = new List<PaginaModel>();
               var Lispaginas = PaRepo.ObtenerPaginas(modu.IdModulo);               
                if(Lispaginas.Count > 0)
                {
                    //add modulo
                    ModuloResulModel newModulo = new ModuloResulModel();
                    newModulo.idModulo = modu.IdModulo;
                    newModulo.nombre = modu.Nombre;
                    List<PaginaResulModel> ListNewPaginas = new List<PaginaResulModel>();
                    foreach (var itempagina in Lispaginas)
                    {
                        PaginaResulModel newPagina = new PaginaResulModel();
                        newPagina.idPagina = itempagina.IdPagina;
                        newPagina.nombre = itempagina.Nombre;
                        ListNewPaginas.Add(newPagina);
                    }
                    //aqui add lis paginas
                    newModulo.listmodulos = ListNewPaginas;
                    //add a la lista general
                    ListaModulos.Add(newModulo);
                }
            }


            var resul = Confi.ReturnResultdo(0, "OK", ListaModulos);
            //var Result = new GenericDataJson<string> { Code = 0, Message = "SE REGISTRO CORRECTAMENTE" };
            return resul;

            
           
        }
        public object ObtenerPermiso()
        {
            ConfiguracionService Confi = new ConfiguracionService();
            PermisoRepository PerRepo = new PermisoRepository();
            var listaPermisos = PerRepo.obtenerPermisos();
            var resul = Confi.ReturnResultdo(0, "OK", listaPermisos);
            return resul;
        }
        public List<ModuloResulModel> FuncionObtenerMooduloPaginas()
        {
            ConfiguracionService Confi = new ConfiguracionService();
            ModuloRepository MoRepo = new ModuloRepository();
            PaginaRepository PaRepo = new PaginaRepository();
            PermisoRepository PerRepo = new PermisoRepository();
            List<ModuloResulModel> ListaModulos = new List<ModuloResulModel>();

            var modulos = MoRepo.ObtenerModulos();

            foreach (var modu in modulos)
            {
                //  List<PaginaModel> Lispaginas = new List<PaginaModel>();
                var Lispaginas = PaRepo.ObtenerPaginas(modu.IdModulo);
                if (Lispaginas.Count > 0)
                {
                    //add modulo
                    ModuloResulModel newModulo = new ModuloResulModel();
                    newModulo.idModulo = modu.IdModulo;
                    newModulo.nombre = modu.Nombre;
                    List<PaginaResulModel> ListNewPaginas = new List<PaginaResulModel>();
                    foreach (var itempagina in Lispaginas)
                    {
                        PaginaResulModel newPagina = new PaginaResulModel();
                        newPagina.idPagina = itempagina.IdPagina;
                        newPagina.nombre = itempagina.Nombre;
                        ListNewPaginas.Add(newPagina);
                    }
                    //aqui add lis paginas
                    newModulo.listmodulos = ListNewPaginas;
                    //add a la lista general
                    ListaModulos.Add(newModulo);
                }
            }
            return ListaModulos;

        }
        public object ObtenerListaRol(int idROl)
        {
            ConfiguracionService Confi = new ConfiguracionService();
            PermisoRepository PerRepo = new PermisoRepository();
            RolRepository rolRepo = new RolRepository();
            List<ModuloResulModel> ListaModulosAll = new List<ModuloResulModel>();

           
            //consultar
            var objRol = rolRepo.obtenerRolXId(idROl);
            if( objRol.IdRol > 0)
            {
                RolResulModel objNewRoleModulo = new RolResulModel();
                objNewRoleModulo.IdRol = objRol.IdRol;
                objNewRoleModulo.Nombre = objRol.Nombre;
                objNewRoleModulo.Descripcion = objRol.Descripcion;


                ListaModulosAll = FuncionObtenerMooduloPaginas();
                List<ModuloResulwithPermisoModel> NewListModulos = new List<ModuloResulwithPermisoModel>();
                foreach(var itemModulo in ListaModulosAll)
                {

                    ModuloResulwithPermisoModel objModulo = new ModuloResulwithPermisoModel();
                    objModulo.idModulo = itemModulo.idModulo;
                    objModulo.nombre = itemModulo.nombre;
                    List<PaginaResulModelWithPermisos> lisNewPaginas = new List<PaginaResulModelWithPermisos>();

                    foreach (var itemPagina in itemModulo.listmodulos)
                    {
                        PaginaResulModelWithPermisos objNewPagina = new PaginaResulModelWithPermisos();
                        objNewPagina.idPagina = itemPagina.idPagina;
                        objNewPagina.nombre = itemPagina.nombre;
                        List<RolPermiso> LisNewPermisos = new List<RolPermiso>();
                        //-------for aqui add permisos true false
                        List<PermisoResulModel> allPermiso = PerRepo.obtenerPermisos();
                        //aqui retornar si no tiene if
                        foreach(var itemPermiso in allPermiso)
                        {
                            RolPermiso objNewPermiso = new RolPermiso();
                            objNewPermiso.idPermiso = itemPermiso.idPermiso;
                            objNewPermiso.permiso1 = itemPermiso.permiso1;                        
                            var objPermiso = PerRepo.ObtenerPermisoPorROl(itemPagina.idPagina, itemPermiso.idPermiso, idROl);
                            if(objPermiso.idPermiso > 0)
                            {
                                objNewPermiso.estado = true;
                            }
                            else
                            {
                                objNewPermiso.estado = false;
                            }
                            LisNewPermisos.Add(objNewPermiso);
                        }
                        objNewPagina.permisos = LisNewPermisos;
                        lisNewPaginas.Add(objNewPagina);

                    }
                    objModulo.listmodulos = lisNewPaginas;
                    NewListModulos.Add(objModulo);
                }
                objNewRoleModulo.Modulos = NewListModulos;
                var resul = Confi.ReturnResultdo(0, "OK", objNewRoleModulo);
                return resul;
            }
            else
            {
                var resul = Confi.ReturnResultdo(1, "No exite rol", "");
                return resul;
            }

        }

        public List<ModuloResulwithPermisoModel> FuncionObtenerListaXRol(int idROl)
        {
            PermisoRepository PerRepo = new PermisoRepository();
            List<ModuloResulModel> ListaModulosAll = new List<ModuloResulModel>();
            ListaModulosAll = FuncionObtenerMooduloPaginas();

            List<ModuloResulwithPermisoModel> NewListModulos = new List<ModuloResulwithPermisoModel>();
            foreach (var itemModulo in ListaModulosAll)
            {
                ModuloResulwithPermisoModel objModulo = new ModuloResulwithPermisoModel();
                objModulo.idModulo = itemModulo.idModulo;
                objModulo.nombre = itemModulo.nombre;
                List<PaginaResulModelWithPermisos> lisNewPaginas = new List<PaginaResulModelWithPermisos>();
                foreach (var itemPagina in itemModulo.listmodulos)
                {
                    PaginaResulModelWithPermisos objNewPagina = new PaginaResulModelWithPermisos();
                    objNewPagina.idPagina = itemPagina.idPagina;
                    objNewPagina.nombre = itemPagina.nombre;
                    List<RolPermiso> LisNewPermisos = new List<RolPermiso>();
                    List<PermisoResulModel> allPermiso = PerRepo.obtenerPermisos();
                    foreach (var itemPermiso in allPermiso)
                    {
                        RolPermiso objNewPermiso = new RolPermiso();
                        objNewPermiso.idPermiso = itemPermiso.idPermiso;
                        objNewPermiso.permiso1 = itemPermiso.permiso1;
                        var objPermiso = PerRepo.ObtenerPermisoPorROl(itemPagina.idPagina, itemPermiso.idPermiso, idROl);
                        if (objPermiso.idPermiso > 0)
                        {
                            objNewPermiso.estado = true;
                        }
                        else
                        {
                            objNewPermiso.estado = false;
                        }
                        LisNewPermisos.Add(objNewPermiso);
                    }
                    objNewPagina.permisos = LisNewPermisos;
                    lisNewPaginas.Add(objNewPagina);

                }
                objModulo.listmodulos = lisNewPaginas;
                NewListModulos.Add(objModulo);
            }
            return NewListModulos;
        }
        public object ObtenerListaRolesWithModulos()
        {
            try {  
                RolRepository rolRepo = new RolRepository();
                var listRol = rolRepo.obtenerRolesAll();
                List<RolResulModel> ListNewRoleModulo = new List<RolResulModel>();
                foreach(var itemR in listRol)
                {
                    RolResulModel objNewRolModule = new RolResulModel();
                    objNewRolModule.IdRol = itemR.IdRol;
                    objNewRolModule.Nombre = itemR.Nombre;
                    objNewRolModule.Descripcion = itemR.Descripcion;
                    objNewRolModule.Habilitado = itemR.Habilitado;
                    var LModulos = this.FuncionObtenerListaXRol(itemR.IdRol);
                    objNewRolModule.Modulos = LModulos;
                    ListNewRoleModulo.Add(objNewRolModule);
                }
                return  Respuesta.ReturnResultdo(0, "OK", ListNewRoleModulo);
            }
            catch (Exception ex)
            {
                List<RolResulModel> Líst = new List<RolResulModel>();
                return Respuesta.ReturnResultdo(1, "OK", Líst);
            }
        }

        public object ActualizarRol(RolActualizarInputModel objRol)
        {
            try
            {
                //var respuesta = rolRepository;
                List<PaginaResulModelWithPermisos> paginas = funcionRecargarpaginas(objRol);
                var update = rolRepository.actualizarRoles(objRol.idRol, objRol.nombre, objRol.descripcion, paginas, objRol.idUsuario);
                if (update == true)
                {
                    return Respuesta.ReturnResultdo(0, "OK", "Se actualizo con exito su rold : " + objRol.nombre);
                }
                else
                {
                    return Respuesta.ReturnResultdo(1, "problemas en el servidor, intente mas tarde", "problemas en el servidor, intente mas tarde");
                }
              
            }
            catch (Exception ex)
            {
                List<RolResulModel> Líst = new List<RolResulModel>();
                return Respuesta.ReturnResultdo(1, "OK", "problemas en el servidor, intente mas tarde");
            }
        }
        public List<PaginaResulModelWithPermisos> funcionRecargarpaginas(RolActualizarInputModel objRol)
        {
            try
            {
                List<PaginaResulModelWithPermisos> newListPaginas = new List<PaginaResulModelWithPermisos>();
                List<PaginaResulModelWithPermisos> newListpagina = new List<PaginaResulModelWithPermisos>();

                //var respuesta = rolRepository;
                foreach (var obj in objRol.modulos)
                {
                   
                    foreach (var objPagina in obj.listmodulos)
                    {
                        PaginaResulModelWithPermisos newObjpag = new PaginaResulModelWithPermisos();
                        newObjpag.idPagina = objPagina.idPagina;
                        newObjpag.nombre = objPagina.nombre;
                        List<RolPermiso> newListPermisos = new List<RolPermiso>();
                        foreach (var objPer in objPagina.permisos)
                        {
                            RolPermiso objNewPermiso = new RolPermiso();
                            objNewPermiso.idPermiso = objPer.idPermiso;
                            objNewPermiso.permiso1 = objPer.permiso1;
                            objNewPermiso.estado = objPer.estado;
                            newListPermisos.Add(objNewPermiso);
                        }
                        newObjpag.permisos = newListPermisos;
                        newListpagina.Add(newObjpag);
                    }
                }
                return newListpagina;
            }
            catch (Exception ex)
            {
                List<PaginaResulModelWithPermisos> newListPaginas = new List<PaginaResulModelWithPermisos>();
                return newListPaginas;
            }

        }

    }
}
