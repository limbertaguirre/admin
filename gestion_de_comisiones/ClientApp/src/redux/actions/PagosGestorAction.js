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
