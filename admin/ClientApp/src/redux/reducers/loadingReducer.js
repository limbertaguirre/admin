import * as Types from './../types/loadingTypes';

const defaultState = {
  loading:false,
}
export default function reducer(state = defaultState, action) { 
  if(action.type === Types.REQUEST_LOADING){
    return {
      ...state,
      loading:true,
    }
  }else if(action.type === Types.REQUEST_END){
    return{
      ...state,
      loading:false,
    }
  }else if(action.type === Types.REQUEST_FAIL){
    return{
      ...state,
      errorMensaje: action.mensaje,
    }
  }else{
    return state;
  }
}