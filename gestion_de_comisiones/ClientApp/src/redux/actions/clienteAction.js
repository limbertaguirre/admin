import * as Types from '../types/usuarioType'
import {requestGet, requestPost} from '../../service/request';
import * as Action from './messageAction';


  export const obtenerClientes= ()=>{
    return (dispatch, getState )=>{        
        
          
          const headers={usuarioLogin:getState().load.userName};
          requestGet('Cliente/ObtenerClientes',headers,dispatch).then((res)=>{ 
            console.log('obtener clientes', res);
                if(res.code === 0){                
                               
                }else{
                    dispatch(Action.showMessage({ message: res.message, variant: "error" }));
                }    
              })   

    }
  }
  export const buscarClientesXnombre= (criterio)=>{
    return (dispatch, getState )=>{        
        console.log('buscarcliente  clien'); 
          const headers={usuarioLogin:getState().load.userName, criterio: criterio };
          requestGet('Cliente/BuscarCliente',headers,dispatch).then((res)=>{ 
            console.log('buscarcliente', res);
                if(res.code === 0){                
                               
                }else{
                    dispatch(Action.showMessage({ message: res.message, variant: "error" }));
                }    
              })   

    }
  }


