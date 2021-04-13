import * as Types from '../types/homeTypes';
import {requestGet} from '../../service/request';
import * as Action from './messageAction'

export const loadHome= ()=>{
  return (dispatch)=>{   
       requestGet('weatherforecast',{},dispatch).then((res)=>{
         console.log("ress : ", res);
           dispatch(Action.showMessage({ message: 'pruebaaa', variant: "error" }) );

       })
 
   
  }
}
