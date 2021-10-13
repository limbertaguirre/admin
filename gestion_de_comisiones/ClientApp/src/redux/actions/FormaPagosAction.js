//import * as Types from '../types/loginTypes';
import {requestGet, requestPost} from '../../service/request';
import * as Action from './messageAction';

const formaPagos= {
    code:0,
    data:[
        {
            idTipoPago:1,
            nombre:"SION PAY",
            icono:'sionpay',
            cantidad:12
        },
        {
          idTipoPago:2,
            nombre:"TRANSFERENCIA",
            icono:'sionpay',
            cantidad:50
        },
        {
          idTipoPago:3,
            nombre:"CHEQUE",
            icono:'sionpay',
            cantidad:12
        },
        {
          idTipoPago:4,
            nombre:"NINGUNO",
            icono:'sionpay',
            cantidad:550
        }
    ]
}
export async function listarFormaPagos(userName, ciSeleccionado,idCiclo, dispatch) {
    return new Promise(resolve =>{
        const data={
            usuarioLogin:userName,
            carnet:ciSeleccionado,
            idCiclo:idCiclo
          };      
      requestPost('pagos/GetListarFormaPagos', data, dispatch )
        .then(response =>{
           // console.log('ger list :', response);
          resolve(response);  
        })
       
    });
  }

  export async function aplicarFormaPago(userName, idComisionDetalle,idtipoPagoSelect, idUsuario,idCiclo, dispatch) {
    return new Promise(resolve =>{
        const data={
            usuarioLogin:userName,
            idComisionDetalle:parseInt(idComisionDetalle),
            idtipoPago:parseInt(idtipoPagoSelect),
            idUsuario:idUsuario,
            idCiclo:idCiclo
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
          //console.log('llego a', data);    
      requestPost('pagos/BuscarComisionCarnetFormaPago', data, dispatch )
        .then(response =>{
           resolve(response);  
        })
       
    });
  }
  export async function getFormaPagoDisponibles(userName,idCiclo, dispatch) {
    return new Promise(resolve =>{
          const data={
            usuarioLogin:userName,
            idCiclo:idCiclo
          };              
         requestPost('pagos/GetFormaPagosDisponibles', data, dispatch )
        .then(response =>{
           // console.log('respuesta api :', response);
            resolve(response);  
        }) 
    });
  }

  export async function ListarComisionFormaPagoFiltrada(userName,idCiclo,idTipoPago, dispatch) {
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

  export async function VerificarAutorizadorComision(userName,idCiclo,idUsuario, dispatch) {
    return new Promise(resolve =>{
        const data={
              usuarioLogin:userName,
              idUsuario:parseInt(idUsuario),
              idCiclo:idCiclo
          };    
       requestPost('pagos/VerificarAutorizadorPorComision', data, dispatch )
        .then(response =>{     
         //   console.log('response verificar :', response);     
            resolve(response);  
        }) 
       
    });
  }

  export async function ConfirmarAutorizacion(userName,idUsuario, idCiclo,idComision,idAutorizacionComision, dispatch) {
    return new Promise(resolve =>{
        const data={
                usuarioLogin:userName,
                idUsuario:idUsuario,
                idCiclo:parseInt(idCiclo),
                idComision:parseInt(idComision),
                idAutorizacionComision:parseInt(idAutorizacionComision)            
          };          
       requestPost('pagos/ConfirmarAutorizacion', data, dispatch )
        .then(response =>{          
            resolve(response);  
        }) 
       
    });
  }

  export async function VerificarCierreFormaPago(userName,idUsuario, idCiclo, dispatch) {
    return new Promise(resolve =>{
        const data={
                usuarioLogin:userName,
                idUsuario:idUsuario,
                idCiclo:parseInt(idCiclo)         
          };          
       requestPost('pagos/VerificarCierreFormaPago', data, dispatch )
        .then(response =>{          
            resolve(response);  
        }) 
       
    });
  }

  export async function CerrarFormaPago(userName,idUsuario, idCiclo, dispatch) {
    return new Promise(resolve =>{
        const data={
                usuarioLogin:userName,
                idUsuario:idUsuario,
                idCiclo:parseInt(idCiclo)         
          };          
       requestPost('pagos/CerrarFormaDePago', data, dispatch )
        .then(response =>{          
            resolve(response);  
        }) 
       
    });
  }