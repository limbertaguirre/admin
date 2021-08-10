import * as Types from '../types/cargarAplicacionesType'
import {requestGet, requestPost} from '../../service/request';
import * as Action from './messageAction';

export const getCiclos= ()=>{
    return (dispatch, getState )=>{ 
        const {userName} = getState().load;
        console.log('getCiclos userName => ', userName)
        if(userName) {
            const headers={usuarioLogin:getState().load.userName};
            requestGet('Aplicaciones/GetCiclos',headers,dispatch).then((res)=>{ 
                console.log('getCiclos response => ', res);
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
        console.log('getAplicaciones userName => ', userName)
        console.log('getAplicaciones cicloId => ', cicloId)
        
        if(userName) {
            //const headers={usuarioLogin:getState().load.userName};
            const data={
                usuarioLogin:userName,
                idCiclo: cicloId
               };
            requestGet('Aplicaciones/ObtenerAplicaciones',data,dispatch).then((res)=>{ 
                console.log('getCiclos response => ', res);
                if(res.code === 0){  
                   console.log('agregando lista aplicaciones :', res)        
                                
                }else{
                    dispatch(Action.showMessage({ message: res.message, variant: "error" }));
                }    
            })   
        } else {

        }
    }
}