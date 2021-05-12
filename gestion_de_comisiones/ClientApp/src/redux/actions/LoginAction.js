import * as Types from '../types/loginTypes'
import * as TypesUsuario from '../types/usuarioType'
import {requestPost} from '../../service/request';
import * as Action from './messageAction';

const listaAreas=[
    {
        id_area:1,
        nombre:'DESARROLLO'
    },
    {
        id_area:2,
        nombre:'CONTABILIDAD'
    },
    {
        id_area:3,
        nombre:'FINANZAS'
    },
    {
        id_area:4,
        nombre:'UIT'
    }
];

const listaSucursales=[
    {
        id_sucursal:1,
        nombre:'CANHOTO'
    },
    {
        id_sucursal:2,
        nombre:'AMBASADOR'
    },
    {
        id_sucursal:3,
        nombre:'IRALA'
    },
    {
        id_sucursal:4,
        nombre:'SAN MARTIN'
    }
];

export const iniciarSesion= (userName,password)=>{
    return (dispatch)=>{     
        let body={
            userName:userName,
            password:password
        }
        requestPost('Login/Sesion',body,dispatch).then((res)=>{ 
            console.log("respuesta :", res);           
            if(res.code === 0){
                dispatch({
                    type: Types.LOAD_LOGIN,
                    userName:userName,
                })   

            }else if(res.code === 1){ 
                dispatch(Action.showMessage({ message: res.message, variant: "error" }));
            }else if(res.code === 2){
                dispatch({
                    type:Types.OPEN_MODAL_USER
                })
                console.log("aqui se registrar el usuario abrir modal");    
                dispatch(Action.showMessage({ message: res.message, variant: "error" }));
            }               
        })              
    }
  }

  export const cerrarSesion= (history)=>{
    return (dispatch)=>{                   
            dispatch({
              type: Types.CLOSE_SESION
            });
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
            dispatch({
                type: Types.LISTA_AREAS,
                areas:listaAreas
            })
        
    }
  }
  export const cargarSucursales= (history)=>{
    return (dispatch)=>{                   
            dispatch({
                type:Types.LISTA_SUCURSALES,
                sucursales:listaSucursales
            })
    }
  }
  