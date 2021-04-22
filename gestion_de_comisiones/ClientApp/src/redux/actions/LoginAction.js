import * as Types from '../types/loginTypes'
import {requestPost} from '../../service/request';
import * as Action from './messageAction'

export const iniciarSesion= (userName,password)=>{
    return (dispatch)=>{     
        let body={
            userName:userName,
            password:password
        }
        requestPost('Login/Sesion',body,dispatch).then((res)=>{            
            if(res.code === 0){
                dispatch({
                    type: Types.LOAD_LOGIN,
                    userName:userName,
                })   
            }else{
                Action.showMessage({ message: "Intente mas tarde", variant: "error" })
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
  