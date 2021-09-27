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
