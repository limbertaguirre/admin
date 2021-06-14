import * as Types from '../types/clienteTypes'
import {requestGet, requestPost} from '../../service/request';
import * as Action from './messageAction';


  export const obtenerClientes= ()=>{
    return (dispatch, getState )=>{        
        
          
    const headers={usuarioLogin:getState().load.userName};
    requestGet('Cliente/ObtenerClientes',headers,dispatch).then((res)=>{ 
    console.log('obtener clientes', res);
        if(res.code === 0){  
            dispatch({
                type:Types.LISTA_CLIENTES,
                listClientes:res.data,
            })              
                        
        }else{
            dispatch(Action.showMessage({ message: res.message, variant: "error" }));
        }    
        })   

    }
  }
  export const buscarClientesXnombre= (criterio)=>{
    return (dispatch, getState )=>{        
        console.log('buscarcliente  clien'); 
          const data={usuarioLogin:getState().load.userName, criterio: criterio };
          requestPost('Cliente/BuscarCliente',data,dispatch).then((res)=>{ 
            console.log('buscarcliente', res);
                if(res.code === 0){  
                    dispatch({
                        type:Types.BUSQUEDA_NOMBRE_CLIENTE,
                        listClientes:res.data,
                    })                    
                               
                }else{
                    dispatch(Action.showMessage({ message: res.message, variant: "error" }));
                }    
              })   
    }
  }
  export const obtenerClienteXId= (idCliente)=>{
    return (dispatch, getState )=>{        
        console.log('ObtenerCliente  Idclien:', idCliente); 
          const data={usuarioLogin:getState().load.userName, idCliente: idCliente };
          requestPost('Cliente/IdObtenerCliente',data,dispatch).then((res)=>{ 
            console.log('ObtenerCliente : ', res);
                if(res.code === 0){  
                  dispatch({
                    type:Types.OBJETO_CLIENTE,
                    objCliente:res.data
                  })         
                               
                }else{
                    dispatch(Action.showMessage({ message: res.message, variant: "error" }));
                }    
              })   
    }
  }

  export const listaPaises= ()=>{
    return (dispatch, getState )=>{              
    const headers={usuarioLogin:getState().load.userName};
    requestGet('Cliente/ListaPaises',headers,dispatch).then((res)=>{ 
    console.log('paises : ', res);
            if(res.code === 0){  
                dispatch({
                  type:Types.LISTA_PAISES,
                  listPaises:res.data
                })                        
            }else{
                dispatch(Action.showMessage({ message: res.message, variant: "error" }));
            }    
        })   
    }
  }
  export const obtenerCiudadesPorPais= (idPais)=>{
    return (dispatch, getState )=>{        
        console.log(' id pais ciudad :', idPais); 
          const data={usuarioLogin:getState().load.userName, idPais: idPais };
          requestPost('Cliente/ListarCiudadesPais',data,dispatch).then((res)=>{ 
            console.log('ciudades : ', res);
                if(res.code === 0){  
                  dispatch({
                    type:Types.LISTA_CIUDADES,
                    listCiudades:res.data
                  })     
                                                       
                }else{
                    dispatch(Action.showMessage({ message: res.message, variant: "error" }));
                }    
              })   
    }
  }