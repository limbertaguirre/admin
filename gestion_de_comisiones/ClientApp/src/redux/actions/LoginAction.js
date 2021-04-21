import * as Types from '../types/loginTypes'


export const iniciarSesion= ()=>{
    return (dispatch)=>{                   
            dispatch({
            type: Types.LOAD_LOGIN,
            userName:'Maria G',
            })      
    }
  }
  