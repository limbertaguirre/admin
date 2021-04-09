import * as Types from '../types/homeTypes';
import {requestGet} from '../../service/request';

export const loadHome= ()=>{
  return (dispatch)=>{   
     requestGet('weatherforecast',dispatch).then((res)=>{
       console.log("ress : ", res);
     })

      // fetch('weatherforecast').then(resp=>{
      //    console.log('respod : ',resp)
      // })
   
  }
}
