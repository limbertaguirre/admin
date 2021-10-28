import {requestGet, requestPost} from '../../service/request';
import * as Action from './messageAction';


export async function ObtenerCiclosPagos(userName, dispatch) {
    return new Promise(resolve =>{
        const data={
            usuarioLogin:userName,
          };      
      requestGet('gestionPagos/GetCiclos', data, dispatch )
        .then(response =>{
           // console.log('ger list :', response);
          resolve(response);  
        })
       
    });
  }

  export async function ObtenerComisionesPagos(userName,cicloId, dispatch) {
    return new Promise(resolve =>{
        const data={
              usuarioLogin:userName,
              idCiclo:cicloId
          };  
      console.log('parametros :', data);
      requestPost('gestionPagos/GetComisionesPagos', data, dispatch )
        .then(response =>{
             resolve(response);  
        })
       
    });
  }

  export async function getFiltroFormaPagoDisponibles(userName,idCiclo, dispatch) {
    return new Promise(resolve =>{
          const data={
            usuarioLogin:userName,
            idCiclo:idCiclo
          };              
         requestPost('gestionPagos/GetFiltroFormaPagosDisponibles', data, dispatch )
        .then(response =>{
           // console.log('respuesta api :', response);
            resolve(response);  
        }) 
    });
  }
  export async function ListarFiltrada(userName,idCiclo,idTipoPago, dispatch) {
    return new Promise(resolve =>{
        const data={
              usuarioLogin:userName,
              idCiclo:idCiclo,
              idTipoPago:parseInt(idTipoPago)
          };    
      // console.log('data : ', data);
       requestPost('pagos/FiltrarComisionPagoPorTipoPago', data, dispatch )
        .then(response =>{          
            resolve(response);  
        }) 
       
    });
  }

  export async function buscarPorCarnetFormaPago(userName, idCiclo,criterio, dispatch) {
    return new Promise(resolve =>{
        const data={
                usuarioLogin:userName,
                idCiclo:parseInt(idCiclo),
                nombreCriterio:criterio
          }; 
          //console.log('llego a', data);    
      requestPost('gestionPagos/BuscarComisionCarnetFormaPago', data, dispatch )
        .then(response =>{
           resolve(response);  
        })
       
    });
  }
  export async function pagarComisionSionPay(userName,usuarioId, idCiclo, dispatch) {
    return new Promise(resolve =>{
        const data={
                usuarioLogin:userName,
                idUsuario:parseInt(usuarioId),
                idCiclo:parseInt(idCiclo)
          }; 
          //console.log('llego a', data);    
      requestPost('gestionPagos/PagarComisionSionPay', data, dispatch )
        .then(response =>{
           resolve(response);  
        })
       
    });
  }

