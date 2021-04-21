import * as Types from '../types/loginTypes'


export const iniciarSesion= ()=>{
    return (dispatch)=>{                   
            dispatch({
            type: Types.LOAD_LOGIN,
            userName:'Maria G',
            })      
    }
  }

  export const cerrarSesion= ()=>{
    return (dispatch)=>{                   
            dispatch({
              type: Types.CLOSE_SESION
            })      
    }
  }
  