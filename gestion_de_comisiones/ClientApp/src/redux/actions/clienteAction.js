import * as Types from '../types/clienteTypes'
import {requestGet, requestPost} from '../../service/request';
import * as Action from './messageAction';


  export const obtenerClientes= ()=>{
    return (dispatch, getState )=>{        
        
          
    const headers={usuarioLogin:getState().load.userName};
    requestGet('Cliente/ObtenerClientes',headers,dispatch).then((res)=>{ 
    //console.log('obtener clientes', res);
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
        //console.log('ObtenerCliente  Idclien:', idCliente); 
          const data={usuarioLogin:getState().load.userName, idCliente: idCliente };
          requestPost('Cliente/IdObtenerCliente',data,dispatch).then((res)=>{ 
           // console.log('ObtenerCliente : ', res);
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
   // console.log('paises : ', res);
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
        //console.log(' id pais ciudad :', idPais); 
          const data={usuarioLogin:getState().load.userName, idPais: idPais };
          requestPost('Cliente/ListarCiudadesPais',data,dispatch).then((res)=>{ 
            //console.log('ciudades : ', res);
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

  export const obtenerBajas= ()=>{
    return (dispatch, getState )=>{        
        
    const headers={usuarioLogin:getState().load.userName};
    requestGet('Cliente/ObtenerBajasClientes',headers,dispatch).then((res)=>{ 
    //console.log('obtener bajas', res);
        if(res.code === 0){  
            dispatch({
              type:Types.LISTA_BAJAS,
              listBajas:res.data,
            })      
                        
        }else{
            dispatch(Action.showMessage({ message: res.message, variant: "error" }));
        }    
        })   

    }
  }
  export const obtenerBancos= ()=>{
    return (dispatch, getState )=>{        
        
    const headers={usuarioLogin:getState().load.userName};
    requestGet('Cliente/obtenerBancosClientes',headers,dispatch).then((res)=>{ 
    //console.log('obtener bancos', res);
        if(res.code === 0){  
          dispatch({
            type:Types.LISTA_BANCOS,
            listBancos:res.data,
          })      
                        
        }else{
            dispatch(Action.showMessage({ message: res.message, variant: "error" }));
        }    
        })   

    }
  }

  export const InicializarCliente= ()=>{
    return (dispatch, getState )=>{        
        dispatch({
          type:Types.CLEAR_OBJETO_CLIENTE
        })  
    }
  }

  export const ActualizarCliente= (idCliente)=>{
    return (dispatch, getState )=>{                
          const data={
             usuarioNameLogueado:getState().load.userName,
             usuarioIDLogueado: getState().load.idUsuario,
             
             nuevoAvatar :false,
             avatar:'',
     
             codigo:2,
             nombre:"maria",
             apellido: "pedraza",
             ci:"5353535",
             telOficina:2222222,
             telMovil : 2,
             telFijo : 2,
             direccion :"moscu",
             
             idCiudad : 2,
             idPais :2,
             correoElectronico :"mm@maria",
             fechaNacimiento :'2021/05/02',
             codigoPatrocinador : "222",
             nombrePatrocinador :"patrocinador bb",
             idNivel :2,
             comentario :"no hay comentario",
     
             tieneCuenta: true,
             idBanco : 2,
             cuentaBancaria : "2352dd2335",
     
             tieneFactura : true,
             razonSocial : "gruposion",
             nit : "235345nit",
     
             tieneBaja : true,
             idFichaTipoBaja : 2,
             idTipoBaja : 2,
             fechaBaja : "2021/06/10",
             motivoBaja : "se le dio de baja",
           };
          requestPost('Cliente/ActualizarCliente',data,dispatch).then((res)=>{ 
           // console.log('ObtenerCliente : ', res);
                if(res.code === 0){  
                      
                               
                }else{
                    dispatch(Action.showMessage({ message: res.message, variant: "error" }));
                }    
              })   
    }
  }