import * as Types from '../types/loginTypes';
import * as TypesHome from '../types/homeTypes';
import {requestGet, requestPost} from '../../service/request';
import * as Action from './messageAction';





export const iniciarSesion= (userName,password)=>{
    return (dispatch)=>{     
        let body={
            userName:userName,
            password:password
        }
        requestPost('Login/Sesion',body,dispatch).then((res)=>{                   
            if(res.code === 0){
            var data= res.data;
                 dispatch({
                    type: TypesHome.MENU_PAGE,
                     menu:data.perfil.menus == null? [] : data.perfil.menus,
                     perfiles:data.perfil.listaHash ==null? [] : data.perfil.listaHash,
                })

                dispatch({
                    type: Types.LOAD_LOGIN,
                    userName:userName,
                    idUsuario:data.perfil.idUsuario,
                    nombre:data.perfil.nombre,
                    apellido:data.perfil.apellido,
                    token:data.token
                });
                localStorage.setItem("token", data.token);    
            }else if(res.code === 1){ 
                dispatch(Action.showMessage({ message: res.message, variant: "error" }));
            }else if(res.code === 2){
                dispatch({
                    type:Types.OPEN_MODAL_USER
                })  
            }               
        })              
    }
  }

  export const cerrarSesion= (history)=>{
    return (dispatch)=>{                   
            dispatch({
              type: Types.CLOSE_SESION
            });
            dispatch({
                type: TypesHome.MENU_PAGE_CLEAR
            })
            history.push("/"); 
            window.localStorage.clear();                          
    }
  }
  export const cerrarRegistroModal= (history)=>{
    return (dispatch)=>{                   
            dispatch({
              type: Types.CLOSE_MODAL_USER
            });
            window.localStorage.clear();                          
    }
  }

  export const cargarAreas= (history)=>{
    return (dispatch)=>{   
        requestGet('Configuracion/Areas',{},dispatch).then((res)=>{ 
            if(res.code === 0){
                dispatch({
                    type: Types.LISTA_AREAS,
                    areas:res.data
                })
            }
        })           
    }
  }
  export const cargarSucursales= (history)=>{
    return (dispatch)=>{    
        requestGet('Configuracion/Sucursales',{},dispatch).then((res)=>{ 
            if(res.code === 0){
                dispatch({
                    type:Types.LISTA_SUCURSALES,
                    sucursales:res.data
                })
           }
        })                        
    }
  }
  export const registrarUsuario= (usuarioName,nombre,apellido,telefono, corporativo,fechaNacimiento,area,sucursal)=>{
        let body={
            userName :usuarioName,
            nombre:nombre,
            apellido:apellido,
            telefono:parseInt(telefono), 
            corporativo:corporativo,
            fechaNacimiento:fechaNacimiento,
            area:parseInt(area),
            sucursal:parseInt(sucursal)
        };
    return (dispatch)=>{  
        requestPost('Usuario/Registro',body,dispatch).then((res)=>{ 
            if(res.code === 0){
                dispatch({
                    type: Types.CLOSE_MODAL_USER
                  });
                dispatch(Action.showMessage({ message: res.message, variant: "success" }));
            }else{
                dispatch(Action.showMessage({ message: res.message, variant: "error" }));
            }   
        })

    }
  }
  