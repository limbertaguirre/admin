import * as Types from '../types/cargarAplicacionesType'
import {requestGet, requestPost} from '../../service/request';
import * as Action from './messageAction';

export const getCiclos= ()=>{
    return (dispatch, getState )=>{ 
        const {userName} = getState().load;
        
        if(userName) {
            const headers={usuarioLogin:getState().load.userName};
            requestGet('Aplicaciones/GetCiclos',headers,dispatch).then((res)=>{ 
                
                if(res.code === 0){  
                    dispatch({
                        type:Types.LISTA_CICLOS,
                        ciclosList:res.data,
                    })              
                                
                }else{
                    dispatch(Action.showMessage({ message: res.message, variant: "error" }));
                }    
            })   
        } else {    

        }
    }
}

export const getAplicaciones = (cicloId)=>{
    return (dispatch, getState )=>{ 
        const {userName} = getState().load;
        
        
        if(userName) {
            //const headers={usuarioLogin:getState().load.userName};
            const data={
                usuarioLogin:userName,
                idCiclo: cicloId
               };
            requestGet('Aplicaciones/ObtenerAplicaciones',data,dispatch).then((res)=>{ 
                
                if(res.code === 0){  
                       
                                
                }else{
                    dispatch(Action.showMessage({ message: res.message, variant: "error" }));
                }    
            })   
        } else {

        }
    }
}