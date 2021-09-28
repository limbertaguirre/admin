//import * as Types from '../types/loginTypes';
import {requestGet, requestPost} from '../../service/request';
import * as Action from './messageAction';

const listaPagos= {
    status:0,
    data:[
        {
            idTipoPago:1,
            nombre:"SION Pay"
        },
        {
            idTipoPago:2,
            nombre:"TRANSFERENCIA"
        },
        {
            idTipoPago:3,
            nombre:"CHEQUE"
        },
        {
            idTipoPago:4,
            nombre:"NINGUNO"
        }
    ]
}
export async function listarFormaPagos(userName, ciSeleccionado, dispatch) {
    return new Promise(resolve =>{
        const data={
            usuarioLogin:userName,
            carnet:ciSeleccionado
          };      
      requestPost('pagos/GetListarFormaPagos', data, dispatch )
        .then(response =>{
            console.log('respuesta api :', response);
          resolve(response);  
        })
       
    });
  }

  export async function aplicarFormaPago(userName, idComisionDetalle,idtipoPagoSelect, idUsuario, dispatch) {
    return new Promise(resolve =>{
        const data={
            usuarioLogin:userName,
            idComisionDetalle:parseInt(idComisionDetalle),
            idtipoPago:parseInt(idtipoPagoSelect),
            idUsuario:idUsuario
          };     
      requestPost('pagos/aplicarMetodoPagoComision', data, dispatch )
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
          console.log('llego a', data);    
      requestPost('pagos/BuscarComisionCarnetFormaPago', data, dispatch )
        .then(response =>{
           resolve(response);  
        })
       
    });
  }
