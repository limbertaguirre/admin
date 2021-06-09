import * as Types from '../types/usuarioType'
import {requestGet, requestPost} from '../../service/request';
import * as Action from './messageAction';


  export const obtenerClientes= ()=>{
    return (dispatch )=>{        
        
          console.log('obtener clientes');
          const headers={usuarioLogin:"USER_DEVELOP"};
          requestGet('Cliente/ObtenerClientes',headers,dispatch).then((res)=>{ 
                if(res.code === 0){                
                               
                }else{
                    dispatch(Action.showMessage({ message: res.message, variant: "error" }));
                }    
              })   

    }
  }

